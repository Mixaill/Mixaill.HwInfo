// Copyright 2022, Mikhail Paulyshka
// SPDX-License-Identifier: MIT

using System;
using System.Runtime.InteropServices;

namespace Mixaill.HwInfo.D3D.Interop
{
    /// <summary>
    /// 
    /// Reference: d3dkmthk.h::_D3DKMT_QUERYVIDEOMEMORYINFO
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public partial struct _D3DKMT_QUERYVIDEOMEMORYINFO
    {
        /// <summary>
        /// in,opt: A handle to a process. If NULL, the current process is used. The process handle must be opened with PROCESS_QUERY_INFORMATION privileges
        /// </summary>
        public nuint handle;

        /// <summary>
        /// in : The adapter to query for this process
        /// </summary>
        public uint hAdapter;

        /// <summary>
        /// in : The memory segment group to query
        /// </summary>
        public _D3DKMT_MEMORY_SEGMENT_GROUP MemorySegmentGroup;

        /// <summary>
        /// out: Total memory the application may use
        /// </summary>
        public ulong     Budget;

        /// <summary>
        /// out: Current memory usage of the device
        /// </summary>
        public ulong CurrentUsage;

        /// <summary>
        /// out: Current reservation of the device
        /// </summary>
        public ulong CurrentReservation;

        /// <summary>
        /// out: Total that the device may reserve
        /// </summary>
        public ulong AvailableForReservation;

        /// <summary>
        /// in: Zero based physical adapter index in the LDA configuration.
        /// </summary>
        public uint PhysicalAdapterIndex;
    }
}
