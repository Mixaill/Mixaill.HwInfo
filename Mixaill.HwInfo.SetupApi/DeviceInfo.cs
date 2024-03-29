﻿// Copyright 2021, Mikhail Paulyshka
// SPDX-License-Identifier: MIT

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;

using Microsoft.Windows.Sdk;

using Mixaill.HwInfo.SetupApi.Defines;

namespace Mixaill.HwInfo.SetupApi
{
    public class DeviceInfo
    {
        public string DeviceDescription { get; } = "";
        public List<string> DeviceHardwareIds { get; } = new List<string>();
        public string DeviceManufacturer { get; } = "";
        public string DeviceLocationInfo { get; } = "";
        public List<string> DeviceLocationPaths { get; } = new List<string>();

        public string DeviceInstanceId { get; } = "";

        public string DriverVersion { get; } = "";

        public List<DeviceResourceMemory> DeviceResourceMemory { get; } = new List<DeviceResourceMemory>();

        internal DeviceInfo(DeviceInfoSet devInfoSet, SP_DEVINFO_DATA devInfo)
        {
            DeviceDescription = (devInfoSet.GetProperty(devInfo, DevicePropertyKey.Device_DeviceDesc) as DevicePropertyValueString)?.Value;

            var deviceHardwareIds = (devInfoSet.GetProperty(devInfo, DevicePropertyKey.Device_HardwareIds) as DevicePropertyValueStringList)?.Value;
            if(deviceHardwareIds != null)
            {
                DeviceHardwareIds = deviceHardwareIds;
            }

            DeviceManufacturer = (devInfoSet.GetProperty(devInfo, DevicePropertyKey.Device_Manufacturer) as DevicePropertyValueString)?.Value;
            DeviceLocationInfo = (devInfoSet.GetProperty(devInfo, DevicePropertyKey.Device_LocationInfo) as DevicePropertyValueString)?.Value;

            var deviceLocationPaths = (devInfoSet.GetProperty(devInfo, DevicePropertyKey.Device_LocationPaths) as DevicePropertyValueStringList)?.Value;
            if (deviceLocationPaths != null)
            {
                DeviceLocationPaths = deviceLocationPaths;
            }

            DeviceInstanceId = (devInfoSet.GetProperty(devInfo, DevicePropertyKey.DeviceInstance_Id) as DevicePropertyValueString)?.Value;

            DriverVersion = (devInfoSet.GetProperty(devInfo, DevicePropertyKey.DeviceDriver_Version) as DevicePropertyValueString)?.Value;

            DeviceResourceMemory = getMemoryResources(devInfo);
        }

        private List<DeviceResourceMemory> getMemoryResources(SP_DEVINFO_DATA devInfo)
        {
            var result = new List<DeviceResourceMemory>();

            var memoryRes = getResources<MEM_RESOURCE>(devInfo, Constants.ALLOC_LOG_CONF);
            if(memoryRes.Count == 0)
            {
                memoryRes = getResources<MEM_RESOURCE>(devInfo, Constants.BOOT_LOG_CONF);
            }

            var largeMemoryRes = getResources<Mem_Large_Resource_s>(devInfo, Constants.ALLOC_LOG_CONF);
            if (largeMemoryRes.Count == 0)
            {
                largeMemoryRes = getResources<Mem_Large_Resource_s>(devInfo, Constants.BOOT_LOG_CONF);
            }

            foreach (var res in memoryRes)
            {
                result.Add(new DeviceResourceMemory(DeviceResourceType.Mem, res.MEM_Header.MD_Alloc_Base, res.MEM_Header.MD_Alloc_End));
            }
            foreach (var res in largeMemoryRes)
            {
                result.Add(new DeviceResourceMemory(DeviceResourceType.MemLarge, res.MEM_LARGE_Header.MLD_Alloc_Base, res.MEM_LARGE_Header.MLD_Alloc_End));
            }

            return result;
        }

        private unsafe List<T> getResources<T>(SP_DEVINFO_DATA devInfo, uint logConfFlags) where T: struct
        {
            var resources = new List<T>();

            UIntPtr logicalConfiguration;
            UIntPtr resourceDescriptor;
            var resourceType = typeof(T) == typeof(MEM_RESOURCE) ? DeviceResourceType.Mem : DeviceResourceType.MemLarge;

            var result = PInvoke.CM_Get_First_Log_Conf(&logicalConfiguration, devInfo.DevInst, logConfFlags);
            if (result == CONFIGRET.CR_SUCCESS)
            {
                var cmResult = PInvoke.CM_Get_Next_Res_Des(out resourceDescriptor, logicalConfiguration, (uint)resourceType, null, 0);
                while (resourceDescriptor != null)
                {
                    uint dataSize = 0;
                    if (PInvoke.CM_Get_Res_Des_Data_Size(out dataSize, resourceDescriptor, 0) != CONFIGRET.CR_SUCCESS)
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
                    resources.Add(res);

                    UIntPtr nextResourceDescriptor;
                    result = PInvoke.CM_Get_Next_Res_Des(out nextResourceDescriptor, resourceDescriptor, (uint)resourceType, null, 0);
                    if (result == CONFIGRET.CR_SUCCESS)
                    {
                        PInvoke.CM_Free_Res_Des_Handle(resourceDescriptor);
                        resourceDescriptor = nextResourceDescriptor;

                    }
                    else if (result == CONFIGRET.CR_NO_MORE_RES_DES)
                    {
                        break;
                    }
                    else
                    {
                        throw new Win32Exception("failed to get resource descriptor");
                    }
                }

                PInvoke.CM_Free_Res_Des_Handle(resourceDescriptor);
                PInvoke.CM_Free_Log_Conf_Handle(logicalConfiguration);
            }
            else if (result != CONFIGRET.CR_NO_MORE_LOG_CONF)
            {
                throw new Win32Exception("failed to get logical configuration");
            }

            return resources;
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
            PciLinkSpeedCurrent = (devInfoSet.GetProperty(devInfo, DevicePropertyKey.PciDevice_CurrentLinkSpeed) as DevicePropertyValueUInt32)?.Value ?? 0;
            PciLinkSpeedMax = (devInfoSet.GetProperty(devInfo, DevicePropertyKey.PciDevice_MaxLinkSpeed) as DevicePropertyValueUInt32)?.Value ?? 0;

            PciLinkWidthCurrent = (devInfoSet.GetProperty(devInfo, DevicePropertyKey.PciDevice_CurrentLinkWidth) as DevicePropertyValueUInt32)?.Value ?? 0;
            PciLinkWidthMax = (devInfoSet.GetProperty(devInfo, DevicePropertyKey.PciDevice_MaxLinkWidth) as DevicePropertyValueUInt32)?.Value ?? 0;
        
            PciBarTypes = (devInfoSet.GetProperty(devInfo, DevicePropertyKey.PciDevice_BarTypes) as DevicePropertyValueUInt32)?.Value ?? 0;
        }
    }

    internal static class DeviceInfoFactory
    {
        internal static DeviceInfo Create(DeviceInfoSet devInfoSet, SP_DEVINFO_DATA devInfo)
        {
            var busType = (devInfoSet.GetProperty(devInfo, DevicePropertyKey.Device_BusTypeGuid) as DevicePropertyValueGuid)?.Value;
            if (busType == DeviceBusType.Pci)
            {
                return new DeviceInfoPci(devInfoSet, devInfo);
            }

            return new DeviceInfo(devInfoSet, devInfo);
        }
    }
}
