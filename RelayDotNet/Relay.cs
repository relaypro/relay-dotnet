// Copyright © 2022 Relay Inc.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Serilog;
using Microsoft.AspNetCore.WebUtilities;

/// The main namespace for the Relay workflow SDK.
namespace RelayDotNet
{
    /// <summary>
    /// The Relay class is responsible for defining the main functionalities that are used within workflows,
    /// such as functions for communicating with the device, sending out notifications to groups, handling 
    /// workflow events, and performing physical actions on the device such as manipulating LEDs and creating vibrations.
    /// </summary>
    public class Relay
    {
        public enum WebSocketConnector
        {
            Fleck,
            SystemHttpListener,
            SystemKestral
        }
        
        private const int DelayTimeout = 5000;
        private const int DelayEventTimeout = 32000;
        private const int DelayRefreshTimeout = 45000;
        private const int DelayNotificationTimeout = 60000;
        
        private readonly IRelayWebSocketConnector _relayWebSocketConnector;
        public IRelayWebSocketConnector RelayWebSocketConnector => _relayWebSocketConnector;
        
        private readonly Dictionary<string, Type> _pathsToRelayWorkflows = new Dictionary<string, Type>();
        private readonly SemaphoreSlim _pathsToRelayWorkflowsSemaphore = new SemaphoreSlim(1);
        
        private readonly Dictionary<IRelayWebSocketConnection, RunningRelayWorkflow> _webSocketConnectionsToRunningRelayWorkflows = new Dictionary<IRelayWebSocketConnection, RunningRelayWorkflow>();
        private readonly Dictionary<IRelayWorkflow, RunningRelayWorkflow> _relayWorkflowsToRunningRelayWorkflows = new Dictionary<IRelayWorkflow, RunningRelayWorkflow>();
        private readonly SemaphoreSlim _runningRelayWorkflowsSemaphore = new SemaphoreSlim(1);
        
        private readonly Dictionary<string, TaskCompletionSource<Dictionary<string, object>>> _requestIdsToTaskCompletionSources = new Dictionary<string, TaskCompletionSource<Dictionary<string, object>>>();
        private readonly SemaphoreSlim _requestIdsToTaskCompletionSourcesSemaphore = new SemaphoreSlim(1);

        private readonly Dictionary<EventType, List<EventTypeTaskCompletionSource>> _eventTypesToTaskCompletionSources = new Dictionary<EventType, List<EventTypeTaskCompletionSource>>();
        private readonly SemaphoreSlim _eventTypesToTaskCompletionSourcesSemaphore = new SemaphoreSlim(1);
        private static readonly Dictionary<string, object> emptyDict = new Dictionary<string, object>();
        
        public Relay(WebSocketConnector webSocketConnector, string ip, int port, bool secure)
        {
            switch (webSocketConnector)
            {
                case WebSocketConnector.Fleck:
                {
                    _relayWebSocketConnector = new FleckRelayWebSocketConnector(this, ip, port, secure);                    
                }
                break;

                case WebSocketConnector.SystemKestral:
                {
                    _relayWebSocketConnector = new SystemKestralRelayWebSocketConnector(this, ip, port, secure);                    
                }
                break;
                
                case WebSocketConnector.SystemHttpListener:
                {
                    _relayWebSocketConnector = new SystemHttpListenerRelayWebSocketConnector(this, ip, port, secure);                    
                }
                    break;
            }
            
        }
        
        public async Task<bool> AddWorkflow(string path, Type relayWorkflowType)
        {
            await _pathsToRelayWorkflowsSemaphore.WaitAsync();
            try
            {
                if (_pathsToRelayWorkflows.ContainsKey(path))
                {
                    return true;
                }
                
                if (relayWorkflowType.GetInterfaces().Contains(typeof(IRelayWorkflow)))
                {
                    _pathsToRelayWorkflows[path] = relayWorkflowType;
                
                    Log.Information("Add {Path} to {@RelayWorkflow}", path, relayWorkflowType);

                    return true;
                }
                else
                {
                    Log.Warning("Add {Path} failed on {@RelayWorkflow}, invalid type", path, relayWorkflowType);

                    return false;
                }
            }
            finally
            {
                _pathsToRelayWorkflowsSemaphore.Release();
            }
        }
        
        public async Task<bool> RemoveWorkflow(string path)
        {
            await _pathsToRelayWorkflowsSemaphore.WaitAsync();
            try
            {
                return _pathsToRelayWorkflows.Remove(path);
            }
            finally
            {
                _pathsToRelayWorkflowsSemaphore.Release();
            }
        }

        private async Task<Type> GetRelayWorkflowType(string path)
        {
            await _pathsToRelayWorkflowsSemaphore.WaitAsync();
            try
            {
                if (_pathsToRelayWorkflows.ContainsKey(path))
                {
                    return _pathsToRelayWorkflows[path];
                }
                else
                {
                    return null;
                }
            }
            finally
            {
                _pathsToRelayWorkflowsSemaphore.Release();
            }
        }

        private async Task<IRelayWorkflow> CreateInstanceRelayWorkflow(string path)
        {
            Type relayWorkflowType = await GetRelayWorkflowType(path);
            if (null == relayWorkflowType)
            {
                Log.Warning("CreateInstanceRelayWorkflow fails for path {Path}, no mapped relay workflow", path);

                return null;
            }
            
            return (IRelayWorkflow) Activator.CreateInstance(relayWorkflowType, this);
        }

        private async Task<RunningRelayWorkflow> CreateRunningRelayWorkflow(IRelayWorkflow relayWorkflow, IRelayWebSocketConnection webSocketConnection)
        {
            RunningRelayWorkflow runningRelayWorkflow = new RunningRelayWorkflow(relayWorkflow, webSocketConnection);

            await _runningRelayWorkflowsSemaphore.WaitAsync();
            try
            {
                _relayWorkflowsToRunningRelayWorkflows[runningRelayWorkflow.RelayWorkflow] = runningRelayWorkflow;
                _webSocketConnectionsToRunningRelayWorkflows[runningRelayWorkflow.WebSocketConnection] = runningRelayWorkflow;
            }
            finally
            {
                _runningRelayWorkflowsSemaphore.Release();
            }

            return runningRelayWorkflow;
        }

        private async Task<RunningRelayWorkflow> RemoveRunningRelayWorkflow(IRelayWorkflow relayWorkflow)
        {
            RunningRelayWorkflow runningRelayWorkflow = null;
            
            await _runningRelayWorkflowsSemaphore.WaitAsync();
            try
            {
                if (_relayWorkflowsToRunningRelayWorkflows.ContainsKey(relayWorkflow))
                {
                    runningRelayWorkflow = _relayWorkflowsToRunningRelayWorkflows[relayWorkflow];
                    InvokeIRelayWorkflowCallback(runningRelayWorkflow, EventType.Stop, new Dictionary<string, object>());
                    _relayWorkflowsToRunningRelayWorkflows.Remove(relayWorkflow);
                    _webSocketConnectionsToRunningRelayWorkflows.Remove(runningRelayWorkflow.WebSocketConnection);
                }
            }
            finally
            {
                _runningRelayWorkflowsSemaphore.Release();
            }

            runningRelayWorkflow?.WebSocketConnection.Close();

            return runningRelayWorkflow;
        }

        private async Task<RunningRelayWorkflow> RemoveRunningRelayWorkflow(IRelayWebSocketConnection webSocketConnection)
        {
            RunningRelayWorkflow runningRelayWorkflow = null;
            
            await _runningRelayWorkflowsSemaphore.WaitAsync();
            try
            {
                if (_webSocketConnectionsToRunningRelayWorkflows.ContainsKey(webSocketConnection))
                {
                    runningRelayWorkflow = _webSocketConnectionsToRunningRelayWorkflows[webSocketConnection];
                    InvokeIRelayWorkflowCallback(runningRelayWorkflow, EventType.Stop, new Dictionary<string, object>());
                    _webSocketConnectionsToRunningRelayWorkflows.Remove(webSocketConnection);
                    _relayWorkflowsToRunningRelayWorkflows.Remove(runningRelayWorkflow.RelayWorkflow);
                }
            }
            finally
            {
                _runningRelayWorkflowsSemaphore.Release();
            }

            runningRelayWorkflow?.WebSocketConnection.Close();

            return runningRelayWorkflow;
        }
        
        private async Task<RunningRelayWorkflow> GetRunningRelayWorkflow(IRelayWebSocketConnection webSocketConnection)
        {
            RunningRelayWorkflow runningRelayWorkflow = null;
            
            await _runningRelayWorkflowsSemaphore.WaitAsync();
            try
            {
                if (_webSocketConnectionsToRunningRelayWorkflows.ContainsKey(webSocketConnection))
                {
                    runningRelayWorkflow = _webSocketConnectionsToRunningRelayWorkflows[webSocketConnection];
                }
            }
            finally
            {
                _runningRelayWorkflowsSemaphore.Release();
            }

            return runningRelayWorkflow;
        }
        
        private async Task<RunningRelayWorkflow> GetRunningRelayWorkflow(IRelayWorkflow relayWorkflow)
        {
            RunningRelayWorkflow runningRelayWorkflow = null;
            
            await _runningRelayWorkflowsSemaphore.WaitAsync();
            try
            {
                if (_relayWorkflowsToRunningRelayWorkflows.ContainsKey(relayWorkflow))
                {
                    runningRelayWorkflow = _relayWorkflowsToRunningRelayWorkflows[relayWorkflow];
                }
            }
            finally
            {
                _runningRelayWorkflowsSemaphore.Release();
            }

            return runningRelayWorkflow;
        }

        public async Task<bool> OnOpen(IRelayWebSocketConnection webSocketConnection)
        {
            Log.Information("{@WscInfo:l} OnOpen", WscInfo(webSocketConnection));

            IRelayWorkflow relayWorkflow = await CreateInstanceRelayWorkflow(webSocketConnection.ConnectionInfo.Path);
                
            if (null == relayWorkflow)
            {
                await webSocketConnection.Close();

                return false;
            }

            await CreateRunningRelayWorkflow(relayWorkflow, webSocketConnection);

            return true;
        }

        public async void OnClose(IRelayWebSocketConnection webSocketConnection)
        {
            Log.Information("{@WscInfo:l} OnClose", WscInfo(webSocketConnection));

            await RemoveRunningRelayWorkflow(webSocketConnection);
        }

        public async void OnMessage(IRelayWebSocketConnection webSocketConnection, string message)
        {
            Log.Debug("{@WscInfo:l} OnMessage JSON: {@Message}", WscInfo(webSocketConnection), message);

            var options = new JsonSerializerOptions
            {
                Converters = {new DictionaryStringObjectJsonConverterCustomWrite()},
                WriteIndented = true,
            };

            var dictionary = JsonSerializer.Deserialize<Dictionary<string, object>>(message, options);
            if (dictionary == null)
            {
                Log.Warning("{@WscInfo:l} OnMessage has null dictionary", WscInfo(webSocketConnection));
                return;
            }

            Log.Debug("{@WscInfo:l} OnMessage Dictionary: {@Dictionary}", WscInfo(webSocketConnection), dictionary);

            if (!dictionary.ContainsKey("_type"))
            {
                Log.Warning("{@WscInfo:l} OnMessage has no _type", WscInfo(webSocketConnection));
                return;
            }
            string type = (string)dictionary["_type"];

            var rxResponse = new Regex(@"^wf_api_(\w+)_response$", RegexOptions.Compiled);
            var rxEvent = new Regex(@"^wf_api_(\w+)_event$", RegexOptions.Compiled);

            var matchResponse = rxResponse.Match(type);
            var matchEvent = rxEvent.Match(type);

            //
            // Response
            //
            if (matchResponse.Success)
            {
                if (RequestType.TryParseSerializedName(matchResponse.Groups[1].ToString(), out RequestType requestType))
                {
                    // All request messages are expected to have a _id field
                    if (!dictionary.ContainsKey("_id"))
                    {
                        Log.Warning("{@WscInfo:l} Request message has no _id: {type}", WscInfo(webSocketConnection), type);
                        return;
                    }

                    string id = (string)dictionary["_id"];

                    TaskCompletionSource<Dictionary<string, object>> taskCompletionSource = await GetAndRemoveTaskCompletionSource(id);

                    if (null != taskCompletionSource)
                    {
                        taskCompletionSource.SetResult(ScrubInternalDictionaryKeys(dictionary));
                    }
                    else
                    {
                        Log.Warning("{@WscInfo:l} unable to match id, ignoring {RequestType} response: {@Dictionary}", WscInfo(webSocketConnection), requestType.Name, dictionary);
                    }
                }
            }
            //
            // Event
            //
            else if (matchEvent.Success)
            {
                if (EventType.TryParseSerializedName(matchEvent.Groups[1].ToString(), out EventType eventType))
                {
                    // All events besides progress events are sent to workflow callbacks
                    if (eventType.Name == "Progress")
                    {
                        // look for a type completion source, if there is one, and send a result to it and replace it with a new task so receiver can continue listening for events
                        string id = (string)dictionary["_id"];
                        TaskCompletionSource<Dictionary<string, object>> taskCompletionSource = await GetAndReplaceTaskCompletionSource(id, dictionary);
                        taskCompletionSource.SetResult(dictionary);         // send the unscrubbed dictionary here so receiver can use _type field
                    }
                    else
                    {
                        Dictionary<string, object> scrubbedDictionary = ScrubInternalDictionaryKeys(dictionary);

                        InvokeIRelayWorkflowCallback(webSocketConnection, eventType, scrubbedDictionary);

                        List<TaskCompletionSource<Dictionary<string, object>>> taskCompletionSources = await GetAndRemoveEventTypeTaskCompletionSources(eventType, dictionary);
                        foreach (TaskCompletionSource<Dictionary<string, object>> taskCompletionSource in taskCompletionSources)
                        {
                            taskCompletionSource.SetResult(scrubbedDictionary);
                        }
                    }
                }
            }
        }

