using Delta.CertXplorer.DocumentModel;

namespace Delta.CertXplorer.Pem
{
    public class PemDocument : BinaryDocument, IDocumentData<PemInfo>
    {
        public PemInfo PemInfo { get; set; }

        #region IDocumentData<PemInfo> Members

        PemInfo IDocumentData<PemInfo>.Data
        {
            get { return PemInfo; }
        }

        #endregion
    }
}
