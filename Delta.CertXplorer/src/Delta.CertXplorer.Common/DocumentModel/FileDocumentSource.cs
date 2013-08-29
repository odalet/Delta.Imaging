using System.IO;

namespace Delta.CertXplorer.DocumentModel
{
    public class FileDocumentSource : IDocumentSource
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileDocumentSource"/> class.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <exception cref="FileNotFoundException">Input file does not exist.</exception>
        public FileDocumentSource(string filename)
        {
            if (!File.Exists(filename))
                throw new FileNotFoundException("Input file does not exist.", filename ?? string.Empty);
            FileName = filename;
        }

        #region IDocumentSource Members

        public string Uri
        {
            get { return FileName; }
        }
        
        public bool IsReadOnly
        {
            get { return true; }
        }
        
        #endregion

        public string FileName
        {
            get;
            private set;
        }
    }
}
