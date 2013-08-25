using System;
using Delta.CertXplorer.Asn1Decoder;

namespace Delta.CertXplorer.DocumentModel
{
    /// <summary>
    /// Base document for ASN1 viewer
    /// </summary>
    internal class BaseAsn1Document : BaseDocument<byte[]>
    {
        public BaseAsn1Document(IDocumentSource<byte[]> source)
        {
            if (source == null) throw new ArgumentNullException("source");
            DocumentSource = source;

            OpenDocument();
        }

        /// <summary>
        /// Creates the view that will contain this document.
        /// </summary>
        /// <returns>An instance of a view.</returns>
        public override IDocumentView CreateView()
        {
            var form = new DocumentWindow();
            form.Document = this;
            form.Text = DocumentCaption;
            return form;
        }

        /////// <summary>
        /////// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /////// </summary>
        /////// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
        /////// <returns>
        /////// 	<c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
        /////// </returns>
        /////// <exception cref="T:System.NullReferenceException">
        /////// The <paramref name="obj"/> parameter is null.
        /////// </exception>
        ////public override bool Equals(object obj)
        ////{
        ////    if (obj is BaseAsn1Document)
        ////    {
        ////        if (DocumentSource == null) return base.Equals(obj);
        ////        return ((BaseAsn1Document)obj).DocumentSource.Equals(DocumentSource);
        ////    }
        ////    return false;
        ////}

        /////// <summary>
        /////// Returns a hash code for this instance.
        /////// </summary>
        /////// <returns>
        /////// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /////// </returns>
        ////public override int GetHashCode()
        ////{
        ////    if (DocumentSource == null) return 0;
        ////    else return DocumentSource.GetHashCode();
        ////}
    }
}
