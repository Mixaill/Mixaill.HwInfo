// Copyright 2021, Mikhail Paulyshka
// SPDX-License-Identifier: MIT

using System;
using System.Reflection;

namespace Mixaill.HwInfo.Vulkan.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"{Assembly.GetEntryAssembly().GetName().Name} v{Assembly.GetEntryAssembly().GetName().Version}\n");


            Vulkan vk = new Vulkan();

            var devices = vk.GetPhysicalDevices();
            Console.WriteLine($"Vulkan device count: {devices.Count}\n");

            for(int devIdx = 0; devIdx < devices.Count; devIdx++)
            {
                var dev = devices[devIdx];

                Console.WriteLine("\n------------\n");
                Console.WriteLine($"Device Index      : {devIdx}");
                Console.WriteLine($"Device Name       : {dev.DeviceName}");
                Console.WriteLine($"Device Type       : {dev.DeviceType}");
                Console.WriteLine("");

                Console.WriteLine($"Vendor ID         : 0x{dev.VendorId:X4}");
                Console.WriteLine($"Device ID         : 0x{dev.DeviceId:X4}");
                Console.WriteLine("");

                Console.WriteLine($"Api Version       : {dev.ApiVersion}");
                Console.WriteLine($"Driver Version    : {dev.DriverVersion}");
                Console.WriteLine("");

                Console.WriteLine($"HostVisibleMemory : {dev.DeviceHostVisibleMemory/1024/1024.0} MiB");
                Console.WriteLine($"ResizableBarInUse : {dev.DeviceResizableBarInUse}");
                Console.WriteLine("");
            }


            vk.Dispose();

            Console.ReadKey();
        }
    }
}
