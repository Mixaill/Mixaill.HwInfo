namespace Mixaill.HwInfo.LowLevel
{
    public interface ILowLevel
    {
        public (bool, uint, uint, uint, uint) CpuidRead(uint index);

        public (bool, uint, uint) MsrRead(uint msrAddress);

        public (bool, uint) PciConfigRead32(uint deviceAddress, uint registerAddress);

        public bool PciConfigWrite32(uint deviceAddress, uint registerAddress, uint data);

    }
}
