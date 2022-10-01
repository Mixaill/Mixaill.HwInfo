namespace Mixaill.HwInfo.D3D.Interop
{
    /// <summary>
    /// Reference: d3dkmthk.h::_D3DKMT_QUERYSTATISTICS_SEGMENT_TYPE
    /// </summary>
    public enum _D3DKMT_QUERYSTATISTICS_SEGMENT_TYPE : uint
    {
        D3DKMT_QUERYSTATISTICS_SEGMENT_TYPE_APERTURE = 0,
        D3DKMT_QUERYSTATISTICS_SEGMENT_TYPE_MEMORY = 1,
        D3DKMT_QUERYSTATISTICS_SEGMENT_TYPE_SYSMEM = 2
    }
}
