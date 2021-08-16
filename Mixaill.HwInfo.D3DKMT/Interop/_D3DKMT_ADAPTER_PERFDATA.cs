// Copyright 2021, Mikhail Paulyshka
// SPDX-License-Identifier: MIT

using System.Runtime.InteropServices;

namespace Mixaill.HwInfo.D3DKMT.Interop
{
    /// <summary>
    /// 
    /// Reference: d3dkmthk.h::_D3DKMT_ADAPTER_PERFDATA
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public partial struct _D3DKMT_ADAPTER_PERFDATA
    {
        /// <summary>
        /// The physical adapter index, in an LDA chain
        /// </summary>
        public uint PhysicalAdapterIndex;

        /// <summary>
        /// Clock frequency of the memory in hertz
        /// </summary>
        public ulong MemoryFrequency;

        /// <summary>
        /// Max memory clock frequency
        /// </summary>
        public ulong MaxMemoryFrequency;

        /// <summary>
        /// Clock frequency of the memory while overclocked in hertz.
        /// </summary>
        public ulong MaxMemoryFrequencyOC;

        /// <summary>
        /// Amount of memory transferred in bytes
        /// </summary>
        public ulong MemoryBandwidth;

        /// <summary>
        /// Amount of memory transferred over PCI-E in bytes
        /// </summary>
        public ulong PCIEBandwidth;

        /// <summary>
        /// Fan rpm
        /// </summary>
        public uint FanRPM;

        /// <summary>
        /// Power draw of the adapter in tenths of a percentage
        /// </summary>
        public uint Power;

        /// <summary>
        /// Temperature in deci-Celsius 1 = 0.1C
        /// </summary>
        public uint Temperature;

        /// <summary>
        /// Overrides dxgkrnls power view of linked adapters.
        /// </summary>
        public byte PowerStateOverride;
    }
}
