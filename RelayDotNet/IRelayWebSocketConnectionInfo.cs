// Copyright © 2022 Relay Inc.

using System;

namespace RelayDotNet
{
    public interface IRelayWebSocketConnectionInfo
    {
        string Id { get; }
        string Path { get; }
        string ClientIpAddress { get; }
        int ClientPort { get; }
    }
}
