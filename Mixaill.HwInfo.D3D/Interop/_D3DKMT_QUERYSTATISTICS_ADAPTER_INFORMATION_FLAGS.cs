using System.Runtime.InteropServices;

using Mixaill.HwInfo.Common;

namespace Mixaill.HwInfo.D3D.Interop
{
    /// <summary>
    /// Reference: d3dkmthk.h::_D3DKMT_QUERYSTATISTICS_ADAPTER_INFORMATION_FLAGS
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct _D3DKMT_QUERYSTATISTICS_ADAPTER_INFORMATION_FLAGS
    {
        ulong data;

        //WDDM 2.1
        public ulong NumberOfMemoryGroups => data.GetValue(0, 2);

        public bool SupportsDemotion => data.IsBitSet(2);
    }
}
