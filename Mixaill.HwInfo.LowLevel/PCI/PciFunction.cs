namespace Mixaill.HwInfo.LowLevel.PCI
{
    public class PciFunction
    {
        #region Properties

        public const uint MaxFunctionsCount = 8;

        public uint BusNumber => Device?.BusNumber ?? 0xFF;
        public uint DeviceNumber => Device?.DeviceNumber ?? 0xFF;
        public uint FunctionNumber { get; } = 0xFF;

        public PciDevice Device { get; } = null;

        private PciConfigurationSpace ConfigurationSpace { get; } = null;

        public ushort VendorID => ConfigurationSpace?.VendorID ?? 0xFFFF;

        public string VendorName { get; } = null;

        public ushort DeviceID => ConfigurationSpace?.DeviceID ?? 0xFFFF;

        public string DeviceName { get; } = null;

        public PciClassCode ClassCode => ConfigurationSpace?.DeviceClass ?? PciClassCode.Unassigned;

        #endregion

        public PciFunction(PciDevice device, uint funcnum)
        {
            Device = device;
            FunctionNumber = funcnum;
            ConfigurationSpace = new PciConfigurationSpace(BusNumber, DeviceNumber, FunctionNumber);

            if (Exists())
            {
                VendorName = DB.PciDatabase.Instance.FindVendor(VendorID)?.VendorName ?? null;
                DeviceName = DB.PciDatabase.Instance.FindDevice(VendorID, DeviceID)?.DeviceName ?? null;
            }
        }

        public bool Exists()
        {
            return ConfigurationSpace.Valid();
        }
    }
}
