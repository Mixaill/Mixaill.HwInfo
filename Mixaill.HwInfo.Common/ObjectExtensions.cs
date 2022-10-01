// Copyright 2021-2022, Mikhail Paulyshka
// SPDX-License-Identifier: MIT

using System;
using System.Runtime.InteropServices;

namespace Mixaill.HwInfo.Common
{
    public static class ObjectExtensions
    {
        public static byte[] ToBytes(this object obj)
        {
            var size = Marshal.SizeOf(obj);
            var result = new byte[size];
            var ptr = ToPointer(obj);
            Marshal.Copy(ptr, result, 0, size);
            Marshal.FreeHGlobal(ptr);

            return result;
        }

        public static byte[] ToBytes(this object obj, int len)
        {
            var result = new byte[len];
            
            var ptr = ToPointer(obj);
            Marshal.Copy(ptr, result, 0, Math.Min(Marshal.SizeOf(obj), len));
            Marshal.FreeHGlobal(ptr);

            return result;
        }

        public static IntPtr ToPointer(this object obj)
        {
            if (!obj.GetType().IsValueType)
                throw new ArgumentException(null, obj.GetType().Name);

            var bufferSize = Marshal.SizeOf(obj);
            var bufferPtr = Marshal.AllocHGlobal(bufferSize);
            Marshal.StructureToPtr(obj, bufferPtr, false);

            return bufferPtr;
        }
    }
}
