// Copyright © 2022 Relay Inc.

using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Serilog;

namespace RelayDotNet
{
    public class SystemRelayWebSocketConnection : IRelayWebSocketConnection
    {
        private readonly WebSocket _webSocket;
        private readonly SystemRelayWebSocketConnectionInfo _systemRelayWebSocketConnectionInfo;
        
        public IRelayWebSocketConnectionInfo ConnectionInfo => _systemRelayWebSocketConnectionInfo;

        public SystemRelayWebSocketConnection(WebSocket webSocket, string id, string path, string clientIpAddress, int clientPort)
        {
            _webSocket = webSocket;
            _systemRelayWebSocketConnectionInfo = new SystemRelayWebSocketConnectionInfo(id, path, clientIpAddress, clientPort);
        }
        
        public async Task Send(string message)
        {
            if (_webSocket.State == WebSocketState.Open)
            {
                Log.Debug("start SendAsync");
                await _webSocket.SendAsync(
                    new ArraySegment<byte>(Encoding.ASCII.GetBytes(message),
                        0,
                        message.Length),
                    WebSocketMessageType.Text,
                    true,
                    CancellationToken.None);
                Log.Debug("done SendAsync");
            }
        }

        public async Task Close()
        {
            await Close(WebSocketCloseStatus.NormalClosure);
        }

        public async Task Close(int code)
        {
            WebSocketCloseStatus webSocketCloseStatus = WebSocketCloseStatus.NormalClosure;
            if (Enum.IsDefined(typeof(WebSocketCloseStatus), code))
            {
                webSocketCloseStatus = (WebSocketCloseStatus) Enum.ToObject(typeof(WebSocketCloseStatus), code);
            }

            await Close(webSocketCloseStatus);
        }
        
        private async Task Close(WebSocketCloseStatus webSocketCloseStatus)
        {
            Log.Debug("start Close {webSocketCloseStatus}", webSocketCloseStatus);
            await _webSocket.CloseOutputAsync(webSocketCloseStatus, "close requested", CancellationToken.None);
            _webSocket.Dispose();
            Log.Debug("end Close");
        }
    }
}
