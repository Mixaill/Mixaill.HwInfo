// Copyright 2022, Mikhail Paulyshka
// SPDX-License-Identifier: MIT

using System;

using Microsoft.Extensions.Logging;

namespace Mixaill.HwInfo.D3D.Demo
{
    internal class DemoDXGI
    {
        public static void Do(ILoggerFactory loggerFactory)
        {
            var dxgi = new DxgiFactory(loggerFactory.CreateLogger<DxgiFactory>());
            var adapters = dxgi.GetAdapters();

            Console.WriteLine("==== DXGI =====");

            foreach (var adapter in adapters)
            {
                Console.WriteLine("Adapter:");
                Console.WriteLine($"    - desc");
                Console.WriteLine($"       - description             : {adapter.Description}");
                Console.WriteLine($"       - vendor ID               : 0x{adapter.VendorId:X4}");
                Console.WriteLine($"       - device ID               : 0x{adapter.DeviceId:X4}");
                Console.WriteLine($"       - subsystem ID            : 0x{adapter.SubsystemId:X4}");
                Console.WriteLine($"       - revision                : 0x{adapter.Revision:X4}");
                Console.WriteLine($"       - memory/dedicated video  : {adapter.DedicatedVideoMemory / 1024 / 1024} MiB");
                Console.WriteLine($"       - memory/dedicated system : {adapter.DedicatedSystemMemory / 1024 / 1024} MiB");
                Console.WriteLine($"       - memory/shared system    : {adapter.SharedSystemMemory / 1024 / 1024} MiB");
                Console.WriteLine($"       - luid                    : {adapter.AdapterLuid.High:X8} {adapter.AdapterLuid.Low:X8}");
                Console.WriteLine($"       - flags                   : {adapter.Flags}");
                Console.WriteLine($"    - video memory info");
                Console.WriteLine($"       - local");
                Console.WriteLine($"           - budget              : {adapter.GetVideoMemoryInfo(Silk.NET.DXGI.MemorySegmentGroup.Local).Budget / 1024 / 1024} MiB");
                Console.WriteLine($"           - current usage       : {adapter.GetVideoMemoryInfo(Silk.NET.DXGI.MemorySegmentGroup.Local).CurrentUsage / 1024 / 1024} MiB");
                Console.WriteLine($"           - available for rsvr  : {adapter.GetVideoMemoryInfo(Silk.NET.DXGI.MemorySegmentGroup.Local).AvailableForReservation / 1024 / 1024} MiB");
                Console.WriteLine($"           - current reservation : {adapter.GetVideoMemoryInfo(Silk.NET.DXGI.MemorySegmentGroup.Local).CurrentReservation / 1024 / 1024} MiB");
                Console.WriteLine($"       - non-local");
                Console.WriteLine($"           - budget              : {adapter.GetVideoMemoryInfo(Silk.NET.DXGI.MemorySegmentGroup.NonLocal).Budget / 1024 / 1024} MiB");
                Console.WriteLine($"           - current usage       : {adapter.GetVideoMemoryInfo(Silk.NET.DXGI.MemorySegmentGroup.NonLocal).CurrentUsage / 1024 / 1024} MiB");
                Console.WriteLine($"           - available for rsvr  : {adapter.GetVideoMemoryInfo(Silk.NET.DXGI.MemorySegmentGroup.NonLocal).AvailableForReservation / 1024 / 1024} MiB");
                Console.WriteLine($"           - current reservation : {adapter.GetVideoMemoryInfo(Silk.NET.DXGI.MemorySegmentGroup.NonLocal).CurrentReservation / 1024 / 1024} MiB");
                Console.WriteLine("");

            }

            Console.WriteLine("\n\n\n");
        }
    }
}
