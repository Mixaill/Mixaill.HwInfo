using Mixaill.HwInfo.CPU.AMD.MSR;
using Mixaill.HwInfo.CPU.AMD.SMN;
using Mixaill.HwInfo.CPU.AMD.SVI;
using Mixaill.HwInfo.CPU.Common;
using Mixaill.HwInfo.LowLevel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mixaill.HwInfo.CPU.AMD
{
    public class AmdCpu
    {

        private ILowLevel m_lowLevel { get; }

        public Cpuid Cpuid { get; }

        public AmdMsr MSR { get; }

        public AmdSmn SMN { get; }
        
        public AmdSvi SVI { get; }

        public AmdCpu(ILowLevel lowLevel)
        {
            m_lowLevel = lowLevel;
            Cpuid = new Cpuid(m_lowLevel);
            MSR = new AmdMsr(m_lowLevel);
            SMN = new AmdSmn(m_lowLevel);
            SVI = new AmdSvi(Cpuid, SMN);
        }
    }
}
