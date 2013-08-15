using System;
using System.Linq;
using System.Collections.Generic;

namespace Delta.CertXplorer
{
    /// <summary>
    /// This class doesn't contain extension methods, but is a helper aimed
    /// at providing generic helpers for enumerations.
    /// </summary>
    /// <typeparam name="T"><c>T</c> must represent an enumeration type.</typeparam>
    public static class Enums<T>
    {
        /// <summary>
        /// Gets the values of an enumeration.
        /// </summary>
        /// <value>The values.</value>
        /// <remarks>
        /// The following sample shows the difference between getting an enumeration's values the old way
        /// and using this property (given an enumeration type called <c>MyEnum</c> exists):
        /// <example>
        /// Old way:
        /// <code lang="cs">
        /// foreach (object o in Enum.GetValues(typeof(MyEnum)))
        /// {
        ///     MyEnum value = (MyEnum)o;
        ///     DoSomething(value);
        /// }
        /// </code>
        /// New way:
        /// <code>
        /// foreach (var value in Enums&lt;MyEnum&gt;.Values)
        ///     DoSomething(value);
        /// </code>
        /// </example>
        /// </remarks>
        public static IEnumerable<T> Values
        {
            get { return Enum.GetValues(typeof(T)).Cast<T>(); }
        }

        /// <summary>
        /// Gets the names of an enumeration.
        /// </summary>
        /// <value>The names.</value>
        /// <remarks>
        /// The following sample shows the difference between getting an enumeration's names the old way
        /// and using this property (given an enumeration type called <c>MyEnum</c> exists):
        /// <example>
        /// Old way:
        /// <code lang="cs">
        /// foreach (string name in Enum.GetNames(typeof(MyEnum)))
        ///     DoSomething(name);
        /// </code>
        /// New way:
        /// <code>
        /// foreach (var value in Enums&lt;MyEnum&gt;.Names)
        ///     DoSomething(value);
        /// </code>        
        /// </example>
        /// </remarks>
        public static IEnumerable<string> Names
        {
            get { return Enum.GetNames(typeof(T)).AsEnumerable(); }
        }

        /// <summary>
        /// Gets the list of the enumeration's values indexed by their name.
        /// </summary>
        /// <value>The values indexed by their name.</value>
        public static IEnumerable<KeyValuePair<string, T>> ValuesByName
        {
            get 
            {
                return Enum.GetValues(typeof(T)).Cast<T>().Select(value => 
                    new KeyValuePair<string, T>(Enum.GetName(typeof(T), value), value));
            }
        }

        /// <summary>
        /// Gets the list of the enumeration's names indexed by their value.
        /// </summary>
        /// <value>The names indexed by their value.</value>
        public static IEnumerable<KeyValuePair<T, string>> NamesByValue
        {
            get
            {
                return Enum.GetValues(typeof(T)).Cast<T>().Select(value =>
                    new KeyValuePair<T, string>(value, Enum.GetName(typeof(T), value)));
            }
        }
    }
}
