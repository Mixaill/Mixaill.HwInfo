namespace Mixaill.HwInfo.CPU.AMD.MSR
{
    /// <summary>
    /// MSR C001_0063 
    /// P-state Status
    /// Core::X86::Msr::PStateStat
    /// </summary>
    public struct AmdPstateStat
    {
        /// <summary>
        /// CurPstate: current P-state
        /// Read,Error-on-write,Volatile.Reset: XXXb.This field provides the frequency
        /// component of the current non-boosted P-state of the core(regardless of the source of the P-state change, including
        /// Core::X86::Msr::PStateCtl[PstateCmd]. 0=P0, 1=P1, etc.). The value of this field is updated when the COF transitions to a new value associated with a P-state.
        /// </summary>
        public uint CurPstate;
    }
}
