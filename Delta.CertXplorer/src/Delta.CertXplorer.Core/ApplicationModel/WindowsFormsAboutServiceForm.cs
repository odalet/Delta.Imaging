using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using System.Collections.Generic;

using Delta.CertXplorer.IO;
using Delta.CertXplorer.UI;

namespace Delta.CertXplorer.ApplicationModel
{
    /// <summary>
    /// Base About Form.
    /// </summary>
    public partial class WindowsFormsAboutServiceForm : Form
    {
        private ListViewGroup CertXplorerGroup = new ListViewGroup("Delta.CertXplorer", HorizontalAlignment.Left);
        private ListViewGroup othersGroup = new ListViewGroup(SR.Others, HorizontalAlignment.Left);
        private WindowsFormsAboutService aboutService = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowsFormsAboutServiceForm"/> class.
        /// </summary>
        public WindowsFormsAboutServiceForm() : this(
            This.GetService<IAboutService>() as WindowsFormsAboutService) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowsFormsAboutServiceForm"/> class.
        /// </summary>
        /// <param name="service">The service.</param>
        public WindowsFormsAboutServiceForm(WindowsFormsAboutService service)
        {
            InitializeComponent();

            if (service == null) service = new WindowsFormsAboutService();
            aboutService = service;

            Text = string.Format(SR.AboutNameVersion, AssemblyTitle, AssemblyVersion);
            labelApplication.Text = AssemblyTitle;
            labelProductName.Text = AssemblyProduct;
            labelVersion.Text = string.Format(SR.VersionWithValue, AssemblyVersion);
            labelCopyright.Text = AssemblyCopyright;
            labelCompanyName.Text = AssemblyCompany;
            textBoxDescription.Text = AssemblyDescription;
        }

        #region Assembly Attribute Accessors

        public string AssemblyTitle { get { return aboutService.AssemblyTitle; } }

        public string AssemblyVersion { get { return aboutService.AssemblyVersion; } }

        public string AssemblyDescription { get { return aboutService.AssemblyDescription; } }

        public string AssemblyProduct { get { return aboutService.AssemblyProduct; } }

        public string AssemblyCopyright { get { return aboutService.AssemblyCopyright; } }

        public string AssemblyCompany { get { return aboutService.AssemblyCompany; } }

        #endregion
        
        /// <summary>
        /// Gets or sets the splash image.
        /// </summary>
        /// <value>The splash image.</value>
        public Image Splash
        {
            get { return logoPictureBox.Image; }
            set { logoPictureBox.Image = value; }
        }

        /// <summary>Gets the picture box containing the splash image.</summary>
        /// <value>The picture box containing the splash image.</value>
        protected PictureBox SplashBox { get { return logoPictureBox; } }

        /// <summary>Gets the credits text.</summary>
        /// <value>The credits text.</value>
        public string Credits { get { return aboutService.Credits; } }

        /// <summary>Gets the history text.</summary>
        /// <value>The history text.</value>
        public string History { get { return aboutService.History; } }

        /// <summary>
        /// Creates the listview groups.
        /// </summary>
        /// <returns>An array of <see cref="ListViewGroup"/> items.</returns>
        protected virtual ListViewGroup[] CreateGroups()
        {
            return new ListViewGroup[] { CertXplorerGroup };
        }

        /// <summary>
        /// Gets the listview group for the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>A <see cref="ListViewGroup"/> item.</returns>
        protected virtual ListViewGroup GetGroup(ListViewItem item)
        {
            if (item.Text.ToLowerInvariant().StartsWith("Delta.CertXplorer")) return CertXplorerGroup;
            else return null;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (string.IsNullOrEmpty(Credits))
                Tabs.TabPages.Remove(tpCredits);
            else tbCredits.Text = Credits;

            if (string.IsNullOrEmpty(History))
                Tabs.TabPages.Remove(tpHistory);
            else tbHistory.Text = History;

            try 
            {
                FillAssemblyList(); 
            }
            catch (Exception ex) { This.Logger.Error(ex); }
        }

        private void AppendModuleDescriptions(StringBuilder builder)
        {
            AppDomain.CurrentDomain.AppendModuleDescriptions(builder);
        }

        private void FillAssemblyList()
        {            
            var groups = CreateGroups();
            // We don't sort the group names: they must be provided in the expected order.
            lvModules.Groups.AddRange(groups);
            lvModules.Groups.Add(othersGroup); // Others is always the last group.

            var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                .Where(ass => !ass.IsDynamic());

            List<ListViewItem> listViewItems = new List<ListViewItem>();
            foreach (var assembly in assemblies)
            {
                var listViewItem = CreateItem(assembly);
                if (listViewItem != null) listViewItems.Add(listViewItem);
            }

            foreach (var item in listViewItems.OrderBy(item => 
                Path.GetFileNameWithoutExtension(item.Text)))
            {
                item.Group = GetGroup(item);
                if (item.Group == null) item.Group = othersGroup;
                if (item != null) lvModules.Items.Add(item);
            }
        }

        private ListViewItem CreateItem(Assembly assembly)
        {
            try
            {
                var title = assembly.GetTitle(true);
                var version = assembly.GetName().Version.ToString();
                var path = assembly.GetLocation();

                var item = new ListViewItem();
                item.Text = title;
                item.Tag = assembly;                
                item.SubItems.Add(version);
                item.SubItems.Add(path);
                // ODT: this seems not to work properly: no tooltips are displayed...
                item.ToolTipText = string.Format("{0} {1}\r\n{2}",
                    title, version, assembly.GetDescription()); 
                return item;
            }
            catch (Exception ex)
            {
                This.Logger.Error(ex);
                return null;
            }
        }

        private string CreateToolTip(Assembly assembly)
        {
            return string.Format("{0} {1}\r\n{2}",
                assembly.GetTitle(true), assembly.GetName().Version.ToString(), assembly.GetDescription());
        }
        
        

        #region Events: Save, Copy

        private void pcsLinks_SaveClick(object sender, EventArgs e)
        {
            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = FileType.CombineFilters(FileType.TXT, FileType.ALL);
                sfd.FilterIndex = 0;
                sfd.DefaultExt = FileType.TXT.Patterns[0];
                sfd.OverwritePrompt = true;
                sfd.SupportMultiDottedExtensions = true;
                sfd.CheckPathExists = true;
                sfd.ValidateNames = true;
                
                if (sfd.ShowDialog(this) == DialogResult.OK)
                {
                    var fileName = string.Empty;

                    try
                    {
                        fileName = sfd.FileName;
                        using (var writer = File.CreateText(fileName))
                        {
                            writer.Write(aboutService.GetAboutText());
                            writer.Flush();
                        }
                    }
                    catch (Exception ex)
                    {
                        This.Logger.Error(string.Format("Error while saving data into file file://{0}: {1}",
                            fileName, ex.Message), ex);
                        ErrorBox.Show(this, string.Format(SR.ErrorWhileSavingFile, ex.Message));
                    }
                }
            }
        }

        private void pcsLinks_CopyClick(object sender, EventArgs e)
        {
            try { Clipboard.SetText(aboutService.GetAboutText()); }
            catch (Exception ex)
            {
                This.Logger.Error(string.Format("Error while copying data to the clipboard: {0}",
                    ex.Message), ex);
            }
        }

        #endregion
    }
}
