// Copyright 2021, Mikhail Paulyshka
// SPDX-License-Identifier: MIT

using System.Runtime.InteropServices;

namespace Mixaill.HwInfo.D3D.Interop
{
    [StructLayout(LayoutKind.Sequential)]
    public struct _LUID
    {
        public uint LowPart;

        public int HighPart;
    }
}
