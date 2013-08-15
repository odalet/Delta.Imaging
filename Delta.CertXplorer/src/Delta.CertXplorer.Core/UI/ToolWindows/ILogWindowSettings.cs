using System;

namespace Delta.CertXplorer.UI.ToolWindows
{
    public interface ILogWindowSettings
    {
        bool WordWrap { get; set; }
        string MinLogLevel { get; set; }
    }
}
