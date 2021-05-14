// Copyright 2021, Mikhail Paulyshka
// SPDX-License-Identifier: MIT

using System;
using System.Reflection;

namespace Mixaill.HwInfo.D3DKMT.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"{Assembly.GetEntryAssembly().GetName().Name} v{Assembly.GetEntryAssembly().GetName().Version}\n");

            var kmt = new Kmt();
            foreach(var adapter in kmt.GetAdapters())
            {
                if(adapter.DeviceIds.VendorID == 0x1414) //skip microsoft
                {
                    adapter.Dispose();
                    continue;
                }

                Console.WriteLine("RegistryInfo:");
                Console.WriteLine($"   - adapter string : {adapter.AdapterRegistryInfo.AdapterString}");
                Console.WriteLine($"   - bios string    : {adapter.AdapterRegistryInfo.BiosString}");
                Console.WriteLine($"   - dac type       : {adapter.AdapterRegistryInfo.DacType}");
                Console.WriteLine($"   - chip type      : {adapter.AdapterRegistryInfo.ChipType}");
                Console.WriteLine("");

                Console.WriteLine("Device Ids:");
                Console.WriteLine($"   - adapter idx    : {adapter.PhysicalAdapterIndex}");
                Console.WriteLine($"   - vendor id      : 0x{adapter.DeviceIds.VendorID:X4}");
                Console.WriteLine($"   - device id      : 0x{adapter.DeviceIds.DeviceID:X4}");
                Console.WriteLine($"   - subvendor id   : 0x{adapter.DeviceIds.SubVendorID:X4}");
                Console.WriteLine($"   - subsystem id   : 0x{adapter.DeviceIds.SubSystemID:X4}");
                Console.WriteLine($"   - revision id    : 0x{adapter.DeviceIds.RevisionID:X4}");
                Console.WriteLine($"   - bus type       : 0x{adapter.DeviceIds.BusType}");
                Console.WriteLine("");

                Console.WriteLine("WDDM 2.7 Capabilities:");
                Console.WriteLine($"   - HAGS supported : {adapter.WddmCapabilities_27.HagsSupported}");
                Console.WriteLine($"   - HAGS enabled   : {adapter.WddmCapabilities_27.HagsEnabled}");
                Console.WriteLine("");

                adapter.Dispose();
            }

            Console.ReadKey();
        }
    }
}
