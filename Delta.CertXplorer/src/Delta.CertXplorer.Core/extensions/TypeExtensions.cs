using System;
using System.Reflection;

namespace Delta.CertXplorer
{
    /// <summary>
    /// Extends the <see cref="System.Type"/> class.
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Determines whether the specified type is a descendant of <paramref name="parentType"/>.
        /// </summary>
        /// <param name="type">The type to test.</param>
        /// <param name="parentType">Type of the supposed parent.</param>
        /// <returns>
        /// 	<c>true</c> if the specified type is a descendant of <paramref name="parentType"/>;
        /// otherwise, <c>false</c>.
        /// </returns>
        public static bool IsA(this Type type, Type parentType)
        {
            if (parentType == null) return type == null;
            return parentType.IsAssignableFrom(type);
        }

        /// <summary>
        /// Determines whether the specified type is a descendant of <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">Type of the supposed parent.</typeparam>
        /// <param name="type">The type to test.</param>
        /// <returns>
        /// 	<c>true</c> if the specified type is a descendant of <typeparamref name="T"/>;
        /// otherwise, <c>false</c>.
        /// </returns>
        public static bool IsA<T>(this Type type)
        {
            return IsA(type, typeof(T));
        }

        /// <summary>
        /// Creates an instance of the given <paramref name="type"/>.
        /// </summary>
        /// <remarks>
        /// Type <paramref name="type"/> must provide a parameterless constructor.
        /// The constructor needn't be public.
        /// </remarks>
        /// <param name="type">The type for which to create an instance.</param>
        /// <returns>An instance of <paramref name="type"/>.</returns>
        public static object CreateInstance(this Type type)
        {
            var flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

            var ci = type.GetConstructor(flags, null, Type.EmptyTypes, null);
            if (ci == null) throw new ApplicationException(string.Format("Type {0} must define a parameterless constructor.", type));
            return ci.Invoke(null);
        }

        /// <summary>
        /// Determines whether the specified type is a nullable type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        /// 	<c>true</c> if the specified type is nullable; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNullable(this Type type)
        {
            if (!type.IsGenericType) return false;
            return type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }
    }
}
