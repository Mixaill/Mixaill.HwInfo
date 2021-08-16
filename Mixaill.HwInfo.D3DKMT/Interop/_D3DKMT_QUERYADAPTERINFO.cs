// Copyright 2021, Mikhail Paulyshka
// SPDX-License-Identifier: MIT

using System;
using System.Runtime.InteropServices;

namespace Mixaill.HwInfo.D3DKMT.Interop
{
    /// <summary>
    /// 
    /// Reference: d3dkmthk.h::_D3DKMT_QUERYADAPTERINFO
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public partial struct _D3DKMT_QUERYADAPTERINFO
    {
        public uint hAdapter;

        public _KMTQUERYADAPTERINFOTYPE Type;

        public IntPtr pPrivateDriverData;

        public uint PrivateDriverDataSize;
    }
}
