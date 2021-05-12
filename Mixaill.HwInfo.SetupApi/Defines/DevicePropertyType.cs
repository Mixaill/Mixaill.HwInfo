namespace Mixaill.SetupApi.Defines
{
    public enum DevicePropertyType
    {
        Empty                    = 0,
        Null                     = 1,
        SByte                    = 2,
        Byte                     = 3,
        Int16                    = 4,
        UInt16                   = 5,
        Int32                    = 6,
        UInt32                   = 7,
        Int64                    = 8,
        UInt64                   = 9,
        Float                    = 10,
        Double                   = 11,
        Decimal                  = 12,
        Guid                     = 13,
        Currency                 = 14,
        Date                     = 15,
        Filetime                 = 16,
        Boolean                  = 17,
        String                   = 18,
        StringList               = String | DevicePropertyTypeModifier.List,
        SecurityDescriptor       = 19,
        SecurityDescriptorString = 20,
        DevPropKey               = 21,
        Binary                   = Byte | DevicePropertyTypeModifier.Array,
        DevPropType              = 22,
        Error                    = 23,
        NtStatus                 = 24,
        StringIndirect           = 25
    }
}
