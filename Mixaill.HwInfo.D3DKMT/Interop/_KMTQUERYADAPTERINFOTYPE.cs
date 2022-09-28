// Copyright 2021, Mikhail Paulyshka
// SPDX-License-Identifier: MIT

namespace Mixaill.HwInfo.D3DKMT.Interop
{
    /// <summary>
    /// 
    /// Reference: d3dkmthk.h::_KMTQUERYADAPTERINFOTYPE
    /// </summary>
    public enum _KMTQUERYADAPTERINFOTYPE: uint
    {
        //WDDM 1.0
        KMTQAITYPE_ADAPTERREGISTRYINFO      = 8 ,
        KMTQAITYPE_DRIVERVERSION            = 13,

        //WDDM 1.2
        KMTQAITYPE_ADAPTERTYPE              = 15,

        //WDDM 2.0
        KMTQAITYPE_WDDM_2_0_CAPS            = 24,
        KMTQAITYPE_CPDRIVERNAME             = 26,
        KMTQAITYPE_PHYSICALADAPTERDEVICEIDS = 31,
        KMTQAITYPE_QUERY_GPUMMU_CAPS        = 34,

        //WDDM 2.4
        KMTQAITYPE_ADAPTERPERFDATA          = 62,
        KMTQAITYPE_ADAPTERPERFDATA_CAPS     = 63,
        KMTQUITYPE_GPUVERSION               = 64,

        //WDDM 2.7
        KMTQAITYPE_WDDM_2_7_CAPS            = 70,

        //WDDM 2.9
        KMTQAITYPE_WDDM_2_9_CAPS            = 75,

        //WDDM 3.0
        KMTQAITYPE_WDDM_3_0_CAPS            = 77,

        //WDDM 3.1
        KMTQAITYPE_WDDM_3_1_CAPS            = 80,
    }
}

