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
            var ok = handler.CanHandleFile(((FileDocumentSource)source).Uri);

            if (ok) This.Logger.Info(string.Format(
                "This file will be handled by plugin [{0}]", plugin.PluginInfo.Name));

            return ok;
        }

        protected override IDocument CreateDocumentFromSource()
        {
            if (handler == null) throw new InvalidOperationException("Inner data handler is null.");
            
            This.Logger.Info(string.Format(
                "Running plugin [{0}] on data source {1}", plugin.PluginInfo.Name, Source.Uri));
            
            IData result = null;
            try
            {
                result = handler.ProcessFile();
            }
            catch (Exception ex)
            {
                This.Logger.Error(string.Format(
                    "Plugin [{0}] failed at processing data from source {1}: {2}",
                    plugin.PluginInfo.Name, Source.Uri, ex.Message), ex);
                return null;
            }

            var doc = new PluginBasedDocument();
            doc.SetData(result.MainData);
            doc.SetAdditionalData(result.AdditionalData);
            return doc;
        }
    }
}
