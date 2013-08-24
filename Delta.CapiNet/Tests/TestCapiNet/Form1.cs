using System;
using System.Linq;
using System.Windows.Forms;
using System.Security.Cryptography.X509Certificates;

using Delta.CapiNet;

namespace TestCapiNet
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {

            var systemStore = Capi32.GetSystemStores(CertificateStoreLocation.FromStoreLocation(StoreLocation.LocalMachine)).First();
            var store = systemStore.GetX509Store();

            // See http://msdn.microsoft.com/en-us/library/aa376559%28VS.85%29.aspx : CERT_STORE_PROV_PHYSICAL
            //var store = new X509Store(systemStore.Name + "\\.Default", systemStore.Location.ToStoreLocation());

            store.Open(OpenFlags.ReadOnly);

            var certificates = store.Certificates;
            var crls = store.GetCertificateRevocationLists();
            store.Close();

            MessageBox.Show(string.Format("Certificates Count = {0}\r\nCrls Count = {1}", certificates.Count, crls.Count()));
        }
    }
}
