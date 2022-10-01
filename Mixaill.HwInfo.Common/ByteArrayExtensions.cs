// Copyright 2021-2022, Mikhail Paulyshka
// SPDX-License-Identifier: MIT

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Mixaill.HwInfo.Common
{
    public static class ByteArrayExtensions
    {
        public static T ToObject<T>(this byte[] bytes)
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


        public static List<string> SeparateRegMultiSz(this byte[] source)
        {
            var result = new List<string>();

            int index_last = 0;
            for(var index = 0; index < source.Length-2; index += 2)
            {
                if(!IsStringSep(source, index)){
                    continue;
                }

                result.Add(Encoding.Unicode.GetString(source, index_last, index-index_last));
                index_last = index + 2;
            }
            return result;
        }

        static bool IsStringSep(this byte[] data, int index)
        {
            for (int i = 0; i < 2; i++)
            {
                if(data[index+i] != 0)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
