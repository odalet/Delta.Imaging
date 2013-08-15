using System;
using System.IO;
using System.Resources;
using System.Reflection;
using System.Collections.Generic;

namespace Delta.CertXplorer.Resources
{
    /// <summary>
    /// This class helps in locating a resource, given its name.
    /// </summary>
    public static class ResourceLocator
    {
        private static Dictionary<string, ResourceManager> resourceManagerCache = 
            new Dictionary<string, ResourceManager>();

        /// <summary>
        /// Gets the string identified by <paramref name="fullResourceName"/> and
        /// located inside Assembly <paramref name="assembly"/>.
        /// </summary>
        /// <param name="fullResourceName">Full name of the resource.</param>
        /// <param name="assembly">The assembly containing the resource.</param>
        /// <returns>A <see cref="System.String"/> object or <c>System.String.Empty</c> if not found.</returns>
        public static string GetString(string fullResourceName, Assembly assembly)
        {
            string resourceName;
            var rm = GetResourceManager(fullResourceName, assembly, out resourceName);
            if (rm == null) return string.Empty;
            else
            {
                try { return rm.GetString(resourceName); }
                catch (Exception ex)
                {
                    var debugEx = ex;
                    // eat the exception and return an empty string.
#if DEBUG
                    return fullResourceName;
#else
                    return string.Empty;
#endif
                }
            }
        }

        /// <summary>
        /// Gets the stream identified by <paramref name="fullResourceName"/> and
        /// located inside Assembly <paramref name="assembly"/>.
        /// </summary>
        /// <param name="fullResourceName">Full name of the resource.</param>
        /// <param name="assembly">The assembly containing the resource.</param>
        /// <returns>A <see cref="System.IO.Stream"/> object or <c>null</c> if not found.</returns>
        public static Stream GetStream(string fullResourceName, Assembly assembly)
        {
            string resourceName;
            var rm = GetResourceManager(fullResourceName, assembly, out resourceName);
            if (rm == null) return null;
            else
            {
                try { return rm.GetStream(resourceName); }
                catch (Exception ex)
                {
                    var debugEx = ex;
                    // eat the exception and return an null stream.
                    return null;
                }
            }
        }

        /// <summary>
        /// Gets the object identified by <paramref name="fullResourceName"/> and
        /// located inside Assembly <paramref name="assembly"/>.
        /// </summary>
        /// <param name="fullResourceName">Full name of the resource.</param>
        /// <param name="assembly">The assembly containing the resource.</param>
        /// <returns>A <see cref="System.Object"/> or <c>null</c> if not found.</returns>
        public static object GetObject(string fullResourceName, Assembly assembly)
        {
            string resourceName;
            var rm = GetResourceManager(fullResourceName, assembly, out resourceName);
            if (rm == null) return null;
            else
            {
                try { return rm.GetStream(resourceName); }
                catch (Exception ex)
                {
                    var debugEx = ex;
                    // eat the exception and return an null object.
                    return null;
                }
            }
        }

        /// <summary>
        /// Gets a resource manager, given the full resource name and the target assembly.
        /// </summary>
        /// <param name="fullResourceName">Full name of the resource.</param>
        /// <param name="assembly">The assembly containing the resource.</param>
        /// <param name="resourceName">This returns the local name of the resource.</param>
        /// <returns>A <see cref="System.Resources.ResourceManager"/> object or <c>null</c> if not found.</returns>
        private static ResourceManager GetResourceManager(string fullResourceName, Assembly assembly, out string resourceName)
        {
            try
            {
                string baseName = string.Empty;

                int index = fullResourceName.LastIndexOf('.');
                if (index == -1) resourceName = fullResourceName;
                else
                {
                    baseName = fullResourceName.Substring(0, index);
                    resourceName = fullResourceName.Substring(index + 1);
                }

                return GetResourceManagerFromCache(baseName, assembly);                
            }
            catch (Exception ex)
            {
                var debugException = ex;
                resourceName = string.Empty;
                return null;
            }
        }

        /// <summary>
        /// Gets a resource manager from cache.
        /// </summary>
        /// <param name="baseName">Base name of the resource.</param>
        /// <param name="assembly">The assembly containing the resource.</param>
        /// <returns>A <see cref="System.Resources.ResourceManager"/> object or <c>null</c> if not found.</returns>
        private static ResourceManager GetResourceManagerFromCache(string baseName, Assembly assembly)
        {
            if (!resourceManagerCache.ContainsKey(baseName))
            {
                var rm = new ResourceManager(baseName, assembly);
                resourceManagerCache.Add(baseName, rm);
            }

            return resourceManagerCache[baseName];
        }
    }
}
