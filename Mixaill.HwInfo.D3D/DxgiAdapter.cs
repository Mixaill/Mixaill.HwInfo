// Copyright 2022, Mikhail Paulyshka
// SPDX-License-Identifier: MIT

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
        public ComPtr<IDXGIAdapter1> ComObject { get; } = default;
        public ComPtr<IDXGIAdapter3> ComObject3 { get; } = default;

        AdapterDesc1 description = default;

        #endregion

        public unsafe DxgiAdapter(ComPtr<IDXGIAdapter1> dxgiAdapter)
        {
            ComObject = dxgiAdapter;
            ComObject.Get().GetDesc1(ref description);

            //try to get IDXGIAdapter3
            var guid = IDXGIAdapter1.Guid;
            ComPtr<IDXGIAdapter3> tempObject = default;
            ComObject.Get().QueryInterface(ref guid, (void**)tempObject.GetAddressOf());
            ComObject3 = tempObject;
        }

        public QueryVideoMemoryInfo GetVideoMemoryInfo(MemorySegmentGroup segmentGroup)
        {
            QueryVideoMemoryInfo qvmi = default;
            ComObject3.Get().QueryVideoMemoryInfo(0U, segmentGroup, ref qvmi);
            return qvmi;
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
                    ComObject3.Dispose();
                    ComObject.Dispose();
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