        private async Task<TaskCompletionSource<Dictionary<string, object>>> CreateAndStoreTaskCompletionSource(Dictionary<string, object> dictionary)
        {
            if (!dictionary.ContainsKey("_id"))
            {
                return null;
            }
            
            if (dictionary.ContainsKey("_Type"))
            {
                RequestType requestType = (RequestType) dictionary["_Type"];
                dictionary.Remove("_Type");
                if (requestType.Equals(RequestType.Terminate))
                {
                    return null;
                }
            }

            string id = (string) dictionary["_id"];

            TaskCompletionSource<Dictionary<string, object>> taskCompletionSource = new TaskCompletionSource<Dictionary<string, object>>();
            
            await _requestIdsToTaskCompletionSourcesSemaphore.WaitAsync();
            try
            {
                _requestIdsToTaskCompletionSources[id] = taskCompletionSource;
            }
            finally
            {
                _requestIdsToTaskCompletionSourcesSemaphore.Release();
            }

            return taskCompletionSource;
        }
        
        private async void StoreEventTypeTaskCompletionSource(EventType eventType, EventTypeTaskCompletionSource eventTypeTaskCompletionSource)
        {
            await _eventTypesToTaskCompletionSourcesSemaphore.WaitAsync();
            try
            {
                List<EventTypeTaskCompletionSource> eventTypeTaskCompletionSources;
                if (_eventTypesToTaskCompletionSources.ContainsKey(eventType))
                {
                    eventTypeTaskCompletionSources = _eventTypesToTaskCompletionSources[eventType];
                }
                else
                {
                    eventTypeTaskCompletionSources = new List<EventTypeTaskCompletionSource>();
                    _eventTypesToTaskCompletionSources[eventType] = eventTypeTaskCompletionSources;
                }
                
                eventTypeTaskCompletionSources.Add(eventTypeTaskCompletionSource);
            }
            finally
            {
                _eventTypesToTaskCompletionSourcesSemaphore.Release();
            }
        }

        private async Task<List<TaskCompletionSource<Dictionary<string, object>>>> GetAndRemoveEventTypeTaskCompletionSources(EventType eventType, Dictionary<string, object> dictionary)
        {
            List<TaskCompletionSource<Dictionary<string, object>>> taskCompletionSources = new List<TaskCompletionSource<Dictionary<string, object>>>();
            
            await _eventTypesToTaskCompletionSourcesSemaphore.WaitAsync();
            try
            {
                if (_eventTypesToTaskCompletionSources.ContainsKey(eventType))
                {
                    List<EventTypeTaskCompletionSource> eventTypeTaskCompletionSources = _eventTypesToTaskCompletionSources[eventType];

                    for (int i = eventTypeTaskCompletionSources.Count - 1; 0 <= i; --i)
                    {
                        if (null == eventTypeTaskCompletionSources[i].KeyValuesToMatch)
                        {
                            taskCompletionSources.Add(eventTypeTaskCompletionSources[i].TaskCompletionSource);
                            eventTypeTaskCompletionSources.RemoveAt(i);
                        }
                        else
                        {
                            bool match = true;

                            foreach(KeyValuePair<string, object> kvp in eventTypeTaskCompletionSources[i].KeyValuesToMatch)
                            {
                                if (!dictionary.ContainsKey(kvp.Key)
                                   || (dictionary[kvp.Key] is null)
                                   || !dictionary[kvp.Key].Equals(kvp.Value))
                                {
                                    match = false;
                                    break;
                                }
                            }

                            if (match)
                            {
                                taskCompletionSources.Add(eventTypeTaskCompletionSources[i].TaskCompletionSource);
                                eventTypeTaskCompletionSources.RemoveAt(i);
                            }
                        }
                    }

                    if (0 == eventTypeTaskCompletionSources.Count)
                    {
                        _eventTypesToTaskCompletionSources.Remove(eventType);
                    }
                }
            }
            finally
            {
                _eventTypesToTaskCompletionSourcesSemaphore.Release();
            }

            return taskCompletionSources;
        }

        private async Task<TaskCompletionSource<Dictionary<string, object>>> GetAndRemoveTaskCompletionSource(string id)
        {
            await _requestIdsToTaskCompletionSourcesSemaphore.WaitAsync();
            try
            {
                if (_requestIdsToTaskCompletionSources.ContainsKey(id))
                {
                    TaskCompletionSource<Dictionary<string, object>> taskCompletionSource = _requestIdsToTaskCompletionSources[id];
                    _requestIdsToTaskCompletionSources.Remove(id);

                    return taskCompletionSource;
                }
            }
            finally
            {
                _requestIdsToTaskCompletionSourcesSemaphore.Release();
            }

            return null;
        }

        private async Task<TaskCompletionSource<Dictionary<string, object>>> GetTaskCompletionSource(string id)
        {
            await _requestIdsToTaskCompletionSourcesSemaphore.WaitAsync();
            try
            {
                if (_requestIdsToTaskCompletionSources.ContainsKey(id))
                {
                    TaskCompletionSource<Dictionary<string, object>> taskCompletionSource = _requestIdsToTaskCompletionSources[id];
                    return taskCompletionSource;
                }
            }
            finally
            {
                _requestIdsToTaskCompletionSourcesSemaphore.Release();
            }

            return null;
        }

        // Returns the taskCompletionSource with id, and replaces it with a new source based on dictionary. used to hotswap tasks for progress events
        private async Task<TaskCompletionSource<Dictionary<string, object>>> GetAndReplaceTaskCompletionSource(string id, Dictionary<string, object> dictionary)
        {
            await _requestIdsToTaskCompletionSourcesSemaphore.WaitAsync();
            try
            {
                if (_requestIdsToTaskCompletionSources.ContainsKey(id))
                {
                    TaskCompletionSource<Dictionary<string, object>> taskCompletionSource = _requestIdsToTaskCompletionSources[id];
                    _requestIdsToTaskCompletionSources.Remove(id);

                    TaskCompletionSource<Dictionary<string, object>> newTaskCompletionSource = new TaskCompletionSource<Dictionary<string, object>>();
                    _requestIdsToTaskCompletionSources[id] = newTaskCompletionSource;

                    return taskCompletionSource;
                }
            }
            finally
            {
                _requestIdsToTaskCompletionSourcesSemaphore.Release();
            }

            return null;
        }
        

        private async void InvokeIRelayWorkflowCallback(IRelayWebSocketConnection webSocketConnection, EventType eventType, IDictionary<string, object> dictionary)
        {
            RunningRelayWorkflow runningRelayWorkflow = await GetRunningRelayWorkflow(webSocketConnection);

            if (null == runningRelayWorkflow)
            {
                Log.Warning("{@WscInfo:l} unable to process {EventType}: {@Dictionary}", WscInfo(webSocketConnection), eventType.Name, dictionary);

                return;
            }
            
            InvokeIRelayWorkflowCallback(runningRelayWorkflow, eventType, dictionary);
        }

        private async void InvokeIRelayWorkflowCallback(RunningRelayWorkflow runningRelayWorkflow, EventType eventType, IDictionary<string, object> dictionary)
        {
            await runningRelayWorkflow.Semaphore.WaitAsync();
            try
            {
                MethodInfo eventCallback = typeof(IRelayWorkflow).GetMethod("On" + eventType.Name);
                if (null != eventCallback)
                {
                    if (eventType.Equals(EventType.Stop) && runningRelayWorkflow.Stopped)
                    {
                        return;
                    }
                    
                    List<object> args = new List<object>(1);
                    args.Add(dictionary);
                    eventCallback.Invoke(runningRelayWorkflow.RelayWorkflow, args.ToArray());

                    if (eventType.Equals(EventType.Stop))
                    {
                        runningRelayWorkflow.Stopped = true;
                    }
                }
                else
                {
                    Log.Warning("unable to dispatch event {EventType}, cannot find a matching event callback", eventType);                    
                }
            }
            finally
            {
                runningRelayWorkflow.Semaphore.Release();
            }
        }

        private Dictionary<string, object> ScrubInternalDictionaryKeys(Dictionary<string, object> dictionary)
        {
            List<string> keysToRemove = (
                from key in dictionary.Keys
                where key.StartsWith("_")
                select key).ToList();

            foreach (string key in keysToRemove)
            {
                dictionary.Remove(key);
            }

            // check for improperly serialized fields and convert to strings here
            ConvertStringValues(dictionary);

            return dictionary;
        }

        private void ConvertStringValues(Dictionary<string, object> dictionary)
        {
            foreach (KeyValuePair<string, object> kv in dictionary)
            {
                if (kv.Value is Dictionary<string, object>)
                {
                    ConvertStringValues((Dictionary<string, object>)kv.Value);
                }
                else if (kv.Value is List<object>)
                {
                    // convert to string if list of ints
                    bool shouldConvert = true;
                    string s = "";
                    foreach (object o in (List<object>)kv.Value)
                    {
                        if (o is Int64)
                        {
                            s += Convert.ToChar(o);
                        }
                        else
                        {
                            shouldConvert = false;
                            break;
                        }
                    }
                    if (shouldConvert)
                    {
                        dictionary[kv.Key] = s;
                    }
                }
            }
        }

        private async Task<Dictionary<string, object>> Send(IRelayWebSocketConnection webSocketConnection, Dictionary<string, object> dictionary)
        {
            return await Send(webSocketConnection, dictionary, DelayTimeout);
        }

        private async Task<Dictionary<string, object>> Send(IRelayWebSocketConnection webSocketConnection, Dictionary<string, object> dictionary, int millisecondsDelay)
        {
            Log.Debug("{@WscInfo:l} Send Dictionary: {@message}", WscInfo(webSocketConnection), dictionary);

            bool isTerminateRequest = (dictionary.ContainsKey("_type") && dictionary["_type"].Equals("wf_api_terminate_request"));
            TaskCompletionSource<Dictionary<string, object>> taskCompletionSource = null;
            // we'll never receive a response to wf_api_terminate_request
            if (!isTerminateRequest) {
                taskCompletionSource = await CreateAndStoreTaskCompletionSource(dictionary);
            }

            string json = ToJson(dictionary);
            
            Log.Debug("{@WscInfo:l} Send JSON: {@message}", WscInfo(webSocketConnection), json);
            
            await webSocketConnection.Send(json);

            if (isTerminateRequest) {
                return null;
            }

            // listen for responses in a loop, repeating if a progress event is received
            for (; ; )
            {
                
                if (taskCompletionSource == null)
                {
                    Log.Error("{@WscInfo:l} Incomplete response for request: {@message}", WscInfo(webSocketConnection), json);
                    return new Dictionary<string, object>();
                }

                Task timeoutTask = Task.Delay(millisecondsDelay);
                var result = await Task.WhenAny(taskCompletionSource.Task, timeoutTask);
                if (timeoutTask.IsCompleted)
                {
                    // timed out listening, return null
                    Log.Error("{@WscInfo:l} Timed out waiting for response for request: {@message}.", WscInfo(webSocketConnection), json);
                    return null;
                }
                else
                {
                    Log.Debug("Send Task.WhenAny completes with {@result}", result);
                    Task<Dictionary<string, object>> waitResult = (Task<Dictionary<string, object>>)result;
                    Dictionary<string, object> res = waitResult.Result;

                    // if the type is progress, ignore this and keep listening, with a new timeout
                    if (res.ContainsKey("_type") && (string)res["_type"] == "wf_api_progress_event")
                    {
                        Log.Debug("{@WscInfo:l} Progress event received, continuing listening", WscInfo(webSocketConnection));
                        // replace the taskcompletionsource so we can continue listening for responses
                        taskCompletionSource = await GetTaskCompletionSource((string)res["_id"]);
                        continue;
                    }
                    // if the message was not a progress event, return the result to the user
                    return res;
                }
            }
        }

