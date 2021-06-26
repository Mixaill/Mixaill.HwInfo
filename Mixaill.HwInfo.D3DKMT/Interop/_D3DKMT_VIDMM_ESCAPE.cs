// Copyright 2018-2020, Simon Mourier
// Copyright 2021, Mikhail Paulyshka
// SPDX-License-Identifier: MIT

using System.Runtime.InteropServices;

using Mixaill.HwInfo.D3DKMT.Helpers;

namespace Mixaill.HwInfo.D3DKMT.Interop
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct _D3DKMT_VIDMM_ESCAPE
    {
        public _D3DKMT_VIDMMESCAPETYPE Type;

        private fixed byte data[120];

        public _D3DKMT_VIDMM_ESCAPE_GETVADS GetVads
        {
            get
            {
                fixed (byte* buf = data)
                {
                    return InteropHelper.Get<_D3DKMT_VIDMM_ESCAPE_GETVADS>(buf, 0, sizeof(_D3DKMT_VIDMM_ESCAPE_GETVADS));
                }
            }

            set
            {
                fixed (byte* buf = data)
                {
                    InteropHelper.Set(value, buf, 0, sizeof(_D3DKMT_VIDMM_ESCAPE_GETVADS));
                }
            }
        }
    }
}
