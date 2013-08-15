using System;
using System.Drawing;
using System.Windows.Forms;

namespace CryptoHelperPlugin.UI
{
    public partial class DataBox : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataBox"/> class.
        /// </summary>
        public DataBox()
        {
            InitializeComponent();

            box.Left = 2;
            box.Top = 2;
            box.Width = base.Width - 4;
            box.Height = base.Height - 4;

            base.SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.UserPaint, true);

            BindMenus();
        }

        public string Text
        {
            get { return box.Text; }
            set { box.Text = value; }
        }
        
        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Paint"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs"/> that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Paint border
            var bounds = new Rectangle(0, 0, base.Width - 1, base.Height - 1);
            using (var pen = new Pen(Color.FromArgb(127, 157, 185)))
                e.Graphics.DrawRectangle(pen, bounds);
        }

        private void BindMenus()
        {
            undoToolStripMenuItem.Click += (s, _) => box.Undo();
            redoToolStripMenuItem.Click += (s, _) => box.Redo();
            cutToolStripMenuItem.Click += (s, _) => box.Cut();
            copyToolStripMenuItem.Click += (s, _) => box.Copy();
            pasteToolStripMenuItem.Click += (s, _) => box.Paste();
            selectAllToolStripMenuItem.Click += (s, _) => box.SelectAll();            
            printToolStripMenuItem.Click += (s, _) => box.Print(true);
            printPreviewToolStripMenuItem.Click += (s, _) => box.PrintPreview();
        }
    }
}
