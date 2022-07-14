// Copyright © 2022 Relay Inc.

using System.Collections.Generic;

namespace RelayDotNet
{
    public class DeviceInfoQuery : SerializedType
    {
        private static readonly Dictionary<string, DeviceInfoQuery> SerializedNamesToDeviceInfoQuery = new Dictionary<string, DeviceInfoQuery>();
        
        private static readonly DeviceInfoQuery _NameQuery = Add("NameQuery", "name");
        private static readonly DeviceInfoQuery _IdQuery = Add("IdQuery", "id");
        private static readonly DeviceInfoQuery _TypeQuery = Add("TypeQuery", "type");
        private static readonly DeviceInfoQuery _AddressQuery = Add("AddressQuery", "address");
        private static readonly DeviceInfoQuery _CoordinatesQuery = Add("CoordinatesQuery", "latlong");
        private static readonly DeviceInfoQuery _BatteryQuery = Add("BatteryQuery", "battery");
        private static readonly DeviceInfoQuery _IndoorLocationQuery = Add("IndoorLocationQuery", "indoor_location");
        private static readonly DeviceInfoQuery _LocationQuery = Add("LocationQuery", "location");
        private static readonly DeviceInfoQuery _LocationEnabledQuery = Add("LocationEnabledQuery", "location_enabled");
        private static readonly DeviceInfoQuery _UsernameQuery = Add("UsernameQuery", "username");

        public static DeviceInfoQuery NameQuery => _NameQuery;
        public static DeviceInfoQuery IdQuery => _IdQuery;
        public static DeviceInfoQuery TypeQuery => _TypeQuery;
        public static DeviceInfoQuery AddressQuery => _AddressQuery;
        public static DeviceInfoQuery CoordinatesQuery => _CoordinatesQuery;
        public static DeviceInfoQuery BatteryQuery => _BatteryQuery;
        public static DeviceInfoQuery IndoorLocationQuery => _IndoorLocationQuery;
        public static DeviceInfoQuery LocationQuery => _LocationQuery;
        public static DeviceInfoQuery LocationEnabledQuery => _LocationEnabledQuery;
        public static DeviceInfoQuery UsernameQuery => _UsernameQuery;

        private static DeviceInfoQuery Add(string name, string serializedName)
        {
            DeviceInfoQuery deviceInfoQuery = new DeviceInfoQuery(name, serializedName);
            SerializedNamesToDeviceInfoQuery.Add(deviceInfoQuery.SerializedName, deviceInfoQuery);
            return deviceInfoQuery;
        }

        public static bool TryParseSerializedName(string serializedName, out DeviceInfoQuery result)
        {
            if (SerializedNamesToDeviceInfoQuery.ContainsKey(serializedName))
            {
                result = SerializedNamesToDeviceInfoQuery[serializedName];
                return true;
            }

            result = null;
            return false;
        }
    
        public DeviceInfoQuery(string name, string serializedName) : base(name, serializedName)
        {
        }
    }
}
