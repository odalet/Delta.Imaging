using System;
using System.ComponentModel;

namespace Delta.CertXplorer.DocumentModel
{
    internal class PluginBasedDocument : BinaryDocument
    {
        public void SetAdditionalData(object additionalData)
        {
            if (AdditionalData != null)
                throw new InvalidOperationException("This document's additional data can only be set once.");
            AdditionalData = additionalData;
        }

    [TypeConverter(typeof(ExpandableObjectConverter))]
        public object AdditionalData
        {
            get;
            private set;
        }
    }
}
