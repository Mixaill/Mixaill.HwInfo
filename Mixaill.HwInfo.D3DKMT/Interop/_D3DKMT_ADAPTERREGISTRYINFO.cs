// Copyright 2021, Mikhail Paulyshka
// SPDX-License-Identifier: MIT

using System.Runtime.InteropServices;
using System.Text;

namespace Mixaill.HwInfo.D3DKMT.Interop
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
    public unsafe struct _D3DKMT_ADAPTERREGISTRYINFO
    {
        fixed byte adapterString[260*2];
        public string AdapterString
        {
            get
            {
                fixed (byte* buf = adapterString)
                {
                    return Encoding.Unicode.GetString(buf, 260 * 2).TrimEnd(new char[] { '\0', ' ' });
                }
            }
        }

        fixed byte biosString[260 * 2];

        public string BiosString
        {
            get
            {
                fixed (byte* buf = biosString)
                {
                    return Encoding.Unicode.GetString(buf, 260 * 2).TrimEnd(new char[] { '\0', ' ' });
                }
            }
        }


        fixed byte dacType[260 * 2];
        public string DacType
        {
            get
            {
                fixed (byte* buf = dacType)
                {
                    return Encoding.Unicode.GetString(buf, 260 * 2).TrimEnd(new char[] { '\0', ' ' });
                }
            }
        }


        fixed byte chipType[260 * 2];
        public string ChipType
        {
            get
            {
                fixed (byte* buf = chipType)
                {
                    return Encoding.Unicode.GetString(buf, 260 * 2).TrimEnd(new char[] { '\0', ' ' });
                }
            }
        }
    }
}
