// Copyright 2022, Mikhail Paulyshka
// SPDX-License-Identifier: MIT

using System;
using System.Collections.Generic;
using System.Text;

using Silk.NET.Core.Native;
using Silk.NET.Direct3D12;
using Silk.NET.DXGI;

namespace Mixaill.HwInfo.D3D
{
    public class D3D12Device : IDisposable
    {
        #region Properties

        public Luid DeviceLuid => ComObject.Get().GetAdapterLuid();
        
        #endregion

        #region COM

        public ComPtr<ID3D12Device> ComObject { get; }

        #endregion

        public unsafe D3D12Device(ComPtr<ID3D12Device> d3d12device)
        {
            ComObject = d3d12device;
        }

        #region IDisposable

        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
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
