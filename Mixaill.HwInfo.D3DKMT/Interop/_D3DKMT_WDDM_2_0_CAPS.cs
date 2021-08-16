// Copyright 2021, Mikhail Paulyshka
// SPDX-License-Identifier: MIT

using System.Runtime.InteropServices;

using Mixaill.HwInfo.D3DKMT.Helpers;

namespace Mixaill.HwInfo.D3DKMT.Interop
{
    /// <summary>
    /// 
    /// Reference: d3dkmdt.h::_D3DKMT_WDDM_2_0_CAPS
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct _D3DKMT_WDDM_2_0_CAPS
    {
        uint data;

        public bool Atomics64Bit => data.IsBitSet(0);

        public bool GpuMmu => data.IsBitSet(1);

        public bool IoMmu => data.IsBitSet(2);

        //WDDM 2.4
        public bool FlipOverwrite => data.IsBitSet(3);

        public bool ContextlessPresent => data.IsBitSet(4);

        //WDDM 2.5

        public bool SurpriseRemoval => data.IsBitSet(5);

    }
}
