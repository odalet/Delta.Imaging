using System;
using System.Drawing;
using System.Windows.Forms;

using Delta.CertXplorer;
using Delta.CertXplorer.ApplicationModel;

namespace Delta.CertXplorer.About
{
    public partial class AboutCertXplorerForm : WindowsFormsAboutServiceForm
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AboutDelta.CertXplorerForm"/> class.
        /// </summary>
        public AboutCertXplorerForm()
        {
            InitializeComponent();

            base.Text = "About " + Program.ApplicationName;

            var service = This.GetService<ILayoutService>();
            if (service != null) service.RegisterForm("Delta.CertXplorer.About", this);
        }

        /// <summary>
        /// Raises the <see cref="E:Load"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            var page = new TabPage("Plugins");
            page.Controls.Add(CreatePluginsPage());

            //base.Tabs.TabBorderColor = Color.FromArgb(130, 0, 0);
            //base.Tabs.TextColor = Color.FromArgb(130, 0, 0);

            ////var text = string.Format("{0} Version {1}", Program.ApplicationName, ThisAssembly.Version);
            ////var splash = new Bitmap(base.Splash);
            ////var rect = new RectangleF(0f, 0f, (float)splash.Width, (float)splash.Height);

            ////using (var g = Graphics.FromImage(splash))
            ////using (var sf = new StringFormat(StringFormatFlags.NoWrap))
            ////using (var font = new Font(FontFamily.GenericSerif, 24f, FontStyle.Bold))
            ////{
            ////    sf.Alignment = StringAlignment.Center;
            ////    sf.LineAlignment = StringAlignment.Center;
            ////    g.DrawString(text, font, Brushes.Black, rect, sf);
            ////}

            ////var old = base.Splash;
            ////base.Splash = splash;
            ////old.Dispose();

            base.Tabs.TabPages.Add(page);
        }

        /// <summary>
        /// Creates the plugins page.
        /// </summary>
        /// <returns>A windows forms control.</returns>
        private Control CreatePluginsPage()     
        {
            var control = new AboutPluginsControl();
            control.Dock = DockStyle.Fill;
            return control;
        }
    }
}
