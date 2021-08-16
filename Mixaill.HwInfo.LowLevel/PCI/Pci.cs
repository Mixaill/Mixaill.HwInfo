using System;
using System.Collections.Generic;
using System.Text;

namespace Mixaill.HwInfo.LowLevel.PCI
{
    public class Pci
    {
        private Dictionary<uint, PciBus> Buses { get; } = new Dictionary<uint, PciBus>();

        public Pci()
        {
            Refresh();
        }


        public void Refresh()
        {
            Buses.Clear();

            for(uint busnum = 0; busnum < PciBus.MaxBusCount; busnum++)
            {
                var bus = new PciBus(busnum);
                if(bus.GetDevices().Count > 0)
                {
                    Buses[bus.BusNumber] = bus;
                }
            }
        }

        public IReadOnlyCollection<PciBus> GetBuses()
        {
            return Buses.Values;
        }

        public void Print()
        {
            foreach (var bus in GetBuses())
            {
                Console.WriteLine($" -- BUS {bus.BusNumber}");

                foreach(var device in bus.GetDevices())
                {
                    Console.WriteLine($"    -- DEV {device.DeviceNumber}");


                    foreach (var function in device.GetFunctions())
                    {
                        Console.WriteLine($"       -- FUN {function.FunctionNumber}");

                        Console.WriteLine($"          -- Vendor = 0x{function.VendorID:X04} ({function.VendorName})");
                        Console.WriteLine($"          -- Device = 0x{function.DeviceID:X04} ({function.DeviceName})");
                        Console.WriteLine($"          -- Class  = 0x{(byte)function.ClassCode:X02} ({function.ClassCode})");

                    }
                }

                Console.WriteLine("");
            }
        }
    }
}
