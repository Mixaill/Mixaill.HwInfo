// Copyright 2021, Mikhail Paulyshka
// SPDX-License-Identifier: MIT

using System.Runtime.InteropServices;

namespace Mixaill.HwInfo.D3D.Interop
{
    /// <summary>
    /// 
    /// Reference: d3dkmthk.h::_D3DKMT_DEVICE_IDS
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public partial struct _D3DKMT_DEVICE_IDS
    {
        public uint VendorID;

        public uint DeviceID;

        public uint SubVendorID;

        public uint SubSystemID;

        public uint RevisionID;

        public uint BusType;
    }
}
