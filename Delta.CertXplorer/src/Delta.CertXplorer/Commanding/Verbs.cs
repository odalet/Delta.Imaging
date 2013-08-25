
namespace Delta.CertXplorer.Commanding
{
    internal static class Verbs
    {
        public static readonly IVerb OpenFile = new OpenFileVerb();
        public static readonly IVerb OpenCertificate = new OpenCertificateVerb();
        public static readonly IVerb OpenExisting = new OpenExistingDocumentVerb();
        public static readonly IVerb CloseDocument = new CloseDocumentVerb();
    }

    internal class OpenExistingDocumentVerb : BaseVerb
    {
        public OpenExistingDocumentVerb() : base("OPEN_EXISTING_DOCUMENT") { }
    }

    internal class OpenFileVerb : BaseVerb
    {
        public OpenFileVerb() : base("OPEN_FILE") { }
    }

    internal class OpenCertificateVerb : BaseVerb
    {
        public OpenCertificateVerb() : base("OPEN_CERTIFICATE") { }
    }

    internal class CloseDocumentVerb : BaseVerb
    {
        public CloseDocumentVerb() : base("CLOSE_DOCUMENT") { }
    }
}
