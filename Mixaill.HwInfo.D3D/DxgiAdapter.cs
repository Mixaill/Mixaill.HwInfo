using System;

using Silk.NET.Core.Native;
using Silk.NET.DXGI;

namespace Mixaill.HwInfo.D3D
{
    public class DxgiAdapter : IDisposable
    {
        #region Properties
        
        public string Description => GetDescription();
        public uint VendorId => description.VendorId;
        public uint DeviceId => description.DeviceId;
        public uint SubsystemId => description.SubSysId;
        public uint Revision => description.Revision;
        public nuint DedicatedVideoMemory => description.DedicatedVideoMemory;
        public nuint DedicatedSystemMemory => description.DedicatedSystemMemory;
        public nuint SharedSystemMemory => description.SharedSystemMemory;
        public Luid AdapterLuid => description.AdapterLuid;
        public uint Flags => description.Flags;
        
        #endregion

        #region COM
        
        ComPtr<IDXGIAdapter1> com_object = default;
        AdapterDesc1 description = default;
        
        #endregion

        public unsafe DxgiAdapter(ComPtr<IDXGIAdapter1> dxgiAdapter)
        {
            com_object = dxgiAdapter;
            com_object.Get().GetDesc1(ref description);
        }

        private unsafe string GetDescription()
        {
            fixed (char* bytes = description.Description)
            {
                return new string(bytes);
            }
        }


        #region IDisposable

        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    com_object.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
