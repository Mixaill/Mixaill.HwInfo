using Mixaill.HwInfo.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mixaill.HwInfo.CPU.Common
{
    /// <summary>
    /// Core::X86::Cpuid::FamModStep
    /// </summary>
    public struct CpuidLeaf1
    {

        public uint Stepping;

        public uint BaseModel;
        public uint ExtModel;
        public uint Model => (ExtModel << 4) + BaseModel;

        public uint BaseFamily;
        public uint ExtFamily;
        public uint Family => ExtFamily + BaseFamily;

        public CpuidLeaf1(uint eax, uint ebx, uint ecx, uint edx)
        {
            Stepping = eax.GetValue(0, 4);
            BaseModel = eax.GetValue(4, 4);
            BaseFamily = eax.GetValue(8, 4);
            ExtModel = eax.GetValue(16, 4);
            ExtFamily = eax.GetValue(20, 8);
        }
    }
}
