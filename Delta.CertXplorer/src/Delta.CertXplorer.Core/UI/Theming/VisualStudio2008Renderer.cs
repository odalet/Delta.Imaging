using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

// Adapted from code by NickThissen available at http://www.vbforums.com/showthread.php?t=539578

namespace Delta.CertXplorer.UI.Theming
{
    internal class VisualStudio2008Renderer : ToolStripProfessionalRenderer
    {
        /// <summary>
        /// The palette used by this renderer.
        /// </summary>
        private static class RendererColors
        {
            public static Color CheckBackGround = Color.FromArgb(0xce, 0xed, 250);
            public static Color HorBackGroundGrayBlue = Color.FromArgb(0xe9, 0xec, 250);
            public static Color HorBackGroundWhite = Color.FromArgb(0xf4, 0xf7, 0xfc);
            public static Color ImageMarginBlue = Color.FromArgb(0xd4, 0xd8, 230);
            public static Color ImageMarginLine = Color.FromArgb(160, 160, 180);
            public static Color ImageMarginWhite = Color.FromArgb(0xf4, 0xf7, 0xfc);
            public static Color MenuBorder = Color.FromArgb(160, 160, 160);
            public static Color SelectedBackGroundBlue = Color.FromArgb(0xba, 0xe4, 0xf6);
            public static Color SelectedBackGroundBorder = Color.FromArgb(150, 0xd9, 0xf9);
            public static Color SelectedBackGroundDropBlue = Color.FromArgb(0x8b, 0xc3, 0xe1);
            public static Color SelectedBackGroundDropBorder = Color.FromArgb(0x30, 0x7f, 0xb1);
            public static Color SelectedBackGroundHeader_Blue = Color.FromArgb(0x92, 0xca, 230);
            public static Color SelectedBackGroundWhite = Color.FromArgb(0xf1, 0xf8, 0xfb);
            public static Color SubmenuBackGround = Color.FromArgb(240, 240, 240);
            public static Color ToolstripBtnBorder = Color.FromArgb(0x29, 0x99, 0xff);
            public static Color ToolstripBtnGradientBlue = Color.FromArgb(0x81, 0xc0, 0xe0);
            public static Color ToolstripBtnGradientBluePressed = Color.FromArgb(0x7c, 0xb1, 0xcc);
            public static Color ToolstripBtnGradientWhite = Color.FromArgb(0xed, 0xf8, 0xfd);
            public static Color ToolstripBtnGradientWhitePressed = Color.FromArgb(0xe4, 0xf5, 0xfc);
            public static Color VerBackGroundGrayBlue = Color.FromArgb(0xc4, 0xcb, 0xdb);
            public static Color VerBackGroundShadow = Color.FromArgb(0xb5, 190, 0xce);
            public static Color VerBackGroundWhite = Color.FromArgb(250, 250, 0xfd);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VisualStudio2008Renderer"/> class.
        /// </summary>
        public VisualStudio2008Renderer() : base() { }

        /// <summary>
        /// When overridden in a derived class, provides for custom initialization of the given <see cref="T:System.Windows.Forms.ToolStrip"/>.
        /// </summary>
        /// <param name="toolStrip">The <see cref="T:System.Windows.Forms.ToolStrip"/> to be initialized.</param>
        protected override void Initialize(ToolStrip toolStrip)
        {
            base.Initialize(toolStrip);
            toolStrip.ForeColor = Color.Black;
        }

        /// <summary>
        /// When overridden in a derived class, provides for custom initialization of the given <see cref="T:System.Windows.Forms.ToolStripItem"/>.
        /// </summary>
        /// <param name="item">The <see cref="T:System.Windows.Forms.ToolStripItem"/> to be initialized.</param>
        protected override void InitializeItem(ToolStripItem item)
        {
            base.InitializeItem(item);
            item.ForeColor = Color.Black;
        }

