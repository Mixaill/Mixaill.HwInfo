using System;
using System.Collections.Generic;
using System.Text;

namespace Mixaill.HwInfo.CPU.AMD
{
    public enum AmdCpuUarch
    {
        Unknown,

        /// <summary>
        /// 17h 01h
        /// </summary>
        Naples,

        /// <summary>
        /// 17h 31h
        /// </summary>
        Rome,

        /// <summary>
        /// 17h 60h
        /// </summary>
        Renoir,

        /// <summary>
        /// 17h 71h
        /// </summary>
        Matisse,

        /// <summary>
        /// 19h 01h
        /// </summary>
        Milan,

        /// <summary>
        /// 19h 21h
        /// </summary>
        Vermeer,

        /// <summary>
        /// 19h 40h
        /// </summary>
        Rembrandt,

        /// <summary>
        /// 19h 50h
        /// </summary>
        Cezanne,

        /// <summary>
        /// 19h 60h
        /// </summary>
        Raphael,
    }
}
