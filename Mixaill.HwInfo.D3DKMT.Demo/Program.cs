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

            Console.WriteLine($"{Assembly.GetEntryAssembly().GetName().Name} v{Assembly.GetEntryAssembly().GetName().Version}\n");

            var kmt = new Kmt(loggerFactory.CreateLogger<Kmt>());
            foreach(var adapter in kmt.GetAdapters())
            {
                if(adapter.AdapterType.SoftwareDevice) //skip software devices
                {
                    adapter.Dispose();
                    continue;
                }

                Console.WriteLine("ID 8, Adapter Registry Info:");
                Console.WriteLine($"   - adapter string : {adapter.AdapterRegistryInfo.AdapterString}");
                Console.WriteLine($"   - bios string    : {adapter.AdapterRegistryInfo.BiosString}");
                Console.WriteLine($"   - dac type       : {adapter.AdapterRegistryInfo.DacType}");
                Console.WriteLine($"   - chip type      : {adapter.AdapterRegistryInfo.ChipType}");
                Console.WriteLine("");

                Console.WriteLine("ID 13, Driver Version:");
                Console.WriteLine($"   - wddm version   : {adapter.DriverVersion}");
                Console.WriteLine("");

                Console.WriteLine("ID 15, Adapter Type:");
                Console.WriteLine($"   - render supp.   : {adapter.AdapterType.RenderSupported}");
                Console.WriteLine($"   - display supp.  : {adapter.AdapterType.DisplaySupported}");
                Console.WriteLine($"   - software device: {adapter.AdapterType.SoftwareDevice}");
                Console.WriteLine($"   - post device    : {adapter.AdapterType.PostDevice}");
                Console.WriteLine($"   - hybrid discrete: {adapter.AdapterType.HybridDiscrete}");
                Console.WriteLine($"   - hybrid integr. : {adapter.AdapterType.HybridIntegrated}");
                Console.WriteLine($"   - indirect displ.: {adapter.AdapterType.IndirectDisplayDevice}");
                Console.WriteLine($"   - paravirtualized: {adapter.AdapterType.Paravirtualized}");
                Console.WriteLine($"   - ACG supported  : {adapter.AdapterType.ACGSupported}");
                Console.WriteLine($"   - tmngs from vidp: {adapter.AdapterType.SupportSetTimingsFromVidPn}");
                Console.WriteLine($"   - detachable     : {adapter.AdapterType.Detachable}");
                Console.WriteLine($"   - compute only   : {adapter.AdapterType.ComputeOnly}");
                Console.WriteLine($"   - prototype      : {adapter.AdapterType.Prototype}");
                Console.WriteLine("");

                Console.WriteLine("ID 31, Physical Adapter Device IDs:");
                Console.WriteLine($"   - adapter idx    : {adapter.DeviceIds.PhysicalAdapterIndex}");
                Console.WriteLine($"   - vendor id      : 0x{adapter.DeviceIds.DeviceIds.VendorID:X4}");
                Console.WriteLine($"   - device id      : 0x{adapter.DeviceIds.DeviceIds.DeviceID:X4}");
                Console.WriteLine($"   - subvendor id   : 0x{adapter.DeviceIds.DeviceIds.SubVendorID:X4}");
                Console.WriteLine($"   - subsystem id   : 0x{adapter.DeviceIds.DeviceIds.SubSystemID:X4}");
                Console.WriteLine($"   - revision id    : 0x{adapter.DeviceIds.DeviceIds.RevisionID:X4}");
                Console.WriteLine($"   - bus type       : 0x{adapter.DeviceIds.DeviceIds.BusType}");
                Console.WriteLine("");

                var perfdata = adapter.GetPerformanceData().Item2;
                Console.WriteLine("ID 62, Adapter Performance Data:");
                Console.WriteLine($"   - adapter idx    : {perfdata.PhysicalAdapterIndex}");
                Console.WriteLine($"   - mem. freq.     : {perfdata.MemoryFrequency} Hz");
                Console.WriteLine($"   - mem. freq. max : {perfdata.MaxMemoryFrequency} Hz");
                Console.WriteLine($"   - mem. freq. OC  : {perfdata.MaxMemoryFrequencyOC} Hz");
                Console.WriteLine($"   - mem. bandwidth : {perfdata.MemoryBandwidth} Bytes");
                Console.WriteLine($"   - pcie bandwidth : {perfdata.PCIEBandwidth} Bytes");
                Console.WriteLine($"   - fan speed      : {perfdata.FanRPM} RPM");
                Console.WriteLine($"   - power draw     : {perfdata.Power/10.0} %");
                Console.WriteLine($"   - temperature    : {perfdata.Temperature / 10.0} *C");
                Console.WriteLine($"   - power state ovr: {perfdata.PowerStateOverride}");
                Console.WriteLine("");

                Console.WriteLine("ID 63, Adapter Performance Data Capabilities:");
                Console.WriteLine($"   - adapter idx    : {adapter.PerformanceDataCapabilities.PhysicalAdapterIndex}");
                Console.WriteLine($"   - max mem bandwth: {adapter.PerformanceDataCapabilities.MaxMemoryBandwidth} Bytes");
                Console.WriteLine($"   - max pci bandwth: {adapter.PerformanceDataCapabilities.MaxPCIEBandwidth} Bytes");
                Console.WriteLine($"   - max fan speed  : {adapter.PerformanceDataCapabilities.MaxFanRPM} RPM");
                Console.WriteLine($"   - temp max       : {adapter.PerformanceDataCapabilities.TemperatureMax} *C");
                Console.WriteLine($"   - temp warning   : {adapter.PerformanceDataCapabilities.TemperatureWarning} *C");
                Console.WriteLine("");

                Console.WriteLine("ID 70, WDDM 2.7 Capabilities:");
                Console.WriteLine($"   - HAGS supported : {adapter.WddmCapabilities_27.HagsSupported}");
                Console.WriteLine($"   - HAGS enabled   : {adapter.WddmCapabilities_27.HagsEnabled}");
                Console.WriteLine("");




                adapter.Dispose();
            }

            Console.ReadKey();
        }
    }
}
