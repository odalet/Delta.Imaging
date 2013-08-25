using System;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

using Delta.CapiNet;

using Delta.CertXplorer.UI;
using Delta.CertXplorer.UI.Actions;
using Delta.CertXplorer.UI.Theming;
using Delta.CertXplorer.Services;
using Delta.CertXplorer.Commanding;
using Delta.CertXplorer.Asn1Decoder;
using Delta.CertXplorer.CertManager.Wrappers;
using Delta.CertXplorer.DocumentModel;

namespace Delta.CertXplorer.CertManager
{
    public partial class CertificateListControl : ServicedUserControl, ISelectionSource
    {
        private const int CERTIFICATE_IMAGE = 0;        
        private const int CERTIFICATE_WITH_PRIVATE_KEY_IMAGE = 1;
        private const int CRL_IMAGE = 2;

        private object parentObject = null;
        private UIAction defaultAction = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="CertificateListControl"/> class.
        /// </summary>
        public CertificateListControl()
        {
            InitializeComponent();

            ThemesManager.RegisterThemeAwareControl(this, (renderer) =>
            {
                    if (renderer is ToolStripProfessionalRenderer)
                        ((ToolStripProfessionalRenderer)renderer).RoundedEdges = false;
                    tstrip.Renderer = renderer;
            });

            tstrip.SetRoundedEdges(false);

            InitializeViewMenu();
            UpdateViewMenu();
        }

        #region ISelectionSource Members

        /// <summary>
        /// Occurs when the currently selected object has changed.
        /// </summary>
        public event EventHandler SelectionChanged;

        /// <summary>
        /// Gets the currently selected object.
        /// </summary>
        /// <value>The selected object.</value>
        public object SelectedObject { get; private set; }

        private string CurrentStoreName { get; set; }

        private StoreLocation CurrentStoreLocation { get; set; }

        #endregion

        #region Properties

        [DefaultValue(BorderStyle.Fixed3D)]
        public BorderStyle InnerBorderStyle
        {
            get { return listView.BorderStyle; }
            set { listView.BorderStyle = value; }
        }

        #endregion

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (DesignMode) return;
           
            CreateAndBindToSelectionService();
            listView.SelectedIndexChanged += (s, ee) =>
            {
                if (listView.SelectedItems.Count == 0) NotifySelectionChanged(null);
                else
                {
                    var selectedItem = listView.SelectedItems[0];
                    NotifySelectionChanged(
                        CreateObjectWrapper(selectedItem.Tag));
                }                
            };

            // Actions
            defaultAction = openCertificateAction;
            listView.MouseDoubleClick += (s, ev) => defaultAction.DoRun();
            
            openCertificateAction.Run += (s, ev) =>
            {
                X509Object x509 = null;
                var certificate = GetSelectedCertificate();
                if (certificate != null) x509 = X509Object.Create(
                    certificate, CurrentStoreName, CurrentStoreLocation);
                else
                {
                    var crl = GetSelectedCrl();
                    if (crl != null) x509 = X509Object.Create(
                        crl, CurrentStoreName, CurrentStoreLocation);
                }

                if (x509 != null) Commands.RunVerb(Verbs.OpenCertificate, x509);
            };

            viewInformationAction.Run += (s, ev) =>
            {
                var certificate = GetSelectedCertificate();
                if (certificate != null) CapiNet.UI.ShowCertificateDialog(base.Handle, certificate);
                else
                {
                    var crl = GetSelectedCrl();
                    if (crl != null) CapiNet.UI.ShowCrlDialog(base.Handle, crl);
                }
            };
        }

        #region View Menu Management

        private void InitializeViewMenu()
        {
            tileToolStripMenuItem.Tag = View.Tile;
            largeIconsToolStripMenuItem.Tag = View.LargeIcon;
            smallIconsToolStripMenuItem.Tag = View.SmallIcon;
            listToolStripMenuItem.Tag = View.List;
            detailsToolStripMenuItem.Tag = View.Details;

            foreach (ToolStripMenuItem item in viewDropDownButton.DropDownItems)
                item.Click += (s, e) =>
                {
                    var menuItem = (ToolStripMenuItem)s;
                    listView.View = (View)menuItem.Tag;
                    listView.HeaderStyle = ColumnHeaderStyle.Clickable;
                    UpdateViewMenu();
                };

        }

        private void UpdateViewMenu()
        {
            foreach (ToolStripMenuItem item in viewDropDownButton.DropDownItems)
                item.Checked = listView.View == (View)item.Tag;
        }

        #endregion

        #region Selection Service

