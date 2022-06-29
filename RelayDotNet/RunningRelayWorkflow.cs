// Copyright © 2022 Relay Inc.

using System.Threading;
using Fleck;

namespace RelayDotNet
{
    public class RunningRelayWorkflow
    {
        private readonly IRelayWorkflow _relayWorkflow;
        private readonly IRelayWebSocketConnection _webSocketConnection;
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1);
        private bool _stopped = false;

        public IRelayWorkflow RelayWorkflow => _relayWorkflow;

        public IRelayWebSocketConnection WebSocketConnection => _webSocketConnection;

        public SemaphoreSlim Semaphore => _semaphore;

        public bool Stopped
        {
            get => _stopped;
            set => _stopped = value;
        }

        public RunningRelayWorkflow(IRelayWorkflow relayWorkflow, IRelayWebSocketConnection webSocketConnection)
        {
            _relayWorkflow = relayWorkflow;
            _webSocketConnection = webSocketConnection;
        }
    }
}
