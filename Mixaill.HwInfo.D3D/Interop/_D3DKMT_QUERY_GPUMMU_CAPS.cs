// Copyright 2021, Mikhail Paulyshka
// SPDX-License-Identifier: MIT

using System.Runtime.InteropServices;

namespace Mixaill.HwInfo.D3D.Interop
{
    /// <summary>
    /// 
    /// Reference: d3dkmthk.h::_D3DKMT_QUERY_GPUMMU_CAPS
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    struct _D3DKMT_QUERY_GPUMMU_CAPS
    {
        /// <summary>
        /// Input
        /// </summary>
        public uint PhysicalAdapterIndex;
     
        /// <summary>
        /// Output
        /// </summary>
        public _D3DKMT_GPUMMU_CAPS Caps;
    }
}
