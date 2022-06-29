// Copyright © 2022 Relay Inc.

using System.Collections.Generic;

namespace RelayDotNet
{
    public class DeviceType : SerializedType
    {
        private static readonly Dictionary<string, DeviceType> SerializedNamesToDeviceType = new Dictionary<string, DeviceType>();
        
        private static readonly DeviceType _Relay = Add("Relay", "relay");
        private static readonly DeviceType _Relay2 = Add("Relay2", "relay2");
        private static readonly DeviceType _RelayApp = Add("RelayApp", "relay_app");
        private static readonly DeviceType _Roip = Add("Roip", "roip");
        private static readonly DeviceType _Dash = Add("Dash", "dash");

        public static DeviceType Relay => _Relay;
        public static DeviceType Relay2 => _Relay2;
        public static DeviceType RelayApp => _RelayApp;
        public static DeviceType Roip => _Roip;
        public static DeviceType Dash => _Dash;

        private static DeviceType Add(string name, string serializedName)
        {
            DeviceType deviceType = new DeviceType(name, serializedName);
            SerializedNamesToDeviceType.Add(deviceType.SerializedName, deviceType);
            return deviceType;
        }

        public static bool TryParseSerializedName(string serializedName, out DeviceType result)
        {
            if (SerializedNamesToDeviceType.ContainsKey(serializedName))
            {
                result = SerializedNamesToDeviceType[serializedName];
                return true;
            }

            result = null;
            return false;
        }
    
        public DeviceType(string name, string serializedName) : base(name, serializedName)
        {
        }
    }
}
