using System.Runtime.InteropServices;

namespace Mixaill.HwInfo.D3D.Interop
{
    /// <summary>
    /// Reference: d3dkmthk.h::_D3DKMT_QUERYSTATISTICS_COUNTER
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct _D3DKMT_QUERYSTATSTICS_ALLOCATIONS
    {
        _D3DKMT_QUERYSTATISTICS_COUNTER Created;
        _D3DKMT_QUERYSTATISTICS_COUNTER Destroyed;
        _D3DKMT_QUERYSTATISTICS_COUNTER Opened;
        _D3DKMT_QUERYSTATISTICS_COUNTER Closed;
        _D3DKMT_QUERYSTATISTICS_COUNTER MigratedSuccess;
        _D3DKMT_QUERYSTATISTICS_COUNTER MigratedFail;
        _D3DKMT_QUERYSTATISTICS_COUNTER MigratedAbandoned;
    }
}
