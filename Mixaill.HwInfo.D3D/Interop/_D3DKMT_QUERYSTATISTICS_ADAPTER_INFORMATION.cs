// Copyright 2022, Mikhail Paulyshka
// SPDX-License-Identifier: MIT

using System.Runtime.InteropServices;

namespace Mixaill.HwInfo.D3D.Interop
{
    /// <summary>
    /// Reference: d3dkmthk.h::_D3DKMT_QUERYSTATISTICS_ADAPTER_INFORMATION
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct _D3DKMT_QUERYSTATISTICS_ADAPTER_INFORMATION
    {
        public uint NbSegments;
        uint NodeCount;
        uint VidPnSourceCount;
        uint VSyncEnabled;
        uint TdrDetectedCount;
        long ZeroLengthDmaBuffers;
        ulong RestartedPeriod;

        _D3DKMT_QUERYSTATSTICS_REFERENCE_DMA_BUFFER ReferenceDmaBuffer;
        _D3DKMT_QUERYSTATSTICS_RENAMING Renaming;
        _D3DKMT_QUERYSTATSTICS_PREPRATION Preparation;
        _D3DKMT_QUERYSTATSTICS_PAGING_FAULT PagingFault;
        _D3DKMT_QUERYSTATSTICS_PAGING_TRANSFER PagingTransfer;
        _D3DKMT_QUERYSTATSTICS_SWIZZLING_RANGE SwizzlingRange;
        _D3DKMT_QUERYSTATSTICS_LOCKS Locks;
        _D3DKMT_QUERYSTATSTICS_ALLOCATIONS Allocations;
        _D3DKMT_QUERYSTATSTICS_TERMINATIONS Terminations;

        //WDDM 2.2
        _D3DKMT_QUERYSTATISTICS_ADAPTER_INFORMATION_FLAGS Flags;
    }
}
