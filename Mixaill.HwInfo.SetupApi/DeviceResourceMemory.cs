// Copyright 2021, Mikhail Paulyshka
// SPDX-License-Identifier: MIT

using System;

using Mixaill.HwInfo.SetupApi.Defines;

namespace Mixaill.HwInfo.SetupApi
{
    public class DeviceResourceMemory
    {
        public DeviceResourceType ResType { get; } = DeviceResourceType.All;

        public UInt64 AddressStart { get; } = 0;

        public UInt64 AddressEnd { get; } = 0;

        public UInt64 Size => AddressEnd - AddressStart + 1;

        public bool Above4g => AddressEnd > UInt32.MaxValue;

        public DeviceResourceMemory(DeviceResourceType type, UInt64 addressStart, UInt64 addressEnd) {
            ResType = type;
            AddressStart = addressStart;
            AddressEnd = addressEnd;
        }

        public override string ToString()
        {
            return $"({AddressStart:X16} - {AddressEnd:X16} ) -- {Size / 1024 / 1024.0} MiB, {(ResType == DeviceResourceType.Mem ? "Standard" : "Large" )}";
        }
    }
}
