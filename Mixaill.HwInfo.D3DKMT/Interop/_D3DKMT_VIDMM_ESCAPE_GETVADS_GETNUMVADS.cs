// Copyright 2021, Mikhail Paulyshka
// SPDX-License-Identifier: MIT

using System.Runtime.InteropServices;

namespace Mixaill.HwInfo.D3DKMT.Interop
{
    [StructLayout(LayoutKind.Sequential)]
    public struct _D3DKMT_VIDMM_ESCAPE_GETVADS_GETNUMVADS
    {
        public uint NumVads;
    }
}
