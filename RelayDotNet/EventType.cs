// Copyright © 2022 Relay Inc.

using System.Collections.Generic;

// The main namespace.
namespace RelayDotNet
{
    /// Events that can be sent from the Relay server to your workflow.
    public class EventType : SerializedType
    {
        private static readonly Dictionary<string, EventType> SerializedNamesToEventType = new Dictionary<string, EventType>();

        private static readonly EventType _Start = Add("Start", "start");
        private static readonly EventType _Stop = Add("Stop", "stop");
        private static readonly EventType _InteractionLifecycle = Add("InteractionLifecycle", "interaction_lifecycle");
        private static readonly EventType _Timer = Add("Timer", "timer");
        private static readonly EventType _TimerFired = Add("TimerFired", "timer_fired");
        private static readonly EventType _Prompt = Add("Prompt", "prompt");
        private static readonly EventType _Speech = Add("Speech", "speech");
        private static readonly EventType _Progress = Add("Progress", "progress");
        private static readonly EventType _Button = Add("Button", "button");
        private static readonly EventType _Notification = Add("Notification", "notification");
        private static readonly EventType _Sms = Add("Sms", "sms");
        private static readonly EventType _Audio = Add("Audio", "audio");
        private static readonly EventType _Incident = Add("Incident", "incident");
        private static readonly EventType _CallStartRequest = Add("CallStartRequest", "call_start_request");
        private static readonly EventType _CallReceived = Add("CallReceived", "call_received");
        private static readonly EventType _CallRinging = Add("CallRinging", "call_ringing");
        private static readonly EventType _CallProgressing = Add("CallProgressing", "call_progressing");
        private static readonly EventType _CallConnected = Add("CallConnected", "call_connected");
        private static readonly EventType _CallDisconnected = Add("CallDisconnected", "call_disconnected");
        private static readonly EventType _CallFailed = Add("CallFailed", "call_failed");
        private static readonly EventType _PlayInboxMessage = Add("PlayInboxMessage", "play_inbox_message");
        
        public static EventType Start => _Start;
        public static EventType Stop => _Stop;
        public static EventType InteractionLifecycle => _InteractionLifecycle;
        public static EventType Timer => _Timer;
        public static EventType TimerFired => _TimerFired;
        public static EventType Prompt => _Prompt;
        public static EventType Speech => _Speech;
        public static EventType Progress => _Progress;
        public static EventType Button => _Button;
        public static EventType Notification => _Notification;
        public static EventType Sms => _Sms;
        public static EventType Audio => _Audio;
        public static EventType Incident => _Incident;
        /// There is a request to make an outbound call. This event can occur on
        /// the caller after using the "Call X" voice command on the Assistant.
        public static EventType CallStartRequest => _CallStartRequest;
        /// The device is receiving an inbound call request. This event can occur on the callee.
        public static EventType CallReceived => _CallReceived;
        /// The device we called is ringing. We are waiting for them to answer.
        /// This event can occur on the caller.
        public static EventType CallRinging => _CallRinging;
        /// The device we called is making progress on getting connected. This may
        /// be interspersed with on_call_ringing. This event can occur on the caller.
        public static EventType CallProgressing => _CallProgressing;
        /// A call attempt that was ringing, progressing, or incoming is now fully
        /// connected. This event can occur on both the caller and the callee.
        public static EventType CallConnected => _CallConnected;
        /// A call that was once connected has become disconnected. This event can
        /// occur on both the caller and the callee.
        public static EventType CallDisconnected => _CallDisconnected;
        /// A call failed to get connected. This event can occur on both the caller
        /// and the callee.
        public static EventType CallFailed => _CallFailed;
        public static EventType PlayInboxMessage => _PlayInboxMessage;
        

        private static EventType Add(string name, string serializedName)
        {
            EventType eventType = new EventType(name, serializedName);
            SerializedNamesToEventType.Add(eventType.SerializedName, eventType);
            return eventType;
        }
        
        public static bool TryParseSerializedName(string serializedName, out EventType result)
        {
            if (SerializedNamesToEventType.ContainsKey(serializedName))
            {
                result = SerializedNamesToEventType[serializedName];
                return true;
            }

            result = null;
            return false;
        }
        
        public EventType(string name, string serializedName) : base(name, serializedName)
        {
        }
    }
}
