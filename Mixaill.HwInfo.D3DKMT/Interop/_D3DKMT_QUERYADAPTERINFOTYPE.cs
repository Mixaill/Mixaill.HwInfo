// Copyright 2021, Mikhail Paulyshka
// SPDX-License-Identifier: MIT

namespace Mixaill.HwInfo.D3DKMT.Interop
{
    public enum _D3DKMT_QUERYADAPTERINFOTYPE
    {
        //WDDM 1.0
        KMTQAITYPE_ADAPTERREGISTRYINFO      = 8 ,
        KMTQAITYPE_DRIVERVERSION            = 13,

        //WDDM 1.2
        KMTQAITYPE_ADAPTERTYPE              = 15,

        //WDDM 2.0
        KMTQAITYPE_PHYSICALADAPTERDEVICEIDS = 31,

        //WDDM 2.4
        KMTQAITYPE_ADAPTERPERFDATA          = 62,
        KMTQAITYPE_ADAPTERPERFDATA_CAPS     = 63,

        //WDDM 2.7
        KMTQAITYPE_WDDM_2_7_CAPS            = 70,
    }
}

