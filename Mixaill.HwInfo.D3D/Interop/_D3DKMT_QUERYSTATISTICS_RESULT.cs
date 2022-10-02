// Copyright 2022, Mikhail Paulyshka
// SPDX-License-Identifier: MIT

using System.Runtime.InteropServices;

using Mixaill.HwInfo.Common;

namespace Mixaill.HwInfo.D3D.Interop
{
    /// <summary>
    /// Reference: d3dkmthk.h::_D3DKMT_QUERYSTATISTICS_COUNTER
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct _D3DKMT_QUERYSTATISTICS_RESULT
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 776)]
        public byte[] data;

        /// <summary>
        /// out: result of D3DKMT_QUERYSTATISTICS_ADAPTER(2) query
        /// </summary>
        public _D3DKMT_QUERYSTATISTICS_ADAPTER_INFORMATION AdapterInformation => data.ToObject<_D3DKMT_QUERYSTATISTICS_ADAPTER_INFORMATION>();

        /// <summary>
        /// out: result of D3DKMT_QUERYSTATISTICS_SEGMENT(2) query
        /// </summary>
        public _D3DKMT_QUERYSTATISTICS_SEGMENT_INFORMATION SegmentInformation => data.ToObject<_D3DKMT_QUERYSTATISTICS_SEGMENT_INFORMATION>();

    }
}
