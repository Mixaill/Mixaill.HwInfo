using System.Runtime.InteropServices;

using Mixaill.HwInfo.D3DKMT.Helpers;
using System;
using System.Collections.Generic;

using System.Text;

namespace Mixaill.HwInfo.D3DKMT.Interop
{
    [Flags]
    public enum _D3DDDI_ESCAPEFLAGS : uint
    {
        None = 0,

        //WDDM 1.0
        HardwareAccess = 1 << 0,

        //WDDM 1.3
        DeviceStatusQuery = 1 << 1,
        ChangeFrameLatency = 1 << 2,

        //WDDM 2.0
        NoAdapterSynchronization = 1 << 3,

        //WDDM 2.2
        VirtualMachineData = 1 << 5,

        //WDDM 2.5
        DriverKnownEscape = 1 << 6,
        DriverCommonEscape = 1 << 7
    }
}
