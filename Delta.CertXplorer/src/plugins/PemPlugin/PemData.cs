using Delta.CertXplorer.Extensibility;

namespace PemPlugin
{
    internal class PemData : IData
    {
        #region IData Members

        public byte[] MainData { get; internal set; }

        public object AdditionalData 
        {
            get
            {
                return new
                {
                    Header = Header,
                    Footer = Footer
                };
            }
        }

        #endregion

        public string Header { get; set; }

        public string Footer { get; set; }
    }
}
