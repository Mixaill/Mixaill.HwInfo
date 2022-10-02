// Copyright 2022, Mikhail Paulyshka
// SPDX-License-Identifier: MIT

using System.Runtime.InteropServices;

namespace Mixaill.HwInfo.D3D.Interop
{
    /// <summary>
    /// Reference: d3dkmthk.h::_D3DKMT_QUERYSTATSTICS_PAGING_TRANSFER
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct _D3DKMT_QUERYSTATSTICS_PAGING_TRANSFER
    {
        ulong BytesFilled;
        ulong BytesDiscarded;
        ulong BytesMappedIntoAperture;
        ulong BytesUnmappedFromAperture;
        ulong BytesTransferredFromMdlToMemory;
        ulong BytesTransferredFromMemoryToMdl;
        ulong BytesTransferredFromApertureToMemory;
        ulong BytesTransferredFromMemoryToAperture;
    }
}
