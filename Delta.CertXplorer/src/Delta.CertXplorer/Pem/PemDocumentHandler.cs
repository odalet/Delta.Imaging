using System;
using System.IO;
using System.Linq;
using System.Text;

using Delta.CertXplorer.Asn1Decoder;
using Delta.CertXplorer.DocumentModel;

namespace Delta.CertXplorer.Pem
{
    // Quick n' dirty PEM reader
    // TODO: make this a plugin
    public class PemDocumentHandler : BaseDocumentHandler<Asn1DocumentView>
    {
        private byte[] rawData = null;

        protected override bool CanHandleSource(IDocumentSource source)
        {
            if (!(source is FileDocumentSource)) return false;

            // Read the source (and cache the data)
            rawData = File.ReadAllBytes(source.Uri);
            var s = Encoding.ASCII.GetString(rawData);
            return s.StartsWith("-----BEGIN") || s.StartsWith("---- BEGIN");
        }

        protected override IDocument CreateDocumentFromSource()
        {
            if (rawData == null) throw new InvalidOperationException("Invalid input data: null");

            var strings = Encoding.ASCII.GetString(rawData).Split('\r', '\n').Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();

            var b64 = string.Join("", strings.Where(s => !s.StartsWith("----")));
            var data = Convert.FromBase64String(b64);

            var doc = new PemDocument();
            doc.PemInfo = new PemInfo() 
            {
                Header = strings[0],
                Footer = strings[strings.Length - 1]
            };

            doc.SetData(data);

            return doc;
        }
    }
}