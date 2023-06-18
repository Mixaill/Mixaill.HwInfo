using Mixaill.HwInfo.LowLevel;
using Mixaill.HwInfo.LowLevel.Backend.OLS;

namespace Mixaill.HwInfo.PCI
{
    class PciConfigurationSpace
    {
        public ushort DeviceID { get; } = 0;

        public ushort VendorID { get; } = 0;

        public PciClassCode DeviceClass { get; } = PciClassCode.Unassigned;

        private uint PciAddress { get; } = 0;


        public PciConfigurationSpace(uint bus, uint device, uint function)
        {
            var ols = LowLevelOls.Instance;

            PciAddress = ols.PciBusDevFunc(bus, device, function);


            //0x00-0x04
            var conf_0 = ols.ReadPciConfigDword(PciAddress, 0x00);
            VendorID = (ushort)(conf_0 & 0xFFFF);
            DeviceID = (ushort)((conf_0 >> 16) & 0xFFFF);

            //0x08-0x012
            var conf_2 = ols.ReadPciConfigDword(PciAddress, 0x08);
            DeviceClass = (PciClassCode)((conf_2 >> 24) & 0xFF);
        }

        public bool Valid()
        {
            return VendorID != 0x0000 && VendorID != 0xFFFF;
        }
    }
}
