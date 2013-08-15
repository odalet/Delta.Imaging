using System;

namespace Delta.CertXplorer.ThisImplementation
{
    /// <summary>
    /// Provides properties for accessing the current local time and
    /// Universal Coordinated Time (equivalent to Greenwich Mean Time)
    /// from the system clock on the local computer.
    /// </summary>
    public class Clock
    {
        /// <summary>
        /// Gets a Date object that contains the current local date and 
        /// time on the computer, expressed as a UTC (GMT) time.
        /// </summary>
        public DateTime GmtTime
        {
            get { return DateTime.UtcNow; }
        }

        /// <summary>
        /// Gets a Date object that contains the current local date and time on this computer
        /// </summary>
        public DateTime LocalTime
        {
            get { return DateTime.Now; }
        }

        /// <summary>
        /// Gets the millisecond count from the computer's system timer.
        /// </summary>
        public int TickCount
        {
            get { return Environment.TickCount; }
        }
    }
}
