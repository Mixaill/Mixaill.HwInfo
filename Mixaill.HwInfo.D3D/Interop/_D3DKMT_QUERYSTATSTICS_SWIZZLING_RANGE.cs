using System.Runtime.InteropServices;

namespace Mixaill.HwInfo.D3D.Interop
{
    /// <summary>
    /// Reference: d3dkmthk.h::_D3DKMT_QUERYSTATISTICS_COUNTER
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct _D3DKMT_QUERYSTATSTICS_SWIZZLING_RANGE
    {
        uint NbRangesAcquired;
        uint NbRangesReleased;
    }
}
