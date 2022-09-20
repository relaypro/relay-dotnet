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

        /// <summary>
        /// There is a request to make an outbound call. This event can occur on
        /// the caller after using the "Call X" voice command on the Assistant.
        /// </summary>
        /// <param name="dictionary">the event that describes the outbound call request.</param>
        public virtual void OnCallStartRequest(IDictionary<string, object> dictionary)
        {
        }

        /// <summary>
        /// The device is receiving an inbound call request. This event can occur on the callee.
        /// </summary>
        /// <param name="dictionary">the event that describes the inbound call request.</param>
        public virtual void OnCallReceived(IDictionary<string, object> dictionary)
        {
        }

        /// <summary>
        /// The device we called is ringing. We are waiting for them to answer.
        /// This event can occur on the caller.
        /// </summary>
        /// <param name="dictionary">the event that describes the call.</param>
        public virtual void OnCallRinging(IDictionary<string, object> dictionary)
        {
        }

        /// <summary>
        /// The device we called is making progress on getting connected. This may
        /// be interspersed with OnCallProgressing. This event can occur on the caller.
        /// </summary>
        /// <param name="dictionary">the event that describes the progress.</param>
        public virtual void OnCallProgressing(IDictionary<string, object> dictionary)
        {
        }

        /// <summary>
        /// A call attempt that was ringing, progressing, or incoming is now fully
        /// connected. This event can occur on both the caller and the callee.
        /// </summary>
        /// <param name="dictionary">the event that describes the call.</param>
        public virtual void OnCallConnected(IDictionary<string, object> dictionary)
        {
        }

        /// <summary>
        /// A call that was once connected has become disconnected. This event can
        /// occur on both the caller and the callee.
        /// </summary>
        /// <param name="dictionary">the event that describes the call.</param>
        public virtual void OnCallDisconnected(IDictionary<string, object> dictionary)
        {
        }

        /// <summary>
        /// A call failed to get connected. This event can occur on both the caller
        /// and the callee.
        /// </summary>
        /// <param name="dictionary">the event that describes the call attempt.</param>
        public virtual void OnCallFailed(IDictionary<string, object> dictionary)
        {
        }

        public virtual void OnPlayInboxMessage(IDictionary<string, object> dictionary)
        {
        }
    }
}