        private string WscInfo(IRelayWebSocketConnection webSocketConnection)
        {
            var sb = new StringBuilder(256);
            sb.Append('[');
            sb.Append(webSocketConnection.ConnectionInfo.Id);
            sb.Append('/');
            sb.Append(webSocketConnection.ConnectionInfo.ClientIpAddress);
            sb.Append(':');
            sb.Append(webSocketConnection.ConnectionInfo.ClientPort);
            sb.Append(webSocketConnection.ConnectionInfo.Path);
            sb.Append(']');
            
            return sb.ToString();
        }

        public void Start()
        {
            _relayWebSocketConnector.Start();

            Log.Debug("started");
        }

        public void Dispose()
        {
            _relayWebSocketConnector.Dispose();
            Log.Debug("disposed");
        }
        
        private static string MakeId()
        {
            Random random = new Random();
            var bytes = new Byte[16];
            random.NextBytes(bytes);
            return BitConverter.ToString(bytes).Replace("-", string.Empty);
        }

        private static Dictionary<string, object> Request(RequestType requestType)
        {
            return Request(requestType, null);
        }
        
        private static Dictionary<string, object> Request(RequestType requestType, Dictionary<string, object> message)
        {
            if (null == message)
            {
                message = new Dictionary<string, object>();
            }
            message["_id"] = MakeId();
            message["_type"] = $"wf_api_{requestType.SerializedName}_request";
            message["_Type"] = requestType;
            
            return (from keyValuePair in message 
                where null != keyValuePair.Value
                select keyValuePair).ToDictionary(keyValuePair => keyValuePair.Key, keyValuePair => keyValuePair.Value);
        }

        private static Dictionary<string, object> Request(RequestType requestType, string target, Dictionary<string, object> message)
        {
            return Request(requestType, new[] { target }, message);
        }

        private static Dictionary<string, object> Request(RequestType requestType, string[] targets, Dictionary<string, object> message)
        {
            Dictionary<string, object> req = Request(requestType, message);
            req["_target"] = new Dictionary<string, object>()
            {
                ["uris"] = targets
            };
            return req;
        }

        private static string ToJson(Dictionary<string, object> dictionary)
        {
            var options = new JsonSerializerOptions();
            options.Converters.Add(new DictionaryStringObjectJsonConverterCustomWrite());
            
            return JsonSerializer.Serialize(dictionary, options);
        }

        private async Task<RunningRelayWorkflow> GetRunningRelayWorkflowOrThrow(IRelayWorkflow relayWorkflow)
        {
            RunningRelayWorkflow runningRelayWorkflow = await GetRunningRelayWorkflow(relayWorkflow);
            if (null == runningRelayWorkflow)
            {
                throw new NullReferenceException("no running relay workflow");
            }

            return runningRelayWorkflow;
        }

        /// <summary>
        /// Creates a target object from a source URN.
        /// </summary>
        /// <param name="sourceUri">source uri that will be used to create a target.</param>
        /// <returns>the target that was created from a source URN.</returns>
         public static Dictionary<string, object> TargetsFromSourceUri(string sourceUri) {
            return new Dictionary<string, object>
            {
                ["uri"] = sourceUri
            };
         }
        
        /// <summary>
        /// Terminates a workflow.  This method is usually called
        /// after your workflow has completed and you would like to end the 
        /// workflow by calling end_interaction(), where you can then terminate
        /// the workflow.
        /// </summary>
        /// <param name="relayWorkflow">the workflow.</param>
        public async void Terminate(IRelayWorkflow relayWorkflow)
        {
            await Send((await GetRunningRelayWorkflowOrThrow(relayWorkflow)).WebSocketConnection, Terminate_());            
        }
        
        private static Dictionary<string, object> Terminate_()
        {
            return Request(RequestType.Terminate);
        }

        /// <summary>
        /// Starts an interaction with the user.  Triggers an INTERACTION_STARTED event
        /// and allows the user to interact with the device via functions that require an 
        /// interaction URN.
        /// </summary>
        /// <param name="relayWorkflow">the workflow.</param>
        /// <param name="target">the device that you would like to start an interaction with.</param>
        /// <param name="name">a name for your interaction.</param>
        /// <param name="options">can be color, home channel, or input types.</param>
        /// <returns>the event response.</returns>
        public async void StartInteraction(IRelayWorkflow relayWorkflow, string target, string name, Dictionary<string, object> options = null)
        {
            await Send((await GetRunningRelayWorkflowOrThrow(relayWorkflow)).WebSocketConnection, StartInteraction_(target, name, options));
        }

        private static Dictionary<string, object> StartInteraction_(string target, string name, Dictionary<string, object> options)
        {
            if (options == null) {
                options = emptyDict;
            }
            return Request(
                RequestType.StartInteraction,
                target,
                new Dictionary<string, object>()
                {
                    ["name"] = name,
                    ["options"] = options
                }
            );
        }

        /// <summary>
        /// Ends an interaction with the user.  Triggers an INTERACTION_ENDED event to signify
        /// that the user is done interacting with the device.
        /// </summary>
        /// <param name="relayWorkflow">the workflow.</param>
        /// <param name="target">the interaction that you would like to end.</param>
        /// <returns>the event response.</returns>
        public async void EndInteraction(IRelayWorkflow relayWorkflow, string target)
        {
            await Send((await GetRunningRelayWorkflowOrThrow(relayWorkflow)).WebSocketConnection, EndInteraction_(target));
        }

        private static Dictionary<string, object> EndInteraction_(string target)
        {
            return Request(
                RequestType.EndInteraction,
                target,
                null
            );
        }

        /// <summary>
        /// Utilizes text to speech capabilities to make the device 'speak' to the user.
        /// </summary>
        /// <param name="relayWorkflow">the workflow.</param>
        /// <param name="target">the interaction URN.</param>
        /// <param name="text">what you would like the device to say.</param>
        /// <returns>the response ID after the device speaks to the user.</returns>
        public async Task<string> Say(IRelayWorkflow relayWorkflow, string target, string text)
        {
            return await Say(relayWorkflow, target, text, Language.English);
        }

        /// <summary>
        /// Utilizes text to speech capabilities to make the device 'speak' to the user.
        /// </summary>
        /// <param name="relayWorkflow">the workflow.</param>
        /// <param name="target">the interaction URN.</param>
        /// <param name="text">what you would like the device to say.</param>
        /// <param name="language">the language of the text that is being spoken.  Defaults to 'en-US'.</param>
        /// <returns>the response ID after the device speaks to the user.</returns>
        public async Task<string> Say(IRelayWorkflow relayWorkflow, string target, string text, Language language)
        {
            Dictionary<string, object> dictionary = await Send((await GetRunningRelayWorkflowOrThrow(relayWorkflow)).WebSocketConnection, Say_(target, text, language));
            return (dictionary != null) ? (string) dictionary["id"] : null;
        }

        /// <summary>
        /// Utilizes text to speech capabilities to make the device 'speak' to the user.
        /// Waits until the text is fully played out on the device before continuing.
        /// </summary>
        /// <param name="relayWorkflow">the workflow.</param>
        /// <param name="target">the interaction URN.</param>
        /// <param name="text">what you would like the device to say.</param>
        /// <returns>the response ID after the device speaks to the user.</returns>
        public async Task<string> SayAndWait(IRelayWorkflow relayWorkflow, string target, string text)
        {
            return await SayAndWait(relayWorkflow, target, text, Language.English);
        }
        
        /// <summary>
        /// Utilizes text to speech capabilities to make the device 'speak' to the user.
        /// Waits until the text is fully played out on the device before continuing.
        /// </summary>
        /// <param name="relayWorkflow">the workflow.</param>
        /// <param name="target">the interaction URN.</param>
        /// <param name="text">what you would like the device to say.</param>
        /// <param name="language">the language of the text that is being spoken.  Defaults to 'en-US'.</param>
        /// <returns>the resonse ID after the device speaks to the user.</returns>
        public async Task<string> SayAndWait(IRelayWorkflow relayWorkflow, string target, string text, Language language)
        {
            Log.Debug("SayAndWait enter for {text}", text);
            string id = await Say(relayWorkflow, target, text, language);
            if (id == null)
            {
                return null;
            }
            await WaitForPromptStop(id);
            Log.Debug("SayAndWait exit for {text}", text);
            return id;
        }
        
        private static Dictionary<string, object> Say_(string target, string text, Language language)
        {
            return Request(
                RequestType.Say,
                target,
                new Dictionary<string, object>()
                {
                    ["text"] = text,
                    ["lang"] = language.SerializedName                    
                }
            );
        }

        /// <summary>
        /// Plays a custom audio file that was uploaded by the user.
        /// </summary>
        /// <param name="relayWorkflow">the workflow.</param>
        /// <param name="target">the interaction URN.</param>
        /// <param name="filename">the name of the audio file.</param>
        /// <returns>the response ID after the audio file has been played on the device.</returns>
        public async Task<string> Play(IRelayWorkflow relayWorkflow, string target, string filename)
        {
            Dictionary<string, object> dictionary = await Send((await GetRunningRelayWorkflowOrThrow(relayWorkflow)).WebSocketConnection, Play_(target, filename));
            return (dictionary != null) ? (string) dictionary["id"] : null;
        }
        
        /// <summary>
        /// Plays a custom audio file that was uploaded by the user.  Waits until the audio
        /// file has finished playing before continuing through the workflow.
        /// </summary>
        /// <param name="relayWorkflow">the workflow.</param>
        /// <param name="target">the interaction URN.</param>
        /// <param name="filename">the name of the audio file.</param>
        /// <returns>the response ID after the audio file has been played on the device.</returns>
        public async Task<string> PlayAndWait(IRelayWorkflow relayWorkflow, string target, string filename)
        {
            Log.Debug("PlayAndWait enter for {text}", filename);
            string id = await Play(relayWorkflow, target, filename);
            await WaitForPromptStop(id);
            Log.Debug("PlayAndWait exit for {text}", filename);
            return id;
        }

        private async Task<string> WaitForPromptStop(string id)
        {
            EventTypeTaskCompletionSource eventTypeTaskCompletionSource = new EventTypeTaskCompletionSource();
            eventTypeTaskCompletionSource.KeyValuesToMatch["id"] = id;
            eventTypeTaskCompletionSource.KeyValuesToMatch["type"] = "stopped";
            StoreEventTypeTaskCompletionSource(EventType.Prompt, eventTypeTaskCompletionSource);

            Log.Debug("Going to wait for prompt stop with id {Id}", id);
            var result = await Task.WhenAny(eventTypeTaskCompletionSource.TaskCompletionSource.Task, Task.Delay(DelayEventTimeout));
            Log.Debug("WaitForPromptStop Task.WhenAny completes with {@result}", result);

            Task<Dictionary<string, object>> waitResult = (Task<Dictionary<string, object>>) result;
            await waitResult;
            
            return id;
        }
        
        private static Dictionary<string, object> Play_(string target, string filename)
        {
            return Request(
                RequestType.Play,
                target,
                new Dictionary<string, object>
                {
                    ["filename"] = filename
                }
            );
        }

        public async Task<Dictionary<string, object>> StopPlayback(IRelayWorkflow relayWorkflow, string target)
        {
            return await StopPlayback(relayWorkflow, target, (string[]) null);
        }
        
        public async Task<Dictionary<string, object>> StopPlayback(IRelayWorkflow relayWorkflow, string target, string id)
        {
            return await StopPlayback(relayWorkflow, target, new[] {id});
        }
        
        public async Task<Dictionary<string, object>> StopPlayback(IRelayWorkflow relayWorkflow, string target, string[] ids)
        {
            return await Send((await GetRunningRelayWorkflowOrThrow(relayWorkflow)).WebSocketConnection, StopPlayback_(target, ids));
        }
        
