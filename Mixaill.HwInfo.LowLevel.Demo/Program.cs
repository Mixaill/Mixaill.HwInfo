using Mixaill.HwInfo.LowLevel.PCI;
using System;
using System.Diagnostics;

namespace Mixaill.HwInfo.LowLevel.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            ulong f = Native.Rdtsc.Frequency();
            Console.WriteLine($"RDTSC frequency: {Math.Round(f/1e6,3)} MHz");
            Console.WriteLine($"RDTSC tick     : {Math.Round(1e9/f,3)} ns");

            Console.WriteLine("");

            Bench_RDTSCP();
            Bench_StopWatch();
            Bench_TickCount();

            return;
        }

        private static void Bench_RDTSCP()
        {
            ulong count = Native.Rdtsc.Test();
            Console.WriteLine($"RDTSCP: {count}, {Math.Round(1e9 / count,3)} ns");
        }

        private static void Bench_StopWatch()
        {
            //warmup
            for (int i = 0; i < 100; i++)
            {
                Stopwatch.GetTimestamp();
            }

            //count
            ulong count = 0;
            long doneTick = Stopwatch.GetTimestamp() + TimeSpan.TicksPerSecond;
            while (Stopwatch.GetTimestamp() <= doneTick)
            {
                count++;
            }

            Console.WriteLine($"StopWatch: {count}, {Math.Round(1e9 / count,3)} ns");
        }

        private static void Bench_TickCount()
        {
            //warmup
            for (int i = 0; i < 100; i++)
            {
                Stopwatch.GetTimestamp();
            }

            //count
            ulong count = 0;
            int doneTick = Environment.TickCount + 1000;
            while (Environment.TickCount <= doneTick)
            {
                count++;
            }

            Console.WriteLine($"TickCount: {count}, {Math.Round(1e9 / count,3)} ns");
        }
    }
}
