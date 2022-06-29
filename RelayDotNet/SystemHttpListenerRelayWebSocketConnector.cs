// Copyright © 2022 Relay Inc.

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Serilog;

namespace RelayDotNet
{
    public class SystemHttpListenerRelayWebSocketConnector : SystemRelayWebSocketConnector
    {
        public SystemHttpListenerRelayWebSocketConnector(Relay relay, string ip, int port, bool secure) : base(relay, ip, port, secure)
        {
            Log.Debug("SystemHttpListenerRelayWebSocketConnector constructed");
        }
        
        public async Task Listen()
        {
            Log.Debug("Listen");
            
            HttpListener httpListener = new HttpListener();
            string prefix = $"{(_secure ? "https" : "http")}://{(_ip.Equals("0.0.0.0") ? "*" : _ip)}:{_port}/";
            httpListener.Prefixes.Add(prefix);
            Log.Debug("Start listener for {prefix}", prefix);
            httpListener.Start();
            
            var requests = new HashSet<Task>();
            requests.Add(httpListener.GetContextAsync());
            
            while (true)
            {
                var t = await Task.WhenAny(requests);
                requests.Remove(t);

                var task = t as Task<HttpListenerContext>;
                if (null == task)
                {
                    continue;
                }
                
                requests.Add(ProcessRequestAsync(task.Result));
                requests.Add(httpListener.GetContextAsync());
            }
        }

        public override async void Start()
        {
            Log.Debug("Start");
            
            await Task.Run(Listen);
        }

        private async Task ProcessRequestAsync(HttpListenerContext context)
        {
            Log.Debug("start ProcessRequestAsync");
            
            if (context.Request.IsWebSocketRequest)
            {
                HttpListenerWebSocketContext webSocketContext = await context.AcceptWebSocketAsync(null);
                Log.Debug("got a websocket request with uri @{uri}", webSocketContext.RequestUri);
                WebSocket webSocket = webSocketContext.WebSocket;
                string path = webSocketContext.RequestUri.AbsolutePath;
                string clientIpAddress = context.Request.RemoteEndPoint?.Address.ToString();
                int clientPort = context.Request.RemoteEndPoint?.Port ?? 0;
                await OnSystemOpen(webSocket, path, clientIpAddress, clientPort);
                var buffer = new byte[4096];
                try
                {
                    while (webSocket.State is WebSocketState.Open or WebSocketState.CloseSent)
                    {
                        Log.Debug("start ReceiveAsync {webSocketState}", webSocket.State);
                        WebSocketReceiveResult result = await webSocket.ReceiveAsync(buffer:
                            new ArraySegment<byte>(buffer),
                            cancellationToken: CancellationToken.None);
                        Log.Debug("end ReceiveAsync");

                        if (0 < result.Count)
                        {
                            OnSystemMessage(webSocket, Encoding.UTF8.GetString(buffer, 0, result.Count));
                        }
                    }
                }
                catch (Exception exception)
                {
                    Log.Debug(exception, "ProcessRequestAsync caught:");
                }
                finally
                {
                    Log.Debug("ProcessRequestAsync ending with websocket in state {webSocketState}", webSocket.State);
                
                    if (webSocket.State == WebSocketState.Open)
                    {
                        Log.Debug("Aborting connection");
                        webSocket.Abort();                    
                    }
                
                    OnSystemClose(webSocket);
                }
                
                Log.Debug("leave ProcessRequestAsync loop with websocket state {@webSocketState}", webSocket.State);
            }
            else
            {
                Log.Debug("not a websocket");
            }
            
            Log.Debug("end ProcessRequestAsync");
        }
    }
}
