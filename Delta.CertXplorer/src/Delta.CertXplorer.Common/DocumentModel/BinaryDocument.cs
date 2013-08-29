using System;

namespace Delta.CertXplorer.DocumentModel
{
    public class BinaryDocument : BaseDocument<byte[]>
    {
        private byte[] data = null;

        public void SetData(byte[] contentData)
        {
            if (data != null)
                throw new InvalidOperationException("This document's data can only be set once.");
            data = contentData;
        }

        protected override byte[] GetData()
        {
            return data;
        }
    }
}
