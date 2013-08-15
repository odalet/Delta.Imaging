using System;
using System.Globalization;

namespace Delta.CertXplorer
{
    /// <summary>
    /// Provides extension methods on the <see cref="System.DateTime"/> class
    /// and the <see cref="DayOfWeek"/> enumeration.
    /// </summary>
    public static class DateTimeExtensions
    {
        private static DateTime[] dayCache = null;

        /// <summary>
        /// Converts the date to a short invariant string representation (format = &quot;yyyy/MM/dd&quot;).
        /// </summary>
        /// <param name="dateTime">The date time to convert.</param>
        /// <returns>The converted string representation.</returns>
        public static string ToShortInvariantString(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy/MM/dd");
        }

        /// <summary>
        /// Converts the date to a long invariant string representation (format = &quot;yyyy/MM/dd hh:mm:ss&quot;).
        /// </summary>
        /// <param name="dateTime">The date time to convert.</param>
        /// <returns>The converted string representation.</returns>
        public static string ToLongInvariantString(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy/MM/dd HH:mm:ss");
        }

        /// <summary>
        /// Converts the date to a very long invariant string representation (format = &quot;yyyy/MM/dd hh:mm:ss.FFFFFFF&quot;).
        /// </summary>
        /// <param name="dateTime">The date time to convert.</param>
        /// <returns>The converted string representation.</returns>
        public static string ToVeryLongInvariantString(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy/MM/dd HH:mm:ss.FFFFFFF");
        }

        public static string ToShortString(this DayOfWeek day)
        {
            return ToShortString(day, CultureInfo.CurrentUICulture);
        }

        public static string ToShortString(this DayOfWeek day, CultureInfo culture)
        {
            return ToFormattedString(day, "ddd", culture);
        }

        public static string ToLongString(this DayOfWeek day)
        {
            return ToLongString(day, CultureInfo.CurrentUICulture);
        }

        public static string ToLongString(this DayOfWeek day, CultureInfo culture)
        {
            return ToFormattedString(day, "dddd", culture);
        }

        private static string ToFormattedString(this DayOfWeek day, string format, CultureInfo culture)
        {
            if (dayCache == null)
            {
                dayCache = new DateTime[7];
                int sundayIndex = 2; // January the 2nd 2000 was a Sunday.                
                for (int i = 0; i < 7; i++)
                {
                    dayCache[i] = new DateTime(2000, 1, sundayIndex);
                    sundayIndex++;
                }
            }

            var dt = dayCache[(int)day];
            return dt.ToString(format, culture);
        }        
    }
}
