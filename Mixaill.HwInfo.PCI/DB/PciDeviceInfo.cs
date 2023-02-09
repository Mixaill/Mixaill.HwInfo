using System;
using System.Diagnostics;

namespace Mixaill.HwInfo.PCI.DB
{
    [DebuggerDisplay("Device, ID={DeviceID}, Name={DeviceName}")]
    public class PciDeviceInfo
    {
        public ushort DeviceID { get; } = 0xFFFF;

        public string DeviceName { get; } = null;

        public ushort VendorID => Vendor?.VendorID ?? 0xFFFF;

        public string VendorName => Vendor?.VendorName ?? null;

        public PciVendorInfo Vendor { get; } = null;

        public PciDeviceInfo(PciVendorInfo vendor, string str)
        {
            Vendor = vendor;


            var tokens = str.Trim('\t').Split(new[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);

            DeviceID = Convert.ToUInt16(tokens[0], 16);
            DeviceName = tokens[1];
        }
    }
}
