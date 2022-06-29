// Copyright © 2022 Relay Inc.

using System.Collections.Generic;

namespace RelayDotNet
{
    public class LedEffect : SerializedType
    {
        private static readonly Dictionary<string, LedEffect> SerializedNamesToEventType = new Dictionary<string, LedEffect>();

        private static readonly LedEffect _Off = Add("Off", "off");
        private static readonly LedEffect _Breathe = Add("Breathe", "breathe");
        private static readonly LedEffect _Flash = Add("Flash", "flash");
        private static readonly LedEffect _Rotate = Add("Rotate", "rotate");
        private static readonly LedEffect _Rainbow = Add("Rainbow", "rainbow");
        private static readonly LedEffect _Static = Add("Static", "static");

        public static LedEffect Off => _Off;

        public static LedEffect Breathe => _Breathe;

        public static LedEffect Flash => _Flash;

        public static LedEffect Rotate => _Rotate;

        public static LedEffect Rainbow => _Rainbow;

        public static LedEffect Static => _Static;


        private static LedEffect Add(string name, string serializedName)
        {
            LedEffect ledEffect = new LedEffect(name, serializedName);
            SerializedNamesToEventType.Add(ledEffect.SerializedName, ledEffect);
            return ledEffect;
        }
        
        public static bool TryParseSerializedName(string serializedName, out LedEffect result)
        {
            if (SerializedNamesToEventType.ContainsKey(serializedName))
            {
                result = SerializedNamesToEventType[serializedName];
                return true;
            }

            result = null;
            return false;
        }
        
        public LedEffect(string name, string serializedName) : base(name, serializedName)
        {
        }
    }
}
