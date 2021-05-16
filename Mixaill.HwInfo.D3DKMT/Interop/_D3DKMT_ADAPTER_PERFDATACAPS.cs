// Copyright 2021, Mikhail Paulyshka
// SPDX-License-Identifier: MIT

using System.Runtime.InteropServices;

namespace Mixaill.HwInfo.D3DKMT.Interop
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct _D3DKMT_ADAPTER_PERFDATACAPS
    {
        /// <summary>
        /// The physical adapter index, in an LDA chain
        /// </summary>
        public uint PhysicalAdapterIndex;

        /// <summary>
        /// Max memory bandwidth in bytes for 1 second
        /// </summary>
        public ulong MaxMemoryBandwidth;

        /// <summary>
        /// Max pcie bandwidth in bytes for 1 second
        /// </summary>
        public ulong MaxPCIEBandwidth;

        /// <summary>
        /// Max fan rpm
        /// </summary>
        public uint MaxFanRPM;

        /// <summary>
        /// Max temperature before damage levels
        /// </summary>
        public uint TemperatureMax;

        /// <summary>
        /// The temperature level where throttling begins.
        /// </summary>
        public uint TemperatureWarning;
    }
}
