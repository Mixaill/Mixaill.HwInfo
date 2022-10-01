// Copyright 2021, Mikhail Paulyshka
// SPDX-License-Identifier: MIT

using System;
using System.Runtime.InteropServices;

using Microsoft.Extensions.Logging;

using Silk.NET.Vulkan;

namespace Mixaill.HwInfo.Vulkan
{
    public class VulkanPhysicalDevice
    {
        #region Properties/Public

        public Version ApiVersion { get; private set; } = new Version(0,0,0);

        public Version DriverVersion { get; private set; } = new Version(0, 0, 0);

        public uint DeviceId { get; private set; } = 0;

        public string DeviceName { get; private set; } = "";

        public PhysicalDeviceType DeviceType { get; private set; } = PhysicalDeviceType.Other;

        public uint VendorId { get; private set; } = 0;

        public ulong DeviceHostVisibleMemory => getHostVisibleMemory();

        public bool DeviceResizableBarInUse => DeviceHostVisibleMemory > (256 * 1024 * 1024);

        #endregion

        #region Properties/Private

        private Vk _vk { get; set; } = null;

        private Instance _vk_instance;

        private PhysicalDevice _vk_physicalDevice;

        private ILogger _logger;

        #endregion

        public VulkanPhysicalDevice(Vk vk, Instance instance, PhysicalDevice physicalDevice, ILogger logger)
        {
            _logger = logger;
            init(vk, instance, physicalDevice);
        }

        public VulkanPhysicalDevice(Vk vk, Instance instance, PhysicalDevice physicalDevice, ILogger<VulkanPhysicalDevice> logger)
        {
            _logger = logger;
            init(vk, instance, physicalDevice);
        }

        private void init(Vk vk, Instance instance, PhysicalDevice physicalDevice)
        {
            _vk = vk;
            _vk_instance = instance;
            _vk_physicalDevice = physicalDevice;

            getProperties();
            getHostVisibleMemory();
        }

        private unsafe void getProperties()
        {
            PhysicalDeviceProperties deviceProperties;
            _vk.GetPhysicalDeviceProperties(_vk_physicalDevice, out deviceProperties);


            DeviceId = deviceProperties.DeviceID;
            DeviceName = Marshal.PtrToStringAnsi((IntPtr)deviceProperties.DeviceName);
            DeviceType = deviceProperties.DeviceType;
            VendorId = deviceProperties.VendorID;


            ApiVersion = new Version(
                (int)((deviceProperties.ApiVersion >> 22) & 0x7FU),
                (int)((deviceProperties.ApiVersion >> 12) & 0x3FFU),
                (int)((deviceProperties.ApiVersion >> 0) & 0xFFFU)
            );

            var driverVerVariant = (int)((deviceProperties.DriverVersion >> 29));
            var driverVerMajor = (int)((deviceProperties.DriverVersion >> 22) & 0x7FU);
            var driverVerMinor = (int)((deviceProperties.DriverVersion >> 12) & 0x3FFU);
            var driverVerPatch = (int)((deviceProperties.DriverVersion >> 0) & 0xFFFU);

            if(driverVerVariant != 0)
            {
                DriverVersion = new Version(driverVerVariant, driverVerMajor, driverVerMinor, driverVerPatch);
            }
            else
            {
                DriverVersion = new Version(driverVerMajor, driverVerMinor, driverVerPatch);
            }
        }

        /// <summary>
        /// Returns size of device host visible memory
        /// </summary>
        /// <returns>size of visible memory in bytes</returns>
        private ulong getHostVisibleMemory()
        {
            ulong hostVisibleMemory = 0;

            PhysicalDeviceMemoryProperties memoryProperties;
            _vk.GetPhysicalDeviceMemoryProperties(_vk_physicalDevice, out memoryProperties);

            var searchMask = MemoryPropertyFlags.DeviceLocalBit | MemoryPropertyFlags.HostVisibleBit | MemoryPropertyFlags.HostCoherentBit;
            for (int typeIdx = 0; typeIdx<memoryProperties.MemoryTypeCount; typeIdx++)
            {
                var memoryType = memoryProperties.MemoryTypes[typeIdx];
                if((memoryType.PropertyFlags & searchMask) == searchMask)
                {
                    hostVisibleMemory = Math.Max(hostVisibleMemory, memoryProperties.MemoryHeaps[(int)memoryType.HeapIndex].Size);
                }
            }

            return hostVisibleMemory;
        }
    }
}
