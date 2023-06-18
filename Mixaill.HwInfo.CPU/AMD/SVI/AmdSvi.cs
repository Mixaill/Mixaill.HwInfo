using Mixaill.HwInfo.CPU.AMD.SMN;
using Mixaill.HwInfo.CPU.Common;
using Mixaill.HwInfo.LowLevel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mixaill.HwInfo.CPU.AMD.SVI
{
    /// <summary>
    /// Sources:
    ///  * https://github.com/torvalds/linux/commit/0a4e668b5d52eed8026f5d717196b02b55fb2dc6
    ///  * https://github.com/Ta180m/zenpower3/blob/master/zenpower.c
    /// </summary>
    public class AmdSvi
    {
        #region Properties

        private Cpuid m_cpuid { get; }
        private AmdSmn m_smn { get; }

        private AmdSviIds m_addr_core { get; } = AmdSviIds.UNKNOWN;

        #endregion

        public AmdSvi(Cpuid cpuid, AmdSmn smn)
        {
            m_cpuid = cpuid;
            m_smn = smn;
            switch (m_cpuid.GetCpuFamily())
            {
                case 0x17:
                    switch (m_cpuid.GetCpuModel())
                    {
                        case 0x01: //Naples
                            m_addr_core = AmdSviIds.F17H_M01H_SVI_TEL_PLANE0;
                            break;
                        case 0x31: //Rome, Castle Peak
                            m_addr_core = AmdSviIds.F17H_M31H_SVI_TEL_PLANE0;
                            break;
                        case 0x60: //Renoir
                            m_addr_core = AmdSviIds.F17H_M60H_SVI_TEL_PLANE0;
                            break;
                        case 0x71: //Matisse
                            m_addr_core = AmdSviIds.F17H_M71H_SVI_TEL_PLANE0;
                            break;
                        default:
                            break;
                    }
                    break;
                case 0x19:
                    switch (m_cpuid.GetCpuModel())
                    {
                        case 0x01: //Milan
                            m_addr_core = AmdSviIds.F19H_M01H_SVI_TEL_PLANE0;
                            break;
                        case 0x21: //Vermeer
                            m_addr_core = AmdSviIds.F19H_M21H_SVI_TEL_PLANE0;
                            break;
                        case 0x50: //Cezanne
                            m_addr_core = AmdSviIds.F19H_M50H_SVI_TEL_PLANE0;
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
        }

        private double GetVoltage(uint address)
        {
            var result = 0.0;

            if (m_addr_core != AmdSviIds.UNKNOWN)
            {

                (var ok, var reg) = m_smn.RegRead(address);
                if (ok)
                {
                    var vid = ((reg >> 16) & 0xFF);
                    result = 1.55 - vid * 0.00625;
                }
            }

            return result;
        }

        public double GetVoltage()
        {
            return GetVoltage((uint)m_addr_core);
        }

        public void DebugVoltage()
        {
            Console.WriteLine("Debug voltage");
            for(uint i = 0x0; i < 0x50; i += 4)
            {
                var result = GetVoltage((uint)AmdSviIds.ZEN_SVI_BASE + i);
                if(result == 1.55)
                {
                    continue;
                }
                Console.WriteLine($"{i:X02} : {result}");
            }
            Console.WriteLine();
        }
    }
}

