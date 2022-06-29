// Copyright © 2022 Relay Inc.

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Fleck;
using Serilog;

namespace RelayDotNet
{
    public class FleckRelayWebSocketConnector : IRelayWebSocketConnector
    {
        private readonly Relay _relay;
        private readonly WebSocketServer _webSocketServer;

        private readonly Dictionary<IWebSocketConnection, FleckRelayWebSocketConnection> _fleckConnectionsToRelayConnections = new Dictionary<IWebSocketConnection, FleckRelayWebSocketConnection>();
        private readonly SemaphoreSlim _connectorsToConnectionsSemaphore = new SemaphoreSlim(1);
        

        public FleckRelayWebSocketConnector(Relay relay, string ip, int port, bool secure)
        {
            _relay = relay;
            
            var uri = (secure ? "wss" : "ws") + "://" + ip + ":" + port;

            Log.Debug("constructing WebSocketServer with uri {Uri}", uri);

            _webSocketServer = new WebSocketServer(uri);
        }

        public void Start()
        {
            _webSocketServer.Start(webSocketConnection =>
            {
                webSocketConnection.OnOpen = () => OnFleckOpen(webSocketConnection);
                webSocketConnection.OnClose = () => OnFleckClose(webSocketConnection);
                webSocketConnection.OnMessage = message => OnFleckMessage(webSocketConnection, message);
            });

            Log.Debug("started");
        }

        public void Dispose()
        {
            _webSocketServer.Dispose();
            Log.Debug("disposed");
        }
        
        private async Task<bool> OnFleckOpen(IWebSocketConnection webSocketConnection)
        {
            FleckRelayWebSocketConnection fleckRelayWebSocketConnection = new FleckRelayWebSocketConnection(webSocketConnection);
            
            await _connectorsToConnectionsSemaphore.WaitAsync();
            try
            {
                _fleckConnectionsToRelayConnections[webSocketConnection] = fleckRelayWebSocketConnection;
            }
            finally
            {
                _connectorsToConnectionsSemaphore.Release();
            }

            return await _relay.OnOpen(fleckRelayWebSocketConnection);
        }

        private async void OnFleckClose(IWebSocketConnection webSocketConnection)
        {
            FleckRelayWebSocketConnection fleckRelayWebSocketConnection = await GetRelayWebsocketConnection(webSocketConnection);
            if (null != fleckRelayWebSocketConnection)
            {
                RemoveRelayWebsocketConnection(webSocketConnection);
                
                _relay.OnClose(fleckRelayWebSocketConnection);                
            }
        }

        private async void OnFleckMessage(IWebSocketConnection webSocketConnection, string message)
        {
            FleckRelayWebSocketConnection fleckRelayWebSocketConnection = await GetRelayWebsocketConnection(webSocketConnection);
            if (null != fleckRelayWebSocketConnection)
            {
                _relay.OnMessage(fleckRelayWebSocketConnection, message);                
            }
        }

        private async Task<FleckRelayWebSocketConnection> GetRelayWebsocketConnection(IWebSocketConnection webSocketConnection)
        {
            await _connectorsToConnectionsSemaphore.WaitAsync();
            try
            {
                if (_fleckConnectionsToRelayConnections.ContainsKey(webSocketConnection))
                {
                    return _fleckConnectionsToRelayConnections[webSocketConnection];
                }
                else
                {
                    return null;
                }
            }
            finally
            {
                _connectorsToConnectionsSemaphore.Release();
            }
        }
        
        private async void RemoveRelayWebsocketConnection(IWebSocketConnection webSocketConnection)
        {
            await _connectorsToConnectionsSemaphore.WaitAsync();
            try
            {
                _fleckConnectionsToRelayConnections.Remove(webSocketConnection);
            }
            finally
            {
                _connectorsToConnectionsSemaphore.Release();
            }
        }
    }
}
