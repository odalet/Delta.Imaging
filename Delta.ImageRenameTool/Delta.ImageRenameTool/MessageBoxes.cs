using System.Windows.Forms;

namespace Delta.ImageRenameTool
{
    internal static class ErrorBox
    {
        public static DialogResult Show(IWin32Window owner, string text)
        {
            return MessageBox.Show(owner, text, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
        }
    }
}
