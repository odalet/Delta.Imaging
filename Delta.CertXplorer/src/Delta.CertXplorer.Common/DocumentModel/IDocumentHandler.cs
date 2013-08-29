namespace Delta.CertXplorer.DocumentModel
{
    public interface IDocumentHandler
    {
        bool CanHandle(IDocumentSource source);
        
        void CreateDocument(IDocumentSource source);

        IDocument Document { get; }
        
        IDocumentView CreateView();
    }
}
