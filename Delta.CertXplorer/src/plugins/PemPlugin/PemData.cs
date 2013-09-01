using System.ComponentModel;
using Delta.CapiNet.Pem;
using Delta.CertXplorer.Extensibility;

namespace PemPlugin
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    internal class PemData : IData
    {
        public PemData(PemInfo info)
        {
            MainData = info.Workload;
            AdditionalData = info;
        }

        #region IData Members

        public byte[] MainData { get; private set; }

        public object AdditionalData { get; private set; }

        #endregion
        
    }
}
