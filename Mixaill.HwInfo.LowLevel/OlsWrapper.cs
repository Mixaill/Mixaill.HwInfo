namespace Mixaill.HwInfo.LowLevel
{
    public class OlsWrapper : OpenLibSys.Ols
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
