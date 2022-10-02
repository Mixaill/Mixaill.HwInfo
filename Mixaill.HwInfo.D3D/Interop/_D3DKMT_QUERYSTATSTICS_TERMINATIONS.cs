// Copyright 2022, Mikhail Paulyshka
// SPDX-License-Identifier: MIT

using System.Runtime.InteropServices;

namespace Mixaill.HwInfo.D3D.Interop
{
    /// <summary>
    /// Reference: d3dkmthk.h::_D3DKMT_QUERYSTATSTICS_TERMINATIONS
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct _D3DKMT_QUERYSTATSTICS_TERMINATIONS
    {
        _D3DKMT_QUERYSTATISTICS_COUNTER TerminatedShared;
        _D3DKMT_QUERYSTATISTICS_COUNTER TerminatedNonShared;
        _D3DKMT_QUERYSTATISTICS_COUNTER DestroyedShared;
        _D3DKMT_QUERYSTATISTICS_COUNTER DestroyedNonShared;
    }
}
