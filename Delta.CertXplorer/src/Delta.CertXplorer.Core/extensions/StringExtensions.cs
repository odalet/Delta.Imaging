using System;
using System.Security;
using System.Threading;
using System.Globalization;
using System.ComponentModel;
using System.Collections.Generic;

namespace Delta.CertXplorer
{
    /// <summary>
    /// Helper methods extending <see cref="System.String"/> and related
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Copies the content of a string into a <see cref="System.Security.SecureString"/>.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns>the protected <see cref="System.Security.SecureString"/>.</returns>
        public static SecureString ToSecureString(this string input)
        {
            if (input == null) return null;

            SecureString ss = new SecureString();
            foreach (char c in input) ss.AppendChar(c);
            ss.MakeReadOnly();

            return ss;
        }

        /// <summary>
        /// Use this method to remove sensitive data (password) from a connection string.
        /// </summary>
        /// <param name="connstr">The connection string.</param>
        /// <returns>The connection string without any password.</returns>
        public static string ToStringNoPassword(this string connstr)
        {
            return ToStringNoPassword(connstr, false);
        }

        /// <summary>
        /// Use this method to remove sensitive data (password) from a connection string.
        /// </summary>
        /// <param name="connstr">The connection string.</param>
        /// <param name="showStars">
        /// if set to <c>true</c> then the passwords are replaces with stars; 
        /// otherwise, the passwords are completly removed (key and value).</param>
        /// <returns>The connection string without any password.</returns>
        public static string ToStringNoPassword(this string connstr, bool showStars)
        {
            if (string.IsNullOrEmpty(connstr)) return connstr;

            // parse the connection string
            string[] parts = connstr.Split(';');
            var pairs = new Dictionary<string, string>();
            
            foreach (string part in parts)
            {
                if (part.Contains("="))
                {
                    int index = part.IndexOf('=');
                    var left = part.Substring(0, index).Trim();
                    var right = part.Substring(index + 1).Trim();
                    pairs.Add(left, right);
                }
                else pairs.Add(part, string.Empty);
            }

            // Rebuild the connection string.
            var builder = new List<string>();

            foreach (string key in pairs.Keys)
            {
                string value = pairs[key];
                if ((string.Compare(key, "password", StringComparison.InvariantCultureIgnoreCase) == 0) ||
                    (string.Compare(key, "pwd", StringComparison.InvariantCultureIgnoreCase) == 0))
                {
                    // depending on the option showStars, we don't echo the key nor its value
                    // or we replace its value with stars.
                    if (showStars)
                    {
                        string temp = key;
                        if (!string.IsNullOrEmpty(value))
                            temp += string.Format("={0}", new string('*', value.Length));
                        builder.Add(temp);
                    }
                }
                else
                {
                    string temp = key;
                    if (!string.IsNullOrEmpty(value))
                        temp += string.Format("={0}", value);
                    builder.Add(temp);
                }
            }

            return string.Join(";", builder.ToArray());
        }

        /// <summary>
        /// Converts a <see cref="StringComparison"/> into a <see cref="StringComparer"/>.
        /// </summary>
        /// <param name="comparison">The <see cref="StringComparison"/> to convert.</param>
        /// <returns>An instance of <see cref="StringComparer"/>.</returns>
        public static StringComparer ToStringComparer(this StringComparison comparison)
        {
            switch (comparison)
            {
                case StringComparison.CurrentCulture:
                    return StringComparer.CurrentCulture;

                case StringComparison.CurrentCultureIgnoreCase:
                    return StringComparer.CurrentCultureIgnoreCase;
                
                case StringComparison.InvariantCulture:
                    return StringComparer.InvariantCulture;

                case StringComparison.InvariantCultureIgnoreCase:
                    return StringComparer.InvariantCultureIgnoreCase;

                case StringComparison.Ordinal:
                    return StringComparer.Ordinal;

                case StringComparison.OrdinalIgnoreCase:
                    return StringComparer.OrdinalIgnoreCase;
            }

            throw new ArgumentException(string.Format(
                "StringComparison {0} can't be converted to StringComparer", comparison), "comparison");
        }

        public static string FirstLetterUpperCased(this string original)
        {
            return FirstLetterUpperCased(original, CultureInfo.CurrentUICulture);
        }

        public static string FirstLetterUpperCasedInvariant(this string original)
        {
            return FirstLetterUpperCased(original, CultureInfo.CurrentUICulture);
        }

        public static string FirstLetterUpperCased(this string original, CultureInfo ci)
        {
            if (string.IsNullOrEmpty(original)) return original;
            if (original.Length == 1) return original.ToUpper(ci);
            else return string.Format("{0}{1}", original[0].ToString().ToUpper(ci), original.Substring(1));
        }

        internal static T ConvertToType<T>(this string value)
        {
            return (T)ConvertToType(value, typeof(T));
        }

        /// <summary>
        /// Converts a string into an object of type <paramref name="targetType"/>.
        /// </summary>
        /// <remarks>
        /// This method is used by <see cref="Delta.CertXplorer.Collections.DictionarySerializer"/>.
        /// </remarks>
        /// <param name="value">The string value.</param>
        /// <param name="targetType">Type of the target object.</param>
        /// <returns>An object of the correct type containing the converted value (or null).</returns>
        internal static object ConvertToType(this string value, Type targetType)
        {
            if (targetType == typeof(string)) return value;
            else
            {
                var converter = TypeDescriptor.GetConverter(targetType);
                if (converter.CanConvertFrom(typeof(string)))
                {
                    // first, we try to convert using invariant culture.
                    try { return converter.ConvertFrom(null, CultureInfo.InvariantCulture, value); }
                    catch (Exception ex)
                    {
                        This.Logger.Verbose(string.Format(
                            "Unable to convert {0} to a {1} using invariant culture.",
                            value, targetType), ex);

                        // Then, second chance: we use the current culture.
                        return converter.ConvertFrom(null, Thread.CurrentThread.CurrentCulture, value);
                    }
                }
            }

            return null;
        }
    }
}
