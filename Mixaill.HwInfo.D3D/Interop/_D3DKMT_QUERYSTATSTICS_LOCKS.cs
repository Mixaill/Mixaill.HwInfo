// Copyright 2022, Mikhail Paulyshka
// SPDX-License-Identifier: MIT

using System.Runtime.InteropServices;

namespace Mixaill.HwInfo.D3D.Interop
{
    /// <summary>
    /// Reference: d3dkmthk.h::_D3DKMT_QUERYSTATSTICS_LOCKS
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct _D3DKMT_QUERYSTATSTICS_LOCKS
    {
        uint NbLocks;
        uint NbLocksWaitFlag;
        uint NbLocksDiscardFlag;
        uint NbLocksNoOverwrite;
        uint NbLocksNoReadSync;
        uint NbLocksLinearization;
        uint NbComplexLocks;
    }
}
