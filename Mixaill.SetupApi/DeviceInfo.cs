using Microsoft.Windows.Sdk;

using Mixaill.SetupApi.Defines;
using System.Collections.Generic;

namespace Mixaill.SetupApi
{
    public class DeviceInfo
    {
        public string DeviceDescription { get; } = null;
        public List<string> DeviceHardwareIds { get; } = null;
        public string DeviceManufacturer { get; } = null;
        public string DeviceLocationInfo { get; } = null;
        public List<string> DeviceLocationPaths { get; } = new List<string>();

        public string DeviceInstanceId { get; } = null;

        public string DriverVersion { get; } = null;

        internal DeviceInfo(DeviceInfoSet devInfoSet, SP_DEVINFO_DATA devInfo)
        {
            DeviceDescription = (devInfoSet.GetProperty(devInfo, DevicePropertyKey.Device_DeviceDesc) as DevicePropertyValueString).Value;
            DeviceHardwareIds = (devInfoSet.GetProperty(devInfo, DevicePropertyKey.Device_HardwareIds) as DevicePropertyValueStringList).Value;
            DeviceManufacturer = (devInfoSet.GetProperty(devInfo, DevicePropertyKey.Device_Manufacturer) as DevicePropertyValueString).Value;
            DeviceLocationInfo = (devInfoSet.GetProperty(devInfo, DevicePropertyKey.Device_LocationInfo) as DevicePropertyValueString).Value;
            DeviceLocationPaths= (devInfoSet.GetProperty(devInfo, DevicePropertyKey.Device_LocationPaths) as DevicePropertyValueStringList).Value;

            DeviceInstanceId = (devInfoSet.GetProperty(devInfo, DevicePropertyKey.DeviceInstance_Id) as DevicePropertyValueString).Value;

            DriverVersion = (devInfoSet.GetProperty(devInfo, DevicePropertyKey.DeviceDriver_Version) as DevicePropertyValueString).Value;
        }
    }

    public class DeviceInfoPci : DeviceInfo
    {
        public uint PciLinkSpeedCurrent { get; } = 0;
        public uint PciLinkSpeedMax { get; } = 0;

        public uint PciLinkWidthCurrent {get;} = 0;
        public uint PciLinkWidthMax { get; } = 0;

        public uint PciBarTypes { get; } = 0;
        public uint PciBarTypes_Io => (PciBarTypes >> 0) & 0xFF;
        public uint PciBarTypes_NonPrefetchable => (PciBarTypes >> 8) & 0xFF;
        public uint PciBarTypes_32BitPrefetchable => (PciBarTypes >> 16) & 0xFF;
        public uint PciBarTypes_64BitPrefetchable => (PciBarTypes >> 24) & 0xFF;

        internal DeviceInfoPci(DeviceInfoSet devInfoSet, SP_DEVINFO_DATA devInfo) : base(devInfoSet, devInfo)
        {
            PciLinkSpeedCurrent = (devInfoSet.GetProperty(devInfo, DevicePropertyKey.PciDevice_CurrentLinkSpeed) as DevicePropertyValueUInt32).Value;
            PciLinkSpeedMax = (devInfoSet.GetProperty(devInfo, DevicePropertyKey.PciDevice_MaxLinkSpeed) as DevicePropertyValueUInt32).Value;

            PciLinkWidthCurrent = (devInfoSet.GetProperty(devInfo, DevicePropertyKey.PciDevice_CurrentLinkWidth) as DevicePropertyValueUInt32).Value;
            PciLinkWidthMax = (devInfoSet.GetProperty(devInfo, DevicePropertyKey.PciDevice_MaxLinkWidth) as DevicePropertyValueUInt32).Value;
        
            PciBarTypes = (devInfoSet.GetProperty(devInfo, DevicePropertyKey.PciDevice_BarTypes) as DevicePropertyValueUInt32).Value;
        }
    }

    internal static class DeviceInfoFactory
    {
        internal static DeviceInfo Create(DeviceInfoSet devInfoSet, SP_DEVINFO_DATA devInfo)
        {
            var busType = (devInfoSet.GetProperty(devInfo, DevicePropertyKey.Device_BusTypeGuid) as DevicePropertyValueGuid).Value;
            if (busType == DeviceBusType.Pci)
            {
                return new DeviceInfoPci(devInfoSet, devInfo);
            }

            return new DeviceInfo(devInfoSet, devInfo);
        }
    }
}
