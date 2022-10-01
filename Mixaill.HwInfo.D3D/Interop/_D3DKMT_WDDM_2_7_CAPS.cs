// Copyright 2021, Mikhail Paulyshka
// SPDX-License-Identifier: MIT

using System.Runtime.InteropServices;

using Mixaill.HwInfo.Common;

namespace Mixaill.HwInfo.D3D.Interop
{
    /// <summary>
    /// 
    /// Reference: d3dkmdt.h::_D3DKMT_WDDM_2_7_CAPS
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct _D3DKMT_WDDM_2_7_CAPS
    {
        uint data;

        public bool HwSchSupported => data.IsBitSet(0);

        public bool HwSchEnabled => data.IsBitSet(1);

        public bool HwSchEnabledByDefault => data.IsBitSet(2);

        public bool IndependentVidPnVSyncControl => data.IsBitSet(3);
    }
}
