// Copyright 2021, Mikhail Paulyshka
// SPDX-License-Identifier: MIT

using System.Collections.Generic;

namespace Mixaill.HwInfo.D3DKMT
{
    public class Kmt
    {
        public List<KmtAdapter> GetAdapters()
        {
            var result = new List<KmtAdapter>();

            var adapters = new Interop._D3DKMT_ENUMADAPTERS();
            if (Interop.Gdi.D3DKMTEnumAdapters(ref adapters) == Interop.NtStatus.STATUS_SUCCESS)
            {
                for (int i = 0; i < adapters.NumAdapters; i++)
                {
                    result.Add(new KmtAdapter(adapters.Adapters[i]));
                }
            }

            return result;
        }
    }
}
