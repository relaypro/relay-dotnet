// Copyright © 2022 Relay Inc.

using System.Threading.Tasks;
using Fleck;

namespace RelayDotNet
{
    public class FleckRelayWebSocketConnection : IRelayWebSocketConnection
    {
        private readonly IWebSocketConnection _webSocketConnection;
        private readonly FleckRelayWebSocketConnectionInfo _fleckRelayWebSocketConnectionInfo;
        
        public IRelayWebSocketConnectionInfo ConnectionInfo => _fleckRelayWebSocketConnectionInfo;

        public FleckRelayWebSocketConnection(IWebSocketConnection webSocketConnection)
        {
            _webSocketConnection = webSocketConnection;
            _fleckRelayWebSocketConnectionInfo = new FleckRelayWebSocketConnectionInfo(webSocketConnection.ConnectionInfo);
        }
        
        public async Task Send(string message)
        {
            await _webSocketConnection.Send(message);
        }

        public Task Close()
        {
            _webSocketConnection.Close();
            return null;
        }

        public Task Close(int code)
        {
            _webSocketConnection.Close(code);
            return null;
        }
    }
}
