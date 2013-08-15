using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Delta.CertXplorer.UI.Theming
{
    public static class ThemesManager
    {
        private static readonly ProfessionalColorTable defaultColorTable = 
            new ProfessionalColorTable() { UseSystemColors = true };

        public static ProfessionalColorTable DefaultColorTable
        {
            get { return defaultColorTable; }
        }

        public static void RegisterThemeAwareControl(Control control, Action<ToolStripRenderer> onThemeChanged)
        {
            if ((control != null) && !control.IsDisposed)
            {
                ToolStripManager.RendererChanged += (s, e) => UpdateRenderer(control, onThemeChanged);
                UpdateRenderer(control, onThemeChanged);                
            }
        }

        public static ToolStripRenderer CloneCurrentRenderer()
        {
            ToolStripRenderer renderer = null;

            IThemingService th = This.GetService<IThemingService>();
            if (th != null) renderer = th.CreateThemeRenderer(th.Current);

            if (renderer == null)
            {
                if (ToolStripManager.Renderer is ToolStripProfessionalRenderer)
                {
                    renderer = new ToolStripProfessionalRenderer(
                        ((ToolStripProfessionalRenderer)ToolStripManager.Renderer).ColorTable);
                }
                else renderer = new ToolStripProfessionalRenderer();
            }

            return renderer;
        }

        public static ProfessionalColorTable GetCurrentColorTable()
        {
            ToolStripRenderer renderer = null;

            IThemingService th = This.GetService<IThemingService>();
            if (th != null) renderer = th.CreateThemeRenderer(th.Current);

            if (renderer == null) renderer = ToolStripManager.Renderer;

            if ((renderer != null) && renderer is ToolStripProfessionalRenderer)
                    return ((ToolStripProfessionalRenderer)renderer).ColorTable;

            return defaultColorTable;
        }

        private static void UpdateRenderer(Control control, Action<ToolStripRenderer> action)
        {
            if ((control == null) || control.IsDisposed) return;

            var clone = CloneCurrentRenderer();
            if (action != null) action(clone);
        }
    }
}
