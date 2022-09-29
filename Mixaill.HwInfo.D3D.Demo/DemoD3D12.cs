// Copyright 2022, Mikhail Paulyshka
// SPDX-License-Identifier: MIT

using System;

using Microsoft.Extensions.Logging;

namespace Mixaill.HwInfo.D3D.Demo
{
    internal class DemoD3D12
    {
        public static void Do(ILoggerFactory loggerFactory)
        {
            var dxgi = new DxgiFactory(loggerFactory.CreateLogger<DxgiFactory>());
            var d3d12 = new D3D12Factory(loggerFactory.CreateLogger<D3D12Factory>());

            Console.WriteLine("==== D3D12 =====");

            var adapters = dxgi.GetAdapters();
            foreach (var adapter in adapters)
            {
                var device = d3d12.CreateDevice(adapter);
                if (device != null)
                {

                    Console.WriteLine("Device:");
                    Console.WriteLine($"   - luid                    : {device.DeviceLuid.High:X8} {device.DeviceLuid.Low:X8}");

                    Console.WriteLine("");
                }
            }

            Console.WriteLine("\n\n\n");
        }
    }
}
