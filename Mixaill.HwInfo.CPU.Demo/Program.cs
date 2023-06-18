using Mixaill.HwInfo.CPU.AMD.SMN;
using Mixaill.HwInfo.Common;
using Mixaill.HwInfo.CPU.AMD.MSR;
using Mixaill.HwInfo.CPU.AMD;

namespace Mixaill.HwInfo.CPU.Demo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var cpu = new AmdCpu(LowLevel.Backend.OLS.LowLevelOls.Instance);

            Console.WriteLine("\n///");
            Console.WriteLine("/// CPUID");
            Console.WriteLine("///\n");

            Console.WriteLine("LEAF 0000_0001");
            Console.WriteLine($"  * Family: {cpu.Cpuid.GetCpuFamily()} ({cpu.Cpuid.GetCpuFamily():X02}h)");
            Console.WriteLine($"  * Model: {cpu.Cpuid.GetCpuModel()} ({cpu.Cpuid.GetCpuModel():X02}h)");
            Console.WriteLine();


            Console.WriteLine("\n///");
            Console.WriteLine("/// MSR");
            Console.WriteLine("///\n");


            var msr_pstate_lim = cpu.MSR.GetPStateCurLim();
            Console.WriteLine("MSR_AMD_PSTATE_CUR_LIM / 0xC001_0061");
            Console.WriteLine($"  * CurPstateLimit: {msr_pstate_lim.CurPstateLimit}");
            Console.WriteLine($"  * PstateMaxVal  : {msr_pstate_lim.PstateMaxVal}");
            Console.WriteLine();

            var msr_pstate_stat = cpu.MSR.GetPStateStatus();
            Console.WriteLine("MSR_AMD_PSTATE_STAT / 0xC001_0063");
            Console.WriteLine($"  * CurPstate: {msr_pstate_stat.CurPstate}");
            Console.WriteLine();


            Console.WriteLine("MSR_AMD_PSTATE_DEF / 0xC001_0064");
            for (uint i = 0; i <= Math.Min(AmdMsr.AMD_PSTATE_DEF_LIMIT, msr_pstate_lim.PstateMaxVal); i++)
            {
                {
                    var msr_val = cpu.MSR.GetPStateDef(i);
                    Console.WriteLine($"  * State {i}:");
                    Console.WriteLine($"     * PstateEn      : {msr_val.PstateEn}");
                    Console.WriteLine($"     * CpuFid        : {msr_val.CpuFid}");
                    Console.WriteLine($"     * CpuDfsId      : {msr_val.CpuDfsId}");
                    Console.WriteLine($"     * CpuVid        : {msr_val.CpuVid}");
                    Console.WriteLine($"     * IddValue      : {msr_val.IddValue}");
                    Console.WriteLine($"     * IddDiv        : {msr_val.IddDiv}");
                    Console.WriteLine($"       ----------");
                    Console.WriteLine($"     * CoreCOF       : {msr_val.CoreCOF}");
                    Console.WriteLine($"     * CoreMultiplier: {msr_val.CoreMultiplier}");
                    Console.WriteLine($"     * CorePower     : {msr_val.CorePower}");
                    Console.WriteLine($"     * CoreVoltage   : {msr_val.CoreVoltage}");

                }
            }
            Console.WriteLine();

            Console.WriteLine("MSR_AMD_PMGT_MISC / 0xC001_0292");
            {
                var msr_val = cpu.MSR.GetPmgtMisc();
                Console.WriteLine($"  * CurHwPstateLimit     : {msr_val.CurHwPstateLimit}");
                Console.WriteLine($"  * StartupPstate        : {msr_val.StartupPstate}");
                Console.WriteLine($"  * DFPstateDis          : {msr_val.DFPstateDis}");
                Console.WriteLine($"  * CurDFVid             : {msr_val.CurDFVid}");
                Console.WriteLine($"  * MaxCpuCof            : {msr_val.MaxCpuCof}");
                Console.WriteLine($"  * MaxDFCof             : {msr_val.MaxDFCof}");
                Console.WriteLine($"  * CpbCap               : {msr_val.CpbCap}");
                Console.WriteLine($"  * PC6En                : {msr_val.PC6En}");
                Console.WriteLine($"    ----------");
                Console.WriteLine($"  * DFVoltage            : {msr_val.DFVoltage} (possibly incorrect)");

            }
            Console.WriteLine();

            Console.WriteLine("MSR_AMD_PSTATE_HW_STATUS / 0xC001_0293");
            {
                var msr_val = cpu.MSR.GetPStateHwStatus();
                Console.WriteLine($"  * CurCpuFid      : {msr_val.CurCpuFid}");
                Console.WriteLine($"  * CurCpuDfsId    : {msr_val.CurCpuDfsId}");
                Console.WriteLine($"  * CurCpuVid      : {msr_val.CurCpuVid}");
                Console.WriteLine($"  * CurHwPstate    : {msr_val.CurHwPstate}");
                Console.WriteLine($"    ----------");
                Console.WriteLine($"  * CoreCOF        : {msr_val.CoreCOF}");
                Console.WriteLine($"  * CoreMultiplier : {msr_val.CoreMultiplier}");
                Console.WriteLine($"  * CoreVoltage    : {msr_val.CoreVoltage}");

            }
            Console.WriteLine();

            Console.WriteLine("\n///");
            Console.WriteLine("/// SMN");
            Console.WriteLine("///\n");

            Console.WriteLine("SMN_F17H_ZEN2_COF / 0x0005d2c4");
            var zen2_cof = cpu.SMN.GetZenCof();            
            Console.WriteLine($"  * BoostRatio:" + zen2_cof.BoostRatio);
            Console.WriteLine($"  * MinRatio  :" + zen2_cof.MinRatio);
            Console.WriteLine($"   ----------");
            Console.WriteLine($"  * CoreCOF   :" + zen2_cof.CoreCof);
            Console.WriteLine();

            Console.WriteLine("\n///");
            Console.WriteLine("/// SVI");
            Console.WriteLine("///\n");

            Console.WriteLine("TEL_PLANE0");
            Console.WriteLine($"  * Voltage  :" + cpu.SVI.GetVoltage());
            cpu.SVI.DebugVoltage();

            Console.ReadKey();
        }
    }
}