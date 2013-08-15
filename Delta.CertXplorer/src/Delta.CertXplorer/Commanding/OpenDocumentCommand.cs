using System.Windows.Forms;
using System.Security.Cryptography.X509Certificates;

using Delta.CertXplorer;

using Delta.CertXplorer.Asn1Decoder;
using Delta.CertXplorer.DocumentModel;

namespace Delta.CertXplorer.Commanding
{
    internal class OpenDocumentCommand<T> : BaseCommand<T>
    {
        private bool isFileDocument = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreatePersonCommand"/> class.
        /// </summary>
        public OpenDocumentCommand(bool isFile) : base(true) 
        {
            isFileDocument = isFile;
        }

        /// <summary>
        /// Gets this command's name.
        /// </summary>
        /// <value>The command name.</value>
        public override string Name
        {
            get { return string.Format("Open {0} Document", isFileDocument ? "File" : "Certificate"); }
        }

        /// <summary>
        /// Runs the command with the specified arguments.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        public override void Run(params object[] arguments)
        {
            var document = OpenDocument(arguments);
            if (document != null) 
                This.GetService<IDocumentManagerService>(true).OpenDocument(document);
        }

        /// <summary>
        /// Opens the document.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        /// <returns></returns>
        protected virtual BaseDocument OpenDocument(object[] arguments)
        {
            return isFileDocument ? OpenFileDocument(arguments) :
                OpenCertificateDocument(arguments);
        }

        private BaseDocument OpenFileDocument(object[] arguments)
        {
            var fileName = string.Empty;
            if (arguments != null && arguments.Length > 0 && arguments[0] is string)
                fileName = (string)arguments[0];

            if (string.IsNullOrEmpty(fileName))
            {
                using (var dialog = new OpenFileDialog())
                {
                    if (dialog.ShowDialog(Globals.MainForm) == DialogResult.OK)
                        fileName = dialog.FileName;
                }
            }

            if (!string.IsNullOrEmpty(fileName))
                return new FileDocument(fileName);
            else return null;
        }

        private BaseDocument OpenCertificateDocument(object[] arguments)
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

            if (x509 != null) return new X509Document(x509);
            else return null;
        }
    }

    internal class OpenFileDocumentCommand : OpenDocumentCommand<string>
    {
        public OpenFileDocumentCommand() : base(true) { }
    }

    internal class OpenCertificateDocumentCommand : OpenDocumentCommand<X509Object>
    {
        public OpenCertificateDocumentCommand() : base(false) { }
    }

    internal class OpenExistingDocumentCommand : OpenDocumentCommand<BaseDocument>
    {
        // Document must exit.
        public OpenExistingDocumentCommand() : base(true) { }

        /// <summary>
        /// Opens the document.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        /// <returns></returns>
        protected override BaseDocument OpenDocument(object[] arguments)
        {
            return (BaseDocument)arguments[0];
        }
    }
}
