using System;
using System.IO;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace Pluralsight.Crypto.UI
{
    public partial class CertDetailsForm : Form
    {
        public StoreLocation CertStoreLocation { get; set; }
        public StoreName CertStoreName { get; set; }
        public X509Certificate2 Certificate { get; set; }

        public CertDetailsForm()
        {
            InitializeComponent();
        }

        private void CertDetailsForm_Load(object sender, EventArgs e)
        {
            txtStore.Text = string.Format("{0}/{1}", CertStoreLocation, CertStoreName);
            txtThumbprint.Text = Certificate.Thumbprint;

            RSACryptoServiceProvider privateKey = Certificate.PrivateKey as RSACryptoServiceProvider;
            if (null != privateKey)
            {
                string keyFile = privateKey.CspKeyContainerInfo.UniqueKeyContainerName;
                txtPrivateKeyFile.Text = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), @"Microsoft\Crypto\RSA\MachineKeys"), keyFile);
            }
            else btnViewPrivateKeyFile.Enabled = false;
        }

        private void btnViewStore_Click(object sender, EventArgs e)
        {
            X509Store store = new X509Store(CertStoreName, CertStoreLocation);
            store.Open(OpenFlags.ReadOnly);

            X509Certificate2UI.SelectFromCollection(store.Certificates, txtStore.Text, "", X509SelectionFlag.SingleSelection);
        }

        private void btnViewCert_Click(object sender, EventArgs e)
        {
            X509Certificate2UI.DisplayCertificate(Certificate);
        }

        private void btnViewPrivateKeyFile_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", "/select," + txtPrivateKeyFile.Text);
        }
    }
}
