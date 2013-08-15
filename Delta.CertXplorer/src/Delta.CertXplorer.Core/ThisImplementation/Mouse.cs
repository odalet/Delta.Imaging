using System;
using System.Windows.Forms;

namespace Delta.CertXplorer.ThisImplementation
{
    /// <summary>
    /// Provides properties for getting information about the 
    /// format and configuration of the mouse installed on 
    /// the local computer.
    /// </summary>
    public class Mouse
    {
        /// <summary>
        /// Gets a value indicating whether the functionality of the left and right 
        /// mouse buttons has been swapped.
        /// </summary>
        public bool ButtonsSwapped
        {
            get
            {
                CheckMouse();
                return SystemInformation.MouseButtonsSwapped;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the mouse has a scroll wheel..
        /// </summary>
        /// <value><c>true</c> if [wheel exists]; otherwise, <c>false</c>.</value>
        public bool WheelExists
        {
            get
            {
                CheckMouse();
                return SystemInformation.MouseWheelPresent;
            }
        }

        /// <summary>
        /// Gets a number that indicates how much to scroll when the mouse 
        /// wheel is rotated one notch.
        /// </summary>
        public int WheelScrollLines
        {
            get
            {
                CheckWheel();
                return SystemInformation.MouseWheelScrollLines;
            }
        }

        /// <summary>
        /// Checks that a mouse is present.
        /// </summary>
        private void CheckMouse()
        {
            if (!SystemInformation.MousePresent)
                throw new InvalidOperationException("No mouse is present.");
        }

        /// <summary>
        /// Checks that a mouse wheel is present.
        /// </summary>
        private void CheckWheel()
        {
            if (!SystemInformation.MouseWheelPresent)
                throw new InvalidOperationException("No mouse wheel is present.");
        }
    }
}
