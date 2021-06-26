using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Mixaill.HwInfo.D3DKMT.Interop
{
    [StructLayout(LayoutKind.Sequential)]
    public struct _D3DKMT_ESCAPE
    {
        /// <summary>
        /// IN: adapter handle
        /// </summary>
        public uint hAdapter;

        /// <summary>
        /// IN: device handle (optional)
        /// </summary>
        public uint hDevice;

        /// <summary>
        /// IN: escape type
        /// </summary>
        public _D3DKMT_ESCAPETYPE Type;

        /// <summary>
        /// IN: flags
        /// </summary>
        public _D3DDDI_ESCAPEFLAGS Flags;

        /// <summary>
        /// IN/OUT: escape data
        /// </summary>
        public IntPtr pPrivateDriverData;

        /// <summary>
        /// IN: size of escape data
        /// </summary>
        public uint PrivateDriverDataSize;

        /// <summary>
        /// IN: context handle (optional)
        /// </summary>
        public uint hContext;
    }
}
