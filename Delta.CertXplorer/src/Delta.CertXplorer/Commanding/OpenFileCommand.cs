using System.Windows.Forms;

using Delta.CertXplorer.DocumentModel;

namespace Delta.CertXplorer.Commanding
{
    internal class OpenFileCommand : BaseOpenDocumentCommand<string>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OpenFileCommand"/> class.
        /// </summary>
        public OpenFileCommand() : base() { }

        /// <summary>
        /// Gets this command's name.
        /// </summary>
        /// <value>The command name.</value>
        public override string Name
        {
            get { return "Open File Document"; }
        }
        
        protected override IDocument OpenDocument(object[] arguments)
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

            if (string.IsNullOrEmpty(fileName)) return null;

            var manager = This.GetService<IDocumentManagerService>(true);
            var source = new FileDocumentSource(fileName);
            return manager.CreateDocument(source);
        }
    }

}
