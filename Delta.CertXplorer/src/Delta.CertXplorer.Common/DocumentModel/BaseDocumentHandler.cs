using System;

namespace Delta.CertXplorer.DocumentModel
{
    public abstract class BaseDocumentHandler : IDocumentHandler
    {
        #region IDocumentHandler Members

        public bool CanHandle(IDocumentSource source)
        {
            if (source == null) throw new ArgumentNullException("source");
            var ok = CanHandleSource(source);
            if (ok) Source = source;

            return ok;
        }

        public void CreateDocument(IDocumentSource source)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (source != Source) throw new ArgumentException(
                "The same source object must be passed to CanHandle and CreateDocument methods", "source");

            var doc = CreateDocumentFromSource();
            doc.Handler = this;
            doc.SetSource(source);
            Document = doc;
        }

        public IDocument Document
        {
            get;
            protected set;
        }

        public IDocumentView CreateView()
        {
            if (Document == null) throw new InvalidOperationException(
                "The document associated with this handler is null.");

            var view = CreateViewForDocument();
            view.SetDocument(Document);
            return view;
        }

        #endregion

        protected IDocumentSource Source { get; private set; }

        protected abstract bool CanHandleSource(IDocumentSource source);

        protected abstract IDocument CreateDocumentFromSource();

        protected abstract IDocumentView CreateViewForDocument();
        
    }

    public abstract class BaseDocumentHandler<TView> : BaseDocumentHandler
        where TView : IDocumentView, new()
    {
        protected override IDocumentView CreateViewForDocument()
        {
            return new TView();
        }
    }
}
