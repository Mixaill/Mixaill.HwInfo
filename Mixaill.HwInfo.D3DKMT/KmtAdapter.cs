// Copyright 2021, Mikhail Paulyshka
// SPDX-License-Identifier: MIT

using System;
using System.Runtime.InteropServices;
using Microsoft.Extensions.Logging;
using Mixaill.HwInfo.D3DKMT.Helpers;

namespace Mixaill.HwInfo.D3DKMT
{
    public class KmtAdapter : IDisposable
    {
        #region Properties/General

        private readonly ILogger _logger = null;

        #endregion


        #region Properties/D3DKMT

        public Interop._D3DKMT_ADAPTERINFO AdapterInfo;

        public uint PhysicalAdapterIndex { get; } = 0;

        public Interop._D3DKMT_ADAPTERREGISTRYINFO AdapterRegistryInfo => getAdapterRegistryInfo();

        public Interop._D3DKMT_ADAPTERTYPE AdapterType => getAdapterType();

        public Interop._D3DKMT_WDDM_2_0_CAPS WddmCapabilities_20 => getWddmCapabilities20();

        public Interop._D3DKMT_CPDRIVERNAME ContentProtectionDriverName => getCPDriverName();

        public Interop._D3DKMT_DEVICE_IDS DeviceIds => getDeviceIds();

        public Interop._QAI_DRIVERVERSION DriverVersion => getDriverVersion();

        public Interop._D3DKMT_GPUMMU_CAPS GpuMmuCapabilities => getGpuMmuCapabilities();

        public Interop._D3DKMT_ADAPTER_PERFDATA PerformanceData => getPerformanceData();

        public Interop._D3DKMT_ADAPTER_PERFDATACAPS PerformanceDataCapabilities => getPerformanceDataCapabilities();

        public Interop._D3DKMT_GPUVERSION GpuVersion => getGpuVersion();

        public Interop._D3DKMT_WDDM_2_7_CAPS WddmCapabilities_27 => getWddmCapabilities27();

        #endregion


        #region Ctor
        
        public KmtAdapter(Interop._D3DKMT_ADAPTERINFO AdapterInfo, ILogger logger)
        {
            _logger = logger;
            this.AdapterInfo = AdapterInfo;
            this.PhysicalAdapterIndex = 0;
        }

