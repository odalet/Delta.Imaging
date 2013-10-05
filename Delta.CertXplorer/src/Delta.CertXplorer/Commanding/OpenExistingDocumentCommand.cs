using Delta.CertXplorer.DocumentModel;

namespace Delta.CertXplorer.Commanding
{
    internal class OpenExistingDocumentCommand : BaseOpenDocumentCommand<IDocument>
    {
        // Document must exit.
        public OpenExistingDocumentCommand() : base() { }

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
        protected override IDocument OpenDocument(object[] arguments)
        {
            return (IDocument)arguments[0];
        }
    }

}
