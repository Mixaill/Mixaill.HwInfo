namespace Mixaill.HwInfo.CPU.Demo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var pc = new AmdPstate();


            var lim = pc.GetPStateCurLim();
            Console.WriteLine("P State Cur Lim");
            Console.WriteLine($" * CurPstateLimit: {lim.CurPstateLimit}");
            Console.WriteLine($" * PstateMaxVal: {lim.PstateMaxVal}");
            Console.WriteLine();

            for(uint i = 0; i <= AmdPstate.AMD_PSTATE_DEF_LIMIT; i++)
            {
                var state = pc.GetPStateDef(i);
                Console.WriteLine($"State {i}:");
                Console.WriteLine($" * PstateEn: {state.PstateEn}");
                Console.WriteLine($" * CpuFid  : {state.CpuFid}");
                Console.WriteLine($" * CpuDfsId: {state.CpuDfsId}");
                Console.WriteLine($" * CpuVid  : {state.CpuVid}");
                Console.WriteLine($" * IddValue: {state.IddValue}");
                Console.WriteLine($" * IddDiv  : {state.IddDiv}");
                Console.WriteLine($" * CoreCOF : {state.CoreCOF}");
                Console.WriteLine("");
            }

            Console.ReadKey();
        }
    }
}