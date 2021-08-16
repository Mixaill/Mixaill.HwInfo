// Copyright 2021, Mikhail Paulyshka
// SPDX-License-Identifier: MIT

using System.Runtime.InteropServices;

namespace Mixaill.HwInfo.D3DKMT.Interop
{
    /// <summary>
    /// 
    /// Reference: d3dkmthk.h::_D3DKMT_QUERY_DEVICE_IDS
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public partial struct _D3DKMT_QUERY_DEVICE_IDS
    {
        /// <summary>
        /// Input
        /// </summary>
        public uint PhysicalAdapterIndex;
        
        /// <summary>
        /// Output
        /// </summary>
        public _D3DKMT_DEVICE_IDS DeviceIds;
    }
}
