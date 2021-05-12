namespace Mixaill.SetupApi.Defines
{
    public enum DeviceResourceType
    {
        All       = 0,
        Mem       = 1,
        IO        = 2,
        DMA       = 3,
        IRQ       = 4,
        DoNotUse  = 5,
        BusNumber = 6,
        MemLarge  = 7,
        MAX       = 7,

        IgnoredBit    = 0x00008000,
        ClassSpecific = 0x0000FFFF,
        Reserved      = 0x00008000,
        DevicePrivate = 0x00008001,
        PcCardConfig  = 0x00008002,
        MfCardConfig  = 0x00008003,
        Connection    = 0x00008004
    }
}