        private static Dictionary<string, object> StopPlayback_(string target, string[] ids)
        {
            if (null == ids)
            {
                return Request(RequestType.StopPlayback, null);                
            }
            
            return Request(
                RequestType.StopPlayback,
                target,
                new Dictionary<string, object>
                {
                    ["ids"] = ids                    
                }
            );
        }
        
        /// <summary>
        /// Makes the device vibrate in a particular pattern. You can specify
        /// how many vibrations you would like, the duration of each vibration in
        /// milliseconds, and how long you would like the pauses between each vibration to last
        /// in milliseconds.
        /// </summary>
        /// <param name="relayWorkflow">the workflow.</param>
        /// <param name="target">the interaction URN.</param>
        /// <param name="pattern">an array representing the pattern of your vibration.  Defaults to none.</param>
        /// <returns>the event response.</returns>
        public async Task<Dictionary<string, object>> Vibrate(IRelayWorkflow relayWorkflow, string target, int[] pattern)
        {
            return await Send((await GetRunningRelayWorkflowOrThrow(relayWorkflow)).WebSocketConnection, Vibrate_(target, pattern));
        }
        
        private static Dictionary<string, object> Vibrate_(string target, int[] pattern)
        {
            return Request(
                RequestType.Vibrate,
                target,
                new Dictionary<string, object>
                {
                    ["pattern"] = pattern                    
                }
            );
        }

        /// <summary>
        /// Starts an unnamed timer, meaning this will be the only timer on your device.
        /// The timer will fire when it reaches the limit of the 'timeout' parameter.
        /// </summary>
        /// <param name="relayWorkflow">the workflow.</param>
        /// <param name="timeout">the number of seconds you would like to wait until the timer fires.</param>
        /// <returns>the event response.</returns>
        public async Task<Dictionary<string, object>> StartTimer(IRelayWorkflow relayWorkflow, int timeout)
        {
            return await Send((await GetRunningRelayWorkflowOrThrow(relayWorkflow)).WebSocketConnection, StartTimer_(timeout));
        }

        private static Dictionary<string, object> StartTimer_(int timeout)
        {
            return Request(
                RequestType.StartTimer,
                new Dictionary<string, object>
                {
                    ["timeout"] = timeout
                }
            );
        }

        /// <summary>
        /// Stops an unnamed timer.
        /// </summary>
        /// <param name="relayWorkflow">the workflow.</param>
        /// <returns>the event response.</returns>
        public async Task<Dictionary<string, object>> StopTimer(IRelayWorkflow relayWorkflow)
        {
            return await Send((await GetRunningRelayWorkflowOrThrow(relayWorkflow)).WebSocketConnection, StopTimer_());
        }

        private static Dictionary<string, object> StopTimer_()
        {
            return Request(
                RequestType.StopTimer,
                new Dictionary<string, object>
                {

                }
            );
        }

        /// <summary>
        /// Serves as a named timer that can be either interval or timeout.  Allows you to specify the unit of time.
        /// </summary>
        /// <param name="relayWorkflow">the workflow.</param>
        /// <param name="name">a name for your timer.</param>
        /// <param name="timerType">can be "timeout" or "interval".  Defaults to "timeout"</param>
        /// <param name="timeout">an integer representing when you would like your timer to fire.</param>
        /// <param name="timeoutType">can be "ms", "secs", "mins", or "hrs".  Defaults to "secs".</param>
        /// <returns>the event response.</returns>
        public async Task<Dictionary<string, object>> SetTimer(IRelayWorkflow relayWorkflow, string name, string timerType, int timeout, string timeoutType)
        {
            return await Send((await GetRunningRelayWorkflow(relayWorkflow)).WebSocketConnection, SetTimer_(name, timerType, timeout, timeoutType));
        }

        private static Dictionary<string, object> SetTimer_(string name, string timerType, int timeout, string timeoutType)
        {
            return Request(
                RequestType.SetTimer,
                new Dictionary<string, object>
                {
                    ["type"] = timerType,
                    ["name"] = name,
                    ["timeout"] = timeout,
                    ["timeout_type"] = timeoutType
                }
            );
        }

        /// <summary>
        /// Clears the specified timer.
        /// </summary>
        /// <param name="relayWorkflow">the workflow.</param>
        /// <param name="name">the name of the timer that you would like to clear.</param>
        /// <returns>the event response.</returns>
        public async Task<Dictionary<string, object>> ClearTimer(IRelayWorkflow relayWorkflow, string name)
        {
            return await Send((await GetRunningRelayWorkflowOrThrow(relayWorkflow)).WebSocketConnection, ClearTimer_(name));
        }

        private static Dictionary<string, object> ClearTimer_(string name)
        {
            return Request(
                RequestType.ClearTimer,
                new Dictionary<string, object>
                {
                    ["name"] = name
                }
            );
        }

        /// <summary>
        /// Creates an incident that will alert the Relay Dash.
        /// </summary>
        /// <param name="relayWorkflow">the workflow.</param>
        /// <param name="originator">the device URN that triggered the incident.</param>
        /// <param name="iType">the type of incident that occurred.</param>
        /// <returns>the event response.</returns>
        public async Task<Dictionary<string, object>> CreateIncident(IRelayWorkflow relayWorkflow, string originator, string iType)
        {
            return await Send((await GetRunningRelayWorkflowOrThrow(relayWorkflow)).WebSocketConnection, CreateIncident_(originator, iType));
        }

        private static Dictionary<string, object> CreateIncident_(string originator, string iType)
        {
            return Request(
                RequestType.CreateIncident,
                new Dictionary<string, object>
                {
                    ["type"] = iType,
                    ["originator_uri"] = originator
                }
            );
        }

        /// <summary>
        /// Resolves an incident that was created.
        /// </summary>
        /// <param name="relayWorkflow">the workflow.</param>
        /// <param name="incidentId">the ID of the incident you would like to resolve.</param>
        /// <param name="reason">the reason for resolving the incident.</param>
        /// <returns></returns>
        public async Task<Dictionary<string, object>> ResolveIncident(IRelayWorkflow relayWorkflow, string incidentId, string reason)
        {
            return await Send((await GetRunningRelayWorkflowOrThrow(relayWorkflow)).WebSocketConnection, ResolveIncident_(incidentId, reason));
        }

        private static Dictionary<string, object> ResolveIncident_(string incidentId, string reason)
        {
            return Request(
                RequestType.ResolveIncident,
                new Dictionary<string, object>
                {
                    ["incident_id"] = incidentId,
                    ["reason"] = reason
                }
            );
        }

        // RESTART/POWERING DOWN THE DEVICE CURRENTLY DO NOT WORK

        // /// <summary>
        // /// Restarts a device during a workflow, without having to physically restart the device via hodling down the '-' button.
        // /// </summary>
        // /// <param name="relayWorkflow">the workflow.</param>
        // /// <param name="target">the URN of the device you would like to restart.</param>
        // /// <returns></returns>
        // public async Task<Dictionary<string, object>> RestartDevice(IRelayWorkflow relayWorkflow, string target)
        // {
        //     return await Send((await GetRunningRelayWorkflowOrThrow(relayWorkflow)).WebSocketConnection, RestartDevice_(target));
        // }

        // private static Dictionary<string, object> RestartDevice_ (string target)
        // {
        //     return Request (
        //         RequestType.DevicePowerOff,
        //         target,
        //         new Dictionary<string, object>
        //         {
        //             ["restart"] = true
        //         }
        //     );
        // }
        
        // /// <summary>
        // /// Powers down a device during a workflow, without having to physically power down the device via holding down the '+' button.
        // /// </summary>
        // /// <param name="relayWorkflow">the workflow.</param>
        // /// <param name="target">the URN of the device you would like to power down.</param>
        // /// <returns></returns>
        // public async Task<Dictionary<string, object>> PowerDownDevice(IRelayWorkflow relayWorkflow, string target)
        // {
        //     return await Send((await GetRunningRelayWorkflowOrThrow(relayWorkflow)).WebSocketConnection, PowerDownDevice_(target));
        // }

        // private static Dictionary<string, object> PowerDownDevice_ (string target)
        // {
        //     return Request (
        //         RequestType.DevicePowerOff,
        //         target,
        //         new Dictionary<string, object>
        //         {
        //             ["restart"] = false
        //         }
        //     );
        // }
        
        /// <summary>
        /// Used for performing actions on the LEDs, such as creating a rainbow, flashing, rotating, etc.
        /// </summary>
        /// <param name="relayWorkflow">the workflow.</param>
        /// <param name="target">the interaction URN.</param>
        /// <param name="ledEffect">effect to perform on the LEDs, can be 'rainbow', 'rotate', 'flash', 'breath', 'static', or 'off'.</param>
        /// <param name="ledInfo">information regarding the actions on the LED, such as the number of rotations, the count, etc.</param>
        /// <returns>the event response.</returns>
        public async Task<Dictionary<string, object>> SetLed(IRelayWorkflow relayWorkflow, string target, LedEffect ledEffect, LedInfo ledInfo)
        {
            return await Send((await GetRunningRelayWorkflowOrThrow(relayWorkflow)).WebSocketConnection, SetLed_(target, ledEffect, ledInfo));
        }
        
        private static Dictionary<string, object> SetLed_(string target, LedEffect ledEffect, LedInfo ledInfo)
        {
            return Request(
                RequestType.SetLed,
                target,
                new Dictionary<string, object>
                {
                    ["effect"] = ledEffect.SerializedName,
                    ["args"] = ledInfo.Dictionary
                }
            );
        }

        /// <summary>
        /// Switches on an LED at a particular index to a specified color.
        /// </summary>
        /// <param name="relayWorkflow">the workflow.</param>
        /// <param name="target">the interaction URN.</param>
        /// <param name="ledIndex">the index of the LED, numbered 1-12.</param>
        /// <param name="color">the hex color code that you would like to set the LED to.</param>
        /// <returns>the event response.</returns>
        public async Task<Dictionary<string, object>> SwitchLedOn(IRelayWorkflow relayWorkflow, string target, LedIndex ledIndex, string color)
        {
            LedInfo ledInfo = new LedInfo();
            ledInfo.SetColor(ledIndex, color);
            return await SetLed(relayWorkflow, target, LedEffect.Static, ledInfo);
        }
        
        /// <summary>
        /// Switches all of the LEDs on a device on to a specified color.
        /// </summary>
        /// <param name="relayWorkflow">the workflow.</param>
        /// <param name="target">the interaction URN.</param>
        /// <param name="color">the hex color code you would like the LEDs to be.</param>
        /// <returns>the event response.</returns>
        public async Task<Dictionary<string, object>> SwitchAllLedOn(IRelayWorkflow relayWorkflow, string target, string color)
        {
            return await SwitchLedOn(relayWorkflow, target, LedIndex.Ring, color);
        }
        
        /// <summary>
        /// Switches all of the LEDs on a device off.
        /// </summary>
        /// <param name="relayWorkflow">the workflow.</param>
        /// <param name="target">the interaction URN.</param>
        /// <returns>the event response.</returns>
        public async Task<Dictionary<string, object>> SwitchAllLedOff(IRelayWorkflow relayWorkflow, string target)
        {
            return await SetLed(relayWorkflow, target, LedEffect.Off, new LedInfo());
        }

        /// <summary>
        /// Switches all of the LEDs on to a configured rainbow pattern and rotates them a 
        /// specified number of times.
        /// </summary>
        /// <param name="relayWorkflow">the workflow.</param>
        /// <param name="target">the interaction URN.</param>
        /// <param name="rotations">the number of times you would like the rainbow to rotate.  Defaults to -1, meaning the 
        /// rainbow will rotate indefinitely.</param>
        /// <returns>the event response.</returns>
        public async Task<Dictionary<string, object>> Rainbow(IRelayWorkflow relayWorkflow, string target, int rotations = -1)
        {
            LedInfo ledInfo = new LedInfo();
            if (-1 != rotations)
            {
                ledInfo.SetRotations(rotations);
            }
            
            return await SetLed(relayWorkflow, target, LedEffect.Rainbow, ledInfo);
        }
        
        /// <summary>
        /// Switches all of the LEDs on a device to a certain color and rotates them a specified number
        /// of times.
        /// </summary>
        /// <param name="relayWorkflow">the workflow.</param>
        /// <param name="target">the interaction URN.</param>
        /// <param name="color">the hex color code you would like to turn the LEDs to. Defaults to 'FFFFFF'.</param>
        /// <returns>the event response.</returns>
        public async Task<Dictionary<string, object>> Rotate(IRelayWorkflow relayWorkflow, string target, string color = "FFFFFF")
        {
            LedInfo ledInfo = new LedInfo();
            ledInfo.SetColor(LedIndex.Led1, color);
            
            return await SetLed(relayWorkflow, target, LedEffect.Rotate, ledInfo);
        }
        
