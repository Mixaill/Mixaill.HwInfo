// Copyright 2021, Mikhail Paulyshka
// SPDX-License-Identifier: MIT

using System.Runtime.InteropServices;

using Mixaill.HwInfo.Common;

namespace Mixaill.HwInfo.D3D.Interop
{
    /// <summary>
    /// 
    /// Reference: d3dkmthk.h::_D3DKMT_ADAPTERTYPE
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct _D3DKMT_ADAPTERTYPE
    {
        uint data;

        //WDDM 1.0
        public bool RenderSupported => data.IsBitSet(0);
        public bool DisplaySupported => data.IsBitSet(1);
        public bool SoftwareDevice => data.IsBitSet(2);
        public bool PostDevice => data.IsBitSet(3);

        //WDDM 1.3
        public bool HybridDiscrete => data.IsBitSet(4);
        public bool HybridIntegrated => data.IsBitSet(5);
        public bool IndirectDisplayDevice => data.IsBitSet(6);

        //WDDM 2.3
        public bool Paravirtualized => data.IsBitSet(7);
        public bool ACGSupported => data.IsBitSet(8);
        public bool SupportSetTimingsFromVidPn => data.IsBitSet(9);
        public bool Detachable => data.IsBitSet(10);

        //WDDM 2.6
        public bool ComputeOnly => data.IsBitSet(11);
        public bool Prototype => data.IsBitSet(12);

        //WDDM 2.9
        public bool RuntimePowerManagement => data.IsBitSet(13);
    }
}
