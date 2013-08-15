using System;
using System.Drawing;
using System.Windows.Forms;

namespace Delta.CertXplorer.UI
{
    public class RichTextBoxFormatter : IDisposable
    {
        private class RichTextBoxSettings
        {
            public Color ForeColor = Color.Empty;
            public Color BackColor = Color.Empty;
            public Font Font = null;

            public void ApplyToRichTextBox(ThreadSafeRichTextBoxWrapper rtb)
            {
                if (Font != null) rtb.SelectionFont = Font;
                if (BackColor != Color.Empty) rtb.SelectionBackColor = BackColor;
                if (ForeColor != Color.Empty) rtb.SelectionColor = ForeColor;                
            }
        }

        private ThreadSafeRichTextBoxWrapper rtbWrapper = null;
        private RichTextBoxSettings oldSettings = null;
        private RichTextBoxSettings settings = null;

        public RichTextBoxFormatter(ThreadSafeRichTextBoxWrapper textboxWrapper, Color foreColor) : this(textboxWrapper, foreColor, Color.Empty, null) { }
        public RichTextBoxFormatter(ThreadSafeRichTextBoxWrapper textboxWrapper, Color foreColor, Color backColor) : this(textboxWrapper, foreColor, backColor, null) { }
        public RichTextBoxFormatter(ThreadSafeRichTextBoxWrapper textboxWrapper, Color foreColor, Font font) : this(textboxWrapper, foreColor, Color.Empty, font) { }
        public RichTextBoxFormatter(ThreadSafeRichTextBoxWrapper textboxWrapper, Color foreColor, Color backColor, Font font)
        {
            if (textboxWrapper == null) throw new ArgumentNullException("textboxWrapper");
            rtbWrapper = textboxWrapper;

            SaveSettings(foreColor, backColor, font);
            ApplySettings();
        }

        #region IDisposable Members

        public void Dispose() { RestoreSettings(); }

        #endregion

        private void SaveSettings(Color foreColor, Color backColor, Font font)
        {
            settings = new RichTextBoxSettings();
            settings.ForeColor = foreColor;
            settings.BackColor = backColor;
            settings.Font = font;

            oldSettings = new RichTextBoxSettings();
            oldSettings.ForeColor = rtbWrapper.SelectionColor;
            oldSettings.BackColor = rtbWrapper.SelectionBackColor;
            oldSettings.Font = rtbWrapper.SelectionFont;
        }

        private void ApplySettings() { settings.ApplyToRichTextBox(rtbWrapper); }

        private void RestoreSettings() { oldSettings.ApplyToRichTextBox(rtbWrapper); }
    }
}
