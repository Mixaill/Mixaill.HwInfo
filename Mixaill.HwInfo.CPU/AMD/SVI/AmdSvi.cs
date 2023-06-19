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

        private AmdCpuUarch m_uarch { get; }
        private AmdSmn m_smn { get; }

        private AmdSviIds m_addr_core { get; } = AmdSviIds.UNKNOWN;
        private AmdSviIds m_addr_soc { get; } = AmdSviIds.UNKNOWN;

        #endregion

        public AmdSvi(AmdCpuUarch uarch, AmdSmn smn)
        {
            m_uarch = uarch;
            m_smn = smn;
            switch (m_uarch)
            {
                case AmdCpuUarch.Naples:
                    m_addr_core = AmdSviIds.F17H_M01H_SVI_TEL_PLANE0;
                    m_addr_soc = AmdSviIds.F17H_M01H_SVI_TEL_PLANE1;
                    break;
                case AmdCpuUarch.Rome:
                    m_addr_core = AmdSviIds.F17H_M31H_SVI_TEL_PLANE0;
                    m_addr_soc = AmdSviIds.F17H_M31H_SVI_TEL_PLANE1;
                    break;
                case AmdCpuUarch.Renoir:
                    m_addr_core = AmdSviIds.F17H_M60H_SVI_TEL_PLANE0;
                    m_addr_soc = AmdSviIds.F17H_M60H_SVI_TEL_PLANE1;
                    break;
                case AmdCpuUarch.Matisse:
                    m_addr_core = AmdSviIds.F17H_M71H_SVI_TEL_PLANE0;
                    m_addr_soc = AmdSviIds.F17H_M71H_SVI_TEL_PLANE1;
                    break;
                case AmdCpuUarch.VanGogh:
                    m_addr_core = AmdSviIds.F17H_M90H_SVI_TEL_PLANE0;
                    m_addr_soc = AmdSviIds.F17H_M90H_SVI_TEL_PLANE1;
                    break;
                case AmdCpuUarch.Milan:
                    m_addr_core = AmdSviIds.F19H_M01H_SVI_TEL_PLANE0;
                    m_addr_soc = AmdSviIds.F19H_M01H_SVI_TEL_PLANE1;
                    break;
                case AmdCpuUarch.Vermeer:
                    m_addr_core = AmdSviIds.F19H_M21H_SVI_TEL_PLANE0;
                    m_addr_soc = AmdSviIds.F19H_M21H_SVI_TEL_PLANE1;
                    break;
                case AmdCpuUarch.Cezanne:
                    m_addr_core = AmdSviIds.F19H_M50H_SVI_TEL_PLANE0;
                    m_addr_soc = AmdSviIds.F19H_M50H_SVI_TEL_PLANE1;
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

        public double GetVoltageCore()
        {
            return GetVoltage((uint)m_addr_core);
        }

        public double GetVoltageSoc()
        {
            return GetVoltage((uint)m_addr_soc);
        }

        private void debugVoltage(uint address)
        {
            (var ok, var reg) = m_smn.RegRead(address);
            if (!ok)
            {
                return;
            }
            if (reg == 0x0 || reg == 0xFFFF_FFFF)
            {
                return;
            }

            var vid = ((reg >> 16) & 0xFF);
            if(vid == 0x0 || vid == 0x80 || vid == 0xFF)
            {
                return;
            }

            var volt = 1.55 - vid * 0.00625;
            var volt2 = vid * 0.00625;
            Console.WriteLine($"{address:X8} --> {vid}, {volt}, {volt2}");
        }

        public void DebugVoltage()
        {
            Console.WriteLine("Debug voltage");
            for(uint i = 0x0; i < 0x1000; i += 4)
            {
                debugVoltage((uint)AmdSviIds.ZEN_SVI_BASE + i);
            }
            for (uint i = 0x0; i < 0x1000; i += 4)
            {
                debugVoltage((uint)AmdSviIds.ZEN_SVI_BASE_2 + i);
            }
            Console.WriteLine();
        }
    }
}