        protected override void OnRenderImageMargin(ToolStripRenderEventArgs e)
        {
            base.OnRenderImageMargin(e);

            // Gray background
            using (var brush = new SolidBrush(RendererColors.SubmenuBackGround))
                e.Graphics.FillRectangle(brush, 0, 0, e.ToolStrip.Width, e.ToolStrip.Height);

            // Draw ImageMargin background gradient
            using (var gradientBrush = new LinearGradientBrush(
                e.AffectedBounds,
                RendererColors.ImageMarginWhite,
                RendererColors.ImageMarginBlue,
                LinearGradientMode.Horizontal))
                e.Graphics.FillRectangle(gradientBrush, e.AffectedBounds);

            // Shadow at the right of image margin
            var rightStroke1 = new Rectangle(e.AffectedBounds.Width, 2, 1, e.AffectedBounds.Height);
            var rightStroke2 = new Rectangle(e.AffectedBounds.Width + 1, 2, 1, e.AffectedBounds.Height);
            using (var darkLineBrush = new SolidBrush(RendererColors.ImageMarginLine))
                e.Graphics.FillRectangle(darkLineBrush, rightStroke1);            
            e.Graphics.FillRectangle(Brushes.White, rightStroke2);

            // Border
            using (var borderPen = new Pen(RendererColors.MenuBorder))
            {
                var toolStripInner = new Rectangle(0, 1, e.ToolStrip.Width - 1, e.ToolStrip.Height - 2);
                e.Graphics.DrawRectangle(borderPen, toolStripInner);
            }
        }
        
        protected override void OnRenderItemCheck(ToolStripItemImageRenderEventArgs e)
        {
            base.OnRenderItemCheck(e);

            using (var outerBrush = new SolidBrush(e.Item.Selected ?
                RendererColors.ToolstripBtnBorder : RendererColors.SelectedBackGroundDropBorder))
                e.Graphics.FillRectangle(outerBrush, 3, 1, 20, 20);

            using (var innerBrush = new SolidBrush(RendererColors.CheckBackGround))
                e.Graphics.FillRectangle(innerBrush, 4, 2, 18, 18);

            e.Graphics.DrawImage(e.Image, 5, 3);
        }

        // Never call the base method!
        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e) { }

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            // No call to base!
            if (e.Item.Enabled)
            {
                if (!e.Item.IsOnDropDown && e.Item.Selected)
                {
                    Rectangle rect = new Rectangle(3, 2, e.Item.Width - 6, e.Item.Height - 4);
                    using (var brush = new LinearGradientBrush(
                        rect, RendererColors.SelectedBackGroundWhite, RendererColors.SelectedBackGroundHeader_Blue, LinearGradientMode.Vertical))
                        e.Graphics.FillRectangle(brush, rect);

                    DrawRoundedRectangle(e.Graphics, rect.Left - 1, rect.Top - 1, rect.Width, rect.Height + 1, 4, RendererColors.ToolstripBtnBorder);
                    DrawRoundedRectangle(e.Graphics, rect.Left - 2, rect.Top - 2, rect.Width + 2, rect.Height + 3, 4, Color.White);
                    e.Item.ForeColor = Color.Black;
                }
                else if (e.Item.IsOnDropDown && e.Item.Selected)
                {
                    Rectangle rect = new Rectangle(4, 2, e.Item.Width - 6, e.Item.Height - 4);
                    using (var brush = new LinearGradientBrush(
                        rect, RendererColors.SelectedBackGroundWhite, RendererColors.SelectedBackGroundBlue, LinearGradientMode.Vertical))
                        e.Graphics.FillRectangle(brush, rect);

                    DrawRoundedRectangle(e.Graphics, rect.Left - 1, rect.Top - 1, rect.Width, rect.Height + 1, 6, RendererColors.SelectedBackGroundBorder);
                    e.Item.ForeColor = Color.Black;
                }
                
                if ((e.Item is ToolStripMenuItem) && ((ToolStripMenuItem)e.Item).DropDown.Visible && !e.Item.IsOnDropDown)    
                {
                    Rectangle rect = new Rectangle(3, 2, e.Item.Width - 6, e.Item.Height - 4);
                    using (var brush = new LinearGradientBrush(
                        rect, Color.White, RendererColors.SelectedBackGroundDropBlue, LinearGradientMode.Vertical))
                        e.Graphics.FillRectangle(brush, rect);

                    DrawRoundedRectangle(e.Graphics, rect.Left - 1, rect.Top - 1, rect.Width, rect.Height + 1, 4, RendererColors.SelectedBackGroundDropBorder);
                    DrawRoundedRectangle(e.Graphics, rect.Left - 2, rect.Top - 2, rect.Width + 2, rect.Height + 3, 4, Color.White);
                    e.Item.ForeColor = Color.Black;
                }
            }
        }

        protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
        {
            base.OnRenderSeparator(e);
            using (var brush = new SolidBrush(RendererColors.ImageMarginLine))
                e.Graphics.FillRectangle(brush, 32, 3, e.Item.Width - 32, 1);
            e.Graphics.FillRectangle(Brushes.White, 32, 4, e.Item.Width - 32, 1);
        }

        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        {
            base.OnRenderToolStripBackground(e);

            if (e.ToolStrip is MenuStrip)
                RenderPanelBackground(e.Graphics, e.AffectedBounds);
            else
            {
                using (var brush = new LinearGradientBrush(
                    e.AffectedBounds, RendererColors.VerBackGroundWhite, RendererColors.VerBackGroundGrayBlue, LinearGradientMode.Vertical))
                    e.Graphics.FillRectangle(brush, e.AffectedBounds);

                using (var brush = new SolidBrush(RendererColors.VerBackGroundShadow))
                    e.Graphics.FillRectangle(brush, 0, e.ToolStrip.Height - 2, e.ToolStrip.Width, 1);
            }
        }

