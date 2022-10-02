// Copyright 2021, Mikhail Paulyshka
// SPDX-License-Identifier: MIT

using System.Runtime.InteropServices;

namespace Mixaill.HwInfo.D3D.Interop
{
    /// <summary>
    /// 
    /// Reference: d3dkmthk._D3DKMT_CPDRIVERNAME
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct _D3DKMT_CPDRIVERNAME
    {
        
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        public string ContentProtectionFileName;

    }
}
