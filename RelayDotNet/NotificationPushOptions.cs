// Copyright © 2022 Relay Inc.

using System.Collections.Generic;

namespace RelayDotNet
{
    public class NotificationPushOptions
    {
        private readonly Dictionary<string, object> _dictionary = new Dictionary<string, object>();

        public Dictionary<string, object> Dictionary => _dictionary;

        public void SetPriority(NotificationPriority notificationPriority)
        {
            _dictionary["priority"] = notificationPriority.SerializedName;
        }

        public void SetTitle(string title)
        {
            _dictionary["title"] = title;
        }

        public void SetBody(string body)
        {
            _dictionary["body"] = body;
        }

        public void SetSound(NotificationSound notificationSound)
        {
            _dictionary["sound"] = notificationSound.SerializedName;
        }
    }
}
