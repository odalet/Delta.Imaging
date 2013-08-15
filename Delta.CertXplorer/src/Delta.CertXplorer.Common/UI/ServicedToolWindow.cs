using System.ComponentModel;
using System.ComponentModel.Design;

using Delta.CertXplorer.Logging;
using Delta.CertXplorer.UI.ToolWindows;

namespace Delta.CertXplorer.UI
{
    public class ServicedToolWindow : ToolWindow
    {
        private IServiceContainer services = null;
        private ILogService logger = null;

        /// <summary>
        /// Gets or sets the caption text.
        /// </summary>
        /// <value>The caption text.</value>
        public string CaptionText
        {
            get { return base.Text; }
            set
            {
                base.Text = value;
                base.TabText = value;
            }
        }

        /// <summary>
        /// Gets or sets the services provider.
        /// </summary>
        /// <value>The services provider.</value>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IServiceContainer Services
        {
            get
            {
                if (services == null) return This.Services;
                else return services;
            }
            set { services = value; }
        }

        /// <summary>
        /// Gets or sets the logger service associated with this control.
        /// </summary>
        /// <value>The logger.</value>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ILogService Logger
        {
            get
            {
                if (logger == null)
                {
                    var service = Services.GetService<ILogService>();
                    if (service == null) return This.Logger;
                    else return service;

                }
                else return logger;
            }
            set { logger = value; }
        }
    }
}
