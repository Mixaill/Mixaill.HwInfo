﻿// Copyright 2022, Mikhail Paulyshka
// SPDX-License-Identifier: MIT

using System;
using System.Collections.Generic;

using Microsoft.Extensions.Logging;

using Silk.NET.Core.Native;
using Silk.NET.DXGI;

namespace Mixaill.HwInfo.D3D
{
    public class DxgiFactory : IDisposable
    {
        #region Properties

        private readonly ILogger _logger = null;

        private DXGI _dxgi { get; set; } = DXGI.GetApi();

        #endregion

        public DxgiFactory()
        {
        }

        public DxgiFactory(ILogger logger)
        {
            _logger = logger;
        }

        public DxgiFactory(ILogger<DxgiFactory> logger)
        {
            _logger = logger;
        }


        public unsafe List<DxgiAdapter> GetAdapters()
        {
            var result = new List<DxgiAdapter>();

            var factoryiid = IDXGIFactory4.Guid;
            ComPtr<IDXGIFactory4> factory = default;

            if (_dxgi.CreateDXGIFactory2(0U, ref factoryiid, (void**)factory.GetAddressOf()) == 0)
            {

                uint adapter_index = 0;
                while (true)
                {
                    ComPtr<IDXGIAdapter1> adapter = default;
                    if (factory.Get().EnumAdapters1(adapter_index++, adapter.GetAddressOf()) != 0)
                    {
                        break;
                    }

                    result.Add(new DxgiAdapter(adapter));
                }
            }

            return result;
        }

        #region IDisposable

        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _dxgi.Dispose();
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
