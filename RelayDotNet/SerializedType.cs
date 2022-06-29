// Copyright © 2022 Relay Inc.

namespace RelayDotNet
{
    public class SerializedType
    {
        private readonly string _name;
        private readonly string _serializedName;

        public SerializedType(string name, string serializedName)
        {
            _name = name;
            _serializedName = serializedName;
        }

        public string Name => _name;

        public string SerializedName => _serializedName;

        public override string ToString()
        {
            return _name;
        }
    }
}
