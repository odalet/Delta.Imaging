using System;
using System.Windows.Forms;

using WeifenLuo.WinFormsUI.Docking;

using Delta.CertXplorer.UI.ToolWindows;

namespace Delta.CertXplorer.ApplicationModel
{
    /// <summary>
    /// This service manages and reminds a form bounds and state. It can also manage
    /// <see cref="WeifenLuo.WinFormsUI.Docking.DockPanel"/> objects.
    /// </summary>
    public interface ILayoutService
    {
        /// <summary>
        /// Registers a form with the service.
        /// </summary>
        /// <param name="key">The key identifying the form class.</param>
        /// <param name="form">The form instance.</param>
        void RegisterForm(string key, Form form);

        /// <summary>
        /// Registers a form and its dock panel with the service.
        /// </summary>
        /// <param name="key">The key identifying the form class.</param>
        /// <param name="form">The form instance.</param>
        /// <param name="workspace">The workspace.</param>
        void RegisterForm(string key, Form form, DockPanel workspace);

        /// <summary>
        /// Registers a docking tool window with this service.
        /// </summary>
        /// <param name="key">The key identifying the containing form class.</param>
        /// <param name="window">The tool window instance.</param>
        void RegisterToolWindow(string key, ToolWindow window);

        /// <summary>
        /// Restores the docking state of a <see cref="WeifenLuo.WinFormsUI.Docking.DockPanel"/>
        /// and its tool windows.
        /// </summary>
        /// <param name="key">The key identifying the containing form class.</param>
        /// <returns><c>true</c> if the deserialization was successful; otherwise, <c>false</c>.</returns>
        bool RestoreDockingState(string key);
    }
}
