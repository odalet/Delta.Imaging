using Delta.CertXplorer.Asn1Decoder;
using Delta.CertXplorer.DocumentModel;

namespace Delta.CertXplorer.Commanding
{
    internal abstract class BaseOpenDocumentCommand<T> : BaseCommand<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseOpenDocumentCommand{T}"/> class.
        /// </summary>
        public BaseOpenDocumentCommand() : base() { }
        
        /// <summary>
        /// Runs the command with the specified arguments.
        /// </summary>
        protected override void RunCommand()
        {
            var document = OpenDocument(Arguments);
            if (document != null)
                This.GetService<IDocumentManagerService>(true).OpenDocument(document);
        }

        /// <summary>
        /// Opens the document.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        /// <returns></returns>
        protected abstract IDocument OpenDocument(object[] arguments);
    }
}
