using System;
using System.Collections.Generic;
using System.Text;

using Mixaill.SetupApi.Defines;

namespace Mixaill.SetupApi
{
    public class DevicePropertyValue
    {
        public DevicePropertyType Type { get; } = DevicePropertyType.Empty;

        public object Value { get; } = null;

        protected DevicePropertyValue(DevicePropertyType type)
        {
            Type = type;
        }
    }

    public class DevicePropertyValueUInt32 : DevicePropertyValue
    {
        public new UInt32 Value { get; }

        public DevicePropertyValueUInt32(DevicePropertyType type, byte[] data) : base(type)
        {
            Value = BitConverter.ToUInt32(data, 0);
        }
    }

    public class DevicePropertyValueGuid : DevicePropertyValue
    {
        public new Guid Value { get; }

        public DevicePropertyValueGuid(DevicePropertyType type, byte[] data) : base(type)
        {
            Value = new Guid(data);
        }
    }

    public class DevicePropertyValueString : DevicePropertyValue
    {
        public new string Value { get; }

        public DevicePropertyValueString(DevicePropertyType type, byte[] data) : base(type)
        {
            Value = Encoding.Unicode.GetString(data);
        }
    }

    public class DevicePropertyValueStringList : DevicePropertyValue
    {
        public new List<string> Value { get; } = new List<string>();

        public DevicePropertyValueStringList(DevicePropertyType type, byte[] data) : base(type)
        {
            Value = data.SeparateRegMultiSz();
        }
    }

    internal static class DevicePropertyValueFactory
    {
        public static DevicePropertyValue Create(DevicePropertyType type, byte[] data)
        {
            switch (type)
            {
                case DevicePropertyType.UInt32:
                    return new DevicePropertyValueUInt32(type, data);
                case DevicePropertyType.Guid:
                    return new DevicePropertyValueGuid(type, data);
                case DevicePropertyType.String:
                    return new DevicePropertyValueString(type, data);
                case DevicePropertyType.StringList:
                    return new DevicePropertyValueStringList(type, data);
            }

            throw new NotImplementedException($"DevPropType {type} is not implemented");
        }
    }
}
