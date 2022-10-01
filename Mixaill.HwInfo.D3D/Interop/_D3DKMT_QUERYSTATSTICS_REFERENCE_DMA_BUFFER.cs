using System.Runtime.InteropServices;

namespace Mixaill.HwInfo.D3D.Interop
{
    /// <summary>
    /// Reference: d3dkmthk.h::_D3DKMT_QUERYSTATSTICS_REFERENCE_DMA_BUFFER
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct _D3DKMT_QUERYSTATSTICS_REFERENCE_DMA_BUFFER
    {
        uint NbCall;
        uint NbAllocationsReferenced;
        uint MaxNbAllocationsReferenced;
        uint NbNULLReference;
        uint NbWriteReference;
        uint NbRenamedAllocationsReferenced;
        uint NbIterationSearchingRenamedAllocation;
        uint NbLockedAllocationReferenced;
        uint NbAllocationWithValidPrepatchingInfoReferenced;
        uint NbAllocationWithInvalidPrepatchingInfoReferenced;
        uint NbDMABufferSuccessfullyPrePatched;
        uint NbPrimariesReferencesOverflow;
        uint NbAllocationWithNonPreferredResources;
        uint NbAllocationInsertedInMigrationTable;
    }
}
