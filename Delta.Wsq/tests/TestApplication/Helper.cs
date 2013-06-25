using System.IO;
using System.Reflection;

namespace TestApplication
{
    internal static class Helper
    {
        public static string GetImagesPath()
        {
            var assembly = Assembly.GetEntryAssembly();
            return Path.Combine(Path.GetDirectoryName(assembly.Location), "Images");
        }
    }
}
