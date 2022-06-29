// Copyright © 2022 Relay Inc.

using System.Collections.Generic;

namespace RelayDotNet
{
    public interface IRelayWorkflow
    {
        public void OnStart(IDictionary<string, object> dictionary);
        public void OnStop(IDictionary<string, object> dictionary);
        public void OnInteractionLifecycle(IDictionary<string, object> dictionary);
        public void OnPrompt(IDictionary<string, object> dictionary);
        public void OnSpeech(IDictionary<string, object> dictionary);
        public void OnTimer(IDictionary<string, object> dictionary);
        public void OnTimerFired(IDictionary<string, object> dictionary);
        public void OnButton(IDictionary<string, object> dictionary);
        public void OnNotification(IDictionary<string, object> dictionary);
        public void OnSms(IDictionary<string, object> dictionary);
        public void OnAudio(IDictionary<string, object> dictionary);
        public void OnIncident(IDictionary<string, object> dictionary);
        public void OnCallStartRequest(IDictionary<string, object> dictionary);
        public void OnCallReceived(IDictionary<string, object> dictionary);
        public void OnCallRinging(IDictionary<string, object> dictionary);
        public void OnCallProgressing(IDictionary<string, object> dictionary);
        public void OnCallConnected(IDictionary<string, object> dictionary);
        public void OnCallDisconnected(IDictionary<string, object> dictionary);
        public void OnCallFailed(IDictionary<string, object> dictionary);
        public void OnPlayInboxMessage(IDictionary<string, object> dictionary);
    }
}
