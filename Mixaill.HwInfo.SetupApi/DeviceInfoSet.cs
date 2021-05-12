// Copyright 2021, Mikhail Paulyshka
// SPDX-License-Identifier: MIT

using System;
using System.Collections.Generic;
using System.ComponentModel;

using Microsoft.Windows.Sdk;

using Mixaill.SetupApi.Defines;

namespace Mixaill.SetupApi
{
    public unsafe class DeviceInfoSet : IDisposable
    {
        #region Properties

        public Guid ClassGuid { get; }        
        
        public List<DeviceInfo> Devices { get; } = new List<DeviceInfo>();

        private HANDLE _handle { get; set; }

        #endregion

        public DeviceInfoSet(Guid guid)
        {
            ClassGuid = guid;
            Initialize();
        }

        private unsafe void Initialize()
        {
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
                Devices.Add(DeviceInfoFactory.Create(this, data));
                idx++;
            }
        }

        internal unsafe DevicePropertyValue GetProperty(SP_DEVINFO_DATA devInfoData, DEVPROPKEY propKey)
        {
            uint propType = 0;
            uint propRequiredSize = 0;

            PInvoke.SetupDiGetDeviceProperty((void*)_handle.Value, devInfoData, propKey, out propType, null, 0, &propRequiredSize, 0);
            if (propRequiredSize == 0)
            {
                throw new Win32Exception();
            }
            
            var buffer = new byte[propRequiredSize];
            fixed (byte* p = buffer)
            {
                if (!PInvoke.SetupDiGetDeviceProperty((void*)_handle.Value, devInfoData, propKey, out propType, p, (uint)buffer.Length, &propRequiredSize, 0))
                {
                    throw new Win32Exception();
                }
            }

            return DevicePropertyValueFactory.Create((DevicePropertyType)propType, buffer);
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
