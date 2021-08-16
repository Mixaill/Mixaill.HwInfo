// Copyright 2018-2020, Simon Mourier
// Copyright 2021, Mikhail Paulyshka
// SPDX-License-Identifier: MIT

using System;
using System.Runtime.InteropServices;

namespace Mixaill.HwInfo.D3DKMT.Helpers
{
    public static class InteropHelper
    {
        public static T BytesToObj<T>(byte[] bytes)
        {
            int bufferSize = Marshal.SizeOf(typeof(T));
            IntPtr bufferPtr = Marshal.AllocHGlobal(bufferSize);

            try
            {
                Marshal.Copy(bytes, 0, bufferPtr, bufferSize);
                return (T)Marshal.PtrToStructure(bufferPtr, typeof(T));
            }
            finally
            {
                Marshal.FreeHGlobal(bufferPtr);
            }
        }


        public static T PointerToObj<T>(IntPtr pointer)
        {
            return (T)PointerToObj(typeof(T), pointer);
        }


        public static object PointerToObj(Type type, IntPtr pointer)
        {
            if (type.IsEnum)
                return Enum.ToObject(type, Marshal.ReadInt32(pointer));
            
            if (type.IsValueType)
                return Marshal.PtrToStructure(pointer, type);

            throw new ArgumentException(null, type.Name);
        }


        public static IntPtr ObjToPointer(object obj)
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
