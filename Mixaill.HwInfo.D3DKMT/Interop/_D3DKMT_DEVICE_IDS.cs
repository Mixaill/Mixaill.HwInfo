// Copyright 2021, Mikhail Paulyshka
// SPDX-License-Identifier: MIT

using System.Runtime.InteropServices;

namespace Mixaill.HwInfo.D3DKMT.Interop
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
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