        protected override void OnRenderToolStripPanelBackground(ToolStripPanelRenderEventArgs e)
        {
            RenderPanelBackground(e.Graphics, e.ToolStripPanel.Bounds);
            e.Handled = true;
        }

        protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
        {
            base.OnRenderButtonBackground(e);

            bool pressed = e.Item.Pressed;
            bool selected = e.Item.Selected;
            if ((e.Item is ToolStripButton) && ((ToolStripButton)e.Item).Checked) selected = true;

            if (pressed || selected)
            {
                Color white = pressed ? RendererColors.ToolstripBtnGradientWhitePressed : RendererColors.ToolstripBtnGradientWhite;
                Color blue = pressed ? RendererColors.ToolstripBtnGradientBluePressed : RendererColors.ToolstripBtnGradientBlue;

                using (var brush = new SolidBrush(RendererColors.ToolstripBtnBorder))
                    e.Graphics.FillRectangle(brush, 1, 1, e.Item.Width - 2, e.Item.Height - 2);

                Rectangle rect = new Rectangle(1, 1, e.Item.Width - 2, e.Item.Height - 2);
                using (var brush = new LinearGradientBrush(rect, white, blue, LinearGradientMode.Vertical))
                    e.Graphics.FillRectangle(brush, rect);
            }
        }

        #region Helper

        private void RenderPanelBackground(Graphics graphics, Rectangle bounds)
        {
            using (var brush = new LinearGradientBrush(
                bounds, RendererColors.HorBackGroundGrayBlue, RendererColors.HorBackGroundWhite, LinearGradientMode.Horizontal))
                graphics.FillRectangle(brush, bounds);
        }

        private void DrawRoundedRectangle(Graphics objGraphics, int m_intxAxis, int m_intyAxis, int m_intWidth, int m_intHeight, int m_diameter, Color color)
        {
            Pen pen = new Pen(color);
            RectangleF BaseRect = new RectangleF((float)m_intxAxis, (float)m_intyAxis, (float)m_intWidth, (float)m_intHeight);
            SizeF VB_t_struct_S0 = new SizeF((float)m_diameter, (float)m_diameter);
            RectangleF ArcRect = new RectangleF(BaseRect.Location, VB_t_struct_S0);
            objGraphics.DrawArc(pen, ArcRect, 180f, 90f);
            objGraphics.DrawLine(pen, m_intxAxis + ((int)Math.Round((double)(((double)m_diameter) / 2.0))), m_intyAxis, (m_intxAxis + m_intWidth) - ((int)Math.Round((double)(((double)m_diameter) / 2.0))), m_intyAxis);
            ArcRect.X = BaseRect.Right - m_diameter;
            objGraphics.DrawArc(pen, ArcRect, 270f, 90f);
            objGraphics.DrawLine(pen, (int)(m_intxAxis + m_intWidth), (int)(m_intyAxis + ((int)Math.Round((double)(((double)m_diameter) / 2.0)))), (int)(m_intxAxis + m_intWidth), (int)((m_intyAxis + m_intHeight) - ((int)Math.Round((double)(((double)m_diameter) / 2.0)))));
            ArcRect.Y = BaseRect.Bottom - m_diameter;
            objGraphics.DrawArc(pen, ArcRect, 0f, 90f);
            objGraphics.DrawLine(pen, (int)(m_intxAxis + ((int)Math.Round((double)(((double)m_diameter) / 2.0)))), (int)(m_intyAxis + m_intHeight), (int)((m_intxAxis + m_intWidth) - ((int)Math.Round((double)(((double)m_diameter) / 2.0)))), (int)(m_intyAxis + m_intHeight));
            ArcRect.X = BaseRect.Left;
            objGraphics.DrawArc(pen, ArcRect, 90f, 90f);
            objGraphics.DrawLine(pen, m_intxAxis, m_intyAxis + ((int)Math.Round((double)(((double)m_diameter) / 2.0))), m_intxAxis, (m_intyAxis + m_intHeight) - ((int)Math.Round((double)(((double)m_diameter) / 2.0))));
        }

        #endregion
    }
}
