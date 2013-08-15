using System;

using Delta.CertXplorer.UI;

namespace Delta.CertXplorer.Logging
{
    /// <summary>
    /// This is the interface a logging service that is <see cref="ITextBoxAppendable"/>
    /// must implement for its returned value.
    /// </summary>
    public interface ITextBoxAppender : IDisposable
    {
        /// <summary>
        /// Gets or sets the current textbox log threshold.
        /// </summary>
        /// <value>The log threshold.</value>
        LogLevel LogThreshold { get; set; }

        /// <summary>
        /// Gets the current text box logger textbox wrapper.
        /// </summary>
        /// <value>The text box wrapper.</value>
        ThreadSafeTextBoxWrapper TextBoxWrapper { get; }
    }
}
