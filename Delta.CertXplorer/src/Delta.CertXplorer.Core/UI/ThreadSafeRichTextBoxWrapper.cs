using System;
using System.Drawing;
using System.Windows.Forms;

namespace Delta.CertXplorer.UI
{
    public class ThreadSafeRichTextBoxWrapper : ThreadSafeTextBoxWrapper
    {
        private RichTextBox control = null;

        private delegate void SetSelectionFontDelegate(Font SelectionFont);
        private delegate Font GetSelectionFontDelegate();
        private delegate void SetSelectionBackColorDelegate(Color SelectionBackColor);
        private delegate Color GetSelectionBackColorDelegate();
        private delegate void SetSelectionColorDelegate(Color SelectionColor);
        private delegate Color GetSelectionColorDelegate();

        private ThreadSafeRichTextBoxWrapper(TextBoxBase textbox) : base(textbox) { }
        public ThreadSafeRichTextBoxWrapper(RichTextBox textbox)
            : base(textbox)
        {
            if (textbox == null) throw new ArgumentNullException("textbox");
            control = textbox;
        }

        public Font SelectionFont { get { return GetSelectionFont(); } set { SetSelectionFont(value); } }

        public void SetSelectionFont(Font font)
        {
            if (control.InvokeRequired)
            {
                SetSelectionFontDelegate del = new SetSelectionFontDelegate(SetSelectionFont);
                control.Invoke(del, new object[] { font });
            }
            else control.SelectionFont = font;
        }

        public Font GetSelectionFont()
        {
            if (control.InvokeRequired)
            {
                GetSelectionFontDelegate del = new GetSelectionFontDelegate(GetSelectionFont);
                return (Font)control.Invoke(del);
            }
            else return control.SelectionFont;
        }

        public Color SelectionBackColor { get { return GetSelectionBackColor(); } set { SetSelectionBackColor(value); } }

        public void SetSelectionBackColor(Color color)
        {
            if (control.InvokeRequired)
            {
                SetSelectionBackColorDelegate del = new SetSelectionBackColorDelegate(SetSelectionBackColor);
                control.Invoke(del, new object[] { color });
            }
            else control.SelectionBackColor = color;
        }

        public Color GetSelectionBackColor()
        {
            if (control.InvokeRequired)
            {
                GetSelectionBackColorDelegate del = new GetSelectionBackColorDelegate(GetSelectionBackColor);
                return (Color)control.Invoke(del);
            }
            else return control.SelectionBackColor;
        }

        public Color SelectionColor { get { return GetSelectionColor(); } set { SetSelectionColor(value); } }

        public void SetSelectionColor(Color color)
        {
            if (control.InvokeRequired)
            {
                SetSelectionColorDelegate del = new SetSelectionColorDelegate(SetSelectionColor);
                control.Invoke(del, new object[] { color });
            }
            else control.SelectionColor = color;
        }

        public Color GetSelectionColor()
        {
            if (control.InvokeRequired)
            {
                GetSelectionColorDelegate del = new GetSelectionColorDelegate(GetSelectionColor);
                return (Color)control.Invoke(del);
            }
            else return control.SelectionColor;
        }
    }
}
