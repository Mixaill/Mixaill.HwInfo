// Copyright 2021, Mikhail Paulyshka
// SPDX-License-Identifier: MIT

using System;
using System.Collections.Generic;

using Microsoft.Extensions.Logging;

namespace Mixaill.HwInfo.D3DKMT
{
    public class Kmt
    {
        private readonly ILogger _logger = null;

        public Kmt()
        {
        }

        public Kmt(ILogger logger)
        {
            _logger = logger;
        }

        public Kmt(ILogger<Kmt> logger)
        {
            _logger = logger;
        }

        public List<KmtAdapter> GetAdapters()
        {
            var result = new List<KmtAdapter>();

            _logger?.LogInformation("m");
            try
            {
                var adapters = new Interop._D3DKMT_ENUMADAPTERS();
                if (Interop.Gdi.D3DKMTEnumAdapters(ref adapters) == Interop.NtStatus.STATUS_SUCCESS)
                {
                    for (int i = 0; i < adapters.NumAdapters; i++)
                    {
                        var adapter = new KmtAdapter(adapters.Adapters[i], _logger);
                        if (adapter.Initialized)
                        {
                            result.Add(adapter);
                        }
                    }
                }
            }
            catch (EntryPointNotFoundException ex)
            {
                _logger?.LogWarning($"function not found: {ex.TargetSite.Name}");
            }
            catch(Exception ex)
            {
                _logger?.LogError(ex, "unkown error");
            }

            return result;
        }
    }
}
