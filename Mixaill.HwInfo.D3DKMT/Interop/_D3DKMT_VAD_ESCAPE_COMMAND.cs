// Copyright 2021, Mikhail Paulyshka
// SPDX-License-Identifier: MIT

namespace Mixaill.HwInfo.D3DKMT.Interop
{
    public enum _D3DKMT_VAD_ESCAPE_COMMAND: uint
    {
        D3DKMT_VAD_ESCAPE_GETNUMVADS,
        D3DKMT_VAD_ESCAPE_GETVAD,
        D3DKMT_VAD_ESCAPE_GETVADRANGE,
        D3DKMT_VAD_ESCAPE_GET_PTE,
        D3DKMT_VAD_ESCAPE_GET_GPUMMU_CAPS,
        D3DKMT_VAD_ESCAPE_GET_SEGMENT_CAPS,
    }
}
