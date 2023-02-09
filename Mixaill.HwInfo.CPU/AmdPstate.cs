﻿using System;

using Mixaill.HwInfo.Common;
using Mixaill.HwInfo.LowLevel;


namespace Mixaill.HwInfo.CPU
{
    public class AmdPstate
    {
        #region Constants

        static UInt32 MSR_AMD_PSTATE_CUR_LIM = 0xC001_0061;
        static UInt32 MSR_AMD_PSTATE_DEF     = 0xC001_0064;

        public static uint AMD_PSTATE_DEF_LIMIT = 7U;

        #endregion

        #region Properties

        private OlsWrapper ols { get; } = null;

        #endregion

        public AmdPstate()
        {
            ols = OlsWrapper.Instance();
        }

        #region MSR raed

        /// <summary>
        /// Get content of MSR Core::X86::Msr::PStateCurLim
        /// </summary>
        /// <returns></returns>
        public AmdPstateCurLim GetPStateCurLim()
        {
            AmdPstateCurLim result = new AmdPstateCurLim();

            UInt32 eax = 0U;
            UInt32 edx = 0U;
            if (ols.Rdmsr(MSR_AMD_PSTATE_CUR_LIM, ref eax, ref edx) != 0)
            {
                result.CurPstateLimit = eax.GetValue(0, 3);
                result.PstateMaxVal = eax.GetValue(4, 3);
            }

            return result;
        }

        public AmdPstateDef GetPStateDef(uint pstate_num)
        {
            AmdPstateDef result = new AmdPstateDef();

            if (pstate_num > AMD_PSTATE_DEF_LIMIT)
            {
                return result;
            }

            UInt32 eax = 0U;
            UInt32 edx = 0U;
            if (ols.Rdmsr(MSR_AMD_PSTATE_DEF + pstate_num, ref eax, ref edx) != 0)
            {
                result.CpuFid = eax.GetValue(0, 8);
                result.CpuDfsId = eax.GetValue(8, 6);
                result.CpuVid = eax.GetValue(14, 8);
                result.IddValue = eax.GetValue(22, 8);
                result.IddDiv = eax.GetValue(30, 2);
                result.PstateEn = edx.GetValue(31, 1) != 0;
            }

            return result;
        }

        #endregion

    }
}
