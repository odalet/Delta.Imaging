using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Delta.CertXplorer.UI.ToolboxModel;

namespace Delta.CertXplorer.UI.ToolboxImplementation
{
    internal partial class Toolbox : UserControl, IToolbox
    {
        private Dictionary<string, ToolboxTab> tabs = new Dictionary<string, ToolboxTab>();

        private ToolboxButton lastCheckedButton = null;
        private ToolboxTab selectedTab = null;
        private ToolboxServiceProxy proxy = null;
        
        public Toolbox() 
        { 
            InitializeComponent();
            proxy = new ToolboxServiceProxy(this);
        }

        #region IToolbox Members

        public IToolboxService ToolboxService { get { return proxy; } }

        public IToolboxTab CreateToolboxTab(string id, string text)
        { 
            return CreateTab(id, text); 
        }

        /// <summary>
        /// Refreshes the layout of all contained tabs
        /// </summary>
        /// <remarks>
        /// This method forces a close then open operation on each tab, 
        /// if it was opened (thus, recomputing its correct height)
        /// </remarks>
        public void RefreshLayout()
        {
            foreach (ToolboxTab tab in tabs.Values) tab.RefreshLayout();
        }

        #endregion

        public ToolboxButton SelectedButton
        {
            get
            {
                ToolboxTab tab = SelectedTab;
                if (tab != null) return tab.SelectedButton;
                else return null;
            }
        }

        public ToolboxTab SelectedTab { get { return selectedTab; } }

        public ToolboxTab[] Tabs
        {
            get
            {
                ToolboxTab[] tabsArray = new ToolboxTab[tabs.Count];
                tabs.Values.CopyTo(tabsArray, 0);
                return tabsArray;
            }
        }

        public void LoadItems(IToolboxLoader loader) { loader.LoadToolbox(this); }

        public ToolboxTab GetTab(string id)
        {
            if (tabs.ContainsKey(id)) return tabs[id];
            else return null;
        }

        public ToolboxTab CreateTab(string id, string text)
        {
            if (tabs.ContainsKey(id)) throw new ArgumentException("Tab " + id + " already exists", "id");

            ToolboxTab tab = new ToolboxTab();
            tab.Name = "tab_" + id;
            tab.Text = text;
            tabs.Add(id, tab);
            AddTabToUi(tab);
            return tab;
        }

        public void AddTab(string id, ToolboxTab tab)
        {
            if (tab == null) throw new ArgumentNullException("tab");
            if (tabs.ContainsKey(id)) throw new ArgumentException("Tab " + id + " already exists", "id");
            tab.Name = "tab_" + id;
            tabs.Add(id, tab);
            AddTabToUi(tab);
        }

        private void AddTabToUi(ToolboxTab tab)
        {
            SuspendLayout();
            pnlMain.SuspendLayout();
            tab.Dock = DockStyle.Top;

            tab.ButtonClick += new ToolboxButtonEventHandler(OnButtonClick);
            tab.ButtonDoubleClick += new ToolboxButtonEventHandler(OnButtonDoubleClick);
            tab.SelectedButtonChanged += new ToolboxButtonEventHandler(OnSelectedButtonChanged);

            tab.CollapsedChanged += new EventHandler(OnCollapsedChanged);
            
            pnlMain.Controls.Add(tab);

            if (tab.Collapsed) tab.OpenTab(); else selectedTab = tab;

            pnlMain.ResumeLayout();
            ResumeLayout();
        }

        private void SelectPointerTool()
        {
            if (selectedTab != null)
            {
                foreach (ToolboxTool tool in selectedTab.Tools)
                {
                    if (tool is ToolboxPointer) SelectTool(tool);
                }
            }
        }

        private void SelectTool(ToolboxTool tool) { if (selectedTab != null) selectedTab.SelectTool(tool); }

        private void btnHeader_Click(object sender, EventArgs e)
        {
            pnlMain.Focus();
        }

        private void OnSelectedButtonChanged(object sender, ToolboxButtonEventArgs e)
        {

        }

        private void OnButtonClick(object sender, ToolboxButtonEventArgs e)
        {
            if (lastCheckedButton != null) lastCheckedButton.Checked = false;
            ToolboxButton button = e.Button;
            if (button != null)
            {
            button.Checked = true;
            lastCheckedButton = button;
            }
            pnlMain.Focus();
        }

        private void OnButtonDoubleClick(object sender, ToolboxButtonEventArgs e)
        {
            ToolboxButton button = e.Button;
            if (button != null) button.Checked = true;
            if (lastCheckedButton != button) lastCheckedButton.Checked = false;
            pnlMain.Focus();
        }

        private void OnCollapsedChanged(object sender, EventArgs e)
        {
            ToolboxTab tab = sender as ToolboxTab;
            if ((tab != null) && !tab.Collapsed) selectedTab = tab;
            pnlMain.Focus();
        }
    }
}
