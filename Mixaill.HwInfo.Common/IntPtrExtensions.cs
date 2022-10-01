// Copyright 2021-2022, Mikhail Paulyshka
// SPDX-License-Identifier: MIT

using System.Runtime.InteropServices;
using System;

namespace Mixaill.HwInfo.Common
{
    public static class IntPtrExtensions
    {
        public static T ToObject<T>(this IntPtr pointer)
        {
            if (typeof(T).IsEnum)
                return (T)Enum.ToObject(typeof(T), Marshal.ReadInt32(pointer));

            if (typeof(T).IsValueType)
                return (T)Marshal.PtrToStructure(pointer, typeof(T));

            throw new ArgumentException(null, typeof(T).Name);
        }
    }
}
