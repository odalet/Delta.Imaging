using System;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.Win32;

namespace Delta.CertXplorer.UI.Theming
{
    internal class CertXplorerColorTable : ProfessionalColorTable
    {
        private static Color toolStripBorder = Color.Black;
        private static Color gradientBegin = Color.White;
        private static Color gradientMiddle = Color.Gray;
        private static Color gradientEnd = Color.Black;

        private bool useGradientBackground = false;

        public CertXplorerColorTable() : this(true) { }

        public CertXplorerColorTable(bool gradientBackground)
        {
            useGradientBackground = gradientBackground;
            base.UseSystemColors = true;
            InitColors();

            SystemEvents.UserPreferenceChanged += (s, e) => InitColors();
        }

        #region Colors

        public override Color ImageMarginGradientBegin { get { return gradientBegin; } }

        public override Color ImageMarginGradientMiddle { get { return gradientMiddle; } }

        public override Color ImageMarginGradientEnd { get { return gradientEnd; } }

        public override Color MenuStripGradientEnd
        {
            get
            {
                if (useGradientBackground)
                    return base.MenuStripGradientEnd;
                else return base.MenuStripGradientBegin;
            }
        }

        public override Color ToolStripPanelGradientEnd
        {
            get
            {
                if (useGradientBackground)
                    return base.ToolStripPanelGradientEnd;
                else return base.ToolStripPanelGradientBegin;
            }
        }

        public override Color ToolStripBorder { get { return toolStripBorder; } }

        public override Color ToolStripGradientBegin { get { return gradientBegin; } }

        public override Color ToolStripGradientEnd { get { return gradientEnd; } }

        #endregion

        private void InitColors()
        {
            toolStripBorder = SystemColors.ControlDark;
            gradientBegin = SystemColors.ButtonHighlight;
            gradientEnd = SystemColors.ButtonShadow;
            gradientMiddle = CalcMiddleColor(gradientBegin, gradientEnd);
        }

        private Color CalcMiddleColor(Color c1, Color c2)
        {
            return Color.FromArgb(
            CalcMiddleByte(c1.R, c2.R),
            CalcMiddleByte(c1.G, c2.G),
            CalcMiddleByte(c1.B, c2.B));
        }

        private byte CalcMiddleByte(Byte b1, byte b2)
        {
            if (b1 > b2) return (byte)((float)b2 + ((float)b1 - (float)b2) / 2f);
            else return (byte)((float)b1 + ((float)b2 - (float)b1) / 2f);
        }
    }
}
