using Mixaill.HwInfo.Common;

namespace Mixaill.HwInfo.CPU.AMD.SMN
{
    public struct SmnZenCof
    {
        /// <summary>
        /// Max Boost ration in 0.25 of multiplier
        /// </summary>
        public uint BoostRatio;

        /// <summary>
        /// 
        /// </summary>
        public uint MinRatio;

        public uint CoreCof => BoostRatio * 100 / 4;

        public SmnZenCof(uint reg)
        {
            BoostRatio = reg.GetValue(17, 8);
            MinRatio = reg.GetValue(25, 7);
        }
    }
}
