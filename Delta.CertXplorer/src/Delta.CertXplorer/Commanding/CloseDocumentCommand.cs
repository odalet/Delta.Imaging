
using Delta.CertXplorer;

using Delta.CertXplorer.Asn1Decoder;
using Delta.CertXplorer.DocumentModel;

namespace Delta.CertXplorer.Commanding
{
    internal class CloseDocumentCommand : BaseCommand<IDocument>
    {
        // Document must exit.
        public CloseDocumentCommand() : base() { }

        /// <summary>
        /// Gets this command's name.
        /// </summary>
        /// <value>The command name.</value>
        public override string Name
        {
            get { return "Close Document"; }
        }

        /// <summary>
        /// Runs the command with the specified arguments.
        /// </summary>
        protected override void RunCommand()
        {
            if (Target != null)
                This.GetService<IDocumentManagerService>(true).CloseDocument(Target);
        }
    }
}
