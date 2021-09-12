using System;

using Mixaill.HwInfo.LowLevel.PCI;

namespace Mixaill.HwInfo.LowLevel.Demo
{
    class d1_pci_enum
    {
        public static void process()
        {
            var pc = new Pci();
            pc.Print();
        }
    }
}
