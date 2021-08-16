using Mixaill.HwInfo.LowLevel.PCI;
using System;

namespace Mixaill.HwInfo.LowLevel.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            var pc = new Pci();
            pc.Print();

            Console.ReadKey();
        }
    }
}
