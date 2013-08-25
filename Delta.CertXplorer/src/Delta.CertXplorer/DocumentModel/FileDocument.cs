////using System.IO;

////namespace Delta.CertXplorer.DocumentModel
////{
////    /// <summary>
////    /// A file-based document
////    /// </summary>
////    internal class FileDocument : BaseAsn1Document
////    {
////        private string filename = string.Empty;

////        /// <summary>
////        /// Initializes a new instance of the <see cref="FileDocument"/> class.
////        /// </summary>
////        /// <param name="file">The file.</param>
////        public FileDocument(string file) : base()
////        {
////            filename = file;
////            base.OpenDocument();
////        }

////        /// <summary>
////        /// Gets the document caption as it should appear on the view's title.
////        /// </summary>
////        /// <value>The document caption.</value>
////        public override string DocumentCaption
////        {
////            get { return filename; }
////        }

////        /// <summary>
////        /// Creates the data this document holds.
////        /// </summary>
////        /// <returns>An array of bytes.</returns>
////        protected override byte[] CreateData()
////        {
////            return File.ReadAllBytes(filename);
////        }

////        /// <summary>
////        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
////        /// </summary>
////        /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
////        /// <returns>
////        /// 	<c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
////        /// </returns>
////        /// <exception cref="T:System.NullReferenceException">
////        /// The <paramref name="obj"/> parameter is null.
////        /// </exception>
////        public override bool Equals(object obj)
////        {
////            if (obj is FileDocument)
////                return ((FileDocument)obj).filename.Equals(filename);
////            return false;
////        }

////        /// <summary>
////        /// Returns a hash code for this instance.
////        /// </summary>
////        /// <returns>
////        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
////        /// </returns>
////        public override int GetHashCode() { return filename.GetHashCode(); }
////    }
////}
