// Copyright 2021, Mikhail Paulyshka
// SPDX-License-Identifier: MIT

using System;
using System.Reflection;

using Microsoft.Extensions.Logging;

namespace Mixaill.HwInfo.D3DKMT.Demo
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

            var kmt = new Kmt(loggerFactory);
            foreach(var adapter in kmt.GetAdapters())
            {
                if(adapter.DeviceIds.VendorID == 0x1414) //skip microsoft
                {
                    adapter.Dispose();
                    continue;
                }

                logger.LogInformation("RegistryInfo:");
                logger.LogInformation($"   - adapter string : {adapter.AdapterRegistryInfo.AdapterString}");
                logger.LogInformation($"   - bios string    : {adapter.AdapterRegistryInfo.BiosString}");
                logger.LogInformation($"   - dac type       : {adapter.AdapterRegistryInfo.DacType}");
                logger.LogInformation($"   - chip type      : {adapter.AdapterRegistryInfo.ChipType}");
                logger.LogInformation("");

                logger.LogInformation("Device Ids:");
                logger.LogInformation($"   - adapter idx    : {adapter.PhysicalAdapterIndex}");
                logger.LogInformation($"   - vendor id      : 0x{adapter.DeviceIds.VendorID:X4}");
                logger.LogInformation($"   - device id      : 0x{adapter.DeviceIds.DeviceID:X4}");
                logger.LogInformation($"   - subvendor id   : 0x{adapter.DeviceIds.SubVendorID:X4}");
                logger.LogInformation($"   - subsystem id   : 0x{adapter.DeviceIds.SubSystemID:X4}");
                logger.LogInformation($"   - revision id    : 0x{adapter.DeviceIds.RevisionID:X4}");
                logger.LogInformation($"   - bus type       : 0x{adapter.DeviceIds.BusType}");
                logger.LogInformation("");

                logger.LogInformation("WDDM 2.7 Capabilities:");
                logger.LogInformation($"   - HAGS supported : {adapter.WddmCapabilities_27.HagsSupported}");
                logger.LogInformation($"   - HAGS enabled   : {adapter.WddmCapabilities_27.HagsEnabled}");
                logger.LogInformation("");

                adapter.Dispose();
            }

            Console.ReadKey();
        }
    }
}
