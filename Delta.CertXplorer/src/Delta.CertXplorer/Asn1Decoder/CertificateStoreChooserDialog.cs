using System;
using System.Windows.Forms;
using System.Security.Cryptography.X509Certificates;

using Delta.CapiNet;

namespace Delta.CertXplorer.Asn1Decoder
{
    public partial class CertificateStoreChooserDialog : Form
    {
        public class CertificateStoreChooserValue
        {
            public string StoreName = string.Empty;
            public StoreLocation StoreLocation = StoreLocation.CurrentUser;

            public CertificateStoreChooserValue(StoreName name, StoreLocation location) :
                this(name.ToString(), location) { }

            public CertificateStoreChooserValue(string name, StoreLocation location)
            {
                StoreName = name;
                StoreLocation = location;
            }

            public string NameWithCount
            {
                get
                {
                    return string.Format("{0} [{1}]", StoreName, CertificateCount);
                }
            }

            public int CertificateCount
            {
                get 
                {
                    return Do(store => store.Certificates.Count, StoreName, StoreLocation);
                    //return GetCertificateCount(StoreName, StoreLocation); 
                }
            }

            public X509Certificate2Collection GetCertificates()
            {
                return Do(store => store.Certificates, StoreName, StoreLocation);
            }

            //private int GetCertificateCount(string storeName, StoreLocation storeLocation)
            //{
            //    return Do(store => store.Certificates.Count, storeName, storeLocation);
            //    //var store = Capi32.GetCertificateStore(storeName, storeLocation);
            //    //var x509Store = store.GetX509Store();

            //    //x509Store.Open(OpenFlags.ReadOnly);
            //    //var count = x509Store.Certificates.Count;
            //    //x509Store.Close();

            //    //return count;
            //}

            private T Do<T>(Func<X509Store, T> function, string storeName, StoreLocation storeLocation)
            {
                var store = Capi32.GetCertificateStore(storeName, storeLocation);
                var x509Store = store.GetX509Store();

                x509Store.Open(OpenFlags.ReadOnly);
                var result = default(T);
                try
                {
                    result = function(x509Store);
                }
                finally { x509Store.Close(); }

                return result;
            }
        }

        private CertificateStoreChooserValue value = null;
        private TreeNode tnCurrentUser = null;
        private TreeNode tnLocalMachine = null;

        public CertificateStoreChooserDialog() { InitializeComponent(); }
        
        private void CertificateStoreChooser_Load(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                tnCurrentUser = new TreeNode("Current User");
                tnLocalMachine = new TreeNode("Local Machine");
                tvStores.Nodes.Add(tnCurrentUser);
                tvStores.Nodes.Add(tnLocalMachine);

                foreach (StoreName name in Enum.GetValues(typeof(StoreName)))
                {
                    CreateNode(name, StoreLocation.CurrentUser);
                    CreateNode(name, StoreLocation.LocalMachine);
                }

                tnCurrentUser.Expand();
                tnLocalMachine.Expand();
            }
            finally { Cursor = Cursors.Default; }
        }

        private void CreateNode(StoreName name, StoreLocation location)
        {
            TreeNode tn = null;
            CertificateStoreChooserValue cscv = 
                new CertificateStoreChooserValue(name, location);

            if (cscv.CertificateCount > 0)
            {
                tn = new TreeNode(cscv.NameWithCount);
                tn.Tag = cscv;
            }

            if (tn == null) return;

            if (location == StoreLocation.CurrentUser)
                tnCurrentUser.Nodes.Add(tn);
            else tnLocalMachine.Nodes.Add(tn);

        }

        public CertificateStoreChooserValue SelectedValue { get { return value; } }

        private void tvStores_AfterSelect(object sender, TreeViewEventArgs e)
        {
            value = e.Node.Tag as CertificateStoreChooserValue;
        }

        private void tvStores_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (value != null)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }        
    }
}