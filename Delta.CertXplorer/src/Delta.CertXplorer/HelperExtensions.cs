using System;
using System.Linq;
using System.Windows.Forms;

namespace Delta.CertXplorer
{
    internal static class HelperExtensions
    {
        public static string ToDebugString(this byte[] data, int maxCount)
        {
            if (data == null) return "Null";
            if (data.Length == 0) return "Empty";
            var count = Math.Min(data.Length, maxCount);
            return string.Join(" ", data.Take(count).Select(b => b.ToString("X2")).ToArray());
        }

        /// <summary>
        /// Sets a value indicating whether the edges of the specified 
        /// toolstrip should have a rounded rather than a square appearance.
        /// </summary>
        /// <param name="toolStrip">The toolstrip.</param>
        /// <param name="rounded">if set to <c>true</c> the edges have a rounded appearance.</param>
        public static void SetRoundedEdges(this ToolStrip toolStrip, bool rounded)
        {
            var renderer = toolStrip.Renderer;
            if (renderer is ToolStripProfessionalRenderer)
                ((ToolStripProfessionalRenderer)renderer).RoundedEdges = rounded;
        }
    }
}