        /// <summary>
        /// Switches all of the LEDs on a device to a certain color and flashes them
        /// a specified number of times.
        /// </summary>
        /// <param name="relayWorkflow">the workflow.</param>
        /// <param name="target">the interaction URN.</param>
        /// <param name="color">the hex color code you would like to turn the LEDs to. Defaults to '0000FF'.</param>
        /// <returns>the event response.</returns>
        public async Task<Dictionary<string, object>> Flash(IRelayWorkflow relayWorkflow, string target, string color = "0000FF")
        {
            LedInfo ledInfo = new LedInfo();
            ledInfo.SetColor(LedIndex.Ring, color);
            
            return await SetLed(relayWorkflow, target, LedEffect.Flash, ledInfo);
        }
        
        /// <summary>
        /// Switches all of the LEDs on a device to a certain color and creates a 'breathing' effect, 
        /// where the LEDs will slowly light up a specified number of times.
        /// </summary>
        /// <param name="relayWorkflow">the workflow.</param>
        /// <param name="target">the interaction URN.</param>
        /// <param name="color">the hex color code you would like to turn the LEDs to. Defaults to '0000FF'.</param>
        /// <returns>the event response.</returns>
        public async Task<Dictionary<string, object>> Breathe(IRelayWorkflow relayWorkflow, string target, string color = "0000FF")
        {
            LedInfo ledInfo = new LedInfo();
            ledInfo.SetColor(LedIndex.Ring, color);
            
            return await SetLed(relayWorkflow, target, LedEffect.Breathe, ledInfo);
        }
        
        /// <summary>
        /// Sets a variable with the corresponding name and value. Scope of
        /// the variable is from start to end of a workflow.
        /// </summary>
        /// <param name="relayWorkflow">the workflow.</param>
        /// <param name="name">name of the variable to be created.</param>
        /// <param name="value">value that the variable will hold.</param>
        /// <returns>the event response.</returns>
        public async Task<Dictionary<string, object>> SetVar(IRelayWorkflow relayWorkflow, string name, string value)
        {
            return await Send((await GetRunningRelayWorkflowOrThrow(relayWorkflow)).WebSocketConnection, SetVar_(name, value));
        }
        
        private static Dictionary<string, object> SetVar_(string name, string value)
        {
            return Request(
                RequestType.SetVar,
                new Dictionary<string, object>
                {
                    ["name"] = name,
                    ["value"] = value
                }
            );
        }
        
        /// <summary>
        /// Unsets the value of a variable.
        /// </summary>
        /// <param name="relayWorkflow">the workflow.</param>
        /// <param name="name">the name of the variable whose value you would like to unset.</param>
        /// <returns>the event response.</returns>
        public async Task<Dictionary<string, object>> UnsetVar(IRelayWorkflow relayWorkflow, string name)
        {
            return await Send((await GetRunningRelayWorkflowOrThrow(relayWorkflow)).WebSocketConnection, UnsetVar_(name));
        }
        
        private static Dictionary<string, object> UnsetVar_(string name)
        {
            return Request(
                RequestType.UnsetVar,
                new Dictionary<string, object>
                {
                    ["name"] = name
                }
            );
        }
        
        /// <summary>
        /// Retrieves a variable that was set either during workflow registration
        /// or through the set_var() function.  The variable can be retrieved anywhere
        /// within the workflow, but is erased after the workflow terminates.
        /// </summary>
        /// <param name="relayWorkflow">the workflow.</param>
        /// <param name="name">name of the variable to be retrieved.</param>
        /// <param name="defaultValue">default value of the variable if it does not exist.  Defaults to undefined.</param>
        /// <returns>the variable requested.</returns>
        public async Task<string> GetVar(IRelayWorkflow relayWorkflow, string name, string defaultValue = null)
        {
            Dictionary<string, object> dictionary = await Send((await GetRunningRelayWorkflowOrThrow(relayWorkflow)).WebSocketConnection, GetVar_(name));
            
            if (null != dictionary && dictionary.ContainsKey("value") && null != dictionary["value"])
            {   
                return (string) dictionary["value"];
            }

            return defaultValue;
        }

        /// <summary>
        /// Retrieves a variable that is an integer type.
        /// </summary>
        /// <param name="relayWorkflow">the workflow.</param>
        /// <param name="name">the name of the variable to retrieve.</param>
        /// <param name="defaultValue">the default value for the variable if it does not exist.</param>
        /// <returns>the variable requested</returns>
        public async Task<int> GetNumberVar(IRelayWorkflow relayWorkflow, string name, int defaultValue = -1)
        {
            Dictionary<string, object> dictionary = await Send((await GetRunningRelayWorkflowOrThrow(relayWorkflow)).WebSocketConnection, GetVar_(name));

            if(null != dictionary && dictionary.ContainsKey("value") && null != dictionary["value"])
            {
                return Int32.Parse((string) dictionary["value"]);
            }
            return defaultValue;
        }
        
        private static Dictionary<string, object> GetVar_(string name)
        {
            return Request(
                RequestType.GetVar,
                new Dictionary<string, object>
                {
                    ["name"] = name
                }
            );
        }

        /// <summary>
        /// Listens for the user to speak into the device.  Utilizes speech to text functionality to interact
        /// with the user.
        /// </summary>
        /// <param name="relayWorkflow">the workflow.</param>
        /// <param name="target">the interaction URN.</param>
        /// <returns>text representation of what the user had spoken into the device.</returns>
         public async Task<Dictionary<string, object>> Listen(IRelayWorkflow relayWorkflow, string target)
        {
            return await Listen(relayWorkflow, target, Array.Empty<string>(), Language.English);
        }
        
        /// <summary>
        /// Listens for the user to speak into the device.  Utilizes speech to text functionality to interact
        /// with the user.
        /// </summary>
        /// <param name="relayWorkflow">the workflow.</param>
        /// <param name="target">the interaction URN.</param>
        /// <param name="phrases">optional phrases that you would like to limit the user's response to.  Defualts to none.</param>
        /// <returns>text representation of what the user had spoken into the device.</returns>
         public async Task<Dictionary<string, object>> Listen(IRelayWorkflow relayWorkflow, string target, string[] phrases)
        {
            return await Listen(relayWorkflow, target, phrases, Language.English);
        }
        
        /// <summary>
        /// Listens for the user to speak into the device.  Utilizes speech to text functionality to interact
        /// with the user.
        /// </summary>
        /// <param name="relayWorkflow">the workflow.</param>
        /// <param name="target">the interaction URN.</param>
        /// <param name="language">the language that the device is listening for.  Defaults to 'en-US'.</param>
        /// <returns>text representation of what the user had spoken into the device.</returns>
         public async Task<Dictionary<string, object>> Listen(IRelayWorkflow relayWorkflow, string target, Language language)
        {
            return await Listen(relayWorkflow, target, Array.Empty<string>(), language);
        }
        
        /// <summary>
        /// Listens for the user to speak into the device.  Utilizes speech to text functionality to interact
        /// with the user.
        /// </summary>
        /// <param name="relayWorkflow">the workflow.</param>
        /// <param name="target">the interaction URN.</param>
        /// <param name="phrases">limits the user's response to these phrases.</param>
        /// <param name="altLanguage">the language that the device will listen for.  Defaults to 'en-US'.</param>
        /// <param name="transcribe">whether you would like to transcribe.</param>
        /// <param name="timeout">how long to wait for a user's response before timing out.</param>
        /// <returns>text representation of what the user had spoken into the device.</returns>
         public async Task<Dictionary<string, object>> Listen(IRelayWorkflow relayWorkflow, string target, string[] phrases, Language altLanguage, bool transcribe = true, int timeout = 60)
        {
            var listenRequest = Listen_(target, phrases, altLanguage, transcribe, timeout);
            await Send((await GetRunningRelayWorkflowOrThrow(relayWorkflow)).WebSocketConnection, listenRequest);
            return await WaitForListenSpeech((string) listenRequest["_id"], timeout);
        }
        
        private async Task<Dictionary<string, object>> WaitForListenSpeech(string id, int timeout)
        {
            var eventTypeTaskCompletionSource = new EventTypeTaskCompletionSource
            {
                KeyValuesToMatch =
                {
                    ["request_id"] = id
                }
            };
            StoreEventTypeTaskCompletionSource(EventType.Speech, eventTypeTaskCompletionSource);

            Log.Debug("Going to wait for speech with request_id {Id}", id);
            var result = await Task.WhenAny(eventTypeTaskCompletionSource.TaskCompletionSource.Task, Task.Delay(timeout * 1000));
            Log.Debug("WaitForListenSpeech Task.WhenAny completes with {@result}", result);

            return await (Task<Dictionary<string, object>>) result;
        }
        
        private static Dictionary<string, object> Listen_(string target, string[] phrases, Language altLanguage, bool transcribe, int timeout)
        {
            var listenRequest = Request(
                RequestType.Listen,
                target,
                new Dictionary<string, object>
                {
                    ["transcribe"] = transcribe,
                    ["phrases"] = phrases,
                    ["timeout"] = timeout,
                    ["alt_lang"] = altLanguage.SerializedName
                }
            );
            
            listenRequest["request_id"] = listenRequest["_id"];

            return listenRequest;
        }

        public async Task<string> Translate(IRelayWorkflow relayWorkflow, string text, Language from, Language to)
        {
            var translateRequest = await Send((await GetRunningRelayWorkflowOrThrow(relayWorkflow)).WebSocketConnection, Translate_(text, from , to));
            return (string) translateRequest["text"];
        }

        private static Dictionary<string, object> Translate_(string text, Language from, Language to)
        {
            return Request(
                RequestType.Translate,
                new Dictionary<string, object>
                {
                    ["text"] = text,
                    ["from_lang"] = from.SerializedName,
                    ["to_lang"] = to.SerializedName
                }
            );

        }

        public async Task<Dictionary<string, object>> PlayUnreadInboxMessages(IRelayWorkflow relayWorkflow, string target)
        {
            return await Send((await GetRunningRelayWorkflowOrThrow(relayWorkflow)).WebSocketConnection, PlayUnreadInboxMessages_(target));

        }

        private static Dictionary<string, object> PlayUnreadInboxMessages_(string target)
        {
            return Request(
                RequestType.PlayInboxMessages, 
                target,
                new Dictionary<string, object>
                {

                }
            );
        }

        public async Task<int> GetUnreadInboxSize(IRelayWorkflow relayWorkflow, string target)
        {
            Dictionary<string, object> dictionary = await Send((await GetRunningRelayWorkflowOrThrow(relayWorkflow)).WebSocketConnection, GetUnreadInboxSize_(target));
            if(null != dictionary && dictionary.ContainsKey("count") && null != dictionary["count"])
            {
                return Int32.Parse((string) dictionary["count"]);
            }
            return -1;
        }

        private static Dictionary<string, object> GetUnreadInboxSize_(string target)
        {
            return Request(
                RequestType.InboxCount,
                target,
                new Dictionary<string, object>
                {

                }
            );
        }

        private async Task<Dictionary<string, object>> SendNotification(IRelayWorkflow relayWorkflow, string originator, NotificationType notificationType, string text, string targets, string name, NotificationPushOptions notificationPushOptions)
        {
            return await Send((await GetRunningRelayWorkflowOrThrow(relayWorkflow)).WebSocketConnection, SendNotification_(originator, notificationType, text, targets, name, notificationPushOptions), DelayNotificationTimeout);
        }
        
        private static Dictionary<string, object> SendNotification_(string originator, NotificationType notificationType, string text, string targets, string name, NotificationPushOptions notificationPushOptions)
        {
            if (notificationPushOptions == null)
            {
                notificationPushOptions = new NotificationPushOptions();
            }
            return Request(
                RequestType.Notification,
                targets,
                new Dictionary<string, object>
                {
                    ["originator"] = originator,
                    ["type"] = notificationType.SerializedName,
                    ["name"] = name,
                    ["text"] = text,
                    ["push_opts"] = notificationPushOptions.Dictionary
                }
            );
        }
        
        /// <summary>
        /// Cancels a notification of any type that was sent to a group of devices.
        /// </summary>
        /// <param name="relayWorkflow">the workflow.</param>
        /// <param name="originator">the device or interacion URN that sent out the message.</param>
        /// <param name="name">the name of the notification to cancel.</param>
        /// <param name="targets">the group URN that received the notification.</param>
        /// <returns>the event response.</returns>
        public async Task<Dictionary<string, object>> CancelNotification(IRelayWorkflow relayWorkflow, string originator, string name, string targets)
        {
            return await SendNotification(relayWorkflow, originator, NotificationType.Cancel, null, targets, name, null);
        }

