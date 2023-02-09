using System.Collections.Generic;

namespace Mixaill.HwInfo.PCI
{
    public class PciDevice
    {
        #region Properties

        public const uint MaxDeviceCount = 32;

        public uint DeviceNumber { get; } = 0xFF;

        public PciBus Bus { get; } = null;

        public uint BusNumber => Bus?.BusNumber ?? 0xFF;

        private Dictionary<uint, PciFunction> Functions = new Dictionary<uint, PciFunction>();


        #endregion


        public PciDevice(PciBus bus, uint devicenum)
        {
            Bus = bus;
            DeviceNumber = devicenum;

            for(uint i = 0; i < PciFunction.MaxFunctionsCount; i++)
            {
                var function = new PciFunction(this, i);
                if (function.Exists())
                {
                    Functions[function.FunctionNumber] = function;
                }
            }
        }


        public IReadOnlyCollection<PciFunction> GetFunctions()
        {
            return Functions.Values;
        }


        public PciFunction FindFunction(uint funcnum)
        {
            if (Functions.ContainsKey(funcnum))
            {
                return Functions[funcnum];
            }

            return null;
        }
    }
}
