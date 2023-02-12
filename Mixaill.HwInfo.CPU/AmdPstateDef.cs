using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;

namespace Mixaill.HwInfo.CPU
{


    /// <summary>
    /// MSR C001_006[4...B]
    /// P-State [7:0]
    /// Core::X86::Msr::PStateDef
    /// R/W
    /// Each of these registers specify the frequency and voltage associated with each of the core P-states.
    /// The CpuVid field in these registers is required to be programmed to the same value in all cores of a processor, but are
    /// allowed to be different between processors in a multi-processor system.All other fields in these registers are required to
    /// be programmed to the same value in each core of the coherent fabric
    /// </summary>
    public struct AmdPstateDef
    {
        /// <summary>
        /// CpuFid[7:0]: core frequency ID. 
        /// Read-write. Reset: XXh. 
        /// Specifies the core frequency multiplier. The core COF is a function of CpuFid and CpuDid, and defined by CoreCOF.
        public uint CpuFid;

        /// <summary>
        /// CpuDfsId: core divisor ID. 
        /// Read-write. Reset: XXXXXXb. 
        /// Specifies the core frequency divisor; see CpuFid. For values[1Ah:08h], 1/8th integer divide steps supported down to VCO/3.25 (Note, L3/L2 fifo logic related to 
        /// 4-cycle data heads-up requires core to be 1/3 of L3 frequency or higher). For values[30h:1Ch], 1/4th integer
        /// divide steps supported down to VCO/6 (DID[0] should zero if DID[5:0]>1Ah). (Note, core and L3 frequencies
        /// below 400MHz are not supported by the architecture). Core supports DID up to 30h, but L3 must be 2Ch (VCO/5.5) or less.
        /// </summary>
        public uint CpuDfsId;

        /// <summary>
        /// CpuVid[7:0]: core VID. 
        /// Read-write. Reset: XXXXXXXXb
        /// </summary>
        public uint CpuVid;

        /// <summary>
        /// IddValue: current value. 
        /// Read-write. Reset: XXXXXXXXb. 
        /// After a reset, IddDiv and IddValue combine to specify the expected maximum current dissipation of a single core that is in the 
        /// P-state corresponding to the MSR number. These values are intended to be used to create ACPI-defined _PSS objects. 
        /// The values are expressed in amps; they are not intended to convey final product power levels; they may not match the power levels specified 
        /// in the Power and Thermal Datasheet
        /// </summary>
        public uint IddValue;

        /// <summary>
        /// IddDiv: current divisor.
        /// Read-write. Reset: XXb. 
        /// See IddValue
        /// </summary>
        public uint IddDiv;

        /// <summary>
        /// PstateEn. Read-write. Reset: X. 0=The P-state specified by this MSR is not valid. 1=The P-state specified by this
        /// MSR is valid.The purpose of this register is to indicate if the rest of the P-state information in the register is valid
        /// after a reset; it controls no hardware
        /// </summary>
        public bool PstateEn;

      
        /// <summary>
        /// Core current operating frequency in MHz.
        /// CoreCOF = (Core::X86::Msr::PStateDef[CpuFid[7:0]] / Core::X86::Msr::PStateDef[CpuDfsId]) * 200.
        /// </summary>
        /// <returns></returns>
        public double CoreCOF => 200.0 * CpuFid / CpuDfsId;

        public double CoreMultiplier => 2.0 * CpuFid / CpuDfsId;

        /// <summary>
        /// Core power in Watts
        /// </summary>
        public double CorePower => (1_550_000 - (6_250 * CpuVid)) / 10 * IddValue / Math.Pow(10, 2 + IddDiv) / 1_000;

        /// <summary>
        /// Core voltage in Volts
        /// </summary>
        public double CoreVoltage => (1_550_000 - (6_250 * CpuVid)) / 1_000_000.0;
    }
}
