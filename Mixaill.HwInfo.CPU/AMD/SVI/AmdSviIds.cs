using System;
using System.Collections.Generic;
using System.Text;

namespace Mixaill.HwInfo.CPU.AMD.SVI
{
    /// <summary>
    /// Sources:
    /// * https://github.com/torvalds/linux/commit/f707bcb5d1cb4c47d27c688c859dcdb70e3c7065
    /// * https://github.com/Ta180m/zenpower3/blob/master/zenpower.c
    /// * https://github.com/irusanov/ZenStates-Core/blob/master/Constants.cs
    /// </summary>
    public enum AmdSviIds: uint
    {
        UNKNOWN = 0x0,

        ZEN_SVI_BASE   = 0x0005A000,
        ZEN_SVI_BASE_2 = 0x0006F000,

        //Naples
        F17H_M01H_SVI_TEL_PLANE0 = (ZEN_SVI_BASE + 0x0C),
        F17H_M01H_SVI_TEL_PLANE1 = (ZEN_SVI_BASE + 0x10),

        //Rome
        F17H_M31H_SVI_TEL_PLANE0 = (ZEN_SVI_BASE + 0x14),
        F17H_M31H_SVI_TEL_PLANE1 = (ZEN_SVI_BASE + 0x10),

        //Renoir
        F17H_M60H_SVI_TEL_PLANE0 = (ZEN_SVI_BASE_2 + 0x38),
        F17H_M60H_SVI_TEL_PLANE1 = (ZEN_SVI_BASE_2 + 0x3C),

        //Matisse
        F17H_M71H_SVI_TEL_PLANE0 = (ZEN_SVI_BASE + 0x10),
        F17H_M71H_SVI_TEL_PLANE1 = (ZEN_SVI_BASE + 0x0C),

        //VanGogh
        F17H_M90H_SVI_TEL_PLANE0 = (ZEN_SVI_BASE_2 + 0x38),
        F17H_M90H_SVI_TEL_PLANE1 = (ZEN_SVI_BASE_2 + 0x3C),

        //Milan
        F19H_M01H_SVI_TEL_PLANE0 = (ZEN_SVI_BASE + 0x10),
        F19H_M01H_SVI_TEL_PLANE1 = (ZEN_SVI_BASE + 0x14),

        //Vermeer
        F19H_M21H_SVI_TEL_PLANE0 = (ZEN_SVI_BASE + 0x10),
        F19H_M21H_SVI_TEL_PLANE1 = (ZEN_SVI_BASE + 0x0C),

        //Cezanne
        F19H_M50H_SVI_TEL_PLANE0 = (ZEN_SVI_BASE_2 + 0x38),
        F19H_M50H_SVI_TEL_PLANE1 = (ZEN_SVI_BASE_2 + 0x3C),
    }
}
