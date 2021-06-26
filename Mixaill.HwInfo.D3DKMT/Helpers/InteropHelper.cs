// Copyright 2018-2020, Simon Mourier
// Copyright 2021, Mikhail Paulyshka
// SPDX-License-Identifier: MIT

using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Mixaill.HwInfo.D3DKMT.Helpers
{
    public static class InteropHelper
    {
        internal static unsafe T Get<T>(byte* bytes, int offset, int count)
        {
            if (bytes == null)
                return default;

            if (!typeof(T).IsValueType)
                throw new NotSupportedException();

            var size = Marshal.SizeOf<T>();
            var buffer = new byte[size];
            for(int i = 0; i < count; i++)
            {
                buffer[i] = bytes[offset + i];
            }

            var ghc = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            try
            {
                return (T)Marshal.PtrToStructure(ghc.AddrOfPinnedObject(), typeof(T));
            }
            finally
            {
                ghc.Free();
            }
        }

     
        public static unsafe void Set<T>(T obj, byte* bytes, int offset, int count)
        {
            if (!typeof(T).IsValueType)
                throw new ArgumentException(null, nameof(T));

            var buffer = new byte[Marshal.SizeOf<T>()];
            var ptr = Marshal.AllocCoTaskMem(buffer.Length);

            try
            {
                Marshal.StructureToPtr(obj, ptr, false);
                Marshal.Copy(ptr, buffer, 0, buffer.Length);
                for(int i = 0; i < buffer.Length; i++)
                {
                    bytes[i] = buffer[i];
                }
            }
            finally
            {
                Marshal.FreeCoTaskMem(ptr);
            }
        }
    }
}
