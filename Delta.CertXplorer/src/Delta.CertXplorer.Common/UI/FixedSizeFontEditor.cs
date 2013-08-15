using System;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.ComponentModel;

namespace Delta.CertXplorer.UI
{
    /// <summary>
    /// Display only fixed-size fonts
    /// </summary>
    internal class FixedSizeFontEditor : FontEditor
    {
        // this ensures the value is not collected.
        private object editedValue = null;

        /// <summary>
        /// Edits the value
        /// </summary>
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            editedValue = value;
            using (var fontDialog = new FontDialog())
            {
                fontDialog.ShowApply = false;
                fontDialog.ShowColor = false;
                fontDialog.AllowVerticalFonts = false;
                fontDialog.AllowScriptChange = false;
                fontDialog.FixedPitchOnly = true;
                fontDialog.ShowEffects = false;
                fontDialog.ShowHelp = false;

                Font font = value as Font;
                if (font != null) fontDialog.Font = font;

                if (fontDialog.ShowDialog() == DialogResult.OK)
                    editedValue = fontDialog.Font;
            }

            value = editedValue;
            editedValue = null;

            return value;
        }

        /// <summary>
        /// Gets the edit style.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
    }
}
