// Copyright 2021, Mikhail Paulyshka
// SPDX-License-Identifier: MIT

using System;
using System.Collections.Generic;
using System.ComponentModel;

using Microsoft.Extensions.Logging;
using Microsoft.Windows.Sdk;

using Mixaill.HwInfo.SetupApi.Defines;

namespace Mixaill.HwInfo.SetupApi
{
    public unsafe class DeviceInfoSet : IDisposable
    {
        #region Properties

        public Guid ClassGuid { get; private set; }        
        
        public List<DeviceInfo> Devices { get; } = new List<DeviceInfo>();

        private HANDLE _handle { get; set; }

        private readonly ILogger _logger = null;

        #endregion

        public DeviceInfoSet(Guid guid)
        {
            Initialize(guid);
        }

        public DeviceInfoSet(Guid guid, ILogger logger)
        {
            _logger = logger;
            Initialize(guid);
        }

        public DeviceInfoSet(Guid guid, ILogger<DeviceInfoSet> logger)
        {
            _logger = logger;
            Initialize(guid);
        }

        private unsafe void Initialize(Guid guid)
        {
            ClassGuid = guid;

            _handle = new HANDLE((IntPtr)PInvoke.SetupDiGetClassDevs(ClassGuid, null, new HWND((IntPtr)0), 0));
            if (_handle.Equals(Constants.INVALID_HANDLE_VALUE))
            {
                throw new Win32Exception();
            }


            uint idx = 0;
            SP_DEVINFO_DATA data;
            data.cbSize = (uint)sizeof(SP_DEVINFO_DATA);
            while (PInvoke.SetupDiEnumDeviceInfo((void*)_handle.Value, idx, out data))
            {
                uint status = 0;
                uint problem_number = 0;
                if(PInvoke.CM_Get_DevNode_Status(out status, out problem_number, data.DevInst, 0) == CONFIGRET.CR_SUCCESS)
                {
                    if ((status & (uint)DeviceNodeStatus.Started) == (uint)DeviceNodeStatus.Started)
                    {
                        Devices.Add(DeviceInfoFactory.Create(this, data));
                    }
                }

                idx++;
            }
        }

        internal unsafe DevicePropertyValue GetProperty(SP_DEVINFO_DATA devInfoData, DEVPROPKEY propKey)
        {
            DevicePropertyValue result = null;

            uint propType = 0;
            uint propRequiredSize = 0;

            PInvoke.SetupDiGetDeviceProperty((void*)_handle.Value, devInfoData, propKey, out propType, null, 0, &propRequiredSize, 0);

            if (propRequiredSize > 0)
            {
                var buffer = new byte[propRequiredSize];
                fixed (byte* p = buffer)
                {
                    if (PInvoke.SetupDiGetDeviceProperty((void*)_handle.Value, devInfoData, propKey, out propType, p, (uint)buffer.Length, &propRequiredSize, 0))
                    {
                        result = DevicePropertyValueFactory.Create((DevicePropertyType)propType, buffer);
                    }
                    else
                    {
                        throw new Win32Exception($"Failed to get device property: guid-->{propKey.fmtid}, pid-->{propKey.pid}");
                    }
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
                if (disposing) { }

                PInvoke.SetupDiDestroyDeviceInfoList((void*)_handle.Value);
                disposedValue = true;
            }
        }

        ~DeviceInfoSet()
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
