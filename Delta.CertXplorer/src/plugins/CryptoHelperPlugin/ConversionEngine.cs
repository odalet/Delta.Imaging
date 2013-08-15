using System;
using System.Web;
using System.Text;
using System.Security.Cryptography;

namespace CryptoHelperPlugin
{
    internal static class ConversionEngine
    {
        public static string Run(string data, DataFormat inputFormat, DataFormat outputFormat, Operation operation)
        {
            if (string.IsNullOrEmpty(data)) return string.Empty;

            var bytes = GetBytes(data, inputFormat);
            var result = Process(bytes, operation);
            return GetString(result, outputFormat);
        }

        private static byte[] Process(byte[] data, Operation operation)
        {
            switch (operation)
            {
                case Operation.Convert:
                    return data; 
                case Operation.Sha1:
                    var prov = new SHA1CryptoServiceProvider();
                    prov.Initialize();
                    return prov.ComputeHash(data);
            }

            return null;
        }

        private static byte[] GetBytes(string data, DataFormat format)
        {
            switch (format)
            {
                case DataFormat.Text:
                    return Encoding.UTF8.GetBytes(data);

                case DataFormat.Base64:
                    return Convert.FromBase64String(data);

                case DataFormat.UrlEncoded:
                    return HttpUtility.UrlDecodeToBytes(data);

                case DataFormat.UrlEncodedBase64:
                    return Convert.FromBase64String(HttpUtility.UrlDecode(data));
            }

            return null;
        }

        private static string GetString(byte[] data, DataFormat format)
        {
            switch (format)
            {
                case DataFormat.Text:
                    return Encoding.UTF8.GetString(data);

                case DataFormat.Base64:
                    return Convert.ToBase64String(data);

                case DataFormat.UrlEncoded:
                    return HttpUtility.UrlEncode(data);

                case DataFormat.UrlEncodedBase64:
                    return HttpUtility.UrlEncode(Convert.ToBase64String(data));
            }

            return null;
        }
    }
}
