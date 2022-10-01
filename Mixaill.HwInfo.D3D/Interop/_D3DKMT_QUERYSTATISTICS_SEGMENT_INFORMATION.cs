using Mixaill.HwInfo.Common;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Mixaill.HwInfo.D3D.Interop
{
    /// <summary>
    /// Reference: d3dkmthk.h::_D3DKMT_QUERYSTATISTICS_SEGMENT_INFORMATION
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct _D3DKMT_QUERYSTATISTICS_SEGMENT_INFORMATION
    {
        public ulong CommitLimit;
        public ulong BytesCommitted;
        public ulong BytesResident;

        public _D3DKMT_QUERYSTATISTICS_MEMORY Memory;

        /// <summary>
        ///Boolean, whether this is an aperture segment 
        /// </summary>
        public uint Aperture;

        /// <summary>
        /// Breakdown of bytes evicted by priority class
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
        public ulong[] TotalBytesEvictedByPriority;


        public ulong SystemMemoryEndAddress;

        public ulong PowerFlags;

        public ulong SegmentProperties;


        #region Flags

        public bool PreservedDuringStandby => PowerFlags.IsBitSet(0);
        public bool PreservedDuringHibernate => PowerFlags.IsBitSet(1);
        public bool PartiallyPreservedDuringHibernate => PowerFlags.IsBitSet(2);
        public bool SystemMemory => SegmentProperties.IsBitSet(0);
        public bool PopulatedByReservedDDRByFirmware => SegmentProperties.IsBitSet(1);
        public _D3DKMT_QUERYSTATISTICS_SEGMENT_TYPE SegmentType => (_D3DKMT_QUERYSTATISTICS_SEGMENT_TYPE)SegmentProperties.GetValue(2,4);

        #endregion

    }
}
