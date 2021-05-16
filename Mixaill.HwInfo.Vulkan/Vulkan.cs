// Copyright 2021, Mikhail Paulyshka
// SPDX-License-Identifier: MIT

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

using Microsoft.Extensions.Logging;

using Silk.NET.Core;
using Silk.NET.Vulkan;

namespace Mixaill.HwInfo.Vulkan
{
    public class Vulkan : IDisposable
    {
        #region Properties

        public bool Initialized { get; private set; } = false;

        private readonly ILogger _logger = null;

        private Vk _vk { get; set;  } = null;

        private Instance _vk_instance;

        #endregion

        public Vulkan()
        {
            init();
        }

        public Vulkan(ILogger logger)
        {
            _logger = logger;
            init();
        }

        public Vulkan(ILogger<Vulkan> logger)
        {
            _logger = logger;
            init();
        }

        #region Vulkan/Instance

        private void init()
        {
            try
            {
                _vk = Vk.GetApi();
                instanceCreate();
            }
            catch (FileNotFoundException)
            {
                _logger.LogWarning("vulkan library was not found");
                return;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "unknown error");
                return;
            }

            Initialized = true;
        }

        private unsafe void instanceCreate()
        {
            var appInfo = new ApplicationInfo
            {
                SType = StructureType.ApplicationInfo,
                PApplicationName = (byte*)Marshal.StringToHGlobalAnsi(Assembly.GetExecutingAssembly().GetName().Name),
                ApplicationVersion = new Version32(1, 0, 0),
                PEngineName = (byte*)Marshal.StringToHGlobalAnsi(Assembly.GetExecutingAssembly().GetName().Name),
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

            fixed (Instance* instance = &_vk_instance)
            {
                if (_vk.CreateInstance(&createInfo, null, instance) != Result.Success)
                {
                    throw new Exception("Failed to create instance!");
                }
            }

            _vk.CurrentInstance = _vk_instance;

            Marshal.FreeHGlobal((IntPtr)appInfo.PApplicationName);
            Marshal.FreeHGlobal((IntPtr)appInfo.PEngineName);
        }

        private unsafe void instanceDestroy()
        {
            _vk?.DestroyInstance(_vk_instance, null);
        }

        #endregion

        #region Vulkan/EnumerateDevices

        public unsafe List<VulkanPhysicalDevice> GetPhysicalDevices()
        {
            var result = new List<VulkanPhysicalDevice>();

            if (Initialized)
            {
                uint physicalDevicesCount = 0;
                if (_vk?.EnumeratePhysicalDevices(_vk_instance, ref physicalDevicesCount, null) == Result.Success && physicalDevicesCount > 0)
                {
                    var physicalDevices = new PhysicalDevice[physicalDevicesCount];
                    fixed (PhysicalDevice* p = physicalDevices)
                    {
                        if (_vk?.EnumeratePhysicalDevices(_vk_instance, ref physicalDevicesCount, p) == Result.Success)
                        {
                            foreach (var physicalDevice in physicalDevices)
                            {
                                result.Add(new VulkanPhysicalDevice(_vk, _vk_instance, physicalDevice, _logger));
                            }
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
                _vk?.Dispose();
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
