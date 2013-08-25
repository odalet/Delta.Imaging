using System;

namespace Delta.CertXplorer.DocumentModel
{
    public abstract class BaseDocument<T> : IDocument
    {
        private string caption = "doc";
        private T data = default(T);

        #region IDocument Members

        public abstract IDocumentView CreateView();

        public IDocumentSource Source
        {
            get { return DocumentSource; }
        }

        public string Key
        {
            get 
            {
                if (Source == null) return string.Empty;
                if (string.IsNullOrEmpty(Source.DataKey)) return string.Empty;
                return string.Format("doc:{0}", Source.DataKey);
            }
        }

        #endregion

        /// <summary>
        /// Gets the document caption as it should appear on the view's title.
        /// </summary>
        /// <value>The document caption.</value>
        public virtual string DocumentCaption
        {
            get { return caption; }
        }

        /// <summary>
        /// Gets this document's data.
        /// </summary>
        /// <value>The data.</value>
        public T Data
        {
            get { return data; }
        }

        protected IDocumentSource<T> DocumentSource
        {
            get;
            set;
        }



        /// <summary>
        /// Opens the document.
        /// </summary>
        protected virtual void OpenDocument()
        {
            if (DocumentSource == null) throw new InvalidOperationException(
                "You must first set the DocumentSource property.");

            data = DocumentSource.CreateData();
            caption = DocumentSource.Caption;
        }
    }
}
