using System;
using System.Collections.Generic;
using System.Text;

namespace Mixaill.HwInfo.LowLevel
{
    class OlsWrapper : OpenLibSys.Ols
    {
        private static OlsWrapper instance;

        private OlsWrapper()
        {
        }

        public static OlsWrapper Instance()
        {
            if (instance == null)
                instance = new OlsWrapper();

            return instance;
        }
    }
}
