using Mixaill.HwInfo.Common;
using System.Text.RegularExpressions;

namespace Mixaill.HwInfo.CPU.AMD.MSR
{

    /// <summary>
    /// MSR C001_0293
    /// P-State Hardware Status
    /// Sources
    ///     * https://github.com/cyring/CoreFreq/blob/master/coretypes.h
    /// </summary>
    public struct AmdPstateHwStatus
    {
        /// <summary>
        /// CurCpuFid: current core frequency ID.
        public uint CurCpuFid;

        /// <summary>
        /// CurCpuDfsId: current core divisor ID. 
        /// </summary>
        public uint CurCpuDfsId;

        /// <summary>
        /// CurCpuVid: current core VID.
        /// </summary>
        public uint CurCpuVid;

        /// <summary>
        /// CurHwPstate
        /// </summary>
        public uint CurHwPstate;

        public double CoreCOF => 200.0 * CurCpuFid / CurCpuDfsId;

        public double CoreMultiplier => 2.0 * CurCpuFid / CurCpuDfsId;


        /// <summary>
        /// Core voltage in Volts
        /// </summary>
        public double CoreVoltage
        {
            get
            {
                switch (UArch)
                {
                    case AmdCpuUarch.Rembrandt:
                    case AmdCpuUarch.Raphael:
                        return 6_250 * CurCpuVid / 1_000_000.0;
                    default:
                        return (1_550_000 - 6_250 * CurCpuVid) / 1_000_000.0;
                }
            }
        }


        /// <summary>
        /// Cpu microarchitecture
        /// </summary>
        private AmdCpuUarch UArch {get;}

        public AmdPstateHwStatus(uint eax, uint edx, AmdCpuUarch uarch)
        {
            UArch = uarch;
            CurCpuFid = eax.GetValue(0, 8);
            CurCpuDfsId = eax.GetValue(8, 6);
            CurCpuVid = eax.GetValue(14, 8);
            CurHwPstate = eax.GetValue(22, 3);
        }
    }
}
