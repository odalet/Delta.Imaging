using System.Windows.Forms;

using Delta.CertXplorer.ApplicationModel;

namespace Delta.CertXplorer.About
{
    internal class AboutCertXplorerService : WindowsFormsAboutService
    {
        public AboutCertXplorerService()
        {
            //base.History = Properties.Resources.ReleaseNotes;
        }

        /// <summary>
        /// Creates the form.
        /// </summary>
        /// <returns></returns>
        protected override Form CreateForm()
        {
            return new AboutCertXplorerForm();
        }
    }
}
