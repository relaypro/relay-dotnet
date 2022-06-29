// Copyright © 2022 Relay Inc.

using System.Collections.Generic;

namespace RelayDotNet
{
    public class NotificationSound : SerializedType
    {
        private static readonly Dictionary<string, NotificationSound> SerializedNamesToNotificationSound = new Dictionary<string, NotificationSound>();
        
        private static readonly NotificationSound _Default = Add("Default", "default");
        private static readonly NotificationSound _Sos = Add("Sos", "sos");

        public static NotificationSound Default => _Default;
        public static NotificationSound Sos => _Sos;

        private static NotificationSound Add(string name, string serializedName)
        {
            NotificationSound notificationSound = new NotificationSound(name, serializedName);
            SerializedNamesToNotificationSound.Add(notificationSound.SerializedName, notificationSound);
            return notificationSound;
        }

        public static bool TryParseSerializedName(string serializedName, out NotificationSound result)
        {
            if (SerializedNamesToNotificationSound.ContainsKey(serializedName))
            {
                result = SerializedNamesToNotificationSound[serializedName];
                return true;
            }

            result = null;
            return false;
        }
    
        public NotificationSound(string name, string serializedName) : base(name, serializedName)
        {
        }
    }
}
