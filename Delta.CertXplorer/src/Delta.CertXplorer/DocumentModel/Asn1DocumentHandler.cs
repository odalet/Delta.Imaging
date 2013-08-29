using System;
using System.IO;
using Delta.CertXplorer.Asn1Decoder;

namespace Delta.CertXplorer.DocumentModel
{
    internal class Asn1DocumentHandler : BaseDocumentHandler<Asn1DocumentView>
    {
        protected override bool CanHandleSource(IDocumentSource source)
        {
            return source != null && source is X509DocumentSource || source is FileDocumentSource;
        }

        protected override IDocument CreateDocumentFromSource()
        {
            var doc = new BinaryDocument();

            if (Source is X509DocumentSource)
            {
                var x509Source = (X509DocumentSource)Source;
                var x509 = x509Source.X509Data;                
                doc.SetData(x509.Data);
            }
            else
            {
                var fileSource = (FileDocumentSource)Source;
                var data = File.ReadAllBytes(Source.Uri);
                doc.SetData(data);
            }

            return doc;
        }
    }
}