        public KmtAdapter(Interop._D3DKMT_ADAPTERINFO AdapterInfo, ILogger<KmtAdapter> logger)
        {
            _logger = logger;
            this.AdapterInfo = AdapterInfo;
            this.PhysicalAdapterIndex = 0;
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
        private Interop._D3DKMT_ADAPTERREGISTRYINFO getAdapterRegistryInfo()
        {
            return queryAdapterInfo<Interop._D3DKMT_ADAPTERREGISTRYINFO>(Interop._KMTQUERYADAPTERINFOTYPE.KMTQAITYPE_ADAPTERREGISTRYINFO);
        }

        //ID 13
        private Interop._QAI_DRIVERVERSION getDriverVersion()
        {
            return queryAdapterInfo<Interop._QAI_DRIVERVERSION>(Interop._KMTQUERYADAPTERINFOTYPE.KMTQAITYPE_DRIVERVERSION);
        }

        //ID 15
        private Interop._D3DKMT_ADAPTERTYPE getAdapterType()
        {
            return queryAdapterInfo<Interop._D3DKMT_ADAPTERTYPE>(Interop._KMTQUERYADAPTERINFOTYPE.KMTQAITYPE_ADAPTERTYPE);
        }

        //ID 24
        private Interop._D3DKMT_WDDM_2_0_CAPS getWddmCapabilities20()
        {
            if (DriverVersion >= Interop._QAI_DRIVERVERSION.KMT_DRIVERVERSION_WDDM_2_0)
            {
                return queryAdapterInfo<Interop._D3DKMT_WDDM_2_0_CAPS>(Interop._KMTQUERYADAPTERINFOTYPE.KMTQAITYPE_WDDM_2_0_CAPS);
            }
            return new Interop._D3DKMT_WDDM_2_0_CAPS();
        }

        //ID 26
        private Interop._D3DKMT_CPDRIVERNAME getCPDriverName()
        {
            if (DriverVersion >= Interop._QAI_DRIVERVERSION.KMT_DRIVERVERSION_WDDM_2_0)
            {
                return queryAdapterInfo<Interop._D3DKMT_CPDRIVERNAME>(Interop._KMTQUERYADAPTERINFOTYPE.KMTQAITYPE_CPDRIVERNAME);
            }
            return new Interop._D3DKMT_CPDRIVERNAME();
        }

        //ID 31
        private Interop._D3DKMT_DEVICE_IDS getDeviceIds()
        {
            var result = new Interop._D3DKMT_QUERY_DEVICE_IDS() { PhysicalAdapterIndex = this.PhysicalAdapterIndex };
            if (DriverVersion >= Interop._QAI_DRIVERVERSION.KMT_DRIVERVERSION_WDDM_2_0)
            {
                queryAdapterInfo(Interop._KMTQUERYADAPTERINFOTYPE.KMTQAITYPE_PHYSICALADAPTERDEVICEIDS, ref result);
            }

            return result.DeviceIds;
        }

        //ID 34
        private Interop._D3DKMT_GPUMMU_CAPS getGpuMmuCapabilities()
        {
            var result = new Interop._D3DKMT_QUERY_GPUMMU_CAPS() { PhysicalAdapterIndex = this.PhysicalAdapterIndex };
            if (DriverVersion >= Interop._QAI_DRIVERVERSION.KMT_DRIVERVERSION_WDDM_2_0)
            {
                queryAdapterInfo(Interop._KMTQUERYADAPTERINFOTYPE.KMTQAITYPE_QUERY_GPUMMU_CAPS, ref result);
            }
            return result.Caps;
        }


        //ID 62
        private Interop._D3DKMT_ADAPTER_PERFDATA getPerformanceData()
        {
            var result = new Interop._D3DKMT_ADAPTER_PERFDATA() { PhysicalAdapterIndex = this.PhysicalAdapterIndex };

            if (DriverVersion >= Interop._QAI_DRIVERVERSION.KMT_DRIVERVERSION_WDDM_2_4)
            {
                queryAdapterInfo(Interop._KMTQUERYADAPTERINFOTYPE.KMTQAITYPE_ADAPTERPERFDATA, ref result);
            }

            return result;
        }

        //ID 63
        private Interop._D3DKMT_ADAPTER_PERFDATACAPS getPerformanceDataCapabilities()
        {
            var result = new Interop._D3DKMT_ADAPTER_PERFDATACAPS() { PhysicalAdapterIndex = this.PhysicalAdapterIndex };
            if (DriverVersion >= Interop._QAI_DRIVERVERSION.KMT_DRIVERVERSION_WDDM_2_4)
            {
                queryAdapterInfo(Interop._KMTQUERYADAPTERINFOTYPE.KMTQAITYPE_ADAPTERPERFDATA_CAPS, ref result);
            }
            return result;
        }

        //ID 64
        private Interop._D3DKMT_GPUVERSION getGpuVersion()
        {
            var result = new Interop._D3DKMT_GPUVERSION() { PhysicalAdapterIndex = this.PhysicalAdapterIndex };
            if (DriverVersion >= Interop._QAI_DRIVERVERSION.KMT_DRIVERVERSION_WDDM_2_4)
            {
                queryAdapterInfo(Interop._KMTQUERYADAPTERINFOTYPE.KMTQUITYPE_GPUVERSION, ref result);
            }
            return result;
        }

        //ID 70
        private Interop._D3DKMT_WDDM_2_7_CAPS getWddmCapabilities27()
        {
            if (DriverVersion >= Interop._QAI_DRIVERVERSION.KMT_DRIVERVERSION_WDDM_2_7)
            {
                return queryAdapterInfo<Interop._D3DKMT_WDDM_2_7_CAPS>(Interop._KMTQUERYADAPTERINFOTYPE.KMTQAITYPE_WDDM_2_7_CAPS);
            }
            return new Interop._D3DKMT_WDDM_2_7_CAPS();
        }

        #region queryAdapterInfo

        private T queryAdapterInfo<T>(Interop._KMTQUERYADAPTERINFOTYPE requestType) where T: struct
        {
            int dataSize = 4;
            if (!typeof(T).IsEnum)
            {
                dataSize = Marshal.SizeOf<T>();
            }

            var dataBuf = Marshal.AllocHGlobal(dataSize);

            try
            {
                if (queryAdapterInfo(requestType, dataBuf, dataSize))
                {
                    return (T)InteropHelper.PointerToObj(typeof(T), dataBuf);
                }
            }
            finally
            {
                Marshal.FreeHGlobal(dataBuf);
            }

            return new T();
        }

        private void queryAdapterInfo<T>(Interop._KMTQUERYADAPTERINFOTYPE requestType, ref T requestStruct) where T: struct{
            var bufferPtr = InteropHelper.ObjToPointer(requestStruct);

            if(queryAdapterInfo(requestType, bufferPtr, Marshal.SizeOf<T>())){
                requestStruct = InteropHelper.PointerToObj<T>(bufferPtr);
                Marshal.FreeHGlobal(bufferPtr);
            }
        }

        private bool queryAdapterInfo(Interop._KMTQUERYADAPTERINFOTYPE requestType, IntPtr dataBuf, int dataSize)
        {
            var queryStruct = new Interop._D3DKMT_QUERYADAPTERINFO()
            {
                hAdapter = AdapterInfo.hAdapter,
                Type = requestType,
                pPrivateDriverData = dataBuf,
                PrivateDriverDataSize = (uint)dataSize
            };

            var queryResult = Interop.NtStatus.STATUS_SUCCESS;
            try
            {
                queryResult = Interop.Gdi.D3DKMTQueryAdapterInfo(ref queryStruct);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"failed to query adapterInfo, hAdapter={AdapterInfo.hAdapter}, requestType={requestType}");
            }

            if (queryResult != Interop.NtStatus.STATUS_SUCCESS)
            {
                _logger?.LogWarning($"failed to get adapterInfo, hAdapter=0x{AdapterInfo.hAdapter:X8}, requestType={requestType}, result={queryResult}");
            }

            return queryResult == Interop.NtStatus.STATUS_SUCCESS;
        }
        
        #endregion

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