// Copyright © 2022 Relay Inc.

using System.Collections.Generic;

namespace RelayDotNet
{
    public class NotificationType : SerializedType
    {
        private static readonly Dictionary<string, NotificationType> SerializedNamesToNotificationType = new Dictionary<string, NotificationType>();
        
        private static readonly NotificationType _Broadcast = Add("Broadcast", "broadcast");
        private static readonly NotificationType _Alert = Add("Alert", "alert");
        private static readonly NotificationType _Notify = Add("Notify", "notify");
        private static readonly NotificationType _Cancel = Add("Cancel", "cancel");

        public static NotificationType Broadcast => _Broadcast;
        public static NotificationType Alert => _Alert;
        public static NotificationType Notify => _Notify;
        public static NotificationType Cancel => _Cancel;

        private static NotificationType Add(string name, string serializedName)
        {
            NotificationType notificationType = new NotificationType(name, serializedName);
            SerializedNamesToNotificationType.Add(notificationType.SerializedName, notificationType);
            return notificationType;
        }

        public static bool TryParseSerializedName(string serializedName, out NotificationType result)
        {
            if (SerializedNamesToNotificationType.ContainsKey(serializedName))
            {
                result = SerializedNamesToNotificationType[serializedName];
                return true;
            }

            result = null;
            return false;
        }
    
        public NotificationType(string name, string serializedName) : base(name, serializedName)
        {
        }
    }
}
