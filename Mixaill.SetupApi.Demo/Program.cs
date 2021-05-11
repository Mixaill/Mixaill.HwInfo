using System;

using Mixaill.SetupApi.Defines;

namespace Mixaill.SetupApi.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            var deviceInfoSet = new DeviceInfoSet(DeviceClassGuid.Display);
            
            foreach(var deviceInfo in deviceInfoSet.Devices)
            {
                Console.WriteLine($"Device_Description        : {deviceInfo.DeviceDescription}");
                Console.WriteLine($"Device_Manufacturer       : {deviceInfo.DeviceManufacturer}");
                Console.WriteLine($"Device_HardwareIds        v");
                foreach (var hardwareId in deviceInfo.DeviceHardwareIds)
                {
                    Console.WriteLine($"                          > {hardwareId}");
                }
                Console.WriteLine("");

                Console.WriteLine($"Device_Location           : {deviceInfo.DeviceLocationInfo}");
                Console.WriteLine($"Device_LocationPaths      v");
                foreach (var locationPath in deviceInfo.DeviceLocationPaths)
                {
                    Console.WriteLine($"                          > {locationPath}");
                }
                Console.WriteLine("");

                Console.WriteLine($"DeviceInstance_Id         : {deviceInfo.DeviceInstanceId}");
                Console.WriteLine("");


                Console.WriteLine($"Driver_Version            : {deviceInfo.DriverVersion}");
                Console.WriteLine("");



                var deviceInfoPci = deviceInfo as DeviceInfoPci;
                if(deviceInfoPci != null)
                {
                    Console.WriteLine($"Pci_LinkSpeed_Current     : {deviceInfoPci.PciLinkSpeedCurrent}");
                    Console.WriteLine($"Pci_LinkSpeed_Max         : {deviceInfoPci.PciLinkSpeedMax}");

                    Console.WriteLine($"Pci_LinkWidth_Current     : {deviceInfoPci.PciLinkWidthCurrent}");
                    Console.WriteLine($"Pci_LinkWidth_Max         : {deviceInfoPci.PciLinkWidthMax}");

                    Console.WriteLine($"Pci_BarTypes              : 0x{deviceInfoPci.PciBarTypes:X8}");
                    Console.WriteLine($"   - IO                   > {deviceInfoPci.PciBarTypes_Io}");
                    Console.WriteLine($"   - Mem_Prefetch_disable > {deviceInfoPci.PciBarTypes_NonPrefetchable}");
                    Console.WriteLine($"   - Mem_Prefetch_32bit   > {deviceInfoPci.PciBarTypes_32BitPrefetchable}");
                    Console.WriteLine($"   - Mem_Prefetch_64bit   > {deviceInfoPci.PciBarTypes_64BitPrefetchable} <-------- Re-sizable BAR");
                }

                Console.WriteLine("\n---------------\n");
            }

            deviceInfoSet.Dispose();

            Console.ReadKey();
        }

    }
}
