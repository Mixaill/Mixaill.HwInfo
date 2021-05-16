// Copyright 2021, Mikhail Paulyshka
// SPDX-License-Identifier: MIT

using System;
using System.Reflection;

using Microsoft.Extensions.Logging;

namespace Mixaill.HwInfo.Vulkan.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            using var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                    .AddSimpleConsole(options =>
                    {
                        options.IncludeScopes = true;
                        options.SingleLine = true;
                    });
            });

            var logger = loggerFactory.CreateLogger<Program>();

            logger.LogInformation($"{Assembly.GetEntryAssembly().GetName().Name} v{Assembly.GetEntryAssembly().GetName().Version}\n");


            Vulkan vk = new Vulkan(loggerFactory.CreateLogger<Vulkan>());

            var devices = vk.GetPhysicalDevices();
            logger.LogInformation($"Vulkan device count: {devices.Count}\n");

            for(int devIdx = 0; devIdx < devices.Count; devIdx++)
            {
                var dev = devices[devIdx];

                logger.LogInformation("\n------------\n");
                logger.LogInformation($"Device Index      : {devIdx}");
                logger.LogInformation($"Device Name       : {dev.DeviceName}");
                logger.LogInformation($"Device Type       : {dev.DeviceType}");
                logger.LogInformation("");

                logger.LogInformation($"Vendor ID         : 0x{dev.VendorId:X4}");
                logger.LogInformation($"Device ID         : 0x{dev.DeviceId:X4}");
                logger.LogInformation("");

                logger.LogInformation($"Api Version       : {dev.ApiVersion}");
                logger.LogInformation($"Driver Version    : {dev.DriverVersion}");
                logger.LogInformation("");

                logger.LogInformation($"HostVisibleMemory : {dev.DeviceHostVisibleMemory/1024/1024.0} MiB");
                logger.LogInformation($"ResizableBarInUse : {dev.DeviceResizableBarInUse}");
                logger.LogInformation("");
            }


            vk.Dispose();

            Console.ReadKey();
        }
    }
}
