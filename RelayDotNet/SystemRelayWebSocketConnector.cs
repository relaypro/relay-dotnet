// Copyright © 2022 Relay Inc.

using System.Collections.Generic;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Serilog;

namespace RelayDotNet
{
    public abstract class SystemRelayWebSocketConnector : IRelayWebSocketConnector
    {
        private readonly Relay _relay;
        protected readonly string _ip;
        protected readonly int _port;
        protected readonly bool _secure;

        private readonly Dictionary<WebSocket, SystemRelayWebSocketConnection> _systemConnectionsToRelayConnections = new Dictionary<WebSocket, SystemRelayWebSocketConnection>();
        private readonly SemaphoreSlim _connectorsToConnectionsSemaphore = new SemaphoreSlim(1);


        public SystemRelayWebSocketConnector(Relay relay, string ip, int port, bool secure)
        {
            _relay = relay;
            _ip = ip;
            _port = port;
            _secure = secure;
        }

        public abstract void Start();

        public void Dispose()
        {
            Log.Debug("disposed");
        }

        public async Task<bool> OnSystemOpen(WebSocket webSocket, string path, string clientIpAddress, int clientPort)
        {
            return await OnSystemOpen(webSocket, null, path, clientIpAddress, clientPort);
        }

        public async Task<bool> OnSystemOpen(WebSocket webSocket, string id, string path, string clientIpAddress, int clientPort)
        {
            Log.Debug("OnSystemOpen");

            SystemRelayWebSocketConnection systemRelayWebsocketConnection = new SystemRelayWebSocketConnection(webSocket, id, path, clientIpAddress, clientPort);

            await _connectorsToConnectionsSemaphore.WaitAsync();
            try
            {
                _systemConnectionsToRelayConnections[webSocket] = systemRelayWebsocketConnection;
            }
            finally
            {
                _connectorsToConnectionsSemaphore.Release();
            }

            return await _relay.OnOpen(systemRelayWebsocketConnection);
        }

        public async void OnSystemClose(WebSocket webSocketConnection)
        {
            Log.Debug("OnSystemClose");

            SystemRelayWebSocketConnection systemRelayWebsocketConnection = await GetRelayWebsocketConnection(webSocketConnection);
            if (null != systemRelayWebsocketConnection)
            {
                RemoveRelayWebsocketConnection(webSocketConnection);

                _relay.OnClose(systemRelayWebsocketConnection);
            }
        }

        public async void OnSystemMessage(WebSocket webSocketConnection, string message)
        {
            Log.Debug("OnSystemMessage");

            SystemRelayWebSocketConnection systemRelayWebsocketConnection = await GetRelayWebsocketConnection(webSocketConnection);
            if (null != systemRelayWebsocketConnection)
            {
                _relay.OnMessage(systemRelayWebsocketConnection, message);
            }
        }

        private async Task<SystemRelayWebSocketConnection> GetRelayWebsocketConnection(WebSocket webSocketConnection)
        {
            await _connectorsToConnectionsSemaphore.WaitAsync();
            try
            {
                if (_systemConnectionsToRelayConnections.ContainsKey(webSocketConnection))
                {
                    return _systemConnectionsToRelayConnections[webSocketConnection];
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

        private async void RemoveRelayWebsocketConnection(WebSocket webSocketConnection)
        {
            await _connectorsToConnectionsSemaphore.WaitAsync();
            try
            {
                _systemConnectionsToRelayConnections.Remove(webSocketConnection);
            }
            finally
            {
                _connectorsToConnectionsSemaphore.Release();
            }
        }
    }
}
