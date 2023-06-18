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
        /// CPUID: 19h 50h
        /// 
        /// </summary>
        Cezanne,

        /// <summary>
        /// CPUID: 19h 61h
        /// PPR  : 56713 ( https://www.amd.com/system/files/TechDocs/56713-B1_3.05.zip )
        /// </summary>
        Raphael,
    }
}
