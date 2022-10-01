// Copyright 2022, Mikhail Paulyshka
// SPDX-License-Identifier: MIT

namespace Mixaill.HwInfo.Common
{
    public static class UlongExtensions
    {
        public static bool IsBitSet(this ulong data, int pos)
        {
            return ((data >> pos) & 1) != 0;
        }

        public static ulong GetValue(this ulong data, int pos, int len)
        {
            return (data >> pos) & ((1UL << len) - 1UL);
        }

        public static ulong SetBit(this ulong data, int pos, bool val)
        {
            if (val)
            {
                data |= (1U << pos);
            }
            else
            {
                data &= ~(1U << pos);
            }

            return data;
        }
    }
}
