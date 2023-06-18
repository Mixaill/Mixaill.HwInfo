using System;
using System.Collections.Generic;
using System.Text;

namespace Mixaill.HwInfo.LowLevel.Backend.OLS
{
    public class LowLevelOls : OpenLibSys.Ols, ILowLevel
    {
        private OpenLibSys.Ols ols;
        private static LowLevelOls instance;

        private LowLevelOls()
        {
            ols = new OpenLibSys.Ols();
            ols.InitializeOls();
        }

        public static LowLevelOls Instance
        {
            get
            {
                if (instance == null)
                    instance = new LowLevelOls();

                return instance;
            }
        }

        public (bool, uint, uint) MsrRead(uint msrAddress)
        {
            uint eax = 0;
            uint edx = 0;
            return (ols.Rdmsr(msrAddress, ref eax, ref edx) == 1, eax, edx);
        }

        public (bool, uint) PciConfigRead32(uint deviceAddress, uint registerAddress)
        {
            uint result = 0;
            return (ols.ReadPciConfigDwordEx(deviceAddress, registerAddress, ref result) == 1, result);
        }

        public bool PciConfigWrite32(uint deviceAddress, uint registerAddress, uint data)
        {
            return ols.WritePciConfigDwordEx(deviceAddress, registerAddress, data) == 1;
        }

        public (bool, uint, uint, uint, uint) CpuidRead(uint index)
        {
            uint eax = 0;
            uint ebx = 0;
            uint ecx = 0;
            uint edx = 0;

            return (ols.Cpuid(index, ref eax, ref ebx, ref ecx, ref edx) == 1, eax, ebx, ecx, edx);
        }
    }
}
