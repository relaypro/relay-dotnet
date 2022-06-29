// Copyright © 2022 Relay Inc.

using System.Collections.Generic;

namespace RelayDotNet
{
    public class NotificationPriority : SerializedType
    {
        private static readonly Dictionary<string, NotificationPriority> SerializedNamesToNotificationPriority = new Dictionary<string, NotificationPriority>();
        
        private static readonly NotificationPriority _Normal = Add("Normal", "normal");
        private static readonly NotificationPriority _High = Add("High", "high");
        private static readonly NotificationPriority _Critical = Add("Critical", "critical");

        public static NotificationPriority Normal => _Normal;
        public static NotificationPriority High => _High;
        public static NotificationPriority Critical => _Critical;

        private static NotificationPriority Add(string name, string serializedName)
        {
            NotificationPriority notificationPriority = new NotificationPriority(name, serializedName);
            SerializedNamesToNotificationPriority.Add(notificationPriority.SerializedName, notificationPriority);
            return notificationPriority;
        }

        public static bool TryParseSerializedName(string serializedName, out NotificationPriority result)
        {
            if (SerializedNamesToNotificationPriority.ContainsKey(serializedName))
            {
                result = SerializedNamesToNotificationPriority[serializedName];
                return true;
            }

            result = null;
            return false;
        }
    
        public NotificationPriority(string name, string serializedName) : base(name, serializedName)
        {
        }
    }
}
