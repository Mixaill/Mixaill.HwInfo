// Copyright 2021-2022, Mikhail Paulyshka
// SPDX-License-Identifier: MIT

namespace Mixaill.HwInfo.Common
{
    public static class UintExtensions
    {
        public static bool IsBitSet(this uint data, int pos)
        {
            return ((data >> pos) & 1) != 0;
        }

        public static uint GetValue(this uint data, int pos, int len)
        {
            return (data >> pos) & ((1U<<len)-1);
        }

        public static uint SetBit(this uint data, int pos, bool val)
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
