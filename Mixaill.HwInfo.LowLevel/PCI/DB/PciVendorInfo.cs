using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace Mixaill.HwInfo.LowLevel.PCI.DB
{
    [DebuggerDisplay("Vendor, ID={VendorID}, Name={VendorName}, Devices={Devices.Count}")]
    public class PciVendorInfo
    {
        public ushort VendorID { get; } = 0xFFFF;

        public string VendorName { get; } = null;

        
        private Dictionary<ushort, PciDeviceInfo> Devices = new Dictionary<ushort, PciDeviceInfo>();


        public PciVendorInfo(string str)
        {
            var tokens = str.TrimStart('\t').Split(new []{ ' '}, 2, StringSplitOptions.RemoveEmptyEntries);
            VendorID = Convert.ToUInt16(tokens[0], 16);
            VendorName = tokens[1];
        }


        internal PciDeviceInfo AddDevice(string str)
        {
            var dev = new PciDeviceInfo(this, str);
            Devices[dev.DeviceID] = dev;

            return dev;
        }

        public IReadOnlyCollection<PciDeviceInfo> GetDevices()
        {
            return Devices.Values;
        }

        public PciDeviceInfo FindDevice(ushort deviceId)
        {
            if (Devices.ContainsKey(deviceId))
            {
                return Devices[deviceId];
            }

            return null;
        }
    }
}
