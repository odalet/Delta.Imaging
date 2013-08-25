using Delta.CertXplorer.DocumentModel;

namespace Delta.CertXplorer.Commanding
{
    internal class OpenExistingDocumentCommand : BaseOpenDocumentCommand<BaseAsn1Document>
    {
        // Document must exit.
        public OpenExistingDocumentCommand() : base(true) { }

        /// <summary>
        /// Gets this command's name.
        /// </summary>
        /// <value>The command name.</value>
        public override string Name
        {
            get { return "Open Existing Document"; }
        }

        /// <summary>
        /// Opens the document.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        /// <returns></returns>
        protected override BaseAsn1Document OpenDocument(object[] arguments)
        {
            return (BaseAsn1Document)arguments[0];
        }
    }

}
