// Copyright 2021, Mikhail Paulyshka
// SPDX-License-Identifier: MIT

using System.Runtime.InteropServices;

namespace Mixaill.HwInfo.D3DKMT.Interop
{
    class Gdi
    {
        [DllImport("gdi32", ExactSpelling = true)]
        public static extern NtStatus D3DKMTCloseAdapter(ref _D3DKMT_CLOSEADAPTER unnamedParam1);

        [DllImport("gdi32", ExactSpelling = true)]
        public static extern NtStatus D3DKMTEnumAdapters(ref _D3DKMT_ENUMADAPTERS unnamedParam1);

        [DllImport("gdi32", ExactSpelling = true)]
        public static extern NtStatus D3DKMTEscape(ref _D3DKMT_ESCAPE unnamedParam1);

        [DllImport("gdi32", ExactSpelling = true)]
        public static extern NtStatus D3DKMTQueryAdapterInfo(ref _D3DKMT_QUERYADAPTERINFO unnamedParam1);

    }
}
