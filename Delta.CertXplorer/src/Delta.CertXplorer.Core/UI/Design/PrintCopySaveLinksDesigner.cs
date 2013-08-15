using System;
using System.Windows.Forms.Design;

namespace Delta.CertXplorer.UI.Design
{
    internal class PrintCopySaveLinksDesigner : ControlDesigner
    {
        /// <summary>
        /// Gets the selection rules that indicate the movement capabilities of a component.
        /// </summary>
        /// <value></value>
        /// <returns>
        /// A bitwise combination of <see cref="T:System.Windows.Forms.Design.SelectionRules"/> values.
        /// </returns>
        public override SelectionRules SelectionRules
        {
            get
            {
                SelectionRules selectionRules = base.SelectionRules;
                selectionRules |= SelectionRules.AllSizeable;
                selectionRules &= ~(SelectionRules.BottomSizeable | SelectionRules.TopSizeable);
                return selectionRules;
            }
        }
    }
}
