using Microsoft.Windows.Sdk;

using Mixaill.SetupApi.Defines;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;

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

        public List<DeviceResourceMemory> DeviceResourceMemory { get; } = new List<DeviceResourceMemory>();

        internal DeviceInfo(DeviceInfoSet devInfoSet, SP_DEVINFO_DATA devInfo)
        {
            DeviceDescription = (devInfoSet.GetProperty(devInfo, DevicePropertyKey.Device_DeviceDesc) as DevicePropertyValueString).Value;
            DeviceHardwareIds = (devInfoSet.GetProperty(devInfo, DevicePropertyKey.Device_HardwareIds) as DevicePropertyValueStringList).Value;
            DeviceManufacturer = (devInfoSet.GetProperty(devInfo, DevicePropertyKey.Device_Manufacturer) as DevicePropertyValueString).Value;
            DeviceLocationInfo = (devInfoSet.GetProperty(devInfo, DevicePropertyKey.Device_LocationInfo) as DevicePropertyValueString).Value;
            DeviceLocationPaths= (devInfoSet.GetProperty(devInfo, DevicePropertyKey.Device_LocationPaths) as DevicePropertyValueStringList).Value;

            DeviceInstanceId = (devInfoSet.GetProperty(devInfo, DevicePropertyKey.DeviceInstance_Id) as DevicePropertyValueString).Value;

            DriverVersion = (devInfoSet.GetProperty(devInfo, DevicePropertyKey.DeviceDriver_Version) as DevicePropertyValueString).Value;

            DeviceResourceMemory = getMemoryResources(devInfo);
        }

        private List<DeviceResourceMemory> getMemoryResources(SP_DEVINFO_DATA devInfo)
        {
            var result = new List<DeviceResourceMemory>();
           
            foreach (var res in getResources<MEM_RESOURCE>(devInfo))
            {
                result.Add(new DeviceResourceMemory(DeviceResourceType.Mem, res.MEM_Header.MD_Alloc_Base, res.MEM_Header.MD_Alloc_End));
            }
           
            foreach (var res in getResources<Mem_Large_Resource_s>(devInfo))
            {
                result.Add(new DeviceResourceMemory(DeviceResourceType.MemLarge, res.MEM_LARGE_Header.MLD_Alloc_Base, res.MEM_LARGE_Header.MLD_Alloc_End));
            }

            return result;
        }

        private unsafe List<T> getResources<T>(SP_DEVINFO_DATA devInfo) where T: struct
        {
            var result = new List<T>();

            UIntPtr logicalConfiguration;
            UIntPtr resourceDescriptor;
            var resourceType = typeof(T) == typeof(MEM_RESOURCE) ? DeviceResourceType.Mem : DeviceResourceType.MemLarge;

            if (PInvoke.CM_Get_First_Log_Conf(&logicalConfiguration, devInfo.DevInst, Constants.ALLOC_LOG_CONF) != CONFIGRET.CR_SUCCESS)
            {
                throw new Win32Exception("Failed to get first logical configuration");
            }

            var cmResult = PInvoke.CM_Get_Next_Res_Des(out resourceDescriptor, logicalConfiguration, (uint)resourceType, null, 0);
            while (resourceDescriptor != null)
            {
                uint dataSize = 0;
                if(PInvoke.CM_Get_Res_Des_Data_Size(out dataSize, resourceDescriptor, 0) != CONFIGRET.CR_SUCCESS)
                {
                    break;
                }


                T res;
                fixed (void* p = new byte[dataSize])
                {
                    if (PInvoke.CM_Get_Res_Des_Data(resourceDescriptor, p, dataSize, 0) != CONFIGRET.CR_SUCCESS)
                    {
                        break;
                    }

                    res = (T)Marshal.PtrToStructure((IntPtr)p, typeof(T));
                }
                result.Add(res);

                UIntPtr nextResourceDescriptor;
                if(PInvoke.CM_Get_Next_Res_Des(out nextResourceDescriptor, resourceDescriptor, (uint)resourceType, null, 0) != CONFIGRET.CR_SUCCESS)
                {
                    break;
                }

                PInvoke.CM_Free_Res_Des_Handle(resourceDescriptor);
                resourceDescriptor = nextResourceDescriptor;
            }

            PInvoke.CM_Free_Res_Des_Handle(resourceDescriptor);
            PInvoke.CM_Free_Log_Conf(logicalConfiguration, 0);

            return result;
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

        public bool Pci_Above4GDecoding => DeviceResourceMemory.Any(x => x.Above4g);

        public bool Pci_LargeMemory => DeviceResourceMemory.Any(x => x.ResType == DeviceResourceType.MemLarge);


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
