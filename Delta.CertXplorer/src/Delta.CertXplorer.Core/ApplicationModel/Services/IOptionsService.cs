using System;
using System.Windows.Forms;
using System.Collections.Generic;

using Delta.CertXplorer.ApplicationModel.UI;

namespace Delta.CertXplorer.ApplicationModel.Services
{
    public interface IOptionsService
    {
        DialogResult ShowOptionsDialog(IWin32Window owner);

        IDictionary<string, Func<IOptionsPanel>> OptionsPanels { get; }

        void AddPanel(string id, IOptionsPanel panel);
        void AddPanel(string id, Func<IOptionsPanel> panelCreator);

        void RemovePanel(string id);
    }
}
