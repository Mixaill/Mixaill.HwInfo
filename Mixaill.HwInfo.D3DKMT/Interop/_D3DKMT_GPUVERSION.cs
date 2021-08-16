using System.Runtime.InteropServices;
using System.Text;

namespace Mixaill.HwInfo.D3DKMT.Interop
{
    /// <summary>
    /// 
    /// Reference: d3dkmthk.h:_D3DKMT_GPUVERSION
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct _D3DKMT_GPUVERSION
    {
        /// <summary>
        /// Input: The physical adapter index, in an LDA chain
        /// </summary>
        public uint PhysicalAdapterIndex;


        /// <summary>
        /// The gpu bios version
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string BiosVersion;


        /// <summary>
        /// The gpu architectures name.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string GpuArchitecture;

    }
}
