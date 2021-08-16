using System.Collections.Generic;

namespace Mixaill.HwInfo.LowLevel.PCI
{
    public class PciBus
    {
        #region Properties

        public const uint MaxBusCount = 256;

        public uint BusNumber { get; } = 0xFF;


        private Dictionary<uint, PciDevice> Devices { get; } = new Dictionary<uint, PciDevice>();

        #endregion

        public PciBus(uint busNumber)
        {
            BusNumber = busNumber;

            for(uint devnum = 0; devnum < PciDevice.MaxDeviceCount; devnum++)
            {
                var device = new PciDevice(this, devnum);
                if (device.GetFunctions().Count > 0)
                {
                    Devices[device.DeviceNumber] = device;
                }
            }
        }

        public IReadOnlyCollection<PciDevice> GetDevices()
        {
            return Devices.Values;
        }

        public PciDevice FindDevice(uint devnum)
        {
            if (Devices.ContainsKey(devnum))
            {
                return Devices[devnum];
            }

            return null;
        }
    }
}
