using Mixaill.HwInfo.CPU.AMD.MSR;
using Mixaill.HwInfo.CPU.AMD.SMN;
using Mixaill.HwInfo.CPU.AMD.SVI;
using Mixaill.HwInfo.CPU.Common;
using Mixaill.HwInfo.LowLevel;
using System;
using System.Collections.Generic;
using System.Text;
using static OpenLibSys.Ols;

namespace Mixaill.HwInfo.CPU.AMD
{
    public class AmdCpu
    {

        private ILowLevel m_lowLevel { get; }

        public Cpuid Cpuid { get; }

        public AmdMsr MSR { get; }

        public AmdSmn SMN { get; }
        
        public AmdSvi SVI { get; }

        public AmdCpu(ILowLevel lowLevel)
        {
            m_lowLevel = lowLevel;
            Cpuid = new Cpuid(m_lowLevel);
            MSR = new AmdMsr(m_lowLevel, GetUarch());
            SMN = new AmdSmn(m_lowLevel);
            SVI = new AmdSvi(GetUarch(), SMN);
        }


        public AmdCpuUarch GetUarch()
        {
            var family = Cpuid.GetCpuFamily();
            var model = Cpuid.GetCpuModel();

            switch (family)
            {
                case 0x17:
                    switch (model)
                    {
                        case 0x01:
                            return AmdCpuUarch.Naples;
                        case 0x31:
                            return AmdCpuUarch.Rome;
                        case 0x60:
                            return AmdCpuUarch.Renoir;
                        case 0x71:
                            return AmdCpuUarch.Matisse;
                        case 0x90:
                            return AmdCpuUarch.VanGogh;
                        default:
                            break;
                    }
                    break;
                case 0x19:
                    switch (model)
                    {
                        case 0x01:
                            return AmdCpuUarch.Milan;
                        case 0x21:
                            return AmdCpuUarch.Vermeer;
                        case 0x40:
                            return AmdCpuUarch.Rembrandt;
                        case 0x50:
                            return AmdCpuUarch.Cezanne;
                        case 0x61:
                            return AmdCpuUarch.Raphael;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
            return AmdCpuUarch.Unknown;
        }
    }
}
