namespace Mixaill.HwInfo.CPU.AMD.MSR
{
    public enum AmdMsrIds: uint
    {
        MSR_AMD_PSTATE_CUR_LIM   = 0xC001_0061,
        MSR_AMD_PSTATE_STAT      = 0xC001_0063,
        MSR_AMD_PSTATE_DEF       = 0xC001_0064,
        MSR_AMD_PMGT_MISC        = 0xC001_0292,
        MSR_AMD_PSTATE_HW_STATUS = 0xC001_0293,
    }
}
