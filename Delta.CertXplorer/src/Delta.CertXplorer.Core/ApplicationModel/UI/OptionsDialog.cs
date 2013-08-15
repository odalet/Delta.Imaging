using System;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;

using Delta.CertXplorer.UI;
using Delta.CertXplorer.ApplicationModel.Services;

namespace Delta.CertXplorer.ApplicationModel.UI
{
    public partial class OptionsDialog : Form
    {
        private List<IOptionsPanel> panels = new List<IOptionsPanel>();

        public OptionsDialog()
        {
            InitializeComponent();

            var layoutService = This.GetService<ILayoutService>();
            if (layoutService != null)
                layoutService.RegisterForm("OPTIONS", this);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            var service = This.GetService<IOptionsService>();
            var contents = new List<IOptionsPanel>();

            if (service != null)
            {
                foreach (var id in service.OptionsPanels.Keys)
                {
                    var creator = service.OptionsPanels[id];
                    IOptionsPanel content = null;
                    try { content = creator(); }
                    catch (Exception ex)
                    {
                        This.Logger.Error(string.Format(
                            "Unable to instantiate panel {0}: {1}", id, ex.Message), ex);
                    }

                    if (content != null)
                    {
                        if (content is Control)
                        {
                            Control control = ((Control)content);
                            control.Margin = new Padding(3);
                            control.Dock = DockStyle.Fill;
                            contents.Add(content);
                        }
                        else This.Logger.Error(string.Format(
                            "Unable to load panel {0}: it is not a control.", id));
                    }
                    else This.Logger.Error(string.Format(
                        "Unable to instantiate panel {0}: returned instance is null.", id));
                }
            }
            else
            {
                var content = new GeneralOptionsPanel();
                content.Dock = DockStyle.Fill;
                contents.Add(content);
            }

            AddContents(contents.ToArray());
        }

        /// <summary>
        /// Gets or sets a value indicating whether the bottom line control is visible.
        /// </summary>
        /// <value><c>true</c> if the bottom line control is visible; otherwise, <c>false</c>.</value>
        protected bool LineControlVisible
        {
            get { return lc.Visible; }
            set { lc.Visible = value; }
        }

        private void AddContents(IOptionsPanel[] optionsPanels)
        {
            var minimumSize = Size.Empty;

            Control[] controls = optionsPanels.OfType<Control>().ToArray();
            if ((controls == null) || (controls.Length == 0)) return;

            if (controls.Length == 1)
            {
                minimumSize.Width = controls[0].MinimumSize.Width;
                minimumSize.Height = controls[0].MinimumSize.Height;

                pnlContent.Controls.Add(controls[0]);
                panels.Add((IOptionsPanel)controls[0]);

                ResizeToFit(minimumSize, pnlContent.ClientSize);                
            }
            else
            {
                Controls.Remove(lc);
                var tc = new CommunityTabControl();
                tc.ShowTabSeparator = true;
                tc.Dock = DockStyle.Fill;                
                pnlContent.Controls.Add(tc);
                foreach (Control control in controls)
                {
                    if (control.MinimumSize.Width > minimumSize.Width)
                        minimumSize.Width = control.MinimumSize.Width;
                    if (control.MinimumSize.Height > minimumSize.Height)
                        minimumSize.Height = control.MinimumSize.Height;

                    var tp = new TabPage(control.Text);
                    tp.UseVisualStyleBackColor = true;
                    tp.Margin = new Padding(3);
                    tp.Padding = new Padding(3);
                    tp.Controls.Add(control);                    
                    tc.TabPages.Add(tp);
                    panels.Add((IOptionsPanel)control);
                }

                ResizeToFit(minimumSize, tc.TabPages[0].ClientSize);   
            }
        }

        private void ResizeToFit(Size minimumSize, Size containerSize)
        {
            var missingSize = Size.Empty;

            missingSize.Width = minimumSize.Width - containerSize.Width;
            missingSize.Height = minimumSize.Height - containerSize.Height;

            MinimumSize = new Size(Width + missingSize.Width, Height + missingSize.Height);
            if (missingSize.Width > 0) Width += missingSize.Width;
            if (missingSize.Height > 0) Height += missingSize.Height;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            foreach (var panel in panels)
            {
                panel.OnClosing(e, DialogResult);
                if (e.Cancel) break;
            }

            base.OnClosing(e);
        }

        protected override void OnClosed(EventArgs e)
        {
            foreach (var panel in panels) panel.OnClosed(e, DialogResult);
            base.OnClosed(e);
        }
    }
}
