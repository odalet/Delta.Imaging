using System.IO;
using System.Linq;
using System.Reflection;

namespace Delta.CertXplorer.Config
{
    internal static class ConfigResources 
    {
        private static string prefix = null;

        public static bool Contains(string name)
        {
            var resourceName = Prefix + name;
            var assembly = Assembly.GetExecutingAssembly();
            return assembly.GetManifestResourceNames().Contains(resourceName);
        }

        public static byte[] GetResource(string name)
        {
            var resourceName = Prefix + name;
            var assembly = Assembly.GetExecutingAssembly();
            if (!assembly.GetManifestResourceNames().Contains(resourceName))
                return null;

            using (var mstream = new MemoryStream())
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                stream.CopyTo(mstream);
                return mstream.ToArray();
            }
        }

        private static string Prefix
        {
            get
            {
                if (prefix == null)
                {
                    var type = typeof(ConfigResources);
                    var full = type.FullName;
                    prefix = full.Substring(0, full.Length - type.Name.Length);
                }

                return prefix;
            }
        }
    }
}
