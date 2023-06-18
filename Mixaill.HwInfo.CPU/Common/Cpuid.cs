using Mixaill.HwInfo.LowLevel;

namespace Mixaill.HwInfo.CPU.Common
{
    public class Cpuid
    {
        private ILowLevel m_lowLevel { get; }

        private CpuidLeaf1 m_leaf1 { get; }

        public Cpuid(ILowLevel lowLevel)
        {
            m_lowLevel = lowLevel;

            (var result, var eax, var ebx, var ecx, var edx) = m_lowLevel.CpuidRead(0x0000_0001);
            if (result)
            {
                m_leaf1 = new CpuidLeaf1(eax, ebx, ecx, edx);
            }
        }

        public uint GetCpuFamily()
        {
            return m_leaf1.Family;
        }

        public uint GetCpuModel()
        {
            return m_leaf1.Model;
        }
    }
}
