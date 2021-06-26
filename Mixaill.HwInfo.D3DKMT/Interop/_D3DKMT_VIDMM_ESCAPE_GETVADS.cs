// Copyright 2018-2020, Simon Mourier
// Copyright 2021, Mikhail Paulyshka
// SPDX-License-Identifier: MIT

using Mixaill.HwInfo.D3DKMT.Helpers;
using System.Runtime.InteropServices;

namespace Mixaill.HwInfo.D3DKMT.Interop
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct _D3DKMT_VIDMM_ESCAPE_GETVADS
    {
        public fixed byte data[112];

        public _D3DKMT_VAD_ESCAPE_COMMAND Command;
        public int Status;

        public _D3DKMT_VIDMM_ESCAPE_GETVADS_GETNUMVADS GetNumVads
        {
            get
            {
                fixed (byte* buf = data)
                {
                    return InteropHelper.Get<_D3DKMT_VIDMM_ESCAPE_GETVADS_GETNUMVADS>(buf, 0, sizeof(_D3DKMT_VIDMM_ESCAPE_GETVADS_GETNUMVADS));
                }
            }

            set
            {
                fixed (byte* buf = data)
                {
                    InteropHelper.Set(value, buf, 0, sizeof(_D3DKMT_VIDMM_ESCAPE_GETVADS_GETNUMVADS));
                }
            }
        }
    }
}
