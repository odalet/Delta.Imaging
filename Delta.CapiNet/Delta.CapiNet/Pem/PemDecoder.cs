using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace Delta.CapiNet.Pem
{
    public class PemDecoder
    {
        private const string headerBegin = "-----BEGIN ";
        private const string headerEnd = "-----";
        private const string footerBegin = "-----END ";
        private const string footerEnd = "-----";

        private const string headerBeginAlt = "---- BEGIN ";
        private const string headerEndAlt = " ----";
        private const string footerBeginAlt = "---- END ";
        private const string footerEndAlt = " ----";

        private static readonly Encoding pemEncoding = Encoding.ASCII;

        private List<String> errors = new List<string>();
        private List<String> warnings = new List<string>();

        private bool dirty = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="PemDecoder"/> class.
        /// </summary>
        public PemDecoder() { }

        internal string TextData { get; private set; }
        internal byte[] Workload { get; private set; }
        internal PemKind Kind { get; private set; }
        internal string FullHeader { get; private set; }
        internal string FullFooter { get; private set; }
        internal string AdditionalText { get; private set; }

        public string[] Errors { get { return errors.ToArray(); } }
        public string[] Warnings { get { return warnings.ToArray(); } }

        public static bool IsPemFile(string filename)
        {            
            if (string.IsNullOrEmpty(filename)) throw new ArgumentNullException("filename");
            if (!File.Exists(filename)) throw new ArgumentException(string.Format(
                "File {0} does not exist", filename), "filename");

            return IsPemData(File.ReadAllBytes(filename));
        }

        public static bool IsPemData(byte[] data)
        {
            if (data == null) throw new ArgumentNullException("data");
            return IsPemString(pemEncoding.GetString(data));
        }

        public static bool IsPemString(string data)
        {
            return data.StartsWith(headerBegin) || data.StartsWith(headerBeginAlt);
        }

        public PemInfo ReadFile(string filename)
        {
            if (string.IsNullOrEmpty(filename)) throw new ArgumentNullException("filename");
            if (!File.Exists(filename)) throw new ArgumentException(string.Format(
                "File {0} does not exist", filename), "filename");

            TextData = File.ReadAllText(filename);
            return Decode();
        }

        public PemInfo ReadData(byte[] data)
        {
            if (data == null) throw new ArgumentNullException("data");
            TextData = pemEncoding.GetString(data);
            return Decode();
        }

        public PemInfo ReadString(string data)
        {
            if (string.IsNullOrEmpty(data)) throw new ArgumentNullException("data");
            TextData = data;
            return Decode();
        }

        private PemInfo Decode() 
        {
            if (dirty) throw new InvalidOperationException("A PemDecoder instance should only be used once.");
            dirty = true; 

            var strings = ReadAllLines(TextData);
            if (strings == null || strings.Length == 0)
            {
                AddError("No data.");
                return null;
            }

            var altHeader = false;
            FullHeader = strings[0];
            if (!IsHeader(FullHeader, out altHeader))
            {
                AddError("First line does not contain a PEM Header.");
                return null;
            }

            var altFooter = false;
            var index = 1;
            var base64Builder = new StringBuilder();
            var additionalDataBuilder = new StringBuilder();
            while (index < strings.Length)
            {
                var current = strings[index];
                if (!IsFooter(current, out altFooter))
                {
                    if (string.IsNullOrEmpty(FullFooter))
                        base64Builder.Append(current);
                    else additionalDataBuilder.AppendLine(current);
                }
                else FullFooter = current;
                index++;
            }

            // All data was read, now go check consistency.

            if (altHeader != altFooter) AddWarning(altHeader ?
                "PEM inconsistency: header uses the alternate form ('---- BEGIN') whereas footer does not ('-----END')." :
                "PEM inconsistency: header uses the normal form ('-----BEGIN') whereas footer does not ('---- END').");

            var header = ExtractHeader(FullHeader, altHeader);
            var footer = ExtractFooter(FullFooter, altFooter);

            if (header != footer) AddWarning(string.Format(
                "PEM inconsistency: header ({0}) does not match footer ({1}).", header, footer));

            Kind = PemKind.Find(header);
            if (Kind == null)
            {
                AddWarning(string.Format("PEM header {0} is not a well-known PEM header.", header));
                Kind = PemKind.GetCustom(header, "Not a well-known PEM header.");
            }

            try
            {
                Workload = Convert.FromBase64String(base64Builder.ToString());
            }
            catch (Exception ex)
            {
                AddError(string.Format("Could not decode input Base64 data: {0}", ex.Message));
                return null;
            }

            AdditionalText = additionalDataBuilder.ToString();

            return new PemInfo(this);
        }

        private bool IsHeader(string input, out bool isAlternateHeader)
        {
            isAlternateHeader = false;
            if (input.StartsWith(headerBegin))
                return true;
            else if (input.StartsWith(headerBeginAlt))
            {
                isAlternateHeader = false;
                return true;
            }

            return false;
        }

        private bool IsFooter(string input, out bool isAlternateFooter)
        {
            isAlternateFooter = false;
            if (input.StartsWith(footerBegin))
                return true;
            else if (input.StartsWith(footerBeginAlt))
            {
                isAlternateFooter = false;
                return true;
            }

            return false;
        }

        private string ExtractHeader(string input, bool alt)
        {
            var prefix = alt ? headerBegin : headerBeginAlt;
            var suffix = alt ? headerEnd : headerEndAlt;

            return input.Substring(prefix.Length, input.Length - prefix.Length - suffix.Length).Trim();
        }

        private string ExtractFooter(string input, bool alt)
        {
            var prefix = alt ? footerBegin : footerBeginAlt;
            var suffix = alt ? footerEnd : footerEndAlt;

            return input.Substring(prefix.Length, input.Length - prefix.Length - suffix.Length).Trim();
        }

        private void AddError(string error)
        {
            errors.Add(error);
        }

        private void AddWarning(string warning)
        {
            warnings.Add(warning);
        }

        private void ClearAll()
        {
            errors = new List<string>();
            warnings = new List<string>();

            TextData = null;
            Workload = null;
            Kind = null;
            FullHeader = null;
            FullFooter = null;
        }

        private static string[] ReadAllLines(string data)
        {
            var result = new List<string>();

            using (var reader = new StringReader(data))
            {
                string str;
                while ((str = reader.ReadLine()) != null)
                    result.Add(str);

                reader.Close();
            }

            return result.ToArray();
        }
    }
}
