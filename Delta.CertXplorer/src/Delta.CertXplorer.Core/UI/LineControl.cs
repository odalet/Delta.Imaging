using System;
using System.Drawing;
using System.Windows.Forms;


namespace Delta.CertXplorer.UI
{
    public class LineControl : Control
    {
        private Color color1 = Color.FromArgb(0xcc, 0xcc, 0xcc);
        private Color color2 = Color.FromArgb(0xf3, 0xf3, 0xf3);
        private Color textColor = Color.FromArgb(96, 128, 186);

        public LineControl()
        {
            base.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            base.Name = "LineControl";
            base.Size = new Size(150, 12);
            base.TabStop = false;
        }

        public Color TextColor { get { return textColor; } set { SetTextColor(value); } }

        protected override void OnTextChanged(EventArgs e) { Invalidate(); }

        protected virtual void OnTextColorChanged(EventArgs e) { Invalidate(); }

        protected override void OnPaint(PaintEventArgs e)
        {
            int y = Math.Max(0, (base.ClientRectangle.Height - 2) / 2);
            int x = 0;

            string text = base.Text;
            if (!string.IsNullOrEmpty(text))
            {
                x = (int)Math.Ceiling((double)(e.Graphics.MeasureString(text, base.Font).Width));

                using (SolidBrush brush = new SolidBrush(textColor))
                using (var sformat = new StringFormat())
                {
                    sformat.Alignment = StringAlignment.Near;
                    sformat.LineAlignment = StringAlignment.Center;

                    RectangleF rectf = new RectangleF(
                        0f, 0f, (float)base.ClientRectangle.Width, (float)base.ClientRectangle.Height);
                    e.Graphics.DrawString(text, base.Font, brush, rectf, sformat);
                }
            }            

            // NB : -1f indique que le crayon fait exactement 1 pixel d'épaisseur
            using (Pen p1 = new Pen(color1, -1f))
                e.Graphics.DrawLine(p1, x, y, ClientRectangle.Width, y);
            using (Pen p2 = new Pen(color2, -1f))
                e.Graphics.DrawLine(p2, x, y + 1, ClientRectangle.Width, y + 1);            
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            if (BackColor != Color.Transparent)
            {
                using (SolidBrush brush = new SolidBrush(BackColor))
                    pevent.Graphics.FillRectangle(brush, ClientRectangle);
            }
            else base.OnPaintBackground(pevent);
        }

        private void SetTextColor(Color color)
        {
            if (color != textColor)
            {
                textColor = color;
                OnTextColorChanged(EventArgs.Empty);
            }
        }
    }
}
