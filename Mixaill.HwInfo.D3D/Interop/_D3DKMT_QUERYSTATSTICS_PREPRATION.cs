using System.Runtime.InteropServices;

namespace Mixaill.HwInfo.D3D.Interop
{
    /// <summary>
    /// Reference: d3dkmthk.h::_D3DKMT_QUERYSTATSTICS_PREPRATION
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct _D3DKMT_QUERYSTATSTICS_PREPRATION
    {
        uint BroadcastStall;
        uint NbDMAPrepared;
        uint NbDMAPreparedLongPath;
        uint ImmediateHighestPreparationPass;
        _D3DKMT_QUERYSTATISTICS_COUNTER AllocationsTrimmed;
    }
}
