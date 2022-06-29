// Copyright © 2022 Relay Inc.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Serilog;

namespace RelayDotNet
{
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
                
                    Log.Debug("Add {Path} to {@RelayWorkflow}", path, relayWorkflowType);

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
            Log.Debug("{@WscInfo:l} OnOpen", WscInfo(webSocketConnection));

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
            Log.Debug("{@WscInfo:l} OnClose", WscInfo(webSocketConnection));

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
                                if (!dictionary.ContainsKey(kvp.Key) || !dictionary[kvp.Key].Equals(kvp.Value))
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

            TaskCompletionSource<Dictionary<string, object>> taskCompletionSource = await CreateAndStoreTaskCompletionSource(dictionary);

            string json = ToJson(dictionary);
            
            Log.Debug("{@WscInfo:l} Send JSON: {@message}", WscInfo(webSocketConnection), json);
            
            await webSocketConnection.Send(json);

            // listen for responses in a loop, repeating if a progress event is received
            for (; ; )
            {
                
                if (taskCompletionSource == null)
                {
                    Log.Error("{@WscInfo:l} Error listening for response for request: {@message}", WscInfo(webSocketConnection), json);
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
        
        public async void Terminate(IRelayWorkflow relayWorkflow)
        {
            await Send((await GetRunningRelayWorkflowOrThrow(relayWorkflow)).WebSocketConnection, Terminate_());            
        }
        
        private static Dictionary<string, object> Terminate_()
        {
            return Request(RequestType.Terminate);
        }

        public async void StartInteraction(IRelayWorkflow relayWorkflow, string sourceUri, string name, Dictionary<string, object> options)
        {
            await Send((await GetRunningRelayWorkflowOrThrow(relayWorkflow)).WebSocketConnection, StartInteraction_(sourceUri, name, options));
        }

        private static Dictionary<string, object> StartInteraction_(string sourceUri, string name, Dictionary<string, object> options)
        {
            return Request(
                RequestType.StartInteraction,
                sourceUri,
                new Dictionary<string, object>()
                {
                    ["name"] = name,
                    ["options"] = options
                }
            );
        }

        public async void EndInteraction(IRelayWorkflow relayWorkflow, string sourceUri, string name)
        {
            await Send((await GetRunningRelayWorkflowOrThrow(relayWorkflow)).WebSocketConnection, EndInteraction_(sourceUri, name));
        }

        private static Dictionary<string, object> EndInteraction_(string sourceUri, string name)
        {
            return Request(
                RequestType.EndInteraction,
                sourceUri,
                new Dictionary<string, object>()
                {
                    ["name"] = name,
                }
            );
        }

        public async Task<string> Say(IRelayWorkflow relayWorkflow, string sourceUri, string text)
        {
            return await Say(relayWorkflow, sourceUri, text, Language.English);
        }

        public async Task<string> Say(IRelayWorkflow relayWorkflow, string sourceUri, string text, Language language)
        {
            Dictionary<string, object> dictionary = await Send((await GetRunningRelayWorkflowOrThrow(relayWorkflow)).WebSocketConnection, Say_(sourceUri, text, language));
            return (dictionary != null) ? (string) dictionary["id"] : null;
        }

        public async Task<string> SayAndWait(IRelayWorkflow relayWorkflow, string sourceUri, string text)
        {
            return await SayAndWait(relayWorkflow, sourceUri, text, Language.English);
        }
        
        public async Task<string> SayAndWait(IRelayWorkflow relayWorkflow, string sourceUri, string text, Language language)
        {
            Log.Debug("SayAndWait enter for {text}", text);
            string id = await Say(relayWorkflow, sourceUri, text, language);
            if (id == null)
            {
                return null;
            }
            await WaitForPromptStop(id);
            Log.Debug("SayAndWait exit for {text}", text);
            return id;
        }
        
        private static Dictionary<string, object> Say_(string sourceUri, string text, Language language)
        {
            return Request(
                RequestType.Say,
                sourceUri,
                new Dictionary<string, object>()
                {
                    ["text"] = text,
                    ["lang"] = language.SerializedName                    
                }
            );
        }

        public async Task<string> Play(IRelayWorkflow relayWorkflow, string sourceUri, string filename)
        {
            Dictionary<string, object> dictionary = await Send((await GetRunningRelayWorkflowOrThrow(relayWorkflow)).WebSocketConnection, Play_(sourceUri, filename));
            return (dictionary != null) ? (string) dictionary["id"] : null;
        }
        
        public async Task<string> PlayAndWait(IRelayWorkflow relayWorkflow, string sourceUri, string filename)
        {
            Log.Debug("PlayAndWait enter for {text}", filename);
            string id = await Play(relayWorkflow, sourceUri, filename);
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
        
        private static Dictionary<string, object> Play_(string sourceUri, string filename)
        {
            return Request(
                RequestType.Play,
                sourceUri,
                new Dictionary<string, object>
                {
                    ["filename"] = filename
                }
            );
        }

        public async Task<Dictionary<string, object>> StopPlayback(IRelayWorkflow relayWorkflow, string sourceUri)
        {
            return await StopPlayback(relayWorkflow, sourceUri, (string[]) null);
        }
        
        public async Task<Dictionary<string, object>> StopPlayback(IRelayWorkflow relayWorkflow, string sourceUri, string id)
        {
            return await StopPlayback(relayWorkflow, sourceUri, new[] {id});
        }
        
        public async Task<Dictionary<string, object>> StopPlayback(IRelayWorkflow relayWorkflow, string sourceUri, string[] ids)
        {
            return await Send((await GetRunningRelayWorkflowOrThrow(relayWorkflow)).WebSocketConnection, StopPlayback_(sourceUri, ids));
        }
        
        private static Dictionary<string, object> StopPlayback_(string sourceUri, string[] ids)
        {
            if (null == ids)
            {
                return Request(RequestType.StopPlayback, null);                
            }
            
            return Request(
                RequestType.StopPlayback,
                sourceUri,
                new Dictionary<string, object>
                {
                    ["ids"] = ids                    
                }
            );
        }
        
        public async Task<Dictionary<string, object>> Vibrate(IRelayWorkflow relayWorkflow, string sourceUri, int[] pattern)
        {
            return await Send((await GetRunningRelayWorkflowOrThrow(relayWorkflow)).WebSocketConnection, Vibrate_(sourceUri, pattern));
        }
        
        private static Dictionary<string, object> Vibrate_(string sourceUri, int[] pattern)
        {
            return Request(
                RequestType.Vibrate,
                sourceUri,
                new Dictionary<string, object>
                {
                    ["pattern"] = pattern                    
                }
            );
        }
        
        public async Task<Dictionary<string, object>> SetLed(IRelayWorkflow relayWorkflow, string sourceUri, LedEffect ledEffect, LedInfo ledInfo)
        {
            return await Send((await GetRunningRelayWorkflowOrThrow(relayWorkflow)).WebSocketConnection, SetLed_(sourceUri, ledEffect, ledInfo));
        }
        
        private static Dictionary<string, object> SetLed_(string sourceUri, LedEffect ledEffect, LedInfo ledInfo)
        {
            return Request(
                RequestType.SetLed,
                sourceUri,
                new Dictionary<string, object>
                {
                    ["effect"] = ledEffect.SerializedName,
                    ["args"] = ledInfo.Dictionary
                }
            );
        }

        public async Task<Dictionary<string, object>> SwitchLedOn(IRelayWorkflow relayWorkflow, string sourceUri, LedIndex ledIndex, string color)
        {
            LedInfo ledInfo = new LedInfo();
            ledInfo.SetColor(ledIndex, color);
            return await SetLed(relayWorkflow, sourceUri, LedEffect.Static, ledInfo);
        }
        
        public async Task<Dictionary<string, object>> SwitchAllLedOn(IRelayWorkflow relayWorkflow, string sourceUri, string color)
        {
            return await SwitchLedOn(relayWorkflow, sourceUri, LedIndex.Ring, color);
        }
        
        public async Task<Dictionary<string, object>> SwitchAllLedOff(IRelayWorkflow relayWorkflow, string sourceUri)
        {
            return await SetLed(relayWorkflow, sourceUri, LedEffect.Off, new LedInfo());
        }

        public async Task<Dictionary<string, object>> Rainbow(IRelayWorkflow relayWorkflow, string sourceUri, int rotations = -1)
        {
            LedInfo ledInfo = new LedInfo();
            if (-1 != rotations)
            {
                ledInfo.SetRotations(rotations);
            }
            
            return await SetLed(relayWorkflow, sourceUri, LedEffect.Rainbow, ledInfo);
        }
        
        public async Task<Dictionary<string, object>> Rotate(IRelayWorkflow relayWorkflow, string sourceUri, string color = "FFFFFF")
        {
            LedInfo ledInfo = new LedInfo();
            ledInfo.SetColor(LedIndex.Led1, color);
            
            return await SetLed(relayWorkflow, sourceUri, LedEffect.Rotate, ledInfo);
        }
        
        public async Task<Dictionary<string, object>> Flash(IRelayWorkflow relayWorkflow, string sourceUri, string color = "0000FF")
        {
            LedInfo ledInfo = new LedInfo();
            ledInfo.SetColor(LedIndex.Ring, color);
            
            return await SetLed(relayWorkflow, sourceUri, LedEffect.Flash, ledInfo);
        }
        
        public async Task<Dictionary<string, object>> Breathe(IRelayWorkflow relayWorkflow, string sourceUri, string color = "0000FF")
        {
            LedInfo ledInfo = new LedInfo();
            ledInfo.SetColor(LedIndex.Ring, color);
            
            return await SetLed(relayWorkflow, sourceUri, LedEffect.Breathe, ledInfo);
        }
        
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
        
        public async Task<string> GetVar(IRelayWorkflow relayWorkflow, string name, string defaultValue = null)
        {
            Dictionary<string, object> dictionary = await Send((await GetRunningRelayWorkflowOrThrow(relayWorkflow)).WebSocketConnection, GetVar_(name));
            
            if (null != dictionary && dictionary.ContainsKey("value") && null != dictionary["value"])
            {
                return (string) dictionary["value"];
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

        public async Task<Dictionary<string, object>> Listen(IRelayWorkflow relayWorkflow, string sourceUri)
        {
            return await Listen(relayWorkflow, sourceUri, Array.Empty<string>(), Language.English);
        }
        
        public async Task<Dictionary<string, object>> Listen(IRelayWorkflow relayWorkflow, string sourceUri, string[] phrases)
        {
            return await Listen(relayWorkflow, sourceUri, phrases, Language.English);
        }
        
        public async Task<Dictionary<string, object>> Listen(IRelayWorkflow relayWorkflow, string sourceUri, Language language)
        {
            return await Listen(relayWorkflow, sourceUri, Array.Empty<string>(), language);
        }
        
        public async Task<Dictionary<string, object>> Listen(IRelayWorkflow relayWorkflow, string sourceUri, string[] phrases, Language altLanguage, bool transcribe = true, int timeout = 60)
        {
            var listenRequest = Listen_(sourceUri, phrases, altLanguage, transcribe, timeout);
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
        
        private static Dictionary<string, object> Listen_(string sourceUri, string[] phrases, Language altLanguage, bool transcribe, int timeout)
        {
            var listenRequest = Request(
                RequestType.Listen,
                sourceUri,
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

        private async Task<Dictionary<string, object>> SendNotification(IRelayWorkflow relayWorkflow, string sourceUri, NotificationType notificationType, string text, string[] targets, string name, NotificationPushOptions notificationPushOptions)
        {
            return await Send((await GetRunningRelayWorkflowOrThrow(relayWorkflow)).WebSocketConnection, SendNotification_(sourceUri, notificationType, text, targets, name, notificationPushOptions), DelayNotificationTimeout);
        }
        
        private static Dictionary<string, object> SendNotification_(string sourceUri, NotificationType notificationType, string text, string[] targets, string name, NotificationPushOptions notificationPushOptions)
        {
            return Request(
                RequestType.Notification,
                sourceUri,
                new Dictionary<string, object>
                {
                    ["type"] = notificationType.SerializedName,
                    ["name"] = name,
                    ["text"] = text,
                    ["target"] = targets,
                    ["push_opts"] = notificationPushOptions
                }
            );
        }
        
        public async Task<Dictionary<string, object>> CancelNotification(IRelayWorkflow relayWorkflow, string sourceUri, string name, string[] targets)
        {
            return await SendNotification(relayWorkflow, sourceUri, NotificationType.Cancel, null, targets, name, null);
        }

        public async Task<Dictionary<string, object>> Broadcast(IRelayWorkflow relayWorkflow, string sourceUri, string name, string text, string[] targets)
        {
            return await Broadcast(relayWorkflow, sourceUri, name, text, targets, null);
        }
        
        public async Task<Dictionary<string, object>> Broadcast(IRelayWorkflow relayWorkflow, string sourceUri, string name, string text, string[] targets, NotificationPushOptions notificationPushOptions)
        {
            return await SendNotification(relayWorkflow, sourceUri, NotificationType.Broadcast, text, targets, name, notificationPushOptions);
        }
        
        public async Task<Dictionary<string, object>> CancelBroadcast(IRelayWorkflow relayWorkflow, string sourceUri, string name, string[] targets)
        {
            return await CancelNotification(relayWorkflow, sourceUri, name, targets);
        }
        
        public async Task<Dictionary<string, object>> Notify(IRelayWorkflow relayWorkflow, string sourceUri, string name, string text, string[] targets)
        {
            return await Notify(relayWorkflow, sourceUri, name, text, targets, null);
        }
        
        public async Task<Dictionary<string, object>> Notify(IRelayWorkflow relayWorkflow, string sourceUri, string name, string text, string[] targets, NotificationPushOptions notificationPushOptions)
        {
            return await SendNotification(relayWorkflow, sourceUri, NotificationType.Notify, text, targets, name, notificationPushOptions);
        }
        
        public async Task<Dictionary<string, object>> CancelNotify(IRelayWorkflow relayWorkflow, string sourceUri, string name, string[] targets)
        {
            return await CancelNotification(relayWorkflow, sourceUri, name, targets);
        }
        
        public async Task<Dictionary<string, object>> Alert(IRelayWorkflow relayWorkflow, string sourceUri, string name, string text, string[] targets)
        {
            return await Alert(relayWorkflow, sourceUri, name, text, targets, null);
        }
        
        public async Task<Dictionary<string, object>> Alert(IRelayWorkflow relayWorkflow, string sourceUri, string name, string text, string[] targets, NotificationPushOptions notificationPushOptions)
        {
            return await SendNotification(relayWorkflow, sourceUri, NotificationType.Alert, text, targets, name, notificationPushOptions);
        }
        
        public async Task<Dictionary<string, object>> CancelAlert(IRelayWorkflow relayWorkflow, string sourceUri, string name, string[] targets)
        {
            return await CancelNotification(relayWorkflow, sourceUri, name, targets);
        }
        
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
        
        private async Task<Dictionary<string, object>> GetDeviceInfo(IRelayWorkflow relayWorkflow, string sourceUri, DeviceInfoQuery deviceInfoQuery, bool refresh = false)
        {
            return await Send((await GetRunningRelayWorkflowOrThrow(relayWorkflow)).WebSocketConnection, GetDeviceInfo_(sourceUri, deviceInfoQuery, refresh), refresh ? DelayRefreshTimeout : DelayTimeout);
        }
        
        private static Dictionary<string, object> GetDeviceInfo_(string sourceUri, DeviceInfoQuery deviceInfoQuery, bool refresh)
        {
            return Request(
                RequestType.GetDeviceInfo,
                sourceUri, 
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
        
        public async Task<string> GetDeviceName(IRelayWorkflow relayWorkflow, string sourceUri)
        {
            DeviceInfoQuery deviceInfoQuery = DeviceInfoQuery.NameQuery;
            return GetDictionaryKeyValueAsString(await GetDeviceInfo(relayWorkflow, sourceUri, deviceInfoQuery), deviceInfoQuery.SerializedName);
        }
        
        public async Task<string> GetDeviceLocation(IRelayWorkflow relayWorkflow, string sourceUri, bool refresh)
        {
            DeviceInfoQuery deviceInfoQuery = DeviceInfoQuery.LocationQuery;
            return GetDictionaryKeyValueAsString(await GetDeviceInfo(relayWorkflow, sourceUri, deviceInfoQuery, refresh), deviceInfoQuery.SerializedName);
        }
        
        public async Task<string> GetDeviceId(IRelayWorkflow relayWorkflow, string sourceUri)
        {
            DeviceInfoQuery deviceInfoQuery = DeviceInfoQuery.IdQuery;
            return GetDictionaryKeyValueAsString(await GetDeviceInfo(relayWorkflow, sourceUri, deviceInfoQuery), deviceInfoQuery.SerializedName);
        }
        
        public async Task<string> GetDeviceAddress(IRelayWorkflow relayWorkflow, string sourceUri, bool refresh)
        {
            return await GetDeviceLocation(relayWorkflow, sourceUri, refresh);
        }
        
        public async Task<float[]> GetDeviceCoordinates(IRelayWorkflow relayWorkflow, string sourceUri, bool refresh)
        {
            DeviceInfoQuery deviceInfoQuery = DeviceInfoQuery.CoordinatesQuery;
            return GetDictionaryKeyValueAsFloatArray(await GetDeviceInfo(relayWorkflow, sourceUri, deviceInfoQuery, refresh), deviceInfoQuery.SerializedName);
        }
        
        public async Task<float[]> GetDeviceLatLong(IRelayWorkflow relayWorkflow, string sourceUri, bool refresh)
        {
            return await GetDeviceCoordinates(relayWorkflow, sourceUri, refresh);
        }
        
        public async Task<float[]> GetDeviceIndoorLocation(IRelayWorkflow relayWorkflow, string sourceUri, bool refresh)
        {
            DeviceInfoQuery deviceInfoQuery = DeviceInfoQuery.IndoorLocationQuery;
            return GetDictionaryKeyValueAsFloatArray(await GetDeviceInfo(relayWorkflow, sourceUri, deviceInfoQuery, refresh), deviceInfoQuery.SerializedName);
        }
        
        public async Task<int> GetDeviceBattery(IRelayWorkflow relayWorkflow, string sourceUri, bool refresh)
        {
            DeviceInfoQuery deviceInfoQuery = DeviceInfoQuery.BatteryQuery;
            return GetDictionaryKeyValueAsInt(await GetDeviceInfo(relayWorkflow, sourceUri, deviceInfoQuery, refresh), deviceInfoQuery.SerializedName);
        }
        
        public async Task<DeviceType> GetDeviceType(IRelayWorkflow relayWorkflow, string sourceUri, bool refresh)
        {
            DeviceInfoQuery deviceInfoQuery = DeviceInfoQuery.TypeQuery;
            return GetDictionaryKeyValueAsDeviceType(await GetDeviceInfo(relayWorkflow, sourceUri, deviceInfoQuery, refresh), deviceInfoQuery.SerializedName);
        }
        
        public async Task<string> GetUserProfile(IRelayWorkflow relayWorkflow, string sourceUri)
        {
            DeviceInfoQuery deviceInfoQuery = DeviceInfoQuery.UsernameQuery;
            return GetDictionaryKeyValueAsString(await GetDeviceInfo(relayWorkflow, sourceUri, deviceInfoQuery), deviceInfoQuery.SerializedName);
        }
        
        public async Task<string[]> SetUserProfile(IRelayWorkflow relayWorkflow, string sourceUri, string username, bool force = false)
        {
            Dictionary<string, object> dictionary = await Send((await GetRunningRelayWorkflowOrThrow(relayWorkflow)).WebSocketConnection, SetUserProfile_(sourceUri, username, force));
            
            if (null != dictionary && dictionary.ContainsKey("device_names") && null != dictionary["device_names"])
            {
                return (string[]) dictionary["device_names"];
            }

            return Array.Empty<string>();
        }
        
        private static Dictionary<string, object> SetUserProfile_(string sourceUri, string username, bool force)
        {
            return Request(
                RequestType.SetUserProfile,
                sourceUri, 
                new Dictionary<string, object>
                {
                    ["username"] = username,
                    ["force"] = force
                }
            );
        }
        
        private async Task<Dictionary<string, object>> SetDeviceInfo(IRelayWorkflow relayWorkflow, string sourceUri, DeviceInfoField deviceInfoField, string value)
        {
            return await Send((await GetRunningRelayWorkflowOrThrow(relayWorkflow)).WebSocketConnection, SetDeviceInfo_(sourceUri, deviceInfoField, value));
        }
        
        private static Dictionary<string, object> SetDeviceInfo_(string sourceUri, DeviceInfoField deviceInfoField, string value)
        {
            return Request(
                RequestType.SetDeviceInfo,
                sourceUri,
                new Dictionary<string, object>
                {
                    ["field"] = deviceInfoField.SerializedName,
                    ["value"] = value
                }
            );
        }
        
        public async Task<Dictionary<string, object>> SetDeviceName(IRelayWorkflow relayWorkflow, string sourceUri, string name)
        {
            return await SetDeviceInfo(relayWorkflow, sourceUri, DeviceInfoField.Label, name);
        }
        
        public async Task<Dictionary<string, object>> SetDeviceChannel(IRelayWorkflow relayWorkflow, string sourceUri, string channel)
        {
            return await SetDeviceInfo(relayWorkflow, sourceUri, DeviceInfoField.Channel, channel);
        }
        
        public async Task<Dictionary<string, object>> SetChannel(IRelayWorkflow relayWorkflow, string sourceUri, string channel, string[] targets, bool suppressTts = false, bool disableHomeChannel = false)
        {
            return await Send((await GetRunningRelayWorkflowOrThrow(relayWorkflow)).WebSocketConnection, SetChannel_(sourceUri, channel, targets, suppressTts, disableHomeChannel));
        }
        
        private static Dictionary<string, object> SetChannel_(string sourceUri, string channel, string[] targets, bool suppressTts, bool disableHomeChannel)
        {
            return Request(
                RequestType.SetChannel,
                sourceUri,
                new Dictionary<string, object>
                {
                    ["channel_name"] = channel,
                    ["target"] = targets,
                    ["suppress_tts"] = suppressTts,
                    ["disable_home_channel"] = disableHomeChannel
                }
            );
        }
        
        public async Task<Dictionary<string, object>> EnableHomeChannel(IRelayWorkflow relayWorkflow, string sourceUri, string target)
        {
            return await EnableHomeChannel(relayWorkflow, sourceUri, new[]{target});
        }
        
        public async Task<Dictionary<string, object>> EnableHomeChannel(IRelayWorkflow relayWorkflow, string sourceUri, string[] targets)
        {
            return await SetHomeChannelState(relayWorkflow, sourceUri, targets, true);
        }
        
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
        
        public async Task<Dictionary<string, object>> PlaceCall(IRelayWorkflow relayWorkflow, string sourceUri, string uri)
        {
            return await Send((await GetRunningRelayWorkflowOrThrow(relayWorkflow)).WebSocketConnection, PlaceCall_(sourceUri, uri));
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
    }
}
