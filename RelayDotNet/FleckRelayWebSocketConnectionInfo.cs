// Copyright © 2022 Relay Inc.

using Fleck;

namespace RelayDotNet
{
    public class FleckRelayWebSocketConnectionInfo : IRelayWebSocketConnectionInfo
    {
        private readonly IWebSocketConnectionInfo _webSocketConnectionInfo;
        private readonly string _id;

        public string Id => _id;
        public string Path => _webSocketConnectionInfo.Path;
        public string ClientIpAddress => _webSocketConnectionInfo.ClientIpAddress;
        public int ClientPort => _webSocketConnectionInfo.ClientPort;

        public FleckRelayWebSocketConnectionInfo(IWebSocketConnectionInfo webSocketConnectionInfo)
        {
            _webSocketConnectionInfo = webSocketConnectionInfo;
            _id = _webSocketConnectionInfo.Id.ToString();
        }
    }
}
