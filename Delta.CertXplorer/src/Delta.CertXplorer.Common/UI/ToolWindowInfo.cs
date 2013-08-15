using System;
using System.Drawing;

using Delta.CertXplorer.UI.ToolWindows;

namespace Delta.CertXplorer.UI
{
    /// <summary>
    /// Stores information about a tool window.
    /// </summary>
    public class ToolWindowInfo
    {
        private bool enabled = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="ToolWindowInfo"/> class.
        /// </summary>
        public ToolWindowInfo() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ToolWindowInfo"/> class.
        /// </summary>
        /// <param name="isEnabled">if set to <c>true</c> the window is enabled.</param>
        public ToolWindowInfo(bool isEnabled) { enabled = isEnabled; }

        /// <summary>
        /// Occurs when the enabled state of the tool window has changed.
        /// </summary>
        public event EventHandler EnabledChanged;

        /// <summary>
        /// Gets or sets the tool window.
        /// </summary>
        /// <value>The tool window.</value>
        public ToolWindow ToolWindow { get; set; }

        /// <summary>
        /// Gets a value indicating whether the tool window is enabled.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if the tool window is enabled; otherwise, <c>false</c>.
        /// </value>
        public virtual bool IsEnabled { get { return enabled; } }

        /// <summary>
        /// Gets the text of the menu associated with the tool window.
        /// </summary>
        /// <value>The menu text.</value>
        public virtual string MenuText
        {
            get
            {
                if (ToolWindow == null) return string.Empty;
                else { return ToolWindow.ToolTipText; }
            }
        }

        /// <summary>
        /// Gets the image of the menu associated with the tool window.
        /// </summary>
        /// <value>The menu image.</value>
        public virtual Image MenuImage
        {
            get
            {
                if (ToolWindow == null) return null;

                var icon = ToolWindow.Icon;
                if (icon == null) return null;

                return icon.ToBitmap();
            }
        }

        /// <summary>
        /// Called when the enabled state of the tool window has changed.
        /// </summary>
        protected void OnEnabledChanged()
        {
            if (EnabledChanged != null) EnabledChanged(this, EventArgs.Empty);
        }
    }

    /// <summary>
    /// Stores information about a tool window.
    /// </summary>
    /// <typeparam name="T">Type of the tool window.</typeparam>
    public class ToolWindowInfo<T> : ToolWindowInfo where T : ToolWindow
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ToolWindowInfo{T}"/> class.
        /// </summary>
        public ToolWindowInfo() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ToolWindowInfo{T}"/> class.
        /// </summary>
        /// <param name="isEnabled">if set to <c>true</c> [is enabled].</param>
        public ToolWindowInfo(bool isEnabled) : base(isEnabled) { }

        /// <summary>
        /// Gets or sets the tool window.
        /// </summary>
        /// <value>The tool window.</value>
        public new T ToolWindow
        {
            get { return base.ToolWindow as T; }
            set { base.ToolWindow = value; }
        }
    }
}
