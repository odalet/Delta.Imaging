using System;
using System.Windows.Forms;
using System.Collections.Generic;

using Delta.CertXplorer.ApplicationModel.UI;

namespace Delta.CertXplorer.ApplicationModel.Services
{
    /// <summary>
    /// Default implementation of <see cref="IOptionsService"/>.
    /// </summary>
    public class DefaultOptionsService : IOptionsService
    {
        public const string GeneralOptionsPanelId = "General";

        private Dictionary<string, Func<IOptionsPanel>> panels = 
            new Dictionary<string, Func<IOptionsPanel>>();

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultOptionsService"/> class.
        /// </summary>
        public DefaultOptionsService()
        {
            AddDefaultOptionsPanels();
        }

        #region IOptionsService Members

        public virtual DialogResult ShowOptionsDialog(IWin32Window owner)
        {
            return new OptionsDialog().ShowDialog(owner);
        }

        public IDictionary<string, Func<IOptionsPanel>> OptionsPanels
        {
            get { return panels; }
        }

        public void AddPanel(string id, IOptionsPanel panel)        
        {
            if (panel == null) throw new ArgumentNullException("panel");
            AddPanel(id, () => panel);
        }

        public void AddPanel(string id, Func<IOptionsPanel> panelCreator)
        {
            if (panelCreator == null) throw new ArgumentNullException("panelCreator");
            panels.Add(id, panelCreator);
        }

        public void RemovePanel(string id)
        {
            panels.Remove(id);
        }

        #endregion

        /// <summary>
        /// Adds the default options panels.
        /// </summary>
        /// <remarks>
        /// In this implementation, one panel is added: <see cref="GeneralOptionsPanel"/> with id
        /// <see cref="GeneralOptionsPanelId"/>.
        /// </remarks>
        protected virtual void AddDefaultOptionsPanels()
        {
            AddPanel(GeneralOptionsPanelId, () => new GeneralOptionsPanel());
        }
    }
}
