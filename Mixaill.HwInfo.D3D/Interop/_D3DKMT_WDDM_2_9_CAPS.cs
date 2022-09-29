// Copyright 2021-2022, Mikhail Paulyshka
// SPDX-License-Identifier: MIT

using System.Runtime.InteropServices;

using Mixaill.HwInfo.D3D.Helpers;

namespace Mixaill.HwInfo.D3D.Interop
{
    /// <summary>
    /// 
    /// Reference: d3dkmdt.h::_D3DKMT_WDDM_2_9_CAPS
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct _D3DKMT_WDDM_2_9_CAPS
    {
        uint data;

        public _DXGK_FEATURE_SUPPORT HwSchSupportState => (_DXGK_FEATURE_SUPPORT)data.GetValue(0, 2);

        public bool HwSchEnabled => data.IsBitSet(2);

        public bool SelfRefreshMemorySupported => data.IsBitSet(3);
    }
}
