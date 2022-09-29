// Copyright 2021, Mikhail Paulyshka
// SPDX-License-Identifier: MIT

using System.Runtime.InteropServices;

namespace Mixaill.HwInfo.D3D.Interop
{
    /// <summary>
    /// 
    /// Reference: d3dkmthk.h_D3DKMT_ADAPTERREGISTRYINFO
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct _D3DKMT_ADAPTERREGISTRYINFO
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        public string AdapterString;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        public string BiosString;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        public string DacType;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        public string ChipType;
    }
}
