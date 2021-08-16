// Copyright 2021, Mikhail Paulyshka
// SPDX-License-Identifier: MIT

using System.Runtime.InteropServices;

using Mixaill.HwInfo.D3DKMT.Helpers;

namespace Mixaill.HwInfo.D3DKMT.Interop
{

    /// <summary>
    /// 
    /// Reference: d3dkmthk.h::_D3DKMT_GPUMMU_CAPS
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct _D3DKMT_GPUMMU_CAPS
    {
        uint flags;

        public uint VirtualAddressBitCount;

        #region Getters
        public bool ReadOnlyMemorySupported => flags.IsBitSet(0);

        public bool NoExecuteMemorySupported => flags.IsBitSet(1);

        public bool CacheCoherentMemorySupported => flags.IsBitSet(2);
        #endregion

    }
}
