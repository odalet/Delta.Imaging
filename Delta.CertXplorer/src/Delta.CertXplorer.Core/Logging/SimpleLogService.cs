using System;
using System.Text;
using System.Globalization;
using System.Diagnostics;

namespace Delta.CertXplorer.Logging
{
    /// <summary>
    /// Very simple trace service: everything is redirected to
    /// the <b>Output</b> window of Visual Studio.
    /// </summary>
    public class SimpleLogService : BaseLogService
    {
        public override void Log(LogEntry entry)
        {
            System.Diagnostics.Debug.WriteLine(entry.ToString());
        }
    }
}
