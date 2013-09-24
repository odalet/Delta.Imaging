using System;
using System.IO;
using System.Xml.Linq;
using System.Windows.Forms;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace Pluralsight.Crypto.UI
{
    public partial class GenerateSelfSignedCertForm : Form
    {
        public GenerateSelfSignedCertForm()
        {
            InitializeComponent();
        }

        public Func<string> GetUserConfigDirectory { get; set; }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadSettings();
            LoadStoreDropdownLists();
            UpdateVersion();
        }

        private void UpdateVersion()
        {
            lnkVersion.Text = "v" + this.GetType().Assembly.GetName().Version.ToString();
        }

        private void LoadStoreDropdownLists()
        {
            cboStoreLocation.Items.Clear();
            foreach (StoreLocation storeLocation in Enum.GetValues(typeof(StoreLocation)))
            {
                int index = cboStoreLocation.Items.Add(storeLocation);
                if (StoreLocation.LocalMachine == storeLocation)
                    cboStoreLocation.SelectedIndex = index;
            }
            
            cboStoreName.Items.Clear();
            foreach (StoreName storeName in Enum.GetValues(typeof(StoreName)))
            {
                int index = cboStoreName.Items.Add(storeName);
                if (StoreName.My == storeName)
                    cboStoreName.SelectedIndex = index;
            }
        }

        private void lnkTitle_LinkClicked(object sender, MouseEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.pluralsight.com/");
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveSettings();
        }

        private void LoadSettings()
        {
            LoadDefaultSettings();
            
            XDocument doc = null;
            try
            {
                doc = XDocument.Load(SettingsFile);
            }
            catch (IOException) { }

            if (null != doc)
                LoadSettings(doc);
        }

        private void LoadDefaultSettings()
        {
            DateTime today = DateTime.Today;

            txtDN.Text = "cn=localhost";
            cboKeySize.Text = "4096";
            dtpValidFrom.Value = today.AddDays(-7); // just to be safe
            dtpValidTo.Value = today.AddYears(10);
            chkExportablePrivateKey.Checked = true;
        }

        private void LoadSettings(XDocument doc)
        {
            txtDN.Text = GetSetting(doc, "dn", "");
            cboKeySize.Text = GetSetting(doc, "keySize", "4096");

            bool isPrivateKeyExportable;
            if (bool.TryParse(GetSetting(doc, "exportPrivateKey", "true"), out isPrivateKeyExportable))
                chkExportablePrivateKey.Checked = isPrivateKeyExportable;
        }

        private string GetSetting(XDocument doc, string elementName, string defaultValue)
        {
            XElement dnElement = doc.Root.Element(elementName);
            return (null != dnElement) ? dnElement.Value : defaultValue;
        }

        private void SaveSettings()
        {
            XDocument doc = new XDocument(
                new XElement("settings",
                    new XElement("dn", txtDN.Text),
                    new XElement("keySize", cboKeySize.Text),
                    new XElement("exportPrivateKey", chkExportablePrivateKey.Checked)
                    ));
            try
            {
                doc.Save(SettingsFile);
            }
            catch (IOException)
            {
                throw;
            }
        }

        private string SettingsFile
        {
            get
            {
                if (GetUserConfigDirectory != null)
                    return Path.Combine(GetUserConfigDirectory(), "pluralsight.settings.xml");
                else return Path.Combine(Application.LocalUserAppDataPath, "pluralsight.settings.xml");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSaveAsPFX_Click(object sender, EventArgs e)
        {
            if (!ValidateCertProperties())
                return;

            X509Certificate2 cert = GenerateCert();
            if (null == cert)
                return; // user must have cancelled the operation

            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Filter = "PFX file (*.pfx)|*.pfx";

            if (DialogResult.OK == fileDialog.ShowDialog(this))
            {
                using (Stream outputStream = File.Create(fileDialog.FileName))
                {
                    byte[] pfx = cert.Export(X509ContentType.Pfx, txtPassword.Text.Length > 0 ? txtPassword.Text : null);
                    outputStream.Write(pfx, 0, pfx.Length);
                    outputStream.Close();
                }

                MessageBox.Show(this,
                    "Successfully saved a new self-signed certificate to "
                    + Path.GetFileName(fileDialog.FileName), "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private bool ValidateCertProperties()
        {
            if (!ValidateDN())
            {
                txtDN.SelectAll();
                txtDN.Focus();
                return false;
            }
            if (!ValidateKeySize())
            {
                cboKeySize.Focus();
                return false;
            }
            return true;
        }

        private void btnSaveToCertStore_Click(object sender, EventArgs e)
        {
            X509Store store = new X509Store((StoreName)cboStoreName.SelectedItem, (StoreLocation)cboStoreLocation.SelectedItem);
            store.Open(OpenFlags.ReadWrite);

            X509Certificate2 cert = GenerateCert();
            if (null != cert)
            {
                // I've not been able to figure out what property isn't getting copied into the store,
                // but IIS can't find the private key when I simply add the cert directly to the store
                // in this fashion:  store.Add(cert);
                // The extra two lines of code here does seem to make IIS happy though.
                // I got this idea from here: http://www.derkeiler.com/pdf/Newsgroups/microsoft.public.inetserver.iis.security/2008-03/msg00020.pdf
                //  (written by David Wang at blogs.msdn.com/David.Wang)
                byte[] pfx = cert.Export(X509ContentType.Pfx);
                cert = new X509Certificate2(pfx, (string)null, X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.MachineKeySet);

                // NOTE: it's not clear to me at this point if this will work if you want to save to StoreLocation.CurrentUser
                //       given that there's also an X509KeyStorageFlags.UserKeySet. That could be DPAPI related though, and not cert store related.

                store.Add(cert);
            }
            store.Close();

            if (null != cert)
            {
                new CertDetailsForm
                {
                    Certificate = cert,
                    CertStoreLocation = (StoreLocation)cboStoreLocation.SelectedItem,
                    CertStoreName = (StoreName)cboStoreName.SelectedItem,
                }.ShowDialog();
            }
        }

        private X509Certificate2 GenerateCert()
        {
            // use a form to initiate a cancellable background worker
            BackgroundCertGenForm form = new BackgroundCertGenForm();
            form.CertProperties = new SelfSignedCertProperties
            {
                Name = new X500DistinguishedName(txtDN.Text),
                ValidFrom = dtpValidFrom.Value,
                ValidTo = dtpValidTo.Value,
                KeyBitLength = int.Parse(cboKeySize.Text),
                IsPrivateKeyExportable = true,
            };
            form.ShowDialog();

            return form.Certificate;
        }

        private void lnkKeithRecommends_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.pluralsight.com/keith/");
        }

        private void lnkAbout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.pluralsight.com/community/blogs/keith/archive/2009/01/22/create-self-signed-x-509-certificates-in-a-flash-with-self-cert.aspx");
        }

        private void lnkVersion_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.pluralsight.com/community/blogs/keith/archive/2009/01/22/create-self-signed-x-509-certificates-in-a-flash-with-self-cert.aspx");
        }

        private void cboKeySize_Validating(object sender, CancelEventArgs e)
        {
            ValidateKeySize();
        }

        private void txtDN_Validating(object sender, CancelEventArgs e)
        {
            ValidateDN();
        }

        private bool ValidateDN()
        {
            try
            {
                new X500DistinguishedName(txtDN.Text);
                errorProvider.SetError(txtDN, "");
                return true;
            }
            catch (CryptographicException x)
            {
                errorProvider.SetError(txtDN, x.Message);
            }
            return false;
        }

        private bool ValidateKeySize()
        {
            string errorMsg = "";

            int keySize;
            if (int.TryParse(cboKeySize.Text, out keySize))
            {

                switch (keySize)
                {
                    case 384:
                    case 512:
                    case 1024:
                    case 2048:
                    case 4096:
                    case 8192:
                    case 16384:
                        break;
                    default:
                        errorMsg = "Invalid key size.";
                        break;
                }
            }
            else errorMsg = "Key size must be an integer value.";
            errorProvider.SetError(cboKeySize, errorMsg);

            return "" == errorMsg;
        }
    }
}
