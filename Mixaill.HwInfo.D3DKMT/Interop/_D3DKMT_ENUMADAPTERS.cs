// Copyright 2021, Mikhail Paulyshka
// SPDX-License-Identifier: MIT

using System.Runtime.InteropServices;

namespace Mixaill.HwInfo.D3DKMT.Interop
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct _D3DKMT_ENUMADAPTERS
    {
        public uint NumAdapters;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public _D3DKMT_ADAPTERINFO[] Adapters;
    }
}
