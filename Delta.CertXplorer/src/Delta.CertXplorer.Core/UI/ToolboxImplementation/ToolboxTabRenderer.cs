using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Delta.CertXplorer.UI.ToolboxImplementation
{
    internal class ToolboxTabRenderer : ToolStripProfessionalRenderer
    {
        private Color deepBlue = Color.FromArgb(255, 150, 179, 225);
        private Color lightBlue = Color.FromArgb(255, 195, 211, 237);
        private Color borderBlue = Color.FromArgb(255, 0, 0, 128);

        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
        {
            // No border
        }

        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        {
            // No background
        }

        protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
        {
            ToolStripButton button = e.Item as ToolStripButton;
            Graphics g = e.Graphics;
            Rectangle r = new Rectangle(Point.Empty, button.Size);
            bool drawBorder = false;

            Brush brush = null;

            if (button.CheckState == CheckState.Unchecked)
            {
                drawBorder = true;

                if (button.Pressed) brush = new SolidBrush(deepBlue);                    
                else if (button.Selected) brush = new SolidBrush(lightBlue);                                        
                else drawBorder = false;
            }
            else
            {
                drawBorder = true;
                if (button.Selected) brush = new SolidBrush(deepBlue);          
                else brush = new SolidBrush(lightBlue);                    
            }

            if (brush != null)
            {
                if ((r.Width != 0) && (r.Height != 0))
                    g.FillRectangle(brush, r);
                brush.Dispose();
            }

            if (drawBorder)
            {
                using (Pen pen = new Pen(borderBlue))
                    g.DrawRectangle(pen, r.X, r.Y, r.Width - 1, r.Height - 1);
            }
        }        
    }
}
