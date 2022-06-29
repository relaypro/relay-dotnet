// Copyright © 2022 Relay Inc.

using System.Collections.Generic;

namespace RelayDotNet
{
    public abstract class AbstractRelayWorkflow : IRelayWorkflow
    {
        private readonly Relay _relay;

        public Relay Relay => _relay;

        protected AbstractRelayWorkflow(Relay relay)
        {
            _relay = relay;
        }
        
        public virtual void OnStart(IDictionary<string, object> dictionary)
        {
        }

        public virtual void OnStop(IDictionary<string, object> dictionary)
        {
        }

        public virtual void OnInteractionLifecycle(IDictionary<string, object> dictionary)
        {
        }

        public virtual void OnPrompt(IDictionary<string, object> dictionary)
        {
        }
        
        public virtual void OnSpeech(IDictionary<string, object> dictionary)
        {
        }

        public virtual void OnTimer(IDictionary<string, object> dictionary)
        {
        }

        public virtual void OnTimerFired(IDictionary<string, object> dictionary)
        {
        }

        public virtual void OnButton(IDictionary<string, object> dictionary)
        {
        }

        public virtual void OnNotification(IDictionary<string, object> dictionary)
        {
        }

        public virtual void OnSms(IDictionary<string, object> dictionary)
        {
        }

        public virtual void OnAudio(IDictionary<string, object> dictionary)
        {
        }

        public virtual void OnIncident(IDictionary<string, object> dictionary)
        {
        }

        public virtual void OnCallStartRequest(IDictionary<string, object> dictionary)
        {
        }

        public virtual void OnCallReceived(IDictionary<string, object> dictionary)
        {
        }

        public virtual void OnCallRinging(IDictionary<string, object> dictionary)
        {
        }

        public virtual void OnCallProgressing(IDictionary<string, object> dictionary)
        {
        }

        public virtual void OnCallConnected(IDictionary<string, object> dictionary)
        {
        }

        public virtual void OnCallDisconnected(IDictionary<string, object> dictionary)
        {
        }

        public virtual void OnCallFailed(IDictionary<string, object> dictionary)
        {
        }

        public virtual void OnPlayInboxMessage(IDictionary<string, object> dictionary)
        {
        }
    }
}
