using System.IO;

namespace Delta.CertXplorer.DocumentModel
{
    internal class FileDocumentSource : IDocumentSource<byte[]>
    {
        private string filename = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileDocumentSource"/> class.
        /// </summary>
        /// <param name="file">The file.</param>
        public FileDocumentSource(string file) : base()
        {
            filename = file;
        }

        #region IDocumentSource<byte[]> Members

        public byte[] CreateData()
        {
            return File.ReadAllBytes(filename);
        }
        
        /// <summary>
        /// Gets the document caption as it should appear on the view's title.
        /// </summary>
        public string Caption
        {
            get { return filename; }
        }

        public bool IsReadOnly { get { return true; } }

        public string DataKey
        {
            get
            {
                if (string.IsNullOrEmpty(filename)) return string.Empty;
                return string.Format("file:{0}", Caption);
            }
        }

        #endregion
    }
}
