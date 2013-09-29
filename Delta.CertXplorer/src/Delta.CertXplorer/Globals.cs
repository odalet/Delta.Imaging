using System;
using System.Windows.Forms;

using Delta.CertXplorer.PluginsManagement;

namespace Delta.CertXplorer
{
    internal static class Globals
    {
        public static PluginsManager PluginsManager { get; set; }

        public static Form MainForm { get; set; }

        public static IntPtr MainFormHandle
        {
            get { return MainForm == null ? IntPtr.Zero : MainForm.Handle;  }
        }

        public static string ApplicationSettingsFileName { get; set; }

        public static string LayoutSettingsFileName { get; set; }

        public static string LoggingSettingsFileName { get; set; }
    }
}
