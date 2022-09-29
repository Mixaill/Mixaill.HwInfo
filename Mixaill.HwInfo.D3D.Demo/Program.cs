// Copyright 2021, Mikhail Paulyshka
// SPDX-License-Identifier: MIT

using System;
using System.Reflection;

using Microsoft.Extensions.Logging;
using Mixaill.HwInfo.D3D.Demo;

namespace Mixaill.HwInfo.D3D.Demo
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

            DemoDXGI.Do(loggerFactory);
            DemoKMT.Do(loggerFactory);
        }
    }
}
