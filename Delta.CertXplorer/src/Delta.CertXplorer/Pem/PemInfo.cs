using System.ComponentModel;
namespace Delta.CertXplorer.Pem
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class PemInfo
    {
        public string Header { get; internal set; }
        public string Footer { get; internal set; }
    }
}
