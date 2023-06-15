
using Mixaill.HwInfo.Common;
using static OpenLibSys.Ols;

namespace Mixaill.HwInfo.CPU.AMD.MSR
{
    /// <summary>
    /// MSR C001_0292
    /// Core::X86::Msr::PMGT_MISC
    /// </summary>
    public struct AmdPmgtMisc
    {
        /// <summary>
        /// Current HW Pstate limit
        /// </summary>
        public uint CurHwPstateLimit;

        /// <summary>
        /// Start up Pstate
        /// </summary>
        public uint StartupPstate;

        /// <summary>
        /// DF Pstate disable
        /// </summary>
        public bool DFPstateDis;

        /// <summary>
        /// Current DF VID
        /// </summary>
        public uint CurDFVid;

        /// <summary>
        /// Maximum CPU COF
        /// </summary>
        public uint MaxCpuCof;

        /// <summary>
        /// Maximum DF COF
        /// </summary>
        public uint MaxDFCof;

        /// <summary>
        /// CPB capability
        /// </summary>
        public uint CpbCap;

        /// <summary>
        /// PC6 enable
        /// </summary>
        public bool PC6En;

        /// <summary>
        /// DF voltage in Volts
        /// </summary>
        public double DFVoltage => (1_550_000 - 6_250 * CurDFVid) / 1_000_000.0;

        public AmdPmgtMisc(uint eax, uint edx)
        {
            CurHwPstateLimit = eax.GetValue(0, 3);
            StartupPstate = eax.GetValue(3, 3);
            DFPstateDis = eax.GetValue(6, 1) != 0;
            CurDFVid = eax.GetValue(7, 8);
            MaxCpuCof = eax.GetValue(15, 6);
            MaxDFCof = edx.GetValue(21, 5);
            CpbCap = eax.GetValue(26, 3);
            PC6En = edx.GetValue(0, 1) != 0;
        }
    }
}
