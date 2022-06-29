// Copyright © 2022 Relay Inc.

using System.Collections.Generic;

namespace RelayDotNet
{
    public class Language : SerializedType
    {
        private static readonly Dictionary<string, Language> SerializedNamesToLanguage = new Dictionary<string, Language>();
        
        private static readonly Language _English = Add("English", "en-US");
        private static readonly Language _German = Add("German", "de-DE");
        private static readonly Language _Spanish = Add("Spanish", "es-ES");
        private static readonly Language _French = Add("French", "fr-FR");
        private static readonly Language _Italian = Add("Italian", "it-IT");
        private static readonly Language _Russian = Add("Russian", "ru-RU");
        private static readonly Language _Swedish = Add("Swedish", "sv-SE");
        private static readonly Language _Turkish = Add("Turkish", "tr-TR");
        private static readonly Language _Hindi = Add("Hindi", "hi-IN");
        private static readonly Language _Icelandic = Add("Icelandic", "is-IS");
        private static readonly Language _Japanese = Add("Japanese", "ja-JP");
        private static readonly Language _Korean = Add("Korean", "ko-KR");
        private static readonly Language _Polish = Add("Polish", "pl-PK");
        private static readonly Language _Portuguese = Add("Portuguese", "pt-BR");
        private static readonly Language _Norwegian = Add("Norwegian", "nb-NO");
        private static readonly Language _Dutch = Add("Dutch", "nl-NL");
        private static readonly Language _Chinese = Add("Chinese", "zh");

        public static Language English => _English;
        public static Language German => _German;
        public static Language Spanish => _Spanish;
        public static Language French => _French;
        public static Language Italian => _Italian;
        public static Language Russian => _Russian;
        public static Language Swedish => _Swedish;
        public static Language Turkish => _Turkish;
        public static Language Hindi => _Hindi;
        public static Language Icelandic => _Icelandic;
        public static Language Japanese => _Japanese;
        public static Language Korean => _Korean;
        public static Language Polish => _Polish;
        public static Language Portuguese => _Portuguese;
        public static Language Norwegian => _Norwegian;
        public static Language Dutch => _Dutch;
        public static Language Chinese => _Chinese;


        private static Language Add(string name, string serializedName)
        {
            Language language = new Language(name, serializedName);
            SerializedNamesToLanguage.Add(language.SerializedName, language);
            return language;
        }

        public static bool TryParseSerializedName(string serializedName, out Language result)
        {
            if (SerializedNamesToLanguage.ContainsKey(serializedName))
            {
                result = SerializedNamesToLanguage[serializedName];
                return true;
            }

            result = null;
            return false;
        }
    
        public Language(string name, string serializedName) : base(name, serializedName)
        {
        }
    }
}
