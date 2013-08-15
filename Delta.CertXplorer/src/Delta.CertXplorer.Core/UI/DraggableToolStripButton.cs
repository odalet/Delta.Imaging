using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace Delta.CertXplorer.UI
{
    /// <summary>
    /// A special <see cref="System.Windows.Forms.ToolStripButton"/> that can be dragged and dropped.
    /// </summary>
    [Designer("System.Windows.Forms.Design.ToolStripItemDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"),
    DefaultEvent("Click")]
    public class DraggableToolStripButton : ToolStripButton
    {
        private Size dragSize = SystemInformation.DragSize; // 4 x 4
        private Point lastLocation = Point.Empty;
        private bool down = false;
        private bool dragging = false;
        private bool dragEnabled = true;
        private Label lblDebug = null;

        /// <summary>
        /// Gets or sets a value indicating whether the dragging functionality is enabled.
        /// </summary>
        /// <value><c>true</c> if the dragging functionality is enabled; otherwise, <c>false</c>.</value>
        [Description("A value indicating whether the dragging functionality is enabled.")]
        [Category("Behavior"), DefaultValue(true)]
        public bool DragEnabled { get { return dragEnabled; } set { dragEnabled = value; } }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.ToolStripItem.MouseDown"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs"/> that contains the event data.</param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (!dragEnabled) return;

            lastLocation = e.Location; 
            dragging = false;
            down = true; UpdateDebug();                       
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.ToolStripItem.MouseUp"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs"/> that contains the event data.</param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (!dragEnabled) return;

            dragging = false;
            down = false; UpdateDebug();
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.ToolStripItem.MouseMove"/> event.
        /// </summary>
        /// <param name="mea">A <see cref="T:System.Windows.Forms.MouseEventArgs"/> that contains the event data.</param>
        protected override void OnMouseMove(MouseEventArgs mea)
        {
            base.OnMouseMove(mea);
            if (!dragEnabled) return;

            if (!down || dragging) return;
            if (CanBeginDrag(mea.Location)) // début drag
            {
                dragging = true; UpdateDebug();
                base.DoDragDrop(GetDataObject(), DragDropEffects.All);               
            }
        }

        //protected override void OnGiveFeedback(GiveFeedbackEventArgs giveFeedbackEvent)
        //{
        //    base.OnGiveFeedback(giveFeedbackEvent);
        //    //down = dragging = false; UpdateDebug();
        //}

        /// <summary>Gets the data object associated with this button.</summary>
        /// <returns>The data object associated with this button.</returns>
        protected virtual object GetDataObject() { return this; }

        /// <summary>
        /// Updates the debug label.
        /// </summary>
        private void UpdateDebug()
        {
            if (lblDebug == null) return;
            lblDebug.Text = base.Text + " ";
            lblDebug.Text += down ? "T" : "F";
            lblDebug.Text += dragging ? "T" : "F";
        }

        /// <summary>
        /// Determines whether the button can be dragged from the specified location.
        /// </summary>
        /// <param name="location">The dragging start location.</param>
        /// <returns>
        /// 	<c>true</c> if the button can be dragged from the specified location; otherwise, <c>false</c>.
        /// </returns>
        private bool CanBeginDrag(Point location)
        {
            if (!dragEnabled) return false;

            int x = location.X - lastLocation.X;
            int y = location.Y - lastLocation.Y;
            return (
                (x >= SystemInformation.DragSize.Width) ||
                (x <= -SystemInformation.DragSize.Width) ||
                (y >= SystemInformation.DragSize.Height) ||
                (y <= -SystemInformation.DragSize.Height));
        }
    }
}
