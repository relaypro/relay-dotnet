// Copyright © 2022 Relay Inc.

using System.Collections.Generic;

namespace RelayDotNet
{
    public class RequestType : SerializedType
    {
        private static readonly Dictionary<string, RequestType> SerializedNamesToRequestType = new Dictionary<string, RequestType>();

        private static readonly RequestType _Terminate = Add("Terminate", "terminate");
        private static readonly RequestType _StartInteraction = Add("StartInteraction", "start_interaction"); 
        private static readonly RequestType _EndInteraction = Add("EndInteraction", "end_interaction");
        private static readonly RequestType _Say = Add("Say", "say");
        private static readonly RequestType _Play = Add("Play", "play");
        private static readonly RequestType _StopPlayback = Add("StopPlayback", "stop_playback");
        private static readonly RequestType _Vibrate = Add("Vibrate", "vibrate");
        private static readonly RequestType _InboxCount = Add("InboxCount", "inbox_count");
        private static readonly RequestType _PlayInboxMessages = Add("PlayInboxMessages", "play_inbox_messages");
        private static readonly RequestType _LogAnalyticsEvent = Add("LogAnalyticsEvent", "log_analytics_event");
        private static readonly RequestType _SetLed = Add("SetLed", "set_led");
        private static readonly RequestType _Translate = Add("Translate", "translate");
        private static readonly RequestType _ListGroupMembers = Add("ListGroupMembers", "list_group_members");
        private static readonly RequestType _GetDeviceInfo = Add("GetDeviceInfo", "get_device_info");
        private static readonly RequestType _SetDeviceInfo = Add("SetDeviceInfo", "set_device_info");
        private static readonly RequestType _SetUserProfile = Add("SetUserProfile", "set_user_profile");
        private static readonly RequestType _SetChannel = Add("SetChannel", "set_channel");
        private static readonly RequestType _SetVar = Add("SetVar", "set_var");
        private static readonly RequestType _UnsetVar = Add("UnsetVar", "unset_var");
        private static readonly RequestType _GetVar = Add("GetVar", "get_var");
        private static readonly RequestType _SetTimer = Add("SetTimer", "set_timer");
        private static readonly RequestType _ClearTimer = Add("ClearTimer", "clear_timer");
        private static readonly RequestType _StartTimer = Add("StartTimer", "start_timer");
        private static readonly RequestType _StopTimer = Add("StopTimer", "stop_timer");
        private static readonly RequestType _Notification = Add("Notification", "notification");
        private static readonly RequestType _Call = Add("Call", "call");
        private static readonly RequestType _Answer = Add("Answer", "answer");
        private static readonly RequestType _Hangup = Add("Hangup", "hangup");
        private static readonly RequestType _Sms = Add("Sms", "sms");
        private static readonly RequestType _Listen = Add("Listen", "listen");
        private static readonly RequestType _CreateIncident = Add("CreateIncident", "create_incident");
        private static readonly RequestType _ResolveIncident = Add("ResolveIncident", "resolve_incident");
        private static readonly RequestType _DevicePowerOff = Add("DevicePowerOff", "device_power_off");
        private static readonly RequestType _SetDeviceMode = Add("SetDeviceMode", "set_device_mode");
        private static readonly RequestType _SetHomeChannelState = Add("SetHomeChannelState", "set_home_channel_state");


        public static RequestType Terminate => _Terminate;
        public static RequestType StartInteraction => _StartInteraction;
        public static RequestType EndInteraction => _EndInteraction;
        public static RequestType Say => _Say;
        public static RequestType Play => _Play;
        public static RequestType StopPlayback => _StopPlayback;
        public static RequestType Vibrate => _Vibrate;
        public static RequestType InboxCount => _InboxCount;
        public static RequestType PlayInboxMessages => _PlayInboxMessages;
        public static RequestType LogAnalyticsEvent => _LogAnalyticsEvent;
        public static RequestType SetLed => _SetLed;
        public static RequestType Translate => _Translate;
        public static RequestType ListGroupMembers => _ListGroupMembers;
        public static RequestType GetDeviceInfo => _GetDeviceInfo;
        public static RequestType SetDeviceInfo => _SetDeviceInfo;
        public static RequestType SetUserProfile => _SetUserProfile;
        public static RequestType SetChannel => _SetChannel;
        public static RequestType SetVar => _SetVar;
        public static RequestType UnsetVar => _UnsetVar;
        public static RequestType GetVar => _GetVar;
        public static RequestType SetTimer => _SetTimer;
        public static RequestType ClearTimer => _ClearTimer;
        public static RequestType StartTimer => _StartTimer;
        public static RequestType StopTimer => _StopTimer;
        public static RequestType Notification => _Notification;
        public static RequestType Call => _Call;
        public static RequestType Answer => _Answer;
        public static RequestType Hangup => _Hangup;
        public static RequestType Sms => _Sms;
        public static RequestType Listen => _Listen;
        public static RequestType CreateIncident => _CreateIncident;
        public static RequestType ResolveIncident => _ResolveIncident;
        public static RequestType DevicePowerOff => _DevicePowerOff;
        public static RequestType SetDeviceMode => _SetDeviceMode;
        public static RequestType SetHomeChannelState => _SetHomeChannelState;

        private static RequestType Add(string name, string serializedName)
        {
            RequestType requestType = new RequestType(name, serializedName);
            SerializedNamesToRequestType.Add(requestType.SerializedName, requestType);
            return requestType;
        }

        public static bool TryParseSerializedName(string serializedName, out RequestType result)
        {
            if (SerializedNamesToRequestType.ContainsKey(serializedName))
            {
                result = SerializedNamesToRequestType[serializedName];
                return true;
            }

            result = null;
            return false;
        }
        
        public RequestType(string name, string serializedName) : base(name, serializedName)
        {
        }
    }
}
