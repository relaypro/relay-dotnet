// Copyright © 2022 Relay Inc.

using System;

namespace RelayDotNet
{
    public class SystemRelayWebSocketConnectionInfo : IRelayWebSocketConnectionInfo
    {
        public string Id { get; }
        public string Path { get; }
        public string ClientIpAddress { get; }
        public int ClientPort { get; }
        
        
        public SystemRelayWebSocketConnectionInfo(string id, string path, string clientIpAddress, int clientPort)
        {
            Id = id;
            if (null == Id)
            {
                Id = Guid.NewGuid().ToString();
            }
            Path = path;
            ClientIpAddress = clientIpAddress;
            ClientPort = clientPort;
        }
    }
}
