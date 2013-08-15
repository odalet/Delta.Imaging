using System;

using Delta.CertXplorer.UI;

namespace Delta.CertXplorer.Logging
{
    /// <summary>
    /// This is an additional interface a <see cref="ILogService"/> service should
    /// implement if it is able to append log entries to a Windows Forms Textbox.
    /// </summary>
    public interface ITextBoxAppendable
    {
        /// <summary>
        /// Adds a textbox to the logger appenders.
        /// </summary>
        /// <param name="textboxWrapper">The textbox wrapper.</param>
        /// <returns>An <see cref="ITextBoxAppender"/> object representing the newly 
        /// added logging destination.</returns>
        /// <remarks>
        /// <para>
        /// We don't pass directly a textbox but rather a custom wrapper
        /// that allows thread-safe updates of an underlying textbox.
        /// </para>
        /// <para>
        /// This method returns an <see cref="IDisposable"/> object.
        /// When the Text box logging is not needed any more, this object
        /// should be disposed.
        /// </para>
        /// </remarks>
        ITextBoxAppender AddLogBox(ThreadSafeTextBoxWrapper textboxWrapper);

        /// <summary>
        /// Adds a textbox to the logger appenders.
        /// </summary>
        /// <param name="textboxWrapper">The textbox wrapper.</param>
        /// <param name="patternLayout">The pattern layout.</param>
        /// <returns>
        /// An <see cref="ITextBoxAppender"/> object representing the newly
        /// added logging destination.
        /// </returns>
        /// <remarks>
        /// 	<para>
        /// We don't pass directly a textbox but rather a custom wrapper
        /// that allows thread-safe updates of an underlying textbox.
        /// </para>
        /// 	<para>
        /// This method returns an <see cref="IDisposable"/> object.
        /// When the Text box logging is not needed any more, this object
        /// should be disposed.
        /// </para>
        /// </remarks>
        ITextBoxAppender AddLogBox(ThreadSafeTextBoxWrapper textboxWrapper, string patternLayout);
    }
}
