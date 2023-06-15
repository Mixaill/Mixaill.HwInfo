using Mixaill.HwInfo.LowLevel;

namespace Mixaill.HwInfo.CPU.AMD.MSR
{
    public class AmdMsr
    {
        #region Constants

        static uint MSR_AMD_PSTATE_CUR_LIM = 0xC001_0061;
        static uint MSR_AMD_PSTATE_STAT = 0xC001_0063;
        static uint MSR_AMD_PSTATE_DEF = 0xC001_0064;
        static uint MSR_AMD_PGMT_MISC = 0xC001_0292;
        static uint MSR_AMD_PSTATE_HW_STATUS = 0xC001_0293;

        public static uint AMD_PSTATE_DEF_LIMIT = 7U;

        #endregion

        #region Properties

        private OlsWrapper ols { get; } = null;

        #endregion

        public AmdMsr()
        {
            ols = OlsWrapper.Instance();
        }

        #region MSR read

        /// <summary>
        /// Get content of MSR Core::X86::Msr::PStateCurLim
        /// </summary>
        /// <returns></returns>
        public AmdPstateCurLim GetPStateCurLim()
        {
            uint eax = 0U;
            uint edx = 0U;
            if (ols.Rdmsr(MSR_AMD_PSTATE_CUR_LIM, ref eax, ref edx) != 0)
            {
                return new AmdPstateCurLim(eax, edx);
            }

            return new AmdPstateCurLim();
        }

        public AmdPstateStat GetPStateStatus()
        {
            uint eax = 0U;
            uint edx = 0U;
            if (ols.Rdmsr(MSR_AMD_PSTATE_STAT, ref eax, ref edx) != 0)
            {
                return new AmdPstateStat(eax, edx);
            }

            return new AmdPstateStat();
        }

        public AmdPstateDef GetPStateDef(uint pstate_num)
        {
            if (pstate_num < AMD_PSTATE_DEF_LIMIT)
            {
                uint eax = 0U;
                uint edx = 0U;
                if (ols.Rdmsr(MSR_AMD_PSTATE_DEF + pstate_num, ref eax, ref edx) != 0)
                {
                    return new AmdPstateDef(eax, edx);
                }
            }

            return new AmdPstateDef();
        }


        public AmdPmgtMisc GetPmgtMisc()
        {
            uint eax = 0U;
            uint edx = 0U;
            if (ols.Rdmsr(MSR_AMD_PSTATE_HW_STATUS, ref eax, ref edx) != 0)
            {
                return new AmdPmgtMisc(eax, edx);
            }

            return new AmdPmgtMisc();
        }

        public AmdPstateHwStatus GetPStateHwStatus()
        {
            uint eax = 0U;
            uint edx = 0U;
            if (ols.Rdmsr(MSR_AMD_PSTATE_HW_STATUS, ref eax, ref edx) != 0)
            {
                return new AmdPstateHwStatus(eax, edx);
            }

            return new AmdPstateHwStatus();
        }

        #endregion
    }
}
