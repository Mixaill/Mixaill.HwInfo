using System.Runtime.InteropServices;

namespace Mixaill.HwInfo.D3D.Interop
{
    /// <summary>
    /// Reference: d3dkmthk.h::_D3DKMT_QUERYSTATISTICS_MEMORY
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct _D3DKMT_QUERYSTATISTICS_MEMORY
    {
        public ulong TotalBytesEvicted;
        public uint AllocsCommitted;
        public uint AllocsResident;
    }
}
