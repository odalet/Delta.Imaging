using System;
namespace Delta.CapiNet.Pem
{
    public class PemInfo
    {
        internal PemInfo(PemDecoder reader)
        {
            if (reader == null) throw new ArgumentNullException("reader");

            TextData = reader.TextData;
            Workload = reader.Workload;
            Kind = reader.Kind;
            FullHeader = reader.FullHeader;
            FullFooter = reader.FullFooter;
            AdditionalText = reader.AdditionalText;
            Warnings = reader.Warnings ?? new string[0];
        }

        public string TextData { get; private set; }
        public byte[] Workload { get; private set; }
        public PemKind Kind { get; private set; }
        public string FullHeader { get; private set; }
        public string FullFooter { get; private set; }
        public string AdditionalText { get; private set; }
        public string[] Warnings { get; private set; }
    }
}
