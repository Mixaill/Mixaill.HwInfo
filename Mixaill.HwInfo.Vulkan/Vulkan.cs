// Copyright 2021, Mikhail Paulyshka
// SPDX-License-Identifier: MIT

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

using Silk.NET.Core;
using Silk.NET.Vulkan;

namespace Mixaill.HwInfo.Vulkan
{
    public class Vulkan : IDisposable
    {
        #region Properties
        private string _name { get; } = Assembly.GetExecutingAssembly().GetName().Name;

        private Vk _vk { get; } = null;

        private Instance _instance;

        #endregion

        public Vulkan()
        {
            _vk = Vk.GetApi();

            instanceCreate();
        }

        #region Vulkan/Instance

        private unsafe void instanceCreate()
        {
            var appInfo = new ApplicationInfo
            {
                SType = StructureType.ApplicationInfo,
                PApplicationName = (byte*)Marshal.StringToHGlobalAnsi(_name),
                ApplicationVersion = new Version32(1, 0, 0),
                PEngineName = (byte*)Marshal.StringToHGlobalAnsi(_name),
                EngineVersion = new Version32(1, 0, 0),
                ApiVersion = Vk.Version11
            };

            var createInfo = new InstanceCreateInfo
            {
                SType = StructureType.InstanceCreateInfo,
                PApplicationInfo = &appInfo,
                EnabledExtensionCount = 0,
                PpEnabledExtensionNames = null,
                EnabledLayerCount = 0,
                PNext = null
            };

            fixed (Instance* instance = &_instance)
            {
                if (_vk.CreateInstance(&createInfo, null, instance) != Result.Success)
                {
                    throw new Exception("Failed to create instance!");
                }
            }

            _vk.CurrentInstance = _instance;

            Marshal.FreeHGlobal((IntPtr)appInfo.PApplicationName);
            Marshal.FreeHGlobal((IntPtr)appInfo.PEngineName);
        }

        private unsafe void instanceDestroy()
        {
            if (_vk != null)
            {
                _vk.DestroyInstance(_instance, null);
            }
        }

        #endregion

        #region Vulkan/EnumerateDevices

        public unsafe List<VulkanPhysicalDevice> GetPhysicalDevices()
        {
            var result = new List<VulkanPhysicalDevice>();

            uint physicalDevicesCount = 0;
            if(_vk?.EnumeratePhysicalDevices(_instance, ref physicalDevicesCount, null) == Result.Success && physicalDevicesCount > 0)
            {
                var physicalDevices = new PhysicalDevice[physicalDevicesCount];
                fixed(PhysicalDevice* p = physicalDevices)
                {
                    if(_vk?.EnumeratePhysicalDevices(_instance, ref physicalDevicesCount, p) == Result.Success)
                    {
                        foreach(var physicalDevice in physicalDevices)
                        {
                            result.Add(new VulkanPhysicalDevice(_vk, _instance, physicalDevice));
                        }
                    }
                }
            }

            return result;
        }

        #endregion

        #region IDisposable

        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                }

                instanceDestroy();
                _vk.Dispose();
                disposedValue = true;
            }
        }

        ~Vulkan()
        {
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
