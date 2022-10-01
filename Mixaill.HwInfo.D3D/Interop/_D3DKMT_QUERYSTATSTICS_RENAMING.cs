using System.Runtime.InteropServices;

namespace Mixaill.HwInfo.D3D.Interop
{
    /// <summary>
    /// Reference: d3dkmthk.h::_D3DKMT_QUERYSTATSTICS_RENAMING
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct _D3DKMT_QUERYSTATSTICS_RENAMING
    {
        uint NbAllocationsRenamed;
        uint NbAllocationsShrinked;
        uint NbRenamedBuffer;
        uint MaxRenamingListLength;
        uint NbFailuresDueToRenamingLimit;
        uint NbFailuresDueToCreateAllocation;
        uint NbFailuresDueToOpenAllocation;
        uint NbFailuresDueToLowResource;
        uint NbFailuresDueToNonRetiredLimit;
    }
}
