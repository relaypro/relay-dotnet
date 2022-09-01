// Copyright © 2022 Relay Inc.

using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using RelayDotNet;
using Serilog;

namespace WebAppSamples
{
    public class WebSocketMiddleware : IMiddleware
    {
        private static readonly CancellationTokenSource LoopCancellationTokenSource = new CancellationTokenSource();

        
        public WebSocketMiddleware()
        {
            Log.Debug("constructed WebSocketMiddleware");
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            Log.Debug("InvokeAsync");

            if (context.WebSockets.IsWebSocketRequest)
            {
                Log.Debug("received a websocket request");

                try
                {
                    WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
                    
                    Relay relay = context.RequestServices.GetService(typeof(Relay)) as Relay;
                    string id = context.Connection.Id;
                    string path = context.Request.Path.ToString();
                    string clientIpAddress = context.Connection.RemoteIpAddress?.ToString();
                    int clientPort = context.Connection.RemotePort;
                    TaskCompletionSource<object> taskCompletionSource = new TaskCompletionSource<object>();
                    
                    _ = Task.Run(() => SocketProcessingLoopAsync(webSocket, relay, taskCompletionSource, id, path, clientIpAddress, clientPort).ConfigureAwait(false));
                    
                    await taskCompletionSource.Task;
                }
                catch (Exception exception)
                {
                    Log.Debug(exception, "InvokeAsync caught:");
                    await next(context);
                }
            }
            else
            {
                Log.Debug("received a non-websocket request");
                
                await next(context);                
            }
        }
        
        private static async Task SocketProcessingLoopAsync(WebSocket webSocket, Relay relay, TaskCompletionSource<object> taskCompletionSource, string id, string path, string clientIpAddress, int clientPort)
        {
            SystemRelayWebSocketConnector systemRelayWebSocketConnector = relay.RelayWebSocketConnector as SystemRelayWebSocketConnector;
            if (null == systemRelayWebSocketConnector)
            {
                Log.Error("SocketProcessingLoopAsync unable to get SystemRelayWebSocketConnector");
                
                return;
            }
            
            var loopCancellationToken = LoopCancellationTokenSource.Token;

            try
            {
                await systemRelayWebSocketConnector.OnSystemOpen(webSocket, id, path, clientIpAddress, clientPort);

                var buffer = WebSocket.CreateServerBuffer(4096);
                
                while (webSocket.State is WebSocketState.Open or WebSocketState.CloseSent && !loopCancellationToken.IsCancellationRequested)
                {
                    Log.Debug("start ReceiveAsync");
                    var receiveResult = await webSocket.ReceiveAsync(buffer, loopCancellationToken);
                    Log.Debug("done ReceiveAsync {@receiveResult}", receiveResult);
                    
                    if (!loopCancellationToken.IsCancellationRequested)
                    {
                        if (webSocket.State == WebSocketState.CloseReceived && receiveResult.MessageType == WebSocketMessageType.Close)
                        {
                            await webSocket.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, "normal closure", CancellationToken.None);
                        } 
                        else if (webSocket.State == WebSocketState.Open && null != buffer.Array)
                        {
                            string message = Encoding.UTF8.GetString(buffer.Array, 0, receiveResult.Count);
                            systemRelayWebSocketConnector.OnSystemMessage(webSocket, message);
                        }
                    }
                }
            }
            catch (OperationCanceledException)
            {
            }
            catch (WebSocketException webSocketException)
            {
                Log.Debug(webSocketException, "SocketProcessingLoopAsync caught:");
            }
            catch (Exception exception)
            {
                Log.Warning(exception, "SocketProcessingLoopAsync caught:");
            }
            finally
            {
                Log.Debug("SocketProcessingLoopAsync ending with websocket in state {webSocketState}", webSocket.State);
                
                if (webSocket.State == WebSocketState.Open)
                {
                    Log.Debug("Aborting connection");
                    webSocket.Abort();                    
                }
                
                systemRelayWebSocketConnector.OnSystemClose(webSocket);
                
                Log.Debug("setting result");
                taskCompletionSource.SetResult(true);
            }
        }
    }
}
