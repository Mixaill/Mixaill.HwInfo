using System;
using System.Collections.Generic;
using System.Text;

namespace Mixaill.HwInfo.LowLevel
{
    /// <summary>
    /// PCI Code and ID Assignment Specification
    /// https://pcisig.com/sites/default/files/files/PCI_Code-ID_r_1_11__v24_Jan_2019.pdf
    /// 
    /// 1. Class Codes
    /// </summary>
    public enum PciClassCode : byte
    {
        Unclassified                      = 0x00,
        MassStorageController             = 0x01,
        NetworkController                 = 0x02,
        DisplayController                 = 0x03,
        MultimediaDevice                  = 0x04,
        MemoryController                  = 0x05,
        BridgeDevice                      = 0x06,
        SimpleCommunicationController     = 0x07,
        BaseSystemPeripherial             = 0x08,
        InputDevice                       = 0x09,
        DockingStation                    = 0x0A,
        Processor                         = 0x0B,
        SerialBusController               = 0x0C,
        WirelessController                = 0x0D,
        IntelligentIOController           = 0x0E,
        SatelliteCommunicationController  = 0x0F,
        EncryptionDecryptionController    = 0x10,
        DataAcquisitionDSPController      = 0x11,
        ProcessingAccelerator             = 0x12,
        NonEssentialInstrumentation       = 0x13,
        Unassigned                        = 0xFF
    }
}
