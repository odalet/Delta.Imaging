using System.Drawing;

using Delta.CertXplorer.Extensibility;

namespace Delta.CertXplorer
{
    public static class PluginExtensions
    {
        public static Image GetIcon(this IPlugin plugin)
        {
            if (plugin == null) return null;
            var image = plugin.Icon;
            if (image == null) image = Properties.Resources.DefaultPluginIcon;

            return image;
        }
    }
}
