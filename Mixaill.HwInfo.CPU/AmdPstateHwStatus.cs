namespace Mixaill.HwInfo.CPU
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
    }
}
