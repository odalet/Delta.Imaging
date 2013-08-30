using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Delta.CertXplorer.DocumentModel
{
    internal static class DocumentModelExtensions
    {
        public static void RegisterHandlerPlugin(this IDocumentHandlerRegistryService registry, PluginBasedDocumentHandler handler)
        {
            registry.Register(() => handler, int.MaxValue);
        }
    }
}
