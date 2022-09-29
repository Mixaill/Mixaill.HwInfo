// Copyright 2021-2022, Mikhail Paulyshka
// SPDX-License-Identifier: MIT

using System.Runtime.InteropServices;

using Mixaill.HwInfo.D3D.Helpers;

namespace Mixaill.HwInfo.D3D.Interop
{
    /// <summary>
    /// 
    /// Reference: d3dkmdt.h::_D3DKMT_WDDM_3_1_CAPS
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct _D3DKMT_WDDM_3_1_CAPS
    {
        uint data;

        public bool NativeGpuFenceSupported => data.IsBitSet(0);
    }
}
