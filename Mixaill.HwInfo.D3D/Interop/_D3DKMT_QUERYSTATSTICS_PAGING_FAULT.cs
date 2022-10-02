// Copyright 2022, Mikhail Paulyshka
// SPDX-License-Identifier: MIT

using System.Runtime.InteropServices;

namespace Mixaill.HwInfo.D3D.Interop
{
    /// <summary>
    /// Reference: d3dkmthk.h::_D3DKMT_QUERYSTATSTICS_PAGING_FAULT
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct _D3DKMT_QUERYSTATSTICS_PAGING_FAULT
    {
        _D3DKMT_QUERYSTATISTICS_COUNTER Faults;
        _D3DKMT_QUERYSTATISTICS_COUNTER FaultsFirstTimeAccess;
        _D3DKMT_QUERYSTATISTICS_COUNTER FaultsReclaimed;
        _D3DKMT_QUERYSTATISTICS_COUNTER FaultsMigration;
        _D3DKMT_QUERYSTATISTICS_COUNTER FaultsIncorrectResource;
        _D3DKMT_QUERYSTATISTICS_COUNTER FaultsLostContent;
        _D3DKMT_QUERYSTATISTICS_COUNTER FaultsEvicted;
        _D3DKMT_QUERYSTATISTICS_COUNTER AllocationsMEM_RESET;
        _D3DKMT_QUERYSTATISTICS_COUNTER AllocationsUnresetSuccess;
        _D3DKMT_QUERYSTATISTICS_COUNTER AllocationsUnresetFail;
        uint AllocationsUnresetSuccessRead;
        uint AllocationsUnresetFailRead;

        _D3DKMT_QUERYSTATISTICS_COUNTER Evictions;
        _D3DKMT_QUERYSTATISTICS_COUNTER EvictionsDueToPreparation;
        _D3DKMT_QUERYSTATISTICS_COUNTER EvictionsDueToLock;
        _D3DKMT_QUERYSTATISTICS_COUNTER EvictionsDueToClose;
        _D3DKMT_QUERYSTATISTICS_COUNTER EvictionsDueToPurge;
        _D3DKMT_QUERYSTATISTICS_COUNTER EvictionsDueToSuspendCPUAccess;
    }
}
