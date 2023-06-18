using Mixaill.HwInfo.LowLevel;


namespace Mixaill.HwInfo.CPU.AMD.SMN
{
    public unsafe class AmdSmn
    {
        private ILowLevel m_lowLevel { get; }

        public AmdSmn(ILowLevel lowLevel)
        {
            m_lowLevel = lowLevel;
        }

        public (bool, uint) RegRead(uint address)
        {
            if (!m_lowLevel.PciConfigWrite32(0x00000000, 0x60, address))
            {
                return (false, 0x0);
            }

            return m_lowLevel.PciConfigRead32(0x00000000, 0x64);
        }

        public (bool, uint) RegRead(AmdSmnRegisters address)
        {
            return RegRead((uint)address);
        }

        public AmdSmnZenCof GetZenCof()
        {
            return new AmdSmnZenCof(RegRead(AmdSmnRegisters.SMU_AMD_F17H_MATISSE_COF).Item2);
        }
    }
}