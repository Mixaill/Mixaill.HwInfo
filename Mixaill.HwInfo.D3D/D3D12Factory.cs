// Copyright 2022, Mikhail Paulyshka
// SPDX-License-Identifier: MIT

using System;

using Microsoft.Extensions.Logging;

using Silk.NET.Core.Native;
using Silk.NET.Direct3D12;


namespace Mixaill.HwInfo.D3D
{
    public class D3D12Factory : IDisposable
    {
        #region Properties

        private readonly ILogger _logger = null;

        private D3D12 _d3d12 { get; set; } = D3D12.GetApi();

        #endregion

        public D3D12Factory()
        {
        }

        public D3D12Factory(ILogger logger)
        {
            _logger = logger;
        }

        public D3D12Factory(ILogger<D3D12Factory> logger)
        {
            _logger = logger;
        }


        #region Vulkan/EnumerateDevices

        public unsafe D3D12Device CreateDevice(DxgiAdapter dxgiAdapater)
        {
            D3D12Device result = null;

            ComPtr<ID3D12Device> device = default;

            var guid = ID3D12Device.Guid;
            if (_d3d12.CreateDevice((IUnknown*)dxgiAdapater.ComObject.GetPinnableReference(), D3DFeatureLevel.Level110, ref guid, (void**)device.GetAddressOf()) == 0)
            {
                result = new D3D12Device(device);
            }

            return result;
        }

        #endregion

        #region IDisposable

        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _d3d12.Dispose();
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
