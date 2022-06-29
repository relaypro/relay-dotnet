// Copyright © 2022 Relay Inc.

using Serilog;

namespace RelayDotNet
{
    public class SystemKestralRelayWebSocketConnector : SystemRelayWebSocketConnector
    {
        public SystemKestralRelayWebSocketConnector(Relay relay, string ip, int port, bool secure) : base(relay, ip, port, secure)
        {
            Log.Debug("SystemKestralRelayWebSocketConnector constructed");
        }

        public override void Start()
        {
            Log.Debug("Start");
        }
    }
}
