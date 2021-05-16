// Copyright 2021, Mikhail Paulyshka
// SPDX-License-Identifier: MIT

namespace Mixaill.HwInfo.D3DKMT.Helpers
{
    public static class UintExtensions
    {
        public static bool IsBitSet(this uint data, int pos)
        {
            return ((data >> pos) & 1) != 0;
        }
    }
}
