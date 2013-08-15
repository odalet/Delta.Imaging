using System;
using System.Windows.Forms;
using System.Drawing.Design;
using System.Collections.Generic;
using Delta.CertXplorer.UI.ToolboxModel;

namespace Delta.CertXplorer.UI.ToolWindows
{
    /// <summary>
    /// Displays a toolbox similar to the Visual Studio one.
    /// </summary>
    public partial class ToolboxWindowControl : UserControl
    {
        private class MockToolboxLoader : IToolboxLoader
        {
            private Type[] windowsFormsTypes = new Type[] 
            {
                typeof(System.Windows.Forms.PropertyGrid), 
                typeof(System.Windows.Forms.Label), 
                typeof(System.Windows.Forms.LinkLabel), 
                typeof(System.Windows.Forms.Button), 
                typeof(System.Windows.Forms.TextBox), 
                typeof(System.Windows.Forms.CheckBox), 
                typeof(System.Windows.Forms.RadioButton), 
                typeof(System.Windows.Forms.GroupBox), 
                typeof(System.Windows.Forms.PictureBox), 
                typeof(System.Windows.Forms.Panel), 
                typeof(System.Windows.Forms.DataGrid), 
                typeof(System.Windows.Forms.ListBox), 
                typeof(System.Windows.Forms.CheckedListBox), 
                typeof(System.Windows.Forms.ComboBox), 
                typeof(System.Windows.Forms.ListView), 
                typeof(System.Windows.Forms.TreeView), 
                typeof(System.Windows.Forms.TabControl), 
                typeof(System.Windows.Forms.DateTimePicker), 
                typeof(System.Windows.Forms.MonthCalendar), 
                typeof(System.Windows.Forms.HScrollBar), 
                typeof(System.Windows.Forms.VScrollBar), 
                typeof(System.Windows.Forms.Timer), 
                typeof(System.Windows.Forms.Splitter), 
                typeof(System.Windows.Forms.DomainUpDown), 
                typeof(System.Windows.Forms.NumericUpDown), 
                typeof(System.Windows.Forms.TrackBar), 
                typeof(System.Windows.Forms.ProgressBar), 
                typeof(System.Windows.Forms.RichTextBox),
                typeof(System.Windows.Forms.ImageList), 
                typeof(System.Windows.Forms.HelpProvider), 
                typeof(System.Windows.Forms.ToolTip), 
                typeof(System.Windows.Forms.ToolBar),
                typeof(System.Windows.Forms.StatusBar), 
                typeof(System.Windows.Forms.UserControl), 
                typeof(System.Windows.Forms.NotifyIcon), 
                typeof(System.Windows.Forms.OpenFileDialog), 
                typeof(System.Windows.Forms.SaveFileDialog), 
                typeof(System.Windows.Forms.FontDialog), 
                typeof(System.Windows.Forms.ColorDialog), 
                typeof(System.Windows.Forms.PrintDialog), 
                typeof(System.Windows.Forms.PrintPreviewDialog), 
                typeof(System.Windows.Forms.PrintPreviewControl), 
                typeof(System.Windows.Forms.ErrorProvider), 
                typeof(System.Drawing.Printing.PrintDocument), 
                typeof(System.Windows.Forms.PageSetupDialog)
            };

            private Type[] componentsTypes = new Type[] 
            {
                typeof(System.IO.FileSystemWatcher), 
                typeof(System.Diagnostics.Process), 
                typeof(System.Timers.Timer)
            };
            
            private Type[] dataTypes = new Type[] 
            {
                //typeof(System.Data.OleDb.OleDbCommandBuilder), 
                //typeof(System.Data.OleDb.OleDbConnection), 
                //typeof(System.Data.SqlClient.SqlCommandBuilder),
                //typeof(System.Data.SqlClient.SqlConnection),
            };

            private Type[] userControlsTypes = new Type[] 
            {
                typeof(System.Windows.Forms.UserControl)
            };

            #region IToolboxLoader Members

            public void LoadToolbox(IToolbox toolbox)
            {
                IToolboxTab tabWindowsForms = toolbox.CreateToolboxTab("WindowsForms", "Windows Forms");
                IToolboxTab tabComponents = toolbox.CreateToolboxTab("Components", "Components");
                IToolboxTab tabData = toolbox.CreateToolboxTab("Data", "Data");
                IToolboxTab tabUserControls = toolbox.CreateToolboxTab("UserControls", "User Controls");

                FillToolboxTab(tabWindowsForms, windowsFormsTypes);
                FillToolboxTab(tabComponents, componentsTypes);
                FillToolboxTab(tabData, dataTypes);
                FillToolboxTab(tabUserControls, userControlsTypes);

                toolbox.RefreshLayout();
            }

            #endregion

            private void FillToolboxTab(IToolboxTab tab, Type[] types)
            {
                tab.AddPointer();
                foreach (Type type in types) tab.AddTool(type);
            }
        }

        public ToolboxWindowControl()
        {
            InitializeComponent();
            InitToolbox();
        }

        private void InitToolbox()
        {
            var loader = new MockToolboxLoader();
            loader.LoadToolbox(tbox);
        }
    }
}
