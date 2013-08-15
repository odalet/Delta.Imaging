
namespace Delta.CertXplorer.Commanding
{
    internal static class Verbs
    {
        public static readonly IVerb OpenFileDocument = new OpenFileDocumentVerb();
        public static readonly IVerb OpenCertificateDocument = new OpenCertificateDocumentVerb();
        public static readonly IVerb OpenExistingDocument = new OpenExistingDocumentVerb();
        public static readonly IVerb CloseDocument = new CloseDocumentVerb();
    }

    internal class OpenExistingDocumentVerb : BaseVerb
    {
        public OpenExistingDocumentVerb() : base("OPEN_EXISTING_DOCUMENT") { }
    }

    internal class OpenFileDocumentVerb : BaseVerb
    {
        public OpenFileDocumentVerb() : base("OPEN_FILE_DOCUMENT") { }
    }

    internal class OpenCertificateDocumentVerb : BaseVerb
    {
        public OpenCertificateDocumentVerb() : base("OPEN_CERTIFICATE_DOCUMENT") { }
    }

    internal class CloseDocumentVerb : BaseVerb
    {
        public CloseDocumentVerb() : base("CLOSE_DOCUMENT") { }
    }
}
