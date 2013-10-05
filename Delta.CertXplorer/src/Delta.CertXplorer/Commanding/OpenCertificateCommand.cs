using System.Windows.Forms;
using System.Security.Cryptography.X509Certificates;

using Delta.CertXplorer.Asn1Decoder;
using Delta.CertXplorer.DocumentModel;

namespace Delta.CertXplorer.Commanding
{
    internal class OpenCertificateCommand : BaseOpenDocumentCommand<X509Object>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OpenCertificateCommand"/> class.
        /// </summary>
        public OpenCertificateCommand() : base() { }

        /// <summary>
        /// Gets this command's name.
        /// </summary>
        /// <value>The command name.</value>
        public override string Name
        {
            get { return "Open Certificate Document"; }
        }

        protected override IDocument OpenDocument(object[] arguments)
        {
            var storeName = string.Empty;
            var storeLocation = StoreLocation.CurrentUser;

            X509Object x509 = null;
            if (arguments != null && arguments.Length > 0 && arguments[0] is X509Object)
                x509 = (X509Object)arguments[0];

            if (x509 == null)
            {
                using (var dialog = new CertificateStoreChooserDialog())
                {
                    if (dialog.ShowDialog(Globals.MainForm) == DialogResult.OK)
                    {
                        storeName = dialog.SelectedValue.StoreName;
                        storeLocation = dialog.SelectedValue.StoreLocation;
                        var certificates = dialog.SelectedValue.GetCertificates();
                        var title = string.Format("{0}/{1}", storeName, storeLocation);
                        var message = string.Format("{0} certificates in store {1}", certificates.Count, title);

                        var result = X509Certificate2UI.SelectFromCollection(
                            certificates, title, message, X509SelectionFlag.SingleSelection, Globals.MainFormHandle);

                        if (result.Count > 0) x509 = X509Object.Create(result[0], storeName, storeLocation);
                    }
                }
            }

            if (x509 == null) return null;

            var manager = This.GetService<IDocumentManagerService>(true);
            var source = new X509DocumentSource(x509);
            return manager.CreateDocument(source);
        }
    }
}
