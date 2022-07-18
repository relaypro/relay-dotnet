// Copyright © 2022 Relay Inc.

using System.Collections.Generic;

namespace RelayDotNet
{
    public class DeviceInfoField : SerializedType
    {
        private static readonly Dictionary<string, DeviceInfoField> SerializedNamesToDeviceInfoField = new Dictionary<string, DeviceInfoField>();
        
        private static readonly DeviceInfoField _Label = Add("Label", "label");
        private static readonly DeviceInfoField _Channel = Add("Channel", "channel");
        private static readonly DeviceInfoField _Location = Add("Location", "location_enabled");

        public static DeviceInfoField Label => _Label;
        public static DeviceInfoField Channel => _Channel;
        public static DeviceInfoField Location => _Location;

        private static DeviceInfoField Add(string name, string serializedName)
        {
            DeviceInfoField deviceInfoField = new DeviceInfoField(name, serializedName);
            SerializedNamesToDeviceInfoField.Add(deviceInfoField.SerializedName, deviceInfoField);
            return deviceInfoField;
        }

        public static bool TryParseSerializedName(string serializedName, out DeviceInfoField result)
        {
            if (SerializedNamesToDeviceInfoField.ContainsKey(serializedName))
            {
                result = SerializedNamesToDeviceInfoField[serializedName];
                return true;
            }

            result = null;
            return false;
        }
    
        public DeviceInfoField(string name, string serializedName) : base(name, serializedName)
        {
        }
    }
}