        /// <summary>
        /// Sends out a broadcasted message to a group of devices.  The message is played out on 
        /// all devices, as well as sent to the Relay Dash.
        /// </summary>
        /// <param name="relayWorkflow">the workflow.</param>
        /// <param name="originator">the device URN that triggered the broadcast.</param>
        /// <param name="name">a name for your broadcast.</param>
        /// <param name="text">the text that you would like to be broadcasted to your group.</param>
        /// <param name="targets">the group URN that you would like to broadcast your message to.</param>
        /// <returns>the event response.</returns>
        public async Task<Dictionary<string, object>> Broadcast(IRelayWorkflow relayWorkflow, string targets, string originator, string name, string text)
        {
            return await Broadcast(relayWorkflow, targets, originator, name, text, null);
        }
        
        /// <summary>
        /// Sends out a broadcasted message to a group of devices.  The message is played out on 
        /// all devices, as well as sent to the Relay Dash.
        /// </summary>
        /// <param name="relayWorkflow">the workflow.</param>
        /// <param name="originator">the device URN that triggered the broadcast.</param>
        /// <param name="name">a name for your broadcast.</param>
        /// <param name="text">the text that you would like to be broadcasted to your group.</param>
        /// <param name="targets">the group URN that you would like to broadcast your message to.</param>
        /// <param name="notificationPushOptions">push options for if the notification is sent to the Relay app on a virtual device.  Defaults to {}.</param>
        /// <returns>the event response.</returns>
        public async Task<Dictionary<string, object>> Broadcast(IRelayWorkflow relayWorkflow, string targets, string originator, string name, string text, NotificationPushOptions notificationPushOptions)
        {
            return await SendNotification(relayWorkflow, originator, NotificationType.Broadcast, text, targets, name, notificationPushOptions);
        }
        
        /// <summary>
        /// Cancels the broadcast that was sent to a group of devices.
        /// </summary>
        /// <param name="relayWorkflow">the workflow.</param>
        /// <param name="originator">the device URN that is cancelling the broadcast.</param>
        /// <param name="name">the name of the broadcast you would like to cancel.</param>
        /// <param name="targets">the group URN that received the broadcast.</param>
        /// <returns>the event response.</returns>
        public async Task<Dictionary<string, object>> CancelBroadcast(IRelayWorkflow relayWorkflow, string originator, string name, string targets)
        {
            return await CancelNotification(relayWorkflow, originator, name, targets);
        }
        
        /// <summary>
        /// Sends out an alert to the specified group of devices and the Relay Dash.
        /// </summary>
        /// <param name="relayWorkflow">the workflow.</param>
        /// <param name="originator">the URN of the device that triggered the alert.</param>
        /// <param name="name">a name for your alert.</param>
        /// <param name="text">the text that you would like to be spoken to the group as your alert.</param>
        /// <param name="targets">the group URN that you would like to send your alert to.</param>
        /// <returns>the event response.</returns>
        public async Task<Dictionary<string, object>> Alert(IRelayWorkflow relayWorkflow, string targets, string originator, string name, string text)
        {
            return await Alert(relayWorkflow, targets, originator, name, text, null);
        }
        
        /// <summary>
        /// Sends out an alert to the specified group of devices and the Relay Dash.
        /// </summary>
        /// <param name="relayWorkflow">the workflow.</param>
        /// <param name="originator">the URN of the device that triggered the alert.</param>
        /// <param name="name">a name for your alert.</param>
        /// <param name="text">the text that you would like to be spoken to the group as your alert.</param>
        /// <param name="targets">the group URN that you would like to send your alert to.</param>
        /// <param name="notificationPushOptions">push options for if the alert is sent to the Relay app on a virtual device. Defaults to {}.</param>
        /// <returns>the event response.</returns>
        public async Task<Dictionary<string, object>> Alert(IRelayWorkflow relayWorkflow, string targets, string originator, string name, string text, NotificationPushOptions notificationPushOptions)
        {
            return await SendNotification(relayWorkflow, originator, NotificationType.Alert, text, targets, name, notificationPushOptions);
        }
        
        /// <summary>
        /// Cancels an alert that was sent to a group of devices.  Particularly useful if you would like to cancel the alert
        /// on all devices after one device has acknowledged the alert.
        /// </summary>
        /// <param name="relayWorkflow">the workflow.</param>
        /// <param name="originator">the URN of the device that acknowledged or is cancelling the alert.</param>
        /// <param name="name">the name of the alert.</param>
        /// <param name="targets">the group URN that received the alert.</param>
        /// <returns>the event response.</returns>
        public async Task<Dictionary<string, object>> CancelAlert(IRelayWorkflow relayWorkflow, string originator, string name, string targets)
        {
            return await CancelNotification(relayWorkflow, originator, name, targets);
        }
        
        /// <summary>
        /// Returns the members of a particular group.
        /// </summary>
        /// <param name="relayWorkflow">the workflow.</param>
        /// <param name="groupName">the name of the group whose members you would like to retrieve. </param>
        /// <returns>an array containing all of the device names in the specified group.</returns>
        public async Task<string[]> GetGroupMembers(IRelayWorkflow relayWorkflow, string groupName)
        {
            Dictionary<string, object> dictionary = await Send((await GetRunningRelayWorkflowOrThrow(relayWorkflow)).WebSocketConnection, GetGroupMembers_(groupName));

            if (dictionary.ContainsKey("device_names"))
            {
                return (string[]) dictionary["device_names"];
            }

            return Array.Empty<string>();
        }
        
        private static Dictionary<string, object> GetGroupMembers_(string groupName)
        {
            return Request(
                RequestType.ListGroupMembers,
                new Dictionary<string, object>
                {
                    ["group_name"] = groupName
                }
            );
        }
        
        private async Task<Dictionary<string, object>> GetDeviceInfo(IRelayWorkflow relayWorkflow, string target, DeviceInfoQuery deviceInfoQuery, bool refresh = false)
        {
            return await Send((await GetRunningRelayWorkflowOrThrow(relayWorkflow)).WebSocketConnection, GetDeviceInfo_(target, deviceInfoQuery, refresh), refresh ? DelayRefreshTimeout : DelayTimeout);
        }
        
        private static Dictionary<string, object> GetDeviceInfo_(string target, DeviceInfoQuery deviceInfoQuery, bool refresh)
        {
            return Request(
                RequestType.GetDeviceInfo,
                target, 
                new Dictionary<string, object>
                {
                    ["query"] = deviceInfoQuery.SerializedName,
                    ["refresh"] = refresh
                }
            );
        }

        private static string GetDictionaryKeyValueAsString(Dictionary<string, object> dictionary, string key)
        {
            if (dictionary.ContainsKey(key))
            {
                return (string) dictionary[key];
            }

            return null;
        }
        
        private static float[] GetDictionaryKeyValueAsFloatArray(Dictionary<string, object> dictionary, string key)
        {
            if (dictionary.ContainsKey(key))
            {
                return (float[]) dictionary[key];
            }

            return Array.Empty<float>();
        }
        
        private static int GetDictionaryKeyValueAsInt(Dictionary<string, object> dictionary, string key)
        {
            if (dictionary.ContainsKey(key))
            {
                return (int) dictionary[key];
            }

            return -1;
        }
        
        private static DeviceType GetDictionaryKeyValueAsDeviceType(Dictionary<string, object> dictionary, string key)
        {
            DeviceType deviceType = null;
            
            if (dictionary.ContainsKey(key))
            {
                DeviceType.TryParseSerializedName((string) dictionary[key], out deviceType);
            }

            return deviceType;
        }

        private static bool GetDictionaryKeyValueAsBool(Dictionary<string, object> dictionary, string key)
        {
            if (dictionary.ContainsKey(key))
            {
                return (bool) dictionary[key];
            }
            return false;
        }
        
        /// <summary>
        /// Returns the name of a targeted device.
        /// </summary>
        /// <param name="relayWorkflow">the workflow.</param>
        /// <param name="target">the device or interaction URN.</param>
        /// <returns>the name of the device.</returns>
        public async Task<string> GetDeviceName(IRelayWorkflow relayWorkflow, string target)
        {
            DeviceInfoQuery deviceInfoQuery = DeviceInfoQuery.NameQuery;
            return GetDictionaryKeyValueAsString(await GetDeviceInfo(relayWorkflow, target, deviceInfoQuery), deviceInfoQuery.SerializedName);
        }
        
        /// <summary>
        /// Returns the location of a targeted device.
        /// </summary>
        /// <param name="relayWorkflow">the workflow.</param>
        /// <param name="target">the device or interaction URN.</param>
        /// <param name="refresh">whether you would like to refresh before retrieving the location.  Defaults to false.</param>
        /// <returns>the location of the device.</returns>
        public async Task<string> GetDeviceLocation(IRelayWorkflow relayWorkflow, string target, bool refresh)
        {
            DeviceInfoQuery deviceInfoQuery = DeviceInfoQuery.LocationQuery;
            return GetDictionaryKeyValueAsString(await GetDeviceInfo(relayWorkflow, target, deviceInfoQuery, refresh), deviceInfoQuery.SerializedName);
        }
        
        /// <summary>
        /// Returns the ID of a targeted device.
        /// </summary>
        /// <param name="relayWorkflow">the workflow.</param>
        /// <param name="target">the device or interaction URN.</param>
        /// <returns>the device ID.</returns>
        public async Task<string> GetDeviceId(IRelayWorkflow relayWorkflow, string target)
        {
            DeviceInfoQuery deviceInfoQuery = DeviceInfoQuery.IdQuery;
            return GetDictionaryKeyValueAsString(await GetDeviceInfo(relayWorkflow, target, deviceInfoQuery), deviceInfoQuery.SerializedName);
        }
        
        /// <summary>
        /// Returns the address of a targeted device.
        /// </summary>
        /// <param name="relayWorkflow">the workflow.</param>
        /// <param name="target">the device or interaction URN.</param>
        /// <param name="refresh">whether you would like to refresh before retrieving the address.  Defaults to false.</param>
        /// <returns>the address of the device.</returns>
        public async Task<string> GetDeviceAddress(IRelayWorkflow relayWorkflow, string target, bool refresh)
        {
            return await GetDeviceLocation(relayWorkflow, target, refresh);
        }
        
        /// <summary>
        /// Retrieves the coordinates of the device's location.
        /// </summary>
        /// <param name="relayWorkflow">the workflow.</param>
        /// <param name="target">the device or interaction URN.</param>
        /// <param name="refresh">whether you would like to refresh before retrieving the coordinates.  Defaults to false.</param>
        /// <returns>the coordinates of the device's location.</returns>
        public async Task<float[]> GetDeviceCoordinates(IRelayWorkflow relayWorkflow, string target, bool refresh)
        {
            DeviceInfoQuery deviceInfoQuery = DeviceInfoQuery.CoordinatesQuery;
            return GetDictionaryKeyValueAsFloatArray(await GetDeviceInfo(relayWorkflow, target, deviceInfoQuery, refresh), deviceInfoQuery.SerializedName);
        }
        
        /// <summary>
        /// Returns the latitude and longitude coordinates of a targeted device.
        /// </summary>
        /// <param name="relayWorkflow">the workflow.</param>
        /// <param name="target">the device or interaction URN.</param>
        /// <param name="refresh">whether you would like to refresh before retrieving the coordinates.  Defaults to false.</param>
        /// <returns>an array containing the latitude and longitude of the device's location.</returns>
        public async Task<float[]> GetDeviceLatLong(IRelayWorkflow relayWorkflow, string target, bool refresh)
        {
            return await GetDeviceCoordinates(relayWorkflow, target, refresh);
        }
        
        /// <summary>
        /// Returns the indoor location of a targeted device.
        /// </summary>
        /// <param name="relayWorkflow">the workflow.</param>
        /// <param name="target">the device or interaction URN.</param>
        /// <param name="refresh">whether you wouldlike to refresh before retrieving the location.  Defaults to false.</param>
        /// <returns>the indoor location of the device.</returns>
        public async Task<float[]> GetDeviceIndoorLocation(IRelayWorkflow relayWorkflow, string target, bool refresh)
        {
            DeviceInfoQuery deviceInfoQuery = DeviceInfoQuery.IndoorLocationQuery;
            return GetDictionaryKeyValueAsFloatArray(await GetDeviceInfo(relayWorkflow, target, deviceInfoQuery, refresh), deviceInfoQuery.SerializedName);
        }
        
