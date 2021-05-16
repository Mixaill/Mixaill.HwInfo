// Copyright 2021, Mikhail Paulyshka
// SPDX-License-Identifier: MIT

using System;

using Microsoft.Extensions.Logging;

namespace Mixaill.HwInfo.D3DKMT
{
    public class KmtAdapter : IDisposable
    {
        #region Properties/General

        public bool Initialized { get; private set; } = false;

        private readonly ILogger _logger = null;

        #endregion


        #region Properties/D3DKMT

        public Interop._D3DKMT_ADAPTERINFO AdapterInfo;

        public Interop._D3DKMT_ADAPTERREGISTRYINFO AdapterRegistryInfo;

        public Interop._D3DKMT_ADAPTERTYPE AdapterType;

        public Interop._D3DKMT_QUERY_DEVICE_IDS DeviceIds;

        public Interop._D3DKMT_DRIVERVERSION DriverVersion = Interop._D3DKMT_DRIVERVERSION.KMT_DRIVERVERSION_WDDM_1_0;

        public Interop._D3DKMT_ADAPTER_PERFDATACAPS PerformanceDataCapabilities;

        public Interop._D3DKMT_WDDM_2_7_CAPS WddmCapabilities_27;

        #endregion


        #region Ctor
        
        public KmtAdapter(Interop._D3DKMT_ADAPTERINFO AdapterInfo, ILogger logger)
        {
            _logger = logger;
            initialize(AdapterInfo);
        }
        public KmtAdapter(Interop._D3DKMT_ADAPTERINFO AdapterInfo, ILogger<KmtAdapter> logger)
        {
            _logger = logger;
            initialize(AdapterInfo);
        }

        private void initialize(Interop._D3DKMT_ADAPTERINFO AdapterInfo)
        {
            this.AdapterInfo = AdapterInfo;

            try
            {
                AdapterRegistryInfo = getAdapterRegistryInfo();
                DriverVersion = getDriverVersion();
                AdapterType = getAdapterType();
                DeviceIds = getDeviceIds();
                PerformanceDataCapabilities = getPerformanceDataCapabilities();
                WddmCapabilities_27 = getWddmCapabilities27();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "unknown error");
                return;
            }

            Initialized = true;
        }

        #endregion


        #region D3DKMT/Adapter Disposing

        private bool adapterClose()
        {
            Interop._D3DKMT_CLOSEADAPTER closeAdaperStruct;
            closeAdaperStruct.hAdapter = AdapterInfo.hAdapter;
            return Interop.Gdi.D3DKMTCloseAdapter(ref closeAdaperStruct) == Interop.NtStatus.STATUS_SUCCESS;
        }

        #endregion


        #region D3DKMT/Query Info

        //ID 8
        private unsafe Interop._D3DKMT_ADAPTERREGISTRYINFO getAdapterRegistryInfo()
        {
            var result = new Interop._D3DKMT_ADAPTERREGISTRYINFO();
            if (DriverVersion >= Interop._D3DKMT_DRIVERVERSION.KMT_DRIVERVERSION_WDDM_1_0)
            {
                queryAdapterInfo(Interop._D3DKMT_QUERYADAPTERINFOTYPE.KMTQAITYPE_ADAPTERREGISTRYINFO, (IntPtr)(&result), sizeof(Interop._D3DKMT_ADAPTERREGISTRYINFO));
            }
            return result;
        }

        //ID 13
        private unsafe Interop._D3DKMT_DRIVERVERSION getDriverVersion()
        {
            var result = new Interop._D3DKMT_DRIVERVERSION();
            if (DriverVersion >= Interop._D3DKMT_DRIVERVERSION.KMT_DRIVERVERSION_WDDM_1_0)
            {
                queryAdapterInfo(Interop._D3DKMT_QUERYADAPTERINFOTYPE.KMTQAITYPE_DRIVERVERSION, (IntPtr)(&result), sizeof(Interop._D3DKMT_DRIVERVERSION));
            }
            return result;
        }

        //ID 15
        private unsafe Interop._D3DKMT_ADAPTERTYPE getAdapterType()
        {
            var result = new Interop._D3DKMT_ADAPTERTYPE();
            if (DriverVersion >= Interop._D3DKMT_DRIVERVERSION.KMT_DRIVERVERSION_WDDM_1_2)
            {
                queryAdapterInfo(Interop._D3DKMT_QUERYADAPTERINFOTYPE.KMTQAITYPE_ADAPTERTYPE, (IntPtr)(&result), sizeof(Interop._D3DKMT_ADAPTERTYPE));
            }
            return result;
        }

