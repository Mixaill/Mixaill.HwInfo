// Copyright 2021, Mikhail Paulyshka
// SPDX-License-Identifier: MIT

using System;
using System.Reflection;

using Microsoft.Extensions.Logging;

using Mixaill.HwInfo.SetupApi.Defines;

namespace Mixaill.HwInfo.SetupApi.Demo
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

            var deviceInfoSet = new DeviceInfoSet(DeviceClassGuid.Display, loggerFactory.CreateLogger<DeviceInfoSet>());
            
            foreach(var deviceInfo in deviceInfoSet.Devices)
            {
                logger.LogInformation($"Device_Description        : {deviceInfo.DeviceDescription}");
                logger.LogInformation($"Device_Manufacturer       : {deviceInfo.DeviceManufacturer}");
                logger.LogInformation($"Device_HardwareIds        v");
                foreach (var hardwareId in deviceInfo.DeviceHardwareIds)
                {
                    logger.LogInformation($"                          > {hardwareId}");
                }
                logger.LogInformation("");

                logger.LogInformation($"Device_Location           : {deviceInfo.DeviceLocationInfo}");
                logger.LogInformation($"Device_LocationPaths      v");
                foreach (var locationPath in deviceInfo.DeviceLocationPaths)
                {
                    logger.LogInformation($"                          > {locationPath}");
                }
                logger.LogInformation("");

                logger.LogInformation($"DeviceInstance_Id         : {deviceInfo.DeviceInstanceId}");
                logger.LogInformation("");


                logger.LogInformation($"Driver_Version            : {deviceInfo.DriverVersion}");
                logger.LogInformation("");



                var deviceInfoPci = deviceInfo as DeviceInfoPci;
                if (deviceInfoPci != null)
                {
                    logger.LogInformation($"Pci_LinkSpeed_Current     : {deviceInfoPci.PciLinkSpeedCurrent}");
                    logger.LogInformation($"Pci_LinkSpeed_Max         : {deviceInfoPci.PciLinkSpeedMax}");

                    logger.LogInformation($"Pci_LinkWidth_Current     : {deviceInfoPci.PciLinkWidthCurrent}");
                    logger.LogInformation($"Pci_LinkWidth_Max         : {deviceInfoPci.PciLinkWidthMax}");

                    logger.LogInformation($"Pci_BarTypes              : 0x{deviceInfoPci.PciBarTypes:X8}");
                    logger.LogInformation($"   - IO                   > {deviceInfoPci.PciBarTypes_Io}");
                    logger.LogInformation($"   - Mem_Prefetch_disable > {deviceInfoPci.PciBarTypes_NonPrefetchable}");
                    logger.LogInformation($"   - Mem_Prefetch_32bit   > {deviceInfoPci.PciBarTypes_32BitPrefetchable}");
                    logger.LogInformation($"   - Mem_Prefetch_64bit   > {deviceInfoPci.PciBarTypes_64BitPrefetchable}");
                    logger.LogInformation("");

                    logger.LogInformation($"Pci_Above4G_Decoding      : {deviceInfoPci.Pci_Above4GDecoding}");
                    logger.LogInformation($"Pci_LargeMemory           : {deviceInfoPci.Pci_LargeMemory}");
                    logger.LogInformation("");
                }

                logger.LogInformation($"Device_ResourceMemory     v");
                foreach (var resource in deviceInfo.DeviceResourceMemory)
                {
                    logger.LogInformation($"                          > {resource}");
                }

                logger.LogInformation("\n---------------\n");
            }


            deviceInfoSet.Dispose();

            Console.ReadKey();
        }

    }
}
