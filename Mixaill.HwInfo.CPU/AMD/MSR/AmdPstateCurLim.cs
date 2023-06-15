using Mixaill.HwInfo.Common;

namespace Mixaill.HwInfo.CPU.AMD.MSR
{
    /// <summary>
    /// MSR C001_0061 
    /// P-state Current Limit
    /// Core::X86::Msr::PStateCurLim
    /// </summary>
    public struct AmdPstateCurLim
    {
        /// <summary>
        /// PstateMaxVal: P-state maximum value
        /// Read,Error-on-write,Volatile. Reset: XXXb. 
        /// Specifies the lowestperformance non-boosted P-state (highest non-boosted value) allowed.
        /// Attempts to change 138 Core::X86::Msr::PStateCtl[PstateCmd] to a lower-performance P-state(higher value) are clipped to the value of this field.
        /// </summary>
        public uint PstateMaxVal;

        /// <summary>
        /// CurPstateLimit: current P-state limit. 
        /// Read,Error-on-write,Volatile. Reset: XXXb. 
        /// Specifies the highestperformance P-state (lowest value) allowed. CurPstateLimit is always bounded by 
        // Core::X86::Msr::PStateCurLim[PstateMaxVal]. Attempts to change the CurPstateLimit to a value greater(lowe performance)
        // than Core::X86::Msr::PStateCurLim[PstateMaxVal] leaves CurPstateLimit unchanged.
        /// </summary>
        public uint CurPstateLimit;

        public AmdPstateCurLim(uint eax, uint edx)
        {
            CurPstateLimit = eax.GetValue(0, 3);
            PstateMaxVal = eax.GetValue(4, 3);
        }
    }
}
