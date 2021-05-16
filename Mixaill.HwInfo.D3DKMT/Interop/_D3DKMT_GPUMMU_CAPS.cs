// Copyright 2021, Mikhail Paulyshka
// SPDX-License-Identifier: MIT

using System.Runtime.InteropServices;

using Mixaill.HwInfo.D3DKMT.Helpers;

namespace Mixaill.HwInfo.D3DKMT.Interop
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct _D3DKMT_GPUMMU_CAPS
    {
        uint flags;

        public uint VirtualAddressBitCount;

        public bool ReadOnlyMemorySupported => flags.IsBitSet(0);

        public bool NoExecuteMemorySupported => flags.IsBitSet(1);

        public bool CacheCoherentMemorySupported => flags.IsBitSet(2);

    }
}
