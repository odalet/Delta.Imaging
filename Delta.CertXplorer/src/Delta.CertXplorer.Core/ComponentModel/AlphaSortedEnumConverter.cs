using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;

namespace Delta.CertXplorer.ComponentModel
{
    internal class AlphaSortedEnumConverter : EnumConverter
    {
        private class EnumValAlphaComparer : IComparer
        {
            public static readonly EnumValAlphaComparer Default = new EnumValAlphaComparer();
            private CompareInfo compareInfo = CultureInfo.InvariantCulture.CompareInfo;

            private EnumValAlphaComparer() { }

            public int Compare(object a, object b)
            {
                return compareInfo.Compare(a.ToString(), b.ToString());
            }
        }

        public AlphaSortedEnumConverter(Type type) : base(type) { }

        protected override IComparer Comparer
        {
            get { return EnumValAlphaComparer.Default; }
        }
    }
}
