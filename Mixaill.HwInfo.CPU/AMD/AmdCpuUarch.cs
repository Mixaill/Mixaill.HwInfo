using System;
using System.Collections.Generic;
using System.Text;

namespace Mixaill.HwInfo.CPU.AMD
{
    /// <summary>
    /// AMD CPU Uarch list
    /// 
    /// Sources:
    ///   * http://instlatx64.atw.hu/
    ///   * https://en.wikichip.org/wiki/amd/cpuid
    /// </summary>
    public enum AmdCpuUarch
    {
        Unknown,

        /// <summary>
        /// 17h 01h
        /// </summary>
        Naples,

        /// <summary>
        /// CPUID: 17h 31h
        /// PPR  : 55803 (https://www.amd.com/system/files/TechDocs/55803-ppr-family-17h-model-31h-b0-processors.pdf)
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
        /// CPUID   : 17h 90h
        /// PPR     : not available
        /// Products:
        ///   * AMD Custom APU 0405 (Valve Steam Deck)
        /// </summary>
        VanGogh,

        /// <summary>
        /// 19h 01h
        /// </summary>
        Milan,

        /// <summary>
        /// CPUID   : 19h 21h
        /// PPR     : 56214 ( https://www.amd.com/system/files/TechDocs/56214-B0-PUB.zip )
        /// Products:
        ///   * AMD Ryzen 9 5950X
        ///   * AMD Ryzen 9 5950X
        /// </summary>
        Vermeer,

        /// <summary>
        /// 19h 40h
        /// </summary>
        Rembrandt,

        /// <summary>
        /// CPUID: 19h 50h
        /// 
        /// </summary>
        Cezanne,

        /// <summary>
        /// CPUID   : 19h 61h
        /// PPR     : 56713 ( https://www.amd.com/system/files/TechDocs/56713-B1_3.05.zip )
        /// Products:
        ///   * AMD Ryzen 5 7600X
        ///   * AMD Ryzen 7 7700X
        ///   * AMD Ryzen 9 7900X
        ///   * AMD Ryzen 9 7900X3D
        ///   * AMD Ryzen 9 7950X
        /// </summary>
        Raphael,
    }
}
