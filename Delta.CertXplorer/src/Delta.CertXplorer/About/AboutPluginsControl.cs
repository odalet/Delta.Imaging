using System;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;

using Delta.CertXplorer.Extensibility;

namespace Delta.CertXplorer.About
{
    public partial class AboutPluginsControl : UserControl
    {
        private class PluginTag
        {
            public PluginTag(IPluginInfo pluginInfo, Image icon)
            {
                PluginInfo = pluginInfo;
                Icon = icon;
            }

            public IPluginInfo PluginInfo { get; set; }
            public Image Icon { get; set; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AboutPluginsControl"/> class.
        /// </summary>
        public AboutPluginsControl()
        {
            InitializeComponent();
            InitializeListHeader();
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.UserControl.Load"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            FillList();
        }

        /// <summary>
        /// Initializes the list header.
        /// </summary>
        private void InitializeListHeader()
        {
            pluginsListView.Columns.AddRange(new ColumnHeader[] 
            {
                new ColumnHeader() { Text = string.Empty, Width = 25 },
                new ColumnHeader() { Text = "Name", Width = 130 },
                new ColumnHeader() { Text = "Version", Width = 50 },
                new ColumnHeader() { Text = "Description", Width = 300 },
                new ColumnHeader() { Text = "Author", Width = 80 },
                new ColumnHeader() { Text = "Company", Width = 70 }
            });
        }

        /// <summary>
        /// Fills the list.
        /// </summary>
        private void FillList()
        {
            var items = Globals.PluginsManager.Plugins.Select(p =>
            {
                var key = p.PluginInfo.Id.ToString("n");
                var icon = p.GetIcon();
                pluginIcons.Images.Add(key, icon);

                var item = new ListViewItem();
                item.ImageKey = key;
                if (p.PluginInfo != null)
                {
                    item.SubItems.AddRange(new string[]
                    {
                        p.PluginInfo.Name,
                        p.PluginInfo.Version,
                        p.PluginInfo.Description,
                        p.PluginInfo.Author,
                        p.PluginInfo.Company
                    });
                }

                item.Tag = new PluginTag(p.PluginInfo, icon);
                return item;
            });

            pluginsListView.Items.AddRange(items.ToArray());
        }

        private void pluginsListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            toolTip.SetToolTip(pluginsListView,
                "Current plugin: ");
        }
    }
}
