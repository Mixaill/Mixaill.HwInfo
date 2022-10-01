// Copyright 2021, Mikhail Paulyshka
// SPDX-License-Identifier: MIT

using System;

namespace Mixaill.HwInfo.Common
{
    public static class MathExtensions
    {
        public static ulong Round(this ulong i, ulong nearest)
        {
            if (nearest <= 0 || nearest % 10 != 0)
                throw new ArgumentOutOfRangeException("nearest", "Must round to a positive multiple of 10");

            return (i + 5 * nearest / 10) / nearest * nearest;
        }
    }
}
