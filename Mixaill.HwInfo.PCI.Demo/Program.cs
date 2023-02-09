namespace Mixaill.HwInfo.PCI.Demo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var pc = new Pci();
            pc.Print();
        }
    }
}