        /// <summary>
        /// Returns the battery level of a targeted device.
        /// </summary>
        /// <param name="relayWorkflow">the workflow.</param>
        /// <param name="target">the device or interaction URN.</param>
        /// <param name="refresh">whether you would like to refresh before retrieving the battery.  Defaults to false.</param>
        /// <returns>the battery level on the device.</returns>
        public async Task<int> GetDeviceBattery(IRelayWorkflow relayWorkflow, string target, bool refresh)
        {
            DeviceInfoQuery deviceInfoQuery = DeviceInfoQuery.BatteryQuery;
            return GetDictionaryKeyValueAsInt(await GetDeviceInfo(relayWorkflow, target, deviceInfoQuery, refresh), deviceInfoQuery.SerializedName);
        }
        
        /// <summary>
        /// Returns the device type of a targeted device, i.e. gen 2, gen 3, etc.
        /// </summary>
        /// <param name="relayWorkflow">the workflow.</param>
        /// <param name="target">the device or interaction URN.</param>
        /// <param name="refresh">whether you would like to refresh before retrieving the device type.</param>
        /// <returns>the device type.</returns>
        public async Task<DeviceType> GetDeviceType(IRelayWorkflow relayWorkflow, string target, bool refresh)
        {
            DeviceInfoQuery deviceInfoQuery = DeviceInfoQuery.TypeQuery;
            return GetDictionaryKeyValueAsDeviceType(await GetDeviceInfo(relayWorkflow, target, deviceInfoQuery, refresh), deviceInfoQuery.SerializedName);
        }

        // public async Task<Dictionary<string, object>> GetUnreadInboxSize(IRelayWorkflow relayWorkflow, string target)
        // {
        //     Dictionary<string, object> response = await Send((await GetRunningRelayWorkflowOrThrow(relayWorkflow)).WebSocketConnection, GetUnreadInboxSize_(target));
        // }

        // private static Dictionary<string, object> GetUnreadInboxSize(string target)
        
        
        /// <summary>
        /// Returns the user profile of a targeted device.
        /// </summary>
        /// <param name="relayWorkflow">the workflow.</param>
        /// <param name="target">the device or interaction URN.</param>
        /// <returns>the user profile registered to the device.</returns>
        public async Task<string> GetUserProfile(IRelayWorkflow relayWorkflow, string target)
        {
            DeviceInfoQuery deviceInfoQuery = DeviceInfoQuery.UsernameQuery;
            return GetDictionaryKeyValueAsString(await GetDeviceInfo(relayWorkflow, target, deviceInfoQuery), deviceInfoQuery.SerializedName);
        }

        /// <summary>
        /// Enables the location services on a device.
        /// </summary>
        /// <param name="relayWorkflow">the workflow.</param>
        /// <param name="target">the device or interaction URN.</param>
        /// <returns>the event response.</returns>
        public async Task<Dictionary<string, object>> EnableLocation(IRelayWorkflow relayWorkflow, string target) 
        {
            return await SetDeviceInfo(relayWorkflow, target, DeviceInfoField.Location, "true");
        }
        
        /// <summary>
        /// Disables the location services on a device.
        /// </summary>
        /// <param name="relayWorkflow">the workflow.</param>
        /// <param name="target">the device or interaction URN.</param>
        /// <returns>the event response.</returns>
        public async Task<Dictionary<string, object>> DisableLocation(IRelayWorkflow relayWorkflow, string target) 
        {
            return await SetDeviceInfo(relayWorkflow, target, DeviceInfoField.Location, "false");
        }

        /// <summary>
        /// Returns true if the device's location services are enabled, false otherwise.
        /// </summary>
        /// <param name="relayWorkflow">the workflow</param>
        /// <param name="target">the device or interaction URN.</param>
        /// <param name="refresh">whether you would like to refresh before retrieving the query information.</param>
        /// <returns>true if the device's location services are enabled, false otherwise.</returns>
        public async Task<bool> GetDeviceLocationEnabled(IRelayWorkflow relayWorkflow, string target, bool refresh) 
        {
            DeviceInfoQuery deviceInfoQuery = DeviceInfoQuery.LocationEnabledQuery;
            return GetDictionaryKeyValueAsBool(await GetDeviceInfo(relayWorkflow, target, deviceInfoQuery, refresh), deviceInfoQuery.SerializedName);
        }

        /// <summary>
        /// Sets the profile of a user by updating the username.
        /// </summary>
        /// <param name="relayWorkflow">the workflow.</param>
        /// <param name="target">the device URN whose profile you would like to update.</param>
        /// <param name="username">the updated username for the device.</param>
        /// <param name="force">whether you would like to force this update.  Defaults to false.</param>
        /// <returns>a string array of the device names under the user's profile.</returns>
        public async Task<string[]> SetUserProfile(IRelayWorkflow relayWorkflow, string target, string username, bool force = false)
        {
            Dictionary<string, object> dictionary = await Send((await GetRunningRelayWorkflowOrThrow(relayWorkflow)).WebSocketConnection, SetUserProfile_(target, username, force));
            
            if (null != dictionary && dictionary.ContainsKey("device_names") && null != dictionary["device_names"])
            {
                return (string[]) dictionary["device_names"];
            }

            return Array.Empty<string>();
        }
        
        private static Dictionary<string, object> SetUserProfile_(string target, string username, bool force)
        {
            return Request(
                RequestType.SetUserProfile,
                target, 
                new Dictionary<string, object>
                {
                    ["username"] = username,
                    ["force"] = force
                }
            );
        }

        // SETDEVICEMODE CURRENTLY DOES NOT WORK

        // /// <summary>
        // /// Sets the mode of the device.
        // /// </summary>
        // /// <param name="relayWorkflow">the workflow.</param>
        // /// <param name="target">the device or interaction URN.</param>
        // /// <param name="mode">the updated mode of the device, which can be "panic", "alarm", or "none".  Defaults to "none".</param>
        // /// <returns></returns>
        // public async Task<Dictionary<string, object>> SetDeviceMode(IRelayWorkflow relayWorkflow, string target, string mode)
        // {
        //     return await Send((await GetRunningRelayWorkflowOrThrow(relayWorkflow)).WebSocketConnection, SetDeviceMode_(target, mode));
        // }

        // private static Dictionary<string, object> SetDeviceMode_(string target, string mode)
        // {
        //     return Request(
        //         RequestType.SetChannel,
        //         target,
        //         new Dictionary<string, object>
        //         {
        //             ["mode"] = mode
        //         }
        //     );
        // }
        
        /// <summary>
        /// Log an analytic event from a workflow with the specified content and
        /// under a specified category.  This includes the device who triggered the workflow
        /// that called this function.
        /// </summary>
        /// <param name="relayWorkflow">the workflow.</param>
        /// <param name="message">a description for your analytical event.</param>
        /// <param name="target">the URN of a device that triggered this function.  Defaults to None.</param>
        /// <param name="category">a category for your analytical event.</param>
        /// <returns></returns>
        public async Task<Dictionary<string, object>> LogUserMessage(IRelayWorkflow relayWorkflow, string message, string target, string category)
        {
            return await Send((await GetRunningRelayWorkflowOrThrow(relayWorkflow)).WebSocketConnection, LogUserMessage_(message, target, category));
        }  

        private static Dictionary<string, object> LogUserMessage_(string message, string target, string category)
        {
            return Request(
                RequestType.LogAnalyticsEvent,
                new Dictionary<string, object>
                {
                    ["content"] = message,
                    ["content_type"] = "text/plain",
                    ["category"] = category,
                    ["device_uri"] = target
                }
            );
        }

        /// <summary>
        /// Log an analytics event from a workflow with the specified content and
        /// under a specified category. This does not log the device who
        /// triggered the workflow that called this function.
        /// </summary>
        /// <param name="relayWorkflow">the workflow.</param>
        /// <param name="message">a description for your analytical event.</param>
        /// <param name="category">a category for your analytical event.</param>
        /// <returns></returns>
        public async Task<Dictionary<string, object>> LogMessage(IRelayWorkflow relayWorkflow, string message, string category)
        {
            return await Send((await GetRunningRelayWorkflowOrThrow(relayWorkflow)).WebSocketConnection, LogMessage_(message, category));
        }

        private static Dictionary<string, object> LogMessage_(string message, string category)
        {
            return Request(
                RequestType.LogAnalyticsEvent,
                new Dictionary<string, object>
                {
                    ["content"] = message,
                    ["content_type"] = "text/plain",
                    ["category"] = category
                }
            );
        }

        private async Task<Dictionary<string, object>> SetDeviceInfo(IRelayWorkflow relayWorkflow, string target, DeviceInfoField deviceInfoField, string value)
        {
            return await Send((await GetRunningRelayWorkflowOrThrow(relayWorkflow)).WebSocketConnection, SetDeviceInfo_(target, deviceInfoField, value));
        }
        
        private static Dictionary<string, object> SetDeviceInfo_(string target, DeviceInfoField deviceInfoField, string value)
        {
            return Request(
                RequestType.SetDeviceInfo,
                target,
                new Dictionary<string, object>
                {
                    ["field"] = deviceInfoField.SerializedName,
                    ["value"] = value
                }
            );
        }
        
        /// <summary>
        /// Sets the name of a targeted device and updates it on the Relay Dash.
        /// The name remains updated until it is set again via a workflow or updated manually
        /// on the Relay Dash.
        /// </summary>
        /// <param name="relayWorkflow">the workflow.</param>
        /// <param name="target">the device or interaction URN.</param>
        /// <param name="name">a new name for your device.</param>
        /// <returns>the event response.</returns>
        public async Task<Dictionary<string, object>> SetDeviceName(IRelayWorkflow relayWorkflow, string target, string name)
        {
            return await SetDeviceInfo(relayWorkflow, target, DeviceInfoField.Label, name);
        }
        
        // SETDEVICECHANNEL CURRENTLY DOES NOT WORK

        // /// <summary>
        // /// Sets the channel of a targeted device and updates it on the Relay Dash.
        // /// The new channel remains until it is set again via a workflow or updated on the
        // /// Relay Dash.
        // /// </summary>
        // /// <param name="relayWorkflow">the workflow.</param> 
        // /// <param name="target">the device or interaction URN.</param>
        // /// <param name="channel">the channel that you would like to update your device to.</param>
        // /// <returns>the event response.</returns>
        // public async Task<Dictionary<string, object>> SetDeviceChannel(IRelayWorkflow relayWorkflow, string target, string channel)
        // {
        //     return await Send((await GetRunningRelayWorkflowOrThrow(relayWorkflow)).WebSocketConnection, SetDeviceChannel_(target, channel));
        // }

        // private static Dictionary<string, object> SetDeviceChannel_(string target, string channel)
        // {
        //     return Request (
        //         RequestType.
        //     )
        // }
        
        /// <summary>
        /// Sets the channel that a device is on.  This can be used to change the channel of a device during a workflow,
        /// where the channel will also be updated on the Relay Dash.
        /// </summary>
        /// <param name="relayWorkflow">the workflow.</param>
        /// <param name="target">the device or interaction URN.</param>
        /// <param name="channel">the name of the channel that you would like to set your device to.</param>
        /// <param name="targets">the group URN whose channel you would like to set.</param>
        /// <param name="suppressTts">whether you would like to surpress the text to speech.  Defaults to false.</param>
        /// <param name="disableHomeChannel">whether you would like to disable the home channel.  Defaults to false.</param>
        /// <returns>the event response.</returns>
        public async Task<Dictionary<string, object>> SetChannel(IRelayWorkflow relayWorkflow, string target, string channel, string[] targets, bool suppressTts = false, bool disableHomeChannel = false)
        {
            return await Send((await GetRunningRelayWorkflowOrThrow(relayWorkflow)).WebSocketConnection, SetChannel_(target, channel, targets, suppressTts, disableHomeChannel));
        }
        
        private static Dictionary<string, object> SetChannel_(string target, string channel, string[] targets, bool suppressTts, bool disableHomeChannel)
        {
            return Request(
                RequestType.SetChannel,
                target,
                new Dictionary<string, object>
                {
                    ["channel_name"] = channel,
                    ["target"] = targets,
                    ["suppress_tts"] = suppressTts,
                    ["disable_home_channel"] = disableHomeChannel
                }
            );
        }
        
