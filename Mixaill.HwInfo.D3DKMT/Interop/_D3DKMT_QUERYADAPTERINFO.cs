// Copyright 2021, Mikhail Paulyshka
// SPDX-License-Identifier: MIT

using System;
using System.Runtime.InteropServices;

namespace Mixaill.HwInfo.D3DKMT.Interop
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public partial struct _D3DKMT_QUERYADAPTERINFO
    {
        public uint hAdapter;

        public _D3DKMT_QUERYADAPTERINFOTYPE Type;

        public IntPtr pPrivateDriverData;

        public uint PrivateDriverDataSize;
    }
}
