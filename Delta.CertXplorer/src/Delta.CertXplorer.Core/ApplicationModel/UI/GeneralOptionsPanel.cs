using System;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;

using Delta.CertXplorer.UI.Theming;
using Delta.CertXplorer.Configuration;

namespace Delta.CertXplorer.ApplicationModel.UI
{
    /// <summary>
    /// This is the default (and unique for now) options panel.
    /// </summary>
    public partial class GeneralOptionsPanel : BaseOptionsPanel
    {
        private class ApplicationSettings
        {
            public bool EnableLogWindow { get; set; }
        }

        private ISettingsService settingsService = null;
        private IThemingService themingService = null;
        private string originalTheme = string.Empty;

        /// <summary>
        /// Called when the <see cref="OptionsDialog"/> raises the <see cref="E:Closed"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <param name="dialogResult">The parent <see cref="OptionsDialog"/> dialog result.</param>
        protected override void OnPanelClosed(EventArgs e, DialogResult dialogResult)
        {
            if (dialogResult == DialogResult.Cancel) // restore old settings
            {
                try
                {
                    if ((themingService != null) &&
                        (originalTheme != themingService.Current) &&
                        themingService.ContainsTheme(originalTheme))
                        themingService.ApplyTheme(originalTheme);
                }
                catch (Exception ex)
                {
                    This.Logger.Warning("Unable to restore previous theme.", ex);
                }
            }
            else if (settingsService != null) // apply new settings
            {
                try
                {
                    settingsService.UpdateApplicationSettingsStore(new
                    {
                        Theme = (string)cboTheme.SelectedItem,
                        EnableLogWindow = cbEnableLogWindow.Checked
                    });
                }
                catch (Exception ex)
                {
                    This.Logger.Warning("Error while saving application settings.", ex);
                }
            }
        }

        public override string Text
        {
            get { return SR.GeneralOptions; }
            set { }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GeneralOptionsPanel"/> class.
        /// </summary>
        public GeneralOptionsPanel()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.UserControl.Load"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (DesignMode) return;

            InitializeThemes();
            InitializeLogging();
        }

        private void InitializeThemes()
        {
            themingService = This.GetService<IThemingService>();
            if (themingService == null)
            {
                pnlTheming.Enabled = false;
                pnlTheming.Controls.Clear();
                pnlTheming.Controls.Add(new Label()
                {
                    Text = "Theming functionality is disabled.",
                    Font = SystemFonts.MessageBoxFont,
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter
                });
            }
            else
            {
                cboTheme.Items.AddRange(themingService.Themes.ToArray());
                originalTheme = themingService.Current;
                cboTheme.SelectedItem = originalTheme;

                cboTheme.SelectedIndexChanged += (s, e) =>
                    themingService.ApplyTheme((string)cboTheme.SelectedItem);
            }
        }

        private void InitializeLogging()
        {
            settingsService = This.GetService<ISettingsService>(true);
            var settings = settingsService.GetApplicationSettingsStore<ApplicationSettings>();
            cbEnableLogWindow.Checked = settings.EnableLogWindow;
        }
    }
}
