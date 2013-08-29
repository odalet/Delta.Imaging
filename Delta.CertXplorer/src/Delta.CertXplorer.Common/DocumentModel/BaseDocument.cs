using System;
using System.ComponentModel;

namespace Delta.CertXplorer.DocumentModel
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public abstract class BaseDocument : IDocument
    {
        private IDocumentSource documentSource = null;

        #region IDocument Members

        public void SetSource(IDocumentSource source)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (documentSource != null) throw new InvalidOperationException("This document's source can only be set once.");

            documentSource = source;
        }

        /// <summary>
        /// Gets or sets the document handler associated with this document.
        /// </summary>
        [Browsable(false)]
        public IDocumentHandler Handler { get; set; }

        [Browsable(false)]
        public IDocumentSource Source
        {
            get { return documentSource; }
        }
        
        public string Caption
        {
            get
            {
                CheckSource();
                return Source.Uri;
            }
        }

        public string Key
        {
            get
            {
                CheckSource();
                return "doc:" + Source.Uri;
            }
        }

        #endregion
        
        protected virtual void CheckSource()
        {
            if (Source == null)
                throw new InvalidOperationException("You must first affect a source to this document.");
        }
    }

    public abstract class BaseDocument<T> : BaseDocument, IDocumentData<T>
    {
        #region IDocumentData<T> Members

        public T Data
        {
            get { return GetData(); }
        }

        #endregion

        protected abstract T GetData();
    }
}