        private void CreateAndBindToSelectionService()
        {
            var service = GlobalSelectionService.GetOrCreateSelectionService(Services);
            service.SelectionChanged += (s, e) =>
            {
                if (service.CurrentSource == this) return;
                if (service.SelectedObject == parentObject) return;

                parentObject = service.SelectedObject;
                if (parentObject is CertificateStore)
                {
                    var systemStore = (CertificateStore)parentObject;
                    var store = systemStore.GetX509Store();
                    
                    // See http://msdn.microsoft.com/en-us/library/aa376559%28VS.85%29.aspx : CERT_STORE_PROV_PHYSICAL
                    //var store = new X509Store(systemStore.Name + "\\.Default", systemStore.Location.ToStoreLocation());

                    store.Open(OpenFlags.ReadOnly);

                    CurrentStoreName = store.Name;
                    CurrentStoreLocation = store.Location;

                    FillList(store.Certificates, store.GetCertificateRevocationLists());
                    store.Close();
                }
                else ClearList();
            };

            service.AddSource(this);
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

        #endregion

        private void ClearList() { listView.Items.Clear(); }

        private void FillList(X509Certificate2Collection certificates, IEnumerable<CertificateRevocationList> crls)
        {
            var items = certificates.Cast<X509Certificate2>().Select(
                certificate =>
                {
                    var item = new ListViewItem(FormatDN(certificate.SubjectName));
                    // TODO: don't use X509Certificate class, but the Certificate wrapper 
                    // (and replace "new Certificate(certificate).IsValid" by "certificate.IsValid"
                    if (!new Certificate(certificate).IsValid) item.ForeColor = Color.Red;
                    item.ImageIndex = certificate.HasPrivateKey ? CERTIFICATE_WITH_PRIVATE_KEY_IMAGE : CERTIFICATE_IMAGE;
                    item.SubItems.AddRange(
                        new ListViewItem.ListViewSubItem[]
                    {
                        new ListViewItem.ListViewSubItem(item, FormatDN(certificate.IssuerName)),
                        new ListViewItem.ListViewSubItem(item, FormatDate(certificate.NotBefore)),
                        new ListViewItem.ListViewSubItem(item, FormatDate(certificate.NotAfter)),                        
                        new ListViewItem.ListViewSubItem(item, certificate.FriendlyName)
                    });
                    item.Tag = certificate;

                    return item;
                });

            var crlItems = crls.Select(crl =>
            {
                var item = new ListViewItem("Revocation List");
                if (!crl.IsValid) item.ForeColor = Color.Red;
                item.ImageIndex = CRL_IMAGE;
                item.SubItems.AddRange(
                    new ListViewItem.ListViewSubItem[]
                    {
                        new ListViewItem.ListViewSubItem(item, FormatDN(crl.IssuerName)),       
                        new ListViewItem.ListViewSubItem(item, FormatDate(crl.PublicationDate)),
                        new ListViewItem.ListViewSubItem(item, FormatDate(crl.NextUpdate)),                        
                        new ListViewItem.ListViewSubItem(item, crl.FriendlyName),
                    });
                item.Tag = crl;

                return item;
            });

            listView.Items.Clear();

            foreach (var item in items) listView.Items.Add(item);
            foreach (var item in crlItems) listView.Items.Add(item);
        }

        private X509Certificate2 GetSelectedCertificate()
        {
            if (listView.SelectedItems.Count == 0) return null;
            else
            {
                var tag = listView.SelectedItems[0].Tag;
                if (tag is X509Certificate2) return (X509Certificate2)tag;
                else return null;
            }                
        }

        private CertificateRevocationList GetSelectedCrl()
        {
            if (listView.SelectedItems.Count == 0) return null;
            else
            {
                var tag = listView.SelectedItems[0].Tag;
                if (tag is CertificateRevocationList) return (CertificateRevocationList)tag;
                else return null;
            }                
        }

        private string FormatDN(X500DistinguishedName dn)
        {
            var cn = dn.ExtractRdn("cn");
            if (!string.IsNullOrEmpty(cn)) return cn;

            var ou = dn.ExtractRdn("ou");
            if (!string.IsNullOrEmpty(ou)) return ou;

            return dn.Name;
        }

        private string FormatDate(DateTime date)
        {
            return date == DateTime.MinValue ? string.Empty : date.ToString("yyyy/MM/dd");
        }

        /// <summary>
        /// Wraps the specified item so that it is displayed friendly in a property grid.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>Wrapped item.</returns>
        private object CreateObjectWrapper(object item)
        {
            if (item == null) return null;
            if (item is X509Certificate2) return new X509CertificateWrapper2((X509Certificate2)item);
            if (item is X509Certificate) return new X509CertificateWrapper((X509Certificate)item);
            return item;
        }
    }
}

