using Mixaill.HwInfo.LowLevel;


namespace Mixaill.HwInfo.CPU.AMD.SMN
{
    public unsafe class SmnManager
    {

        private OlsWrapper ols { get; set; } = null;

        public SmnManager()
        {
            ols = OlsWrapper.Instance();
        }
        
        public uint RegRead(uint addr)
        {
            ols.WritePciConfigDwordEx(0x00000000, SmnPciRegisters.AMD_F17H_SMU_INDEX, addr);

            uint data = 0;
            ols.ReadPciConfigDwordEx(0x00000000, SmnPciRegisters.AMD_F17H_SMU_DATA, ref data);
            return data;
        }

        public SmnZenCof GetZenCof()
        {
            return new SmnZenCof(RegRead(SmnRegisters.SMU_AMD_F17H_MATISSE_COF));
        }

    }
}