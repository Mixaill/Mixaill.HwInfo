using Mixaill.HwInfo.LowLevel;

namespace Mixaill.HwInfo.CPU.AMD.MSR
{
    public class AmdMsr
    {
        #region Constants

        public static uint AMD_PSTATE_DEF_LIMIT = 7U;

        #endregion

        #region Properties

        private ILowLevel m_lowLevel { get; } = null;

        #endregion

        public AmdMsr(ILowLevel lowLevel)
        {
            m_lowLevel = lowLevel;
        }

        #region MSR read

        public AmdPstateCurLim GetPStateCurLim()
        {
            (var result, var eax, var edx) = m_lowLevel.MsrRead((uint)AmdMsrIds.MSR_AMD_PSTATE_CUR_LIM);
            return result ? new AmdPstateCurLim(eax, edx) : new AmdPstateCurLim();
        }

        public AmdPstateStat GetPStateStatus()
        {
            (var result, var eax, var edx) = m_lowLevel.MsrRead((uint)AmdMsrIds.MSR_AMD_PSTATE_STAT);
            return result ? new AmdPstateStat(eax, edx) : new AmdPstateStat();
        }

        public AmdPstateDef GetPStateDef(uint pstate_num)
        {
            if (pstate_num < AMD_PSTATE_DEF_LIMIT)
            {
                (var result, var eax, var edx) = m_lowLevel.MsrRead((uint)AmdMsrIds.MSR_AMD_PSTATE_DEF + pstate_num);
                if (result)
                {
                    return new AmdPstateDef(eax, edx);
                }
            }

            return new AmdPstateDef();
        }


        public AmdPmgtMisc GetPmgtMisc()
        {
            (var result, var eax, var edx) = m_lowLevel.MsrRead((uint)AmdMsrIds.MSR_AMD_PMGT_MISC);
            return result ? new AmdPmgtMisc(eax, edx) : new AmdPmgtMisc();
        }

        public AmdPstateHwStatus GetPStateHwStatus()
        {
            (var result, var eax, var edx) = m_lowLevel.MsrRead((uint)AmdMsrIds.MSR_AMD_PSTATE_HW_STATUS);
            return result ? new AmdPstateHwStatus(eax, edx) : new AmdPstateHwStatus();
        }

        #endregion
    }
}
