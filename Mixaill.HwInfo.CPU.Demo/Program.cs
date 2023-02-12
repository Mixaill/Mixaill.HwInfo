using Mixaill.HwInfo.CPU.AMD.SMN;
using Mixaill.HwInfo.Common;
using Mixaill.HwInfo.CPU.AMD.MSR;

namespace Mixaill.HwInfo.CPU.Demo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var pstate = new AmdPstate();
            var smn = new SmnManager();

            var msr_pstate_lim = pstate.GetPStateCurLim();
            Console.WriteLine("MSR_AMD_PSTATE_CUR_LIM / 0xC001_0061");
            Console.WriteLine($"  * CurPstateLimit: {msr_pstate_lim.CurPstateLimit}");
            Console.WriteLine($"  * PstateMaxVal  : {msr_pstate_lim.PstateMaxVal}");
            Console.WriteLine();

            var msr_pstate_stat = pstate.GetPStateStatus();
            Console.WriteLine("MSR_AMD_PSTATE_STAT / 0xC001_0063");
            Console.WriteLine($"  * CurPstate: {msr_pstate_stat.CurPstate}");
            Console.WriteLine();


            Console.WriteLine("MSR_AMD_PSTATE_DEF / 0xC001_0064");
            for (uint i = 0; i <= Math.Min(AmdPstate.AMD_PSTATE_DEF_LIMIT, msr_pstate_lim.PstateMaxVal); i++)
            {
                {
                    var msr = pstate.GetPStateDef(i);
                    Console.WriteLine($"  * State {i}:");
                    Console.WriteLine($"     * PstateEn      : {msr.PstateEn}");
                    Console.WriteLine($"     * CpuFid        : {msr.CpuFid}");
                    Console.WriteLine($"     * CpuDfsId      : {msr.CpuDfsId}");
                    Console.WriteLine($"     * CpuVid        : {msr.CpuVid}");
                    Console.WriteLine($"     * IddValue      : {msr.IddValue}");
                    Console.WriteLine($"     * IddDiv        : {msr.IddDiv}");
                    Console.WriteLine($"       ----------");
                    Console.WriteLine($"     * CoreCOF       : {msr.CoreCOF}");
                    Console.WriteLine($"     * CoreMultiplier: {msr.CoreMultiplier}");
                    Console.WriteLine($"     * CorePower     : {msr.CorePower}");
                    Console.WriteLine($"     * CoreVoltage   : {msr.CoreVoltage}");

                }
            }
            Console.WriteLine();

            Console.WriteLine("MSR_AMD_PSTATE_HW_STATUS / 0xC001_0293");
            {
                var msr = pstate.GetPStateHwStatus();
                Console.WriteLine($"  * CurCpuFid      : {msr.CurCpuFid}");
                Console.WriteLine($"  * CurCpuDfsId    : {msr.CurCpuDfsId}");
                Console.WriteLine($"  * CurCpuVid      : {msr.CurCpuVid}");
                Console.WriteLine($"  * CurHwPstate    : {msr.CurHwPstate}");
                Console.WriteLine($"    ----------");
                Console.WriteLine($"  * CoreCOF        : {msr.CoreCOF}");
                Console.WriteLine($"  * CoreMultiplier : {msr.CoreMultiplier}");
                Console.WriteLine($"  * CoreVoltage    : {msr.CoreVoltage}");

            }
            Console.WriteLine();

            Console.WriteLine("SMN_F17H_ZEN2_COF / 0x0005d2c4");
            var zen2_cof = smn.GetZenCof();            
            Console.WriteLine($"  * BoostRatio:" + zen2_cof.BoostRatio);
            Console.WriteLine($"  * MinRatio  :" + zen2_cof.MinRatio);
            Console.WriteLine($"   ----------");
            Console.WriteLine($"  * CoreCOF   :" + zen2_cof.CoreCof);
            Console.WriteLine();

            Console.ReadKey();
        }
    }
}