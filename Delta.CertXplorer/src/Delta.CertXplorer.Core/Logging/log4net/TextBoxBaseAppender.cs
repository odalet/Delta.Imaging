using System;
using System.Drawing;
using System.Collections.Generic;

using log4net.Core;
using log4net.Layout;
using log4net.Appender;

using Delta.CertXplorer.UI;

namespace Delta.CertXplorer.Logging.log4net
{
    public class TextBoxBaseAppender : AppenderSkeleton, ITextBoxAppender
    {
        private class TrivialDisposable : IDisposable { public void Dispose() { } }

        private class DisposableList : IDisposable
        {
            private List<IDisposable> list = new List<IDisposable>(2);

            public DisposableList(params IDisposable[] disposableItems)
            {
                foreach (IDisposable disposable in disposableItems) Add(disposable);
            }

            public void Add(IDisposable disposable) 
            { 
                if (disposable != null) list.Add(disposable); 
            }

            #region IDisposable Members

            public void Dispose()
            {
                foreach (IDisposable disposable in list) disposable.Dispose();
            }

            #endregion
        }

        private const string defaultLayout = "%date [%thread] %-8level- %message%newline";

        private ThreadSafeTextBoxWrapper controlWrapper = null;
        private bool closed = false;
        private bool isRichTextBox = false;

        public TextBoxBaseAppender(ThreadSafeTextBoxWrapper textboxWrapper) :
            this(textboxWrapper, new PatternLayout(defaultLayout)) { }

        public TextBoxBaseAppender(ThreadSafeTextBoxWrapper textboxWrapper, ILayout layout)
        {
            if (textboxWrapper == null) throw new ArgumentNullException("textboxWrapper");
            controlWrapper = textboxWrapper;
            isRichTextBox = (controlWrapper is ThreadSafeRichTextBoxWrapper);

            base.Layout = layout ?? new PatternLayout(defaultLayout);
        }

        #region ITextBoxAppender Members

        /// <summary>
        /// Gets the current text box logger textbox wrapper.
        /// </summary>
        /// <value>The text box wrapper.</value>
        public ThreadSafeTextBoxWrapper TextBoxWrapper
        {
            get { return controlWrapper; }
        }

        #endregion

        #region IDisposable Members

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose() { Close(); }

        #endregion

        public LogLevel LogThreshold
        {
            get { return Helper.Log4NetLevelToLogLevel(base.Threshold); }
            set { base.Threshold = Helper.LogLevelToLog4NetLevel(value); }
        }

        protected override void Append(LoggingEvent loggingEvent)
        {
            if (closed) return;

            if (isRichTextBox) AppendRich((ThreadSafeRichTextBoxWrapper)controlWrapper, loggingEvent);
            else AppendNormal(controlWrapper, loggingEvent);
        }

        protected override void OnClose()
        {
            base.OnClose();
            closed = true;
        }

        private void AppendRich(ThreadSafeRichTextBoxWrapper textboxWrapper, LoggingEvent loggingEvent)
        {
            using (GetFormat(textboxWrapper, loggingEvent)) AppendNormal(textboxWrapper, loggingEvent);
        }

        private void AppendNormal(ThreadSafeTextBoxWrapper textboxWrapper, LoggingEvent loggingEvent)
        {
            string text = base.RenderLoggingEvent(loggingEvent);

            textboxWrapper.AppendText(text);
            textboxWrapper.ScrollToCaret();
        }

        private IDisposable GetFormat(ThreadSafeRichTextBoxWrapper textboxWrapper, LoggingEvent loggingEvent)
        {
            if (loggingEvent.Level == Level.All)  return new TrivialDisposable();

            if ((loggingEvent.Level == Level.Verbose) || (loggingEvent.Level == Level.Finest))
                return CreateRichTextBoxFormatter(textboxWrapper, Color.Blue);
            if ((loggingEvent.Level == Level.Trace) || (loggingEvent.Level == Level.Finer))
                return CreateRichTextBoxFormatter(textboxWrapper, Color.Blue, FontStyle.Bold | FontStyle.Underline);
            if ((loggingEvent.Level == Level.Debug) || (loggingEvent.Level == Level.Fine))
                return CreateRichTextBoxFormatter(textboxWrapper, Color.Blue, FontStyle.Bold);                
            
            if (loggingEvent.Level == Level.Info) // Black on White
                return new TrivialDisposable();
            
            if (loggingEvent.Level == Level.Notice)
                return CreateRichTextBoxFormatter(textboxWrapper, Color.Orange);
            if (loggingEvent.Level == Level.Warn)
                return CreateRichTextBoxFormatter(textboxWrapper, Color.Orange, FontStyle.Bold);
            
            if (loggingEvent.Level == Level.Error)
                return CreateRichTextBoxFormatter(textboxWrapper, Color.Red);
            if (loggingEvent.Level == Level.Severe)
                return CreateRichTextBoxFormatter(textboxWrapper, Color.Red, FontStyle.Bold);
            if (loggingEvent.Level == Level.Critical)
                return CreateRichTextBoxFormatter(textboxWrapper, Color.Red, FontStyle.Bold|FontStyle.Underline);
            
            if (loggingEvent.Level == Level.Alert)
                return CreateRichTextBoxFormatter(textboxWrapper, Color.White, Color.Red);
            if (loggingEvent.Level == Level.Fatal)                
                return CreateRichTextBoxFormatter(textboxWrapper, Color.White, Color.Red, FontStyle.Bold);
            if (loggingEvent.Level == Level.Emergency)
                return CreateRichTextBoxFormatter(textboxWrapper, Color.White, Color.Red, FontStyle.Bold|FontStyle.Underline);
            
            return new TrivialDisposable();
        }

        private IDisposable CreateRichTextBoxFormatter(ThreadSafeRichTextBoxWrapper textboxWrapper, Color foreColor, FontStyle style)
        {
            return CreateRichTextBoxFormatter(textboxWrapper, foreColor, Color.Empty, style);
        }

        private IDisposable CreateRichTextBoxFormatter(ThreadSafeRichTextBoxWrapper textboxWrapper, Color foreColor)
        {
            return CreateRichTextBoxFormatter(textboxWrapper, foreColor, Color.Empty);
        }

        private IDisposable CreateRichTextBoxFormatter(ThreadSafeRichTextBoxWrapper textboxWrapper, Color foreColor, Color backColor)
        {
            return new RichTextBoxFormatter(textboxWrapper, foreColor, backColor);
        }

        private IDisposable CreateRichTextBoxFormatter(ThreadSafeRichTextBoxWrapper textboxWrapper, Color foreColor, Color backColor, FontStyle style)
        {
            Font font = new Font(textboxWrapper.Font, style);
            RichTextBoxFormatter formatter = new RichTextBoxFormatter(textboxWrapper, foreColor, backColor, font);

            return new DisposableList(formatter, font);
        }
    }
}
