// Copyright © 2022 Relay Inc.

using System.Threading.Tasks;

namespace RelayDotNet
{
    public interface IRelayWebSocketConnection
    {
        Task Send(string message);

        Task Close();

        Task Close(int code);
        
        IRelayWebSocketConnectionInfo ConnectionInfo { get; }        
    }
}
