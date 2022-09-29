// Copyright 2021, Mikhail Paulyshka
// SPDX-License-Identifier: MIT

namespace Mixaill.HwInfo.D3D.Interop
{
    public enum NtStatus : uint
    {
        STATUS_SUCCESS               = 0x00000000,
        STATUS_INVALID_PARAMETER     = 0xC000000D,
        STATUS_ACCESS_DENIED         = 0xC0000022,
        STATUS_OBJECT_NAME_NOT_FOUND = 0xC0000034,
    }
}
