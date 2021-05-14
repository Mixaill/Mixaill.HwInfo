// Copyright 2021, Mikhail Paulyshka
// SPDX-License-Identifier: MIT

using System.Runtime.InteropServices;

namespace Mixaill.HwInfo.D3DKMT.Interop
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public partial struct _D3DKMT_QUERY_DEVICE_IDS
    {
        public uint PhysicalAdapterIndex;
        public _D3DKMT_DEVICE_IDS DeviceIds;
    }
}
