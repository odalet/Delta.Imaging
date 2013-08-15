using System;
using System.Linq;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

using Delta.CapiNet;

using Delta.CertXplorer.UI;
using Delta.CertXplorer.UI.Theming;
using Delta.CertXplorer.Services;

namespace Delta.CertXplorer.CertManager
{
    public partial class CertificateStoreListControl : ServicedUserControl, ISelectionSource
    {
        private const int STORES_IMAGE = 0;
        private const int CLOSED_LOCATION_IMAGE = 1;
        private const int OPENED_LOCATION_IMAGE = 2;
        private const int CLOSED_STORE_IMAGE = 1;
        private const int OPENED_STORE_IMAGE = 2;
        private const int LOCATION_IMAGE = 3;

        private TreeNodeEx rootNode = null;
        private Dictionary<string, TreeNodeEx> nodesDictionary = new Dictionary<string, TreeNodeEx>();

        /// <summary>
        /// Initializes a new instance of the <see cref="CertificateStoreListControl"/> class.
        /// </summary>
        public CertificateStoreListControl()
        {
            InitializeComponent();

            ShowPhysicalStores = false; // default value
            ShowOtherLocations = false; // default value

            ThemesManager.RegisterThemeAwareControl(this, (renderer) =>
            {
                if (renderer is ToolStripProfessionalRenderer)
                    ((ToolStripProfessionalRenderer)renderer).RoundedEdges = false;
                tstrip.Renderer = renderer;
            });

            tstrip.SetRoundedEdges(false);
        }

        #region Properties

        [DefaultValue(BorderStyle.Fixed3D)]
        public BorderStyle InnerBorderStyle
        {
            get { return tvStores.BorderStyle; }
            set { tvStores.BorderStyle = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to show physical stores.
        /// </summary>
        /// <remarks>
        /// This fails with an<c>OutOfMemoryException</c> under Vista...
        /// </remarks>
        /// <value><c>true</c> to show physical stores; otherwise, <c>false</c>.</value>
        [DefaultValue(false)]
        public bool ShowPhysicalStores { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to show locations others than <b>LocalMachine</b>
        /// and <b>CurrentUser</b>.
        /// </summary>
        /// <value><c>true</c> to show other locations; otherwise, <c>false</c>.</value>
        [DefaultValue(false)]
        public bool ShowOtherLocations { get; set; }

        #endregion

        #region ISelectionSource Members

        public event EventHandler SelectionChanged;

        public object SelectedObject { get; private set; }

        #endregion

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.UserControl.Load"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (DesignMode) return;

            CreateSelectionService();

            FillNodes();

            tvStores.SelectedNodeChanged += (s, ev) =>
            {
                //UpdateStrips(ev.Node);
                NotifySelectionChanged(ev.Node.Tag);
            };
        }

        /// <summary>
        /// Fills the nodes of the treeview.
        /// </summary>
        private void FillNodes()
        {
            // The nodes
            rootNode = new TreeNodeEx(SR.CertificateStores);
            rootNode.ImageIndex = STORES_IMAGE;
            rootNode.SelectedImageIndex = STORES_IMAGE;
            tvStores.Nodes.Add(rootNode);

            // 1st level: locations
            IEnumerable<CertificateStoreLocation> locations = null;
            if (ShowOtherLocations)
                locations = Capi32.GetSystemStoreLocations();
            else locations = new StoreLocation[] 
            { 
                StoreLocation.LocalMachine, 
                StoreLocation.CurrentUser 
            }.Select(l => CertificateStoreLocation.FromStoreLocation(l));

            rootNode.Nodes.AddRange(locations
                .OrderBy(location => location.Id)
                .Select(location =>
                {
                    var locationNode = new FolderTreeNode(location.ToString());
                    locationNode.CollapsedImageIndex = CLOSED_LOCATION_IMAGE;
                    locationNode.SelectedCollapsedImageIndex = CLOSED_LOCATION_IMAGE;
                    locationNode.ExpandedImageIndex = OPENED_LOCATION_IMAGE;
                    locationNode.SelectedExpandedImageIndex = OPENED_LOCATION_IMAGE;
                    locationNode.Tag = location;

                    // 2nd level: stores
                    locationNode.Nodes.AddRange(Capi32.GetSystemStores(location)
                        .Select(store =>
                        {
                            var storeNode = new FolderTreeNode(store.ToLongString());
                            storeNode.CollapsedImageIndex = CLOSED_STORE_IMAGE;
                            storeNode.SelectedCollapsedImageIndex = CLOSED_STORE_IMAGE;
                            storeNode.ExpandedImageIndex = OPENED_STORE_IMAGE;
                            storeNode.SelectedExpandedImageIndex = OPENED_STORE_IMAGE;
                            storeNode.Tag = store;

                            // 3rd level: physical stores
                            if (ShowPhysicalStores) storeNode.Nodes.AddRange(
                                Capi32.GetPhysicalStores(store.Name)
                                .Select(pstore =>
                                {
                                    var label = string.Format("{0} [{1}]", pstore, Capi32.LocalizeName(pstore));
                                    var pstoreNode = new TreeNodeEx(label);
                                    pstoreNode.ImageIndex = CLOSED_STORE_IMAGE;
                                    pstoreNode.SelectedImageIndex = OPENED_STORE_IMAGE;
                                    pstoreNode.Tag = store;

                                    return pstoreNode;
                                }).ToArray());

                            return storeNode;
                        }).ToArray());

                    return locationNode;
                }).ToArray());

            rootNode.Expand();
        }

        private void CreateSelectionService()
        {
            GlobalSelectionService
                .GetOrCreateSelectionService(Services)
                .AddSource(this);
        }

        /// <summary>
        /// Notifies that the current selection has changed.
        /// </summary>
        /// <param name="selection">The currently selected object.</param>
        private void NotifySelectionChanged(object selection)
        {
            if (SelectedObject != selection)
            {
                SelectedObject = selection;
                if (SelectionChanged != null) SelectionChanged(this, EventArgs.Empty);
            }
        }

        private void refreshToolStripButton_Click(object sender, EventArgs e)
        {
            //CapiNet.UI.ShowCertificatesDialog(this);
            //CapiNet.UI.ShowTrustedPublishersDialog(this);
            CapiNet.UI.ShowBuildCtlWizard(base.Handle);
        }
    }
}
