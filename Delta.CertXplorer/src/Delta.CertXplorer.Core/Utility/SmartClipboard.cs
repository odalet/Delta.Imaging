/*------------------------------------------------------------------------------
 * Debugging Microsoft .NET 2.0 Applications
 * Copyright © 1997-2006 John Robbins -- All rights reserved. 
 -----------------------------------------------------------------------------*/
using System;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;

namespace Delta.CertXplorer.Utility
{
    /// <summary>
    /// Copy and paste data without requiring STA threads!
    /// </summary>
    /// <remarks>
    /// The FCL clipboard class requires the thread calling it to be marked with
    /// <see cref="STAThreadAttribute"/>.  For the most part, that's fine, but 
    /// there are enough instances where this isn't the case; hence this class.
    /// <para>
    /// Credit where credit's due: 
    /// http://www.codinghorror.com/blog/archives/000429.html.  Thanks to Jeff
    /// Atwood showing the basic trick of spawning a new thread with STA set.
    /// (I was ready to do all of this at the API level!  Yeech!)
    /// </para>
    /// <para>
    /// Credits (bis):
    /// Debugging Microsoft .NET 2.0 Applications
    /// Copyright © 1997-2006 John Robbins -- All rights reserved. 
    /// </para>
    /// <para>
    /// Right now this just wraps up the text types.  As future needs dictate, 
    /// I'll add the more interesting data types.
    /// </para>
    /// </remarks>
    public static class SmartClipboard
    {
        private class TextGetData
        {
            internal TextGetData(TextDataFormat format)
            {
                Format = format;
                Data = string.Empty;
            }

            public TextDataFormat Format { get; set; }

            public string Data { get; set; }
        }

        #region SetText Methods

        /// <summary>
        /// Adds text data to the Clipboard in UnicodeText format.
        /// </summary>
        /// <param name="text">
        /// The text to add to the Clipboard.
        /// </param>
        public static void SetText(string text)
        {
            SetText(text, TextDataFormat.UnicodeText);
        }

        /// <summary>
        /// Adds text data to the Clipboard in the format indicated by the 
        /// specified <see cref="TextDataFormat "/> value. 
        /// </summary>
        /// <param name="text">
        /// The text to add to the Clipboard.
        /// </param>
        /// <param name="format">
        /// One of the <see cref="TextDataFormat"/> values.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="text"/> is null or empty.
        /// Thrown if <paramref name="format"/> is not in the appropriate 
        /// <see cref="TextDataFormat"/> range.
        /// </exception>
        public static void SetText(string text, TextDataFormat format)
        {
            if (string.IsNullOrEmpty(text))
                throw new ArgumentException("Invalid parameter", "owner");
            
            if (!IsEnumValid((int)format, 0, 4))
                throw new ArgumentException("Invalid parameter", "format");

            if (Thread.CurrentThread.GetApartmentState() != ApartmentState.STA)
            {
                // Everything should be good so package up the data and let the 
                // background thread get it to the clipboard.
                IDataObject data = new DataObject(ConvertToDataFormats(format), text);
                StartSetThread(data);
            }
            else Clipboard.SetText(text, format); // We're on an STA thread so I can do the call directly.
        }

        private static void StartSetThread(IDataObject data)
        {
            Thread t = new Thread(new ParameterizedThreadStart(SetThread));
            // The whole reason for this class is because it's possible to need
            // to put stuff on the clipboard and the thread isn't marked STA.
            t.SetApartmentState(ApartmentState.STA);
            t.Start(data);
            t.Join();
        }

        private static void SetThread(Object data)
        {
            IDataObject realData = data as IDataObject;
            Clipboard.SetDataObject(realData, true);
        }

        #endregion

        /// <summary>
        /// Retrieves the data from the <see cref="Clipboard"/> in Unicode 
        /// format.
        /// </summary>
        /// <returns>
        /// The <see cref="Clipboard"/> text data or <see cref="String.Empty"/> 
        /// if the <see cref="Clipboard"/> does not contain data in the 
        /// UnicodeText format.
        /// </returns>
        public static string GetText()
        {
            return (GetText(TextDataFormat.UnicodeText));
        }

        /// <summary>
        /// Retrieves text data from the <see cref="Clipboard"/> in the format 
        /// indicated by the specified <see cref="TextDataFormat "/> value. 
        /// </summary>
        /// <param name="format">
        /// One of the <see cref="TextDataFormat "/> values.
        /// </param>
        /// <returns>
        /// The <see cref="Clipboard"/> text data or <see cref="String.Empty"/> 
        /// if the <see cref="Clipboard"/> does not contain data in the 
        /// specified format. 
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="format"/> is not in the appropriate 
        /// <see cref="TextDataFormat"/> range.
        /// </exception>
        public static string GetText(TextDataFormat format)
        {
            // Check the enum value.
            if (!IsEnumValid((int)format, 0, 4))
                throw new ArgumentException("Invalid parameter", "format");

            string returnString;
            if (Thread.CurrentThread.GetApartmentState() == ApartmentState.STA)
                returnString = Clipboard.GetText(format);
            else returnString = StartGetTextThread(format);
            return (returnString);
        }
        
        private static String StartGetTextThread(TextDataFormat format)
        {
            TextGetData data = new TextGetData(format);
            Thread t = new Thread(new ParameterizedThreadStart(GetTextThread));
            // The whole reason for this class is because it's possible to need
            // to put stuff on the clipboard and the thread isn't marked STA.
            t.SetApartmentState(ApartmentState.STA);
            t.Start(data);
            t.Join();
            return (data.Data);
        }

        private static void GetTextThread(Object data)
        {
            TextGetData realData = data as TextGetData;
            realData.Data = Clipboard.GetText(realData.Format);
        }

        #region Private Helper Methods

        // Stolen from the FCL source code.
        private static string ConvertToDataFormats(TextDataFormat format)
        {
            switch (format)
            {
                case TextDataFormat.Text: return DataFormats.Text;
                case TextDataFormat.UnicodeText: return DataFormats.UnicodeText;
                case TextDataFormat.Rtf: return DataFormats.Rtf;
                case TextDataFormat.Html: return DataFormats.Html;
                case TextDataFormat.CommaSeparatedValue: return DataFormats.CommaSeparatedValue;
            }

            return DataFormats.UnicodeText;
        }

        #endregion

        /// <summary>
        /// Checks if <paramref name="value"/> falls in the correct sequential 
        /// enumeration range.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="minValue">The minimum value in <paramref name="enumValue"/>.</param>
        /// <param name="maxValue">The maximum value in <paramref name="enumValue"/>.</param>
        /// <returns>True if correct, false otherwise.</returns>
        private static bool IsEnumValid(int value, int minValue, int maxValue)
        {
            return (value >= minValue) && (value <= maxValue);
        }
    }
}
