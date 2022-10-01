// Copyright 2021, Mikhail Paulyshka
// SPDX-License-Identifier: MIT

using Mixaill.HwInfo.Common;
using System.Runtime.InteropServices;

namespace Mixaill.HwInfo.D3D.Interop
{
    /// <summary>
    /// 
    /// Reference: d3dkmthk.h::_D3DKMT_QUERYSTATISTICS
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public partial struct _D3DKMT_QUERYSTATISTICS
    {
        /// <summary>
        /// in: type of data requested
        /// </summary>
        public _D3DKMT_QUERYSTATISTICS_TYPE Type;

        /// <summary>
        /// in: adapter to get export / statistics from
        /// </summary>
        public _LUID Luid;

        /// <summary>
        /// in: process to get statistics for, if required for this query type
        /// </summary>
        public nuint hProcess;

        /// <summary>
        /// out: requested data
        /// </summary>
        public _D3DKMT_QUERYSTATISTICS_RESULT QueryResult;

        /// <summary>
        /// in: additional request data
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U1, SizeConst = 12)]
        public byte[] QueryRequest;


        public _D3DKMT_QUERYSTATISTICS_QUERY_SEGMENT QuerySegment
        {
            get
            {
                return QueryRequest.ToObject<_D3DKMT_QUERYSTATISTICS_QUERY_SEGMENT>();
            }
            set
            {
                QueryRequest = value.ToBytes(12);
            }
        }
    }
}
