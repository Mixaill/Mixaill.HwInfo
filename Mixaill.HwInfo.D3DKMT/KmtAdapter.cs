﻿// Copyright 2021, Mikhail Paulyshka
// SPDX-License-Identifier: MIT

using System;

using Microsoft.Extensions.Logging;

namespace Mixaill.HwInfo.D3DKMT
{
    public class KmtAdapter : IDisposable
    {
        #region properties

        public Interop._D3DKMT_ADAPTERINFO AdapterInfo;
        
        public Interop._D3DKMT_ADAPTERREGISTRYINFO AdapterRegistryInfo;
        
        public Interop._D3DKMT_DEVICE_IDS DeviceIds;
        
        public Interop._D3DKMT_WDDM_2_7_CAPS WddmCapabilities_27;

        public bool Initialized { get; private set; } = false;

        public uint PhysicalAdapterIndex { get; private set; } = 0;

        private readonly ILogger _logger = null;
        #endregion

        public KmtAdapter(Interop._D3DKMT_ADAPTERINFO AdapterInfo, ILogger logger)
        {
            _logger = logger;
            init(AdapterInfo);
        }
        public KmtAdapter(Interop._D3DKMT_ADAPTERINFO AdapterInfo, ILogger<KmtAdapter> logger)
        {
            _logger = logger;
            init(AdapterInfo);
        }

        private void init(Interop._D3DKMT_ADAPTERINFO AdapterInfo)
        {
            this.AdapterInfo = AdapterInfo;

            try
            {
                getRegistryInfo();
                getDeviceIds();
                getWddm27();
            }
            catch (EntryPointNotFoundException ex)
            {
                _logger?.LogWarning($"function not found: {ex.TargetSite.Name}");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "unkown error");
            }

            Initialized = true;
        }

        private unsafe bool getRegistryInfo()
        {
            bool result = false;

            fixed (void* buf = &AdapterRegistryInfo)
            {
                var adapterQuery = new Interop._D3DKMT_QUERYADAPTERINFO()
                {
                    hAdapter = AdapterInfo.hAdapter,
                    Type = Interop._KMTQUERYADAPTERINFOTYPE.KMTQAITYPE_ADAPTERREGISTRYINFO,
                    pPrivateDriverData = (IntPtr)buf,
                    PrivateDriverDataSize = (uint)sizeof(Interop._D3DKMT_ADAPTERREGISTRYINFO)
                };

                result = Interop.Gdi.D3DKMTQueryAdapterInfo(ref adapterQuery) == Interop.NtStatus.STATUS_SUCCESS;
            }

            return result;
        }

        private unsafe bool getDeviceIds()
        {
            Interop._D3DKMT_QUERY_DEVICE_IDS deviceIdsQuery;
            var adapterQuery = new Interop._D3DKMT_QUERYADAPTERINFO()
            {
                hAdapter = AdapterInfo.hAdapter,
                Type = Interop._KMTQUERYADAPTERINFOTYPE.KMTQAITYPE_PHYSICALADAPTERDEVICEIDS,
                pPrivateDriverData = (IntPtr)(&deviceIdsQuery),
                PrivateDriverDataSize = (uint)sizeof(Interop._D3DKMT_QUERY_DEVICE_IDS)
            };

            var result = Interop.Gdi.D3DKMTQueryAdapterInfo(ref adapterQuery) == Interop.NtStatus.STATUS_SUCCESS;
            if (result)
            {
                DeviceIds = deviceIdsQuery.DeviceIds;
                PhysicalAdapterIndex = deviceIdsQuery.PhysicalAdapterIndex;
            }


            return result;
        }

        private unsafe bool getWddm27()
        {
            bool result = false;

            fixed (void* buf = &WddmCapabilities_27) {
                var adapterQuery = new Interop._D3DKMT_QUERYADAPTERINFO()
                {
                    hAdapter = AdapterInfo.hAdapter,
                    Type = Interop._KMTQUERYADAPTERINFOTYPE.KMTQAITYPE_WDDM_2_7_CAPS,
                    pPrivateDriverData = (IntPtr)buf,
                    PrivateDriverDataSize = (uint)sizeof(Interop._D3DKMT_WDDM_2_7_CAPS)
                };

                result = Interop.Gdi.D3DKMTQueryAdapterInfo(ref adapterQuery) == Interop.NtStatus.STATUS_SUCCESS;
            }

            return result;
        }

        private bool closeAdapter()
        {
            Interop._D3DKMT_CLOSEADAPTER closeAdaperStruct;
            closeAdaperStruct.hAdapter = AdapterInfo.hAdapter;

            return Interop.Gdi.D3DKMTCloseAdapter(ref closeAdaperStruct) == Interop.NtStatus.STATUS_SUCCESS;
        }

        #region IDisposable

        private bool disposedValue;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                }


                closeAdapter();
                disposedValue = true;
            }
        }

        ~KmtAdapter()
        {
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}