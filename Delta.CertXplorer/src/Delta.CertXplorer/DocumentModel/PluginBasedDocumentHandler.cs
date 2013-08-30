using System;
using Delta.CertXplorer.Asn1Decoder;
using Delta.CertXplorer.Extensibility;

namespace Delta.CertXplorer.DocumentModel
{
    internal class PluginBasedDocumentHandler : BaseDocumentHandler<Asn1DocumentView>
    {
        private IDataHandlerPlugin plugin = null;

        private IDataHandler handler = null;

        public PluginBasedDocumentHandler(IDataHandlerPlugin dataHandlerPlugin)
        {
            if (dataHandlerPlugin == null) throw new ArgumentNullException("dataHandlerPlugin");
            plugin = dataHandlerPlugin;
        }

        protected override bool CanHandleSource(IDocumentSource source)
        {
            if (source == null || !(source is FileDocumentSource))
                return false;

            handler = plugin.CreateHandler();
            return handler.CanHandleFile(((FileDocumentSource)source).Uri);
        }

        protected override IDocument CreateDocumentFromSource()
        {
            if (handler == null) throw new InvalidOperationException("Inner data handler is null.");
            var data = handler.ProcessFile();

            var doc = new PluginBasedDocument();
            doc.SetData(data.MainData);
            doc.SetAdditionalData(data.AdditionalData);
            return doc;
        }
    }
}
