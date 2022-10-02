// Copyright 2022, Mikhail Paulyshka
// SPDX-License-Identifier: MIT

namespace Mixaill.HwInfo.D3D.Interop
{
    /// <summary>
    /// Reference: d3dkmthk.h::_D3DKMT_MEMORY_SEGMENT_GROUP
    /// </summary>
    public enum _D3DKMT_MEMORY_SEGMENT_GROUP : uint
    {
        D3DKMT_MEMORY_SEGMENT_GROUP_LOCAL = 0,
        D3DKMT_MEMORY_SEGMENT_GROUP_NON_LOCAL = 1
    }
}