        //ID 31
        private unsafe Interop._D3DKMT_QUERY_DEVICE_IDS getDeviceIds()
        {
            var result = new Interop._D3DKMT_QUERY_DEVICE_IDS();
            if (DriverVersion >= Interop._D3DKMT_DRIVERVERSION.KMT_DRIVERVERSION_WDDM_2_0)
            {
                queryAdapterInfo(Interop._D3DKMT_QUERYADAPTERINFOTYPE.KMTQAITYPE_PHYSICALADAPTERDEVICEIDS, (IntPtr)(&result), sizeof(Interop._D3DKMT_QUERY_DEVICE_IDS));
            }
            return result;
        }

        //ID 62
        public unsafe (bool, Interop._D3DKMT_ADAPTER_PERFDATA) GetPerformanceData()
        {
            var result_bool = false;
            var result_struct = new Interop._D3DKMT_ADAPTER_PERFDATA();

            var s = sizeof(Interop._D3DKMT_ADAPTER_PERFDATA);
            if (DriverVersion >= Interop._D3DKMT_DRIVERVERSION.KMT_DRIVERVERSION_WDDM_2_4)
            {
                result_bool = queryAdapterInfo(Interop._D3DKMT_QUERYADAPTERINFOTYPE.KMTQAITYPE_ADAPTERPERFDATA, (IntPtr)(&result_struct), sizeof(Interop._D3DKMT_ADAPTER_PERFDATA));
            }

            return (result_bool, result_struct);
        }

        //ID 63
        private unsafe Interop._D3DKMT_ADAPTER_PERFDATACAPS getPerformanceDataCapabilities()
        {
            var result = new Interop._D3DKMT_ADAPTER_PERFDATACAPS();
            if (DriverVersion >= Interop._D3DKMT_DRIVERVERSION.KMT_DRIVERVERSION_WDDM_2_4)
            {
                queryAdapterInfo(Interop._D3DKMT_QUERYADAPTERINFOTYPE.KMTQAITYPE_ADAPTERPERFDATA_CAPS, (IntPtr)(&result), sizeof(Interop._D3DKMT_ADAPTER_PERFDATACAPS));
            }
            return result;
        }

        //ID 70
        private unsafe Interop._D3DKMT_WDDM_2_7_CAPS getWddmCapabilities27()
        {
            var result = new Interop._D3DKMT_WDDM_2_7_CAPS();
            if (DriverVersion >= Interop._D3DKMT_DRIVERVERSION.KMT_DRIVERVERSION_WDDM_2_7)
            {
                queryAdapterInfo(Interop._D3DKMT_QUERYADAPTERINFOTYPE.KMTQAITYPE_WDDM_2_7_CAPS, (IntPtr)(&result), sizeof(Interop._D3DKMT_WDDM_2_7_CAPS));
            }
            return result;
        }

        private unsafe bool queryAdapterInfo(Interop._D3DKMT_QUERYADAPTERINFOTYPE requestType, IntPtr buf, int size)
        {
            var adapterQuery = new Interop._D3DKMT_QUERYADAPTERINFO()
            {
                hAdapter = AdapterInfo.hAdapter,
                Type = requestType,
                pPrivateDriverData = buf,
                PrivateDriverDataSize = (uint)size
            };

            var result = Interop.NtStatus.STATUS_SUCCESS;

            try
            {
                result = Interop.Gdi.D3DKMTQueryAdapterInfo(ref adapterQuery);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"failed to get adapterInfo, hAdapter={AdapterInfo.hAdapter}, requestType={requestType}");
                return false;
            }

            switch (result)
            {
                case Interop.NtStatus.STATUS_SUCCESS:
                    return true;
                case Interop.NtStatus.STATUS_OBJECT_NAME_NOT_FOUND:
                    return false;
                default:
                    _logger?.LogWarning($"failed to get adapterInfo, hAdapter=0x{AdapterInfo.hAdapter:X8}, requestType={requestType}, result={result}");
                    return false;
            }
        }

        #endregion


        #region IDisposable

        private bool disposedValue;
        
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing){}

                adapterClose();
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