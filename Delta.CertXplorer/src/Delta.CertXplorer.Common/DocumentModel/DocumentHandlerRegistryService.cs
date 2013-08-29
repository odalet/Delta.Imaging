using System;
using System.Linq;
using System.Collections.Generic;

namespace Delta.CertXplorer.DocumentModel
{
    internal class DocumentHandlerRegistryService : IDocumentHandlerRegistryService //: IDocumentBuilderRegistryService, IViewBuilderRegistryService
    {
        public class RegistryEntry<T>
        {
            public int Priority { get; set; }
            public T Value { get; set; }
        }

        private List<RegistryEntry<Func<IDocumentHandler>>> list = new List<RegistryEntry<Func<IDocumentHandler>>>();


        #region IDocumentHandlerRegistryService Members

        public void Register(Func<IDocumentHandler> handlerConstructor, int priority = 0)
        {
            var entry = new RegistryEntry<Func<IDocumentHandler>>()
            {
                Priority = priority,
                Value = handlerConstructor
            };

            list.Add(entry);
        }

        public IDocumentHandler[] Find(IDocumentSource source)
        {
            List<IDocumentHandler> result = new List<IDocumentHandler>();

            foreach (var current in list.OrderByDescending(e => e.Priority).Select(e => e.Value))
            {
                var function = current;
                IDocumentHandler handler = null;               
                
                try
                {
                    handler = function();
                }
                catch (Exception ex)
                {
                    This.Logger.Error(string.Format("Handler creation error: {0}", ex.Message), ex);
                }

                if (handler == null)  continue;
                var canHandle = false;
                try
                {
                    canHandle = handler.CanHandle(source);
                }
                catch (Exception ex)
                {
                    This.Logger.Error(string.Format("Handler.CanHandle invocation error: {0}", ex.Message), ex);
                }
                
                if (canHandle)
                    result.Add(handler);
            }

            if (result.Count == 0)
                return new IDocumentHandler[] { new DefaultDocumentHandler() };
            else return result.ToArray();
        }

        #endregion
    }
}