        /// <summary>
        /// Sets the home channel state on the device to true.
        /// </summary>
        /// <param name="relayWorkflow">the workflow.</param>
        /// <param name="sourceUri">the URN of the device that called the function.</param>
        /// <param name="target">the device URN whose home channel you would like to set.</param>
        /// <returns>the event response.</returns>
        public async Task<Dictionary<string, object>> EnableHomeChannel(IRelayWorkflow relayWorkflow, string sourceUri, string target)
        {
            return await EnableHomeChannel(relayWorkflow, sourceUri, new[]{target});
        }
        
        public async Task<Dictionary<string, object>> EnableHomeChannel(IRelayWorkflow relayWorkflow, string sourceUri, string[] targets)
        {
            return await SetHomeChannelState(relayWorkflow, sourceUri, targets, true);
        }
        
        /// <summary>
        /// Sets the home channel state on the device to false.
        /// </summary>
        /// <param name="relayWorkflow">the workflow.</param>
        /// <param name="sourceUri">the URN of the device that called the function.</param>
        /// <param name="target">the device URN whose home channel you would like to set.</param>
        /// <returns>the event response.</returns>
        public async Task<Dictionary<string, object>> DisableHomeChannel(IRelayWorkflow relayWorkflow, string sourceUri, string target)
        {
            return await DisableHomeChannel(relayWorkflow, sourceUri, new[]{target});
        }
        
        public async Task<Dictionary<string, object>> DisableHomeChannel(IRelayWorkflow relayWorkflow, string sourceUri, string[] targets)
        {
            return await SetHomeChannelState(relayWorkflow, sourceUri, targets, false);
        }
        
        private async Task<Dictionary<string, object>> SetHomeChannelState(IRelayWorkflow relayWorkflow, string sourceUri, string[] targets, bool enabled)
        {
            return await Send((await GetRunningRelayWorkflowOrThrow(relayWorkflow)).WebSocketConnection, SetHomeChannelState_(sourceUri, targets, enabled));
        }
        
        private static Dictionary<string, object> SetHomeChannelState_(string sourceUri, string[] targets, bool enabled)
        {
            return Request(
                RequestType.SetHomeChannelState,
                sourceUri,
                new Dictionary<string, object>
                {
                    ["target"] = targets,
                    ["enabled"] = enabled
                }
            );
        }
        
        /// <summary>
        /// Answers an incoming call on your device.
        /// </summary>
        /// <param name="relayWorkflow">the workflow.</param>
        /// <param name="sourceUri">the device URN which will answer the call.</param>
        /// <param name="callId">the ID of the call to answer.</param>
        /// <returns></returns>
        public async Task<Dictionary<string, object>> AnswerCall(IRelayWorkflow relayWorkflow, string sourceUri, string callId)
        {
            return await Send((await GetRunningRelayWorkflowOrThrow(relayWorkflow)).WebSocketConnection, AnswerCall_(sourceUri, callId));
        }

        private static Dictionary<string, object> AnswerCall_(string sourceUri, string callId)
        {
            return Request(
                RequestType.Answer,
                sourceUri,
                new Dictionary<string, object>
                {
                    ["call_id"] = callId
                }
            );
        }

        /// <summary>
        /// Places a call to another device.
        /// </summary>
        /// <param name="relayWorkflow">the workflow.</param>
        /// <param name="sourceUri">the device that is placing the call.</param>
        /// <param name="calleeUri">the device that you would like to call.</param>
        /// <returns>the event response.</returns>
        public async Task<Dictionary<string, object>> PlaceCall(IRelayWorkflow relayWorkflow, string sourceUri, string calleeUri)
        {
            return await Send((await GetRunningRelayWorkflowOrThrow(relayWorkflow)).WebSocketConnection, PlaceCall_(sourceUri, calleeUri));
        }
        
        private static Dictionary<string, object> PlaceCall_(string sourceUri, string uri)
        {
            return Request(
                RequestType.Call,
                sourceUri,
                new Dictionary<string, object>
                {
                    ["uri"] = uri
                }
            );
        }
        
        /// <summary>
        /// Ends a call on a device.
        /// </summary>
        /// <param name="relayWorkflow">the workflow.</param>
        /// <param name="sourceUri">the device that is hanging up the call.</param>
        /// <param name="callId">the ID of the call to hangup.</param>
        /// <returns>the event response.</returns>
        public async Task<Dictionary<string, object>> HangupCall(IRelayWorkflow relayWorkflow, string sourceUri, string callId)
        {
            return await Send((await GetRunningRelayWorkflowOrThrow(relayWorkflow)).WebSocketConnection, HangupCall_(sourceUri, callId));
        }
        
        private static Dictionary<string, object> HangupCall_(string sourceUri, string callId)
        {
            return Request(
                RequestType.Hangup,
                sourceUri,
                new Dictionary<string, object>
                {
                    ["call_id"] = callId
                }
            );
        }

        /// <summary>
        /// Parses out and retrieves the source URN from a Start Event
        /// </summary>
        /// <param name="startEvent">the start event</param>
        /// <returns>the source URN</returns>
        public static string GetSourceUriFromStartEvent(IDictionary<string, object> startEvent) {
            IDictionary<string, object> trigger = null;
            IDictionary<string, object> args = null;
            string sourceUri = null;
            if (startEvent is not null) {
                trigger = startEvent["trigger"] as IDictionary<string, object>;
            }
            if (trigger is not null) {
                args = trigger["args"] as IDictionary<string, object>;
            }
            if (args is not null) {
                sourceUri = args["source_uri"] as string;
            }
            return sourceUri;
        }

        static string serverHostname = "all-main-pro-ibot.relaysvr.com";
        static string version = "relay-sdk-dotnet/2.0.0-pre";
        static string auth_hostname = "auth.relaygo.com";

        private static async Task<string> UpdateAccessToken(string refreshToken, string clientId) {
            // Create a uri object with the following url
            string url = $"https://{auth_hostname}/oauth2/token";
            var grantUrl = new Uri(@url);

            // Create a payload to be sent with the request
            var grantPayload = new Dictionary<string, string>() {
                ["grant_type"] = "refresh_token",
                ["refresh_token"] = refreshToken,
                ["client_id"] = clientId
            };

            // Create an http client that will post the request
            HttpClient httpClient = new HttpClient();

            // Add headers and the encoded content to the URL
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add("User-Agent", version);
            var encodedContent = new FormUrlEncodedContent(grantPayload);

            // Post the request and retrieve the response
            var grantResponse = await httpClient.PostAsync(grantUrl, encodedContent);

            // If the resonse is not successful, throw an exception back to the client
            if(grantResponse.StatusCode != System.Net.HttpStatusCode.OK) {
                throw new Exception($"Unable to get access token: {grantResponse.StatusCode}");
            }

            // Parse grantResponse content into an instance of a Dictionary type, set the access 
            // token equal to the 'access_token' key's value in the dictionary and return the access token
            Dictionary<string, object> dictionary = (Dictionary<string, object>) JsonSerializer.Deserialize(await grantResponse.Content.ReadAsStringAsync(), typeof(Dictionary<string, object>));
            return (string) dictionary["access_token"].ToString();
        }

        /// <summary>
        /// A convenience method for sending an HTTP trigger to the Relay server.
        /// This generally would be used in a third-party system to start a Relay 
        /// workflow via an HTTP trigger and optionally pass data to it with
        /// action_args.  
        /// 
        /// If the access_token has expired and the request gets a 401 response 
        /// a new access_token will be automatically generated via the refreshToken
        /// and the request will be resubmitted with the new accessToken. Otherwise
        /// the refresh token won't be used.
        /// 
        /// </summary>
        /// <param name="accessToken">the current access token.  Can be a placeholder value and this method will 
        /// generate a new one and return it.</param>
        /// <param name="refreshToken">the permanent refresh token that can be used to obtain a new access token.  
        /// The caller should treat the refresh token as very sensitive data, and secure it appropriately.</param>
        /// <param name="clientId">the auth_sdk_id as returned from "relay env".</param>
        /// <param name="workflowId">the workflow_id as returned from "relay workflow list". Usually starts with "wf_".</param>
        /// <param name="subscriberId">the subcriber UUID as returned from "relay whoami".</param>
        /// <param name="userId">the IMEI of the target device, such as 990007560023456.</param>
        /// <param name="targets">optional targets consisting of an array of the device IMEIs that you would like to run the triggered workflow on.</param>
        /// <param name="actionArgs">a dict of any key/value arguments you want to pass in to the workflow that gets started by this trigger.</param>
        /// <returns>a dictionary containing the response information and access token.</returns>
        public static async Task<Dictionary<string, object>> TriggerWorkflow(string accessToken, string refreshToken, string clientId, string workflowId, string subscriberId, string userId, string[] targets, Dictionary<string, string> actionArgs) {
            // Create the url string
            var url = $"https://{serverHostname}/ibot/workflow/{workflowId}";

            // Set the query params
            var queryParams = new Dictionary<string, string>()  {
                ["subscriber_id"] = subscriberId,
                ["user_id"] = userId,
            };

            // Set the payload
            var payload = new Dictionary<string, string>() {
                ["action"] = "invoke",
            };
            
            // Add actionArgs if not null
            if (actionArgs != null) {
                payload.Add("action_args", $"{actionArgs}");
            }

            // Add targets if not null
            if (targets != null) {
                payload.Add("target_device_ids", $"{targets}");
            }

            // Create an http client that will post the request
            HttpClient httpClient = new HttpClient();

            // Add the headers
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));   
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
            httpClient.DefaultRequestHeaders.Add("User-Agent", version);


            // Add the query string to the url
            url = QueryHelpers.AddQueryString(url, queryParams);

            // Create the uri object, and serialize the payload
            Uri uri = new Uri(url);
            var json = JsonSerializer.Serialize<Dictionary<string, string>>(payload);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            // Post the request, await the response
            var response = await httpClient.PostAsync(uri, data);

            // If the accessToken is expired, create a new one
            if((response.StatusCode) == System.Net.HttpStatusCode.Unauthorized) {
                Log.Debug("Got 401 on workflow trigger, trying to get new access token");
                accessToken = await UpdateAccessToken(refreshToken, clientId);
                httpClient.DefaultRequestHeaders.Remove("Authorization");
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
                response = await httpClient.PostAsync(uri, data);
            }

            Log.Debug($"Workflow trigger status code: {response.StatusCode}");

            // Return the response and access token as a dictionary object
            return new Dictionary<string, object> {
                ["response"] = await response.Content.ReadAsStringAsync(),
                ["access_token"] = accessToken
            };
        }

        /// <summary>
        /// A convenience method for getting all the details of a device. 
        /// 
        /// This will return quite a bit of data regarding device configuration and
        /// state. The result, if the query was successful, should have a large JSON dictionary.
        /// </summary>
        /// <param name="accessToken">the current access token. Can be a placeholder value 
        /// and this method will generate a new one and return it. If the
        /// original value of the access token passed in here has expired,
        /// this method will also generate a new one and return it.</param>
        /// <param name="refreshToken">the permanent refresh token that can be used to
        /// obtain a new access_token. The caller should treat the refresh
        /// token as very sensitive data, and secure it appropriately.</param>
        /// <param name="clientId">the auth_sdk_id as returned from "relay env".</param>
        /// <param name="subscriberId">the subcriber UUID as returned from "relay whoami".</param>
        /// <param name="userId">the IMEI of the target device, such as 990007560023456.</param>
        /// <returns>a dictionary containing the response and access token.</returns>
        public static async Task<Dictionary<string, object>> FetchDevice(string accessToken, string refreshToken, string clientId, string subscriberId, string userId) {
            // Create the url string
            string url = $"https://{serverHostname}/relaypro/api/v1/device/{userId}";

            // Set the query params and add them to the URI
            var queryParams = new Dictionary<string, string>() {
                ["subscriber_id"] = subscriberId
            };
            var uri = QueryHelpers.AddQueryString(url, queryParams);

            // Create http client and add the headers
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
            httpClient.DefaultRequestHeaders.Add("User-Agent", version);

            // Post the request, await the response
            var response = await httpClient.GetAsync(uri);

            // If the status code is Unauthorized, update the access token and post the request again
            if(response.StatusCode == System.Net.HttpStatusCode.Unauthorized) {
                Log.Debug("Got 401 on get, trying new access token");
                accessToken = await UpdateAccessToken(refreshToken, clientId);
                httpClient.DefaultRequestHeaders.Remove("Authorization");
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
                response = await httpClient.GetAsync(uri);
            }
            
            Log.Debug($"Device info status code: {response.StatusCode}");

            // Return the response and access token as a dictionary object
            return new Dictionary<string, object> {
                ["response"] = await response.Content.ReadAsStringAsync(),
                ["access_token"] = accessToken
            };
        }

    }
        
}
