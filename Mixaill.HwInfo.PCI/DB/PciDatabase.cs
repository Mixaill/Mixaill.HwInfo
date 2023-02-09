using System.Collections.Generic;
using System.IO;

namespace Mixaill.HwInfo.PCI.DB
{
    public class PciDatabase
    {
        #region Properties

        private Dictionary<ushort, PciVendorInfo> Vendors = new Dictionary<ushort, PciVendorInfo>();

        public bool Initialized { get; private set; } = false;

        #endregion


        #region Singleton

        private static PciDatabase _Instance = null;

        public static PciDatabase Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new PciDatabase();

                return _Instance;

            }
        }

        #endregion

        #region Initialization

        private PciDatabase()
        {
            Load("pci.ids");
        }


        private void Load(string filepath)
        {
            ushort last_vid = 0xFFFF;
            ushort last_did = 0xFFFF;


            Initialized = false;
            Vendors.Clear();

            if (!File.Exists(filepath))
            {
                return;
            }

            using (var sr = new StreamReader(filepath)) {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();

                    //skip empty
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        continue;
                    }

                    //skip comments
                    if (line[0] == '#')
                    {
                        continue;
                    }

                    //skip subvendors
                    if (line[1] == '\t')
                    {
                        continue;
                    }


                    //device
                    if (line[0] == '\t')
                    {
                        var dev = Vendors[last_vid].AddDevice(line);
                        last_did = dev.DeviceID;
                        continue;
                    }

                    //vendor
                    var vendor = new PciVendorInfo(line);
                    Vendors[vendor.VendorID] = vendor;
                    last_vid = vendor.VendorID;
                }

                Initialized = true;
            }
        }

        #endregion

        #region Public

        public IReadOnlyCollection<PciVendorInfo> GetVendors()
        {
            return Vendors.Values;
        }

        public PciVendorInfo FindVendor(ushort vendorID)
        {
            if (Vendors.ContainsKey(vendorID))
            {
                return Vendors[vendorID];
            }

            return null;
        }

        public PciDeviceInfo FindDevice(ushort vendorID, ushort deviceID)
        {
            if (Vendors.ContainsKey(vendorID))
            {
                return Vendors[vendorID].FindDevice(deviceID);
            }

            return null;
        }


        #endregion
    }
}
