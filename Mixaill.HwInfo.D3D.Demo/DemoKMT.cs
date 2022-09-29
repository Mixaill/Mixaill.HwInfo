using System;

using Microsoft.Extensions.Logging;

namespace Mixaill.HwInfo.D3D.Demo
{
    internal class DemoKMT
    {
        public static void Do(ILoggerFactory loggerFactory)
        {

            Console.WriteLine("==== D3DKMT =====");

            var kmt = new Kmt(loggerFactory.CreateLogger<Kmt>());
            foreach (var adapter in kmt.GetAdapters())
            {
                if (adapter.AdapterType.SoftwareDevice) //skip software devices
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
                Console.WriteLine($"   - rt power mngmt : {adapter.AdapterType.RuntimePowerManagement}");
                Console.WriteLine("");

                Console.WriteLine("ID 24, WDDM 2.0 Capabilities:");
                Console.WriteLine($"   - 64-bit atomics  : {adapter.WddmCapabilities_20.Atomics64Bit}");
                Console.WriteLine($"   - GPU MMU         : {adapter.WddmCapabilities_20.GpuMmu}");
                Console.WriteLine($"   - Io MMU          : {adapter.WddmCapabilities_20.IoMmu}");
                Console.WriteLine($"   - flip overwrite  : {adapter.WddmCapabilities_20.FlipOverwrite}");
                Console.WriteLine($"   - ctxless present : {adapter.WddmCapabilities_20.ContextlessPresent}");
                Console.WriteLine($"   - surprise removal: {adapter.WddmCapabilities_20.SurpriseRemoval}");
                Console.WriteLine("");

                Console.WriteLine("ID 26, Content Protection Driver Name:");
                Console.WriteLine($"   - cp driver name  : {adapter.ContentProtectionDriverName.ContentProtectionFileName}");
                Console.WriteLine("");

                Console.WriteLine("ID 31, Physical Adapter Device IDs:");
                Console.WriteLine($"   - vendor id      : 0x{adapter.DeviceIds.VendorID:X4}");
                Console.WriteLine($"   - device id      : 0x{adapter.DeviceIds.DeviceID:X4}");
                Console.WriteLine($"   - subvendor id   : 0x{adapter.DeviceIds.SubVendorID:X4}");
                Console.WriteLine($"   - subsystem id   : 0x{adapter.DeviceIds.SubSystemID:X4}");
                Console.WriteLine($"   - revision id    : 0x{adapter.DeviceIds.RevisionID:X4}");
                Console.WriteLine($"   - bus type       : 0x{adapter.DeviceIds.BusType}");
                Console.WriteLine("");

                Console.WriteLine("ID 34, GPU MMU Capabilities:");
                Console.WriteLine($"   - VA bit count   : {adapter.GpuMmuCapabilities.VirtualAddressBitCount}");
                Console.WriteLine($"   - r/o            : {adapter.GpuMmuCapabilities.ReadOnlyMemorySupported}");
                Console.WriteLine($"   - no_exec        : {adapter.GpuMmuCapabilities.NoExecuteMemorySupported}");
                Console.WriteLine($"   - cache_coherent : {adapter.GpuMmuCapabilities.CacheCoherentMemorySupported}");
                Console.WriteLine("");

                Console.WriteLine("ID 62, Adapter Performance Data:");
                Console.WriteLine($"   - mem. freq.     : {adapter.PerformanceData.MemoryFrequency} Hz");
                Console.WriteLine($"   - mem. freq. max : {adapter.PerformanceData.MaxMemoryFrequency} Hz");
                Console.WriteLine($"   - mem. freq. OC  : {adapter.PerformanceData.MaxMemoryFrequencyOC} Hz");
                Console.WriteLine($"   - mem. bandwidth : {adapter.PerformanceData.MemoryBandwidth} Bytes");
                Console.WriteLine($"   - pcie bandwidth : {adapter.PerformanceData.PCIEBandwidth} Bytes");
                Console.WriteLine($"   - fan speed      : {adapter.PerformanceData.FanRPM} RPM");
                Console.WriteLine($"   - power draw     : {adapter.PerformanceData.Power / 10.0} %");
                Console.WriteLine($"   - temperature    : {adapter.PerformanceData.Temperature / 10.0} *C");
                Console.WriteLine($"   - power state ovr: {adapter.PerformanceData.PowerStateOverride}");
                Console.WriteLine("");

                Console.WriteLine("ID 63, Adapter Performance Data Capabilities:");
                Console.WriteLine($"   - adapter idx    : {adapter.PerformanceDataCapabilities.PhysicalAdapterIndex}");
                Console.WriteLine($"   - max mem bandwth: {adapter.PerformanceDataCapabilities.MaxMemoryBandwidth} Bytes");
                Console.WriteLine($"   - max pci bandwth: {adapter.PerformanceDataCapabilities.MaxPCIEBandwidth} Bytes");
                Console.WriteLine($"   - max fan speed  : {adapter.PerformanceDataCapabilities.MaxFanRPM} RPM");
                Console.WriteLine($"   - temp max       : {adapter.PerformanceDataCapabilities.TemperatureMax / 10.0} *C");
                Console.WriteLine($"   - temp warning   : {adapter.PerformanceDataCapabilities.TemperatureWarning / 10.0} *C");
                Console.WriteLine("");

                Console.WriteLine("ID 64, GPU version   :");
                Console.WriteLine($"   - adapter idx    : {adapter.GpuVersion.PhysicalAdapterIndex}");
                Console.WriteLine($"   - bios version   : {adapter.GpuVersion.BiosVersion}");
                Console.WriteLine($"   - gpu version    : {adapter.GpuVersion.GpuArchitecture}");
                Console.WriteLine("");

                Console.WriteLine("ID 70, WDDM 2.7 Capabilities:");
                Console.WriteLine($"   - HwSch supported : {adapter.WddmCapabilities_27.HwSchSupported}");
                Console.WriteLine($"   - HwSch enabled   : {adapter.WddmCapabilities_27.HwSchEnabled}");
                Console.WriteLine($"   - HwSch enbl deflt: {adapter.WddmCapabilities_27.HwSchEnabledByDefault}");
                Console.WriteLine($"   - idpdt vdpn vsync: {adapter.WddmCapabilities_27.IndependentVidPnVSyncControl}");
                Console.WriteLine("");

                Console.WriteLine("ID 75, WDDM 2.9 Capabilities:");
                Console.WriteLine($"   - Hw Scheduler state   : {adapter.WddmCapabilities_29.HwSchSupportState}");
                Console.WriteLine($"   - Hw Scheduler enabled : {adapter.WddmCapabilities_29.HwSchEnabled}");
                Console.WriteLine($"   - Self refresh memory  : {adapter.WddmCapabilities_29.SelfRefreshMemorySupported}");
                Console.WriteLine("");

                Console.WriteLine("ID 77, WDDM 3.0 Capabilities:");
                Console.WriteLine($"   - Hw Flip state        : {adapter.WddmCapabilities_30.HwFlipQueueSupportState}");
                Console.WriteLine($"   - Hw Flip enabled      : {adapter.WddmCapabilities_30.HwFlipQueueEnabled}");
                Console.WriteLine($"   - Displayable supported: {adapter.WddmCapabilities_30.DisplayableSupported}");
                Console.WriteLine("");

                Console.WriteLine("ID 80, WDDM 3.1 Capabilities:");
                Console.WriteLine($"   - Native GPU fence     : {adapter.WddmCapabilities_31.NativeGpuFenceSupported}");
                Console.WriteLine("");


                adapter.Dispose();
            }

            Console.WriteLine("\n====       =====\n\n");
        }

    }
}
