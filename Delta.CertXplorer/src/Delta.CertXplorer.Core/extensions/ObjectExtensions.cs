using System;
using System.Linq;
using System.Threading;
using System.Reflection;
using System.Globalization;
using System.ComponentModel;

namespace Delta.CertXplorer
{
    /// <summary>
    /// Helper methods extending <see cref="System.Object"/>.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Converts an object to a string by using a Type converter (if any exists).
        /// </summary>
        /// <remarks>
        /// This method extends <see cref="System.Object"/>; it is symetrical
        /// to <see cref="StringExtensions.ConvertToType"/>.
        /// </remarks>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The string representation of the input value 
        /// or an empty string if the input value is <c>null</c>.
        /// </returns>
        public static string ConvertToString(this object value)
        {
            if (value is string) return (string)value;
            else if (value == null) return string.Empty;
            else
            {
                var objectType = value.GetType();
                var converter = TypeDescriptor.GetConverter(objectType);
                if (converter.CanConvertTo(typeof(string)))
                {
                    // we always convert using invariant culture.
                    return converter.ConvertToInvariantString(value);
                }
                else return value.ToString();
            }
        }

        /// <summary>
        /// Fills the specified object's properties with default values (specified with
        /// <see cref="System.ComponentModel.DefaultValueAttribute"/>).
        /// </summary>
        /// <typeparam name="T">Type of the value to fill.</typeparam>
        /// <param name="target">The value to fill.</param>
        /// <returns>The specified value is returned.</returns>
        public static T FillWithDefaultValues<T>(this T target)
        {
            if ((object)target == null) return target;

            var targetType = typeof(T);
            var bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            foreach (var pi in targetType.GetProperties(bindingFlags))
            {
                var set = pi.GetSetMethod(true);
                if (set != null) // we ignore read-only properties.
                {
                    object value = null;
                    // Search for a DefaultAttribute
                    var attribute = pi.GetCustomAttributes(typeof(DefaultValueAttribute), false)
                        .FirstOrDefault() as DefaultValueAttribute;
                    if (attribute != null) value = attribute.Value;

                    var targetValue = value.ConvertToType(pi.PropertyType);
                    if (targetValue != null) set.Invoke(target, new object[] { targetValue });
                }
            }

            return target;
        }

        /// <summary>
        /// Converts an object of an unknown type into an object of type <paramref name="targetType"/>.
        /// </summary>
        /// <remarks>
        /// This method is used by <see cref="Delta.CertXplorer.Collections.DictionarySerializer"/>.
        /// </remarks>
        /// <param name="value">The string value.</param>
        /// <param name="targetType">Type of the target object.</param>
        /// <returns>An object of the correct type containing the converted value (or null).</returns>
        internal static object ConvertToType(this object value, Type targetType)
        {
            if (value == null) return null;
            if (targetType.IsAssignableFrom(value.GetType())) return value;
            if (value is string) return StringExtensions.ConvertToType((string)value, targetType);

            // If we are here, we try to convert using a TypeConverter.
            var converter = TypeDescriptor.GetConverter(targetType);
            if (converter.CanConvertFrom(value.GetType()))
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

            return null;
        }
    }
}
