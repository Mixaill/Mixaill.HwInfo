namespace Mixaill.HwInfo.CPU.Demo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var pc = new AmdPstate();

            
            var msr_pstate_lim = pc.GetPStateCurLim();
            Console.WriteLine("MSR_AMD_PSTATE_CUR_LIM / 0xC001_0061");
            Console.WriteLine($"  * CurPstateLimit: {msr_pstate_lim.CurPstateLimit}");
            Console.WriteLine($"  * PstateMaxVal: {msr_pstate_lim.PstateMaxVal}");
            Console.WriteLine();
           

            Console.WriteLine("MSR_AMD_PSTATE_DEF / 0xC001_0064");
            for (uint i = 0; i <= Math.Min(AmdPstate.AMD_PSTATE_DEF_LIMIT, msr_pstate_lim.PstateMaxVal); i++)
            {
                {
                    var msr = pc.GetPStateDef(i);
                    Console.WriteLine($"  * State {i}:");
                    Console.WriteLine($"     * PstateEn: {msr.PstateEn}");
                    Console.WriteLine($"     * CpuFid  : {msr.CpuFid}");
                    Console.WriteLine($"     * CpuDfsId: {msr.CpuDfsId}");
                    Console.WriteLine($"     * CpuVid  : {msr.CpuVid}");
                    Console.WriteLine($"     * IddValue: {msr.IddValue}");
                    Console.WriteLine($"     * IddDiv  : {msr.IddDiv}");
                    Console.WriteLine($"       ----------");
                    Console.WriteLine($"     * CoreCOF : {msr.CoreCOF}");
                }
            }
            Console.WriteLine();

            Console.WriteLine("MSR_AMD_PSTATE_HW_STATUS / 0xC001_0293");
            {
                var msr = pc.GetPStateHwStatus();
                Console.WriteLine($"  * CurCpuFid   : {msr.CurCpuFid}");
                Console.WriteLine($"  * CurCpuDfsId : {msr.CurCpuDfsId}");
                Console.WriteLine($"  * CurCpuVid   : {msr.CurCpuVid}");
                Console.WriteLine($"  * CurHwPstate : {msr.CurHwPstate}");
                Console.WriteLine($"    ----------");
                Console.WriteLine($"  * CoreCOF     : {msr.CoreCOF}");
            }

            Console.ReadKey();
        }
    }
}