using System;
using System.Text;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace Delta.CertXplorer.ApplicationModel
{
    public class WindowsFormsAboutService : IAboutService
    {
        #region IAboutService Members

        public DialogResult ShowAboutDialog(IWin32Window owner)
        {
            using (var dialog = CreateForm())
            {
                if (owner == null) dialog.StartPosition = FormStartPosition.CenterScreen;
                
                if ((Splash != null) && (dialog is WindowsFormsAboutServiceForm))
                    ((WindowsFormsAboutServiceForm)dialog).Splash = Splash;
                return dialog.ShowDialog(owner);
            }
        }

        public string GetAboutText()
        {
            var sb = new StringBuilder();
            sb.AppendLine(string.Format(SR.AboutNameVersion, AssemblyTitle, AssemblyVersion));
            sb.AppendLine(string.Format("Product:     {0}", AssemblyProduct));
            sb.AppendLine(string.Format("Version:     {0}", AssemblyVersion));
            sb.AppendLine(string.Format("Copyright:   {0}", AssemblyCopyright));
            sb.AppendLine(string.Format("Company:     {0}", AssemblyCompany));
            sb.AppendLine(string.Format("Description: {0}", AssemblyDescription));
            sb.AppendLine();
            sb.AppendLine(SR.LoadedModules);
            try { AppDomain.CurrentDomain.AppendModuleDescriptions(sb); }
            catch (Exception ex)
            {
                sb.AppendLine(string.Format("Error: Unable to retrieve the loaded assemblies list:\r\n",
                    ex.ToFormattedString()));
            }

            if (!string.IsNullOrEmpty(Credits))
            {
                sb.AppendLine();
                sb.AppendLine(SR.Credits);
                sb.AppendLine();
                sb.AppendLine(Credits);
            }

            return sb.ToString();
        }

        #endregion

        /// <summary>
        /// Gets or sets the credits text.
        /// </summary>
        /// <value>The credits text.</value>
        public string Credits { get; set; }

        /// <summary>
        /// Gets or sets the history text.
        /// </summary>
        /// <value>The history text.</value>
        public string History { get; set; }

        public Image Splash { get; set; }

        #region Assembly Attribute Accessors

        public string AssemblyTitle
        {
            get { return GetAssemblyTitle(TargetAssembly); }
        }

        public string AssemblyVersion
        {
            get { return GetAssemblyVersion(TargetAssembly); }
        }

        public string AssemblyDescription
        {
            get { return TargetAssembly.GetDescription(); }
        }

        public string AssemblyProduct
        {
            get
            {
                object[] attributes = TargetAssembly.GetCustomAttributes(
                    typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0) return string.Empty;

                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                var attributes = TargetAssembly.GetCustomAttributes(
                    typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0) return string.Empty;

                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                var attributes = TargetAssembly.GetCustomAttributes(
                    typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0) return string.Empty;

                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }

        protected virtual Assembly TargetAssembly
        {
            get
            {
                Assembly assembly = null;
                try { assembly = This.Application.EntryAssembly; }
                catch (Exception ex)
                {
                    var debugEx = ex;
                    assembly = Assembly.GetExecutingAssembly();
                }

                return assembly;
            }
        }

        #endregion

        protected virtual Form CreateForm()
        {
            return new WindowsFormsAboutServiceForm(this);
        }

        private string GetAssemblyTitle(Assembly assembly)
        {
            return assembly.GetTitle(true);
        }

        private string GetAssemblyVersion(Assembly assembly)
        {
            return assembly.GetName().Version.ToString();
        }

        private string GetAssemblyPath(Assembly assembly) { return assembly.Location; }

        private string GetAssemblyDescription(Assembly assembly)
        {
            return assembly.GetDescription();
        }
    }
}
