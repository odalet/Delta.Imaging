
namespace Delta.CertXplorer.DocumentModel
{
    public interface IDocumentData<T>
    {
        T Data { get; }
    }

    public interface IDocumentData : IDocumentData<object>
    {
    }
}
