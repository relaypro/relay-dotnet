// Copyright © 2022 Relay Inc.

using System.Collections.Generic;

namespace RelayDotNet
{
    public class LedIndex : SerializedType
    {
        private static readonly Dictionary<string, LedIndex> SerializedNamesToEventType = new Dictionary<string, LedIndex>();

        private static readonly LedIndex _Ring = Add("Ring", "ring");
        private static readonly LedIndex _Led1 = Add("Led1", "1");
        private static readonly LedIndex _Led2 = Add("Led2", "2");
        private static readonly LedIndex _Led3 = Add("Led3", "3");
        private static readonly LedIndex _Led4 = Add("Led4", "4");
        private static readonly LedIndex _Led5 = Add("Led5", "5");
        private static readonly LedIndex _Led6 = Add("Led6", "6");
        private static readonly LedIndex _Led7 = Add("Led7", "7");
        private static readonly LedIndex _Led8 = Add("Led8", "8");
        private static readonly LedIndex _Led9 = Add("Led9", "9");
        private static readonly LedIndex _Led10 = Add("Led10", "10");
        private static readonly LedIndex _Led11 = Add("Led11", "11");
        private static readonly LedIndex _Led12 = Add("Led12", "12");
        private static readonly LedIndex _Led13 = Add("Led13", "13");
        private static readonly LedIndex _Led14 = Add("Led14", "14");
        private static readonly LedIndex _Led15 = Add("Led15", "15");
        private static readonly LedIndex _Led16 = Add("Led16", "16");

        public static LedIndex Ring => _Ring;
        public static LedIndex Led1 => _Led1;
        public static LedIndex Led2 => _Led2;
        public static LedIndex Led3 => _Led3;
        public static LedIndex Led4 => _Led4;
        public static LedIndex Led5 => _Led5;
        public static LedIndex Led6 => _Led6;
        public static LedIndex Led7 => _Led7;
        public static LedIndex Led8 => _Led8;
        public static LedIndex Led9 => _Led9;
        public static LedIndex Led10 => _Led10;
        public static LedIndex Led11 => _Led11;
        public static LedIndex Led12 => _Led12;
        public static LedIndex Led13 => _Led13;
        public static LedIndex Led14 => _Led14;
        public static LedIndex Led15 => _Led15;
        public static LedIndex Led16 => _Led16;

        private static LedIndex Add(string name, string serializedName)
        {
            LedIndex ledIndex = new LedIndex(name, serializedName);
            SerializedNamesToEventType.Add(ledIndex.SerializedName, ledIndex);
            return ledIndex;
        }
        
        public static bool TryParseSerializedName(string serializedName, out LedIndex result)
        {
            if (SerializedNamesToEventType.ContainsKey(serializedName))
            {
                result = SerializedNamesToEventType[serializedName];
                return true;
            }

            result = null;
            return false;
        }
        
        public LedIndex(string name, string serializedName) : base(name, serializedName)
        {
        }
    }
}
