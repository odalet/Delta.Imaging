using System;
using System.Drawing;
using System.Windows.Forms;

namespace Delta.CertXplorer.UI
{
    public class ThreadSafeTextBoxWrapper
    {
        private TextBoxBase control = null;

        private delegate void SetTextDelegate(string text);
        private delegate string GetTextDelegate();
        private delegate void SetFontDelegate(Font font);
        private delegate Font GetFontDelegate();
        private delegate void AppendTextDelegate(string text);
        private delegate void ScrollToCaretDelegate();

        public ThreadSafeTextBoxWrapper(TextBoxBase textbox)
        {
            if (textbox == null) throw new ArgumentNullException("textbox");
            control = textbox;
        }

        public string Text { get { return GetText(); } set { SetText(value); } }

        public void SetText(string text)
        {
            if (control.IsDisposed) return;
            if (control.InvokeRequired)
            {
                SetTextDelegate del = new SetTextDelegate(SetText);
                control.Invoke(del, new object[] { text });
            }
            else control.Text = text;
        }

        public string GetText()
        {
            if (control.IsDisposed) return string.Empty;
            if (control.InvokeRequired)
            {
                GetTextDelegate del = new GetTextDelegate(GetText);
                return (string)control.Invoke(del);
            }
            else return control.Text;
        }

        public Font Font { get { return GetFont(); } set { SetFont(value); } }

        public void SetFont(Font font)
        {
            if (control.IsDisposed) return;
            if (control.InvokeRequired)
            {
                SetFontDelegate del = new SetFontDelegate(SetFont);
                control.Invoke(del, new object[] { font });
            }
            else control.Font = font;
        }

        public Font GetFont()
        {
            if (control.IsDisposed) return null;
            if (control.InvokeRequired)
            {
                GetFontDelegate del = new GetFontDelegate(GetFont);
                return (Font)control.Invoke(del);
            }
            else return control.Font;
        }

        public void AppendText(string text)
        {
            if (control.IsDisposed) return;
            if (control.InvokeRequired)
            {
                AppendTextDelegate del = new AppendTextDelegate(AppendText);
                control.Invoke(del, new object[] { text });
            }
            else control.AppendText(text);
        }

        public void ScrollToCaret()
        {
            if (control.IsDisposed) return;
            if (control.InvokeRequired)
            {
                ScrollToCaretDelegate del = new ScrollToCaretDelegate(ScrollToCaret);
                control.Invoke(del);
            }
            else control.ScrollToCaret();
        }
    }
}
