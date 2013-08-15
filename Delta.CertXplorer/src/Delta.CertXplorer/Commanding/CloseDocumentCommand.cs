
using Delta.CertXplorer;

using Delta.CertXplorer.Asn1Decoder;
using Delta.CertXplorer.DocumentModel;

namespace Delta.CertXplorer.Commanding
{
    internal class CloseDocumentCommand : BaseCommand<BaseDocument>
    {
        // Document must exit.
        public CloseDocumentCommand() : base(true) { }

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
        /// <param name="arguments">The arguments.</param>
        public override void Run(params object[] arguments)
        {
            if (arguments != null && arguments.Length > 0)
            {
                var doc = (BaseDocument)arguments[0];
                if (doc != null)
                This.GetService<IDocumentManagerService>(true).CloseDocument(doc);
            }
        }
    }
}
