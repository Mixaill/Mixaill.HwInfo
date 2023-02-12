using static OpenLibSys.Ols;

namespace Mixaill.HwInfo.CPU.AMD.MSR
{

    /// <summary>
    /// MSR C001_0293
    /// P-State Hardware Status
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
        public double CoreVoltage => (1_550_000 - 6_250 * CurCpuVid) / 1_000_000.0;
    }
}
