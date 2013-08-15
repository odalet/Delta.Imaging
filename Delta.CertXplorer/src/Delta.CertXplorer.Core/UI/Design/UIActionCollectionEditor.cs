/* 
 * Grabbed from Marco De Sanctis' Actions
 * see http://blogs.ugidotnet.org/crad/articles/38329.aspx
 * Original namespace: Crad.Windows.Forms.Actions
 * License: Common Public License Version 1.0
 * 
 */ 

using System;
using System.ComponentModel;
using System.ComponentModel.Design;

using Delta.CertXplorer.UI.Actions;

namespace Delta.CertXplorer.UI.Design
{
    /// <summary>
    /// Design-Time Editor for <see cref="Delta.CertXplorer.UI.Actions.UIAction"/> objects collections.
    /// </summary>
    internal sealed class UIActionCollectionEditor : CollectionEditor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UIActionCollectionEditor"/> class.
        /// </summary>
        public UIActionCollectionEditor() : base(typeof(UIActionCollection)) { }

        /// <summary>
        /// Gets the data types that this collection editor can contain.
        /// </summary>
        /// <returns>
        /// An array of data types that this collection can contain.
        /// </returns>
        protected override Type[] CreateNewItemTypes()
        {
            return new Type[] { typeof(UIAction) };
        }

        /// <summary>
        /// Edits the value of the specified object using the specified service provider and context.
        /// </summary>
        /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext"/> that can be used to gain additional context information.</param>
        /// <param name="provider">A service provider object through which editing services can be obtained.</param>
        /// <param name="value">The object to edit the value of.</param>
        /// <returns>
        /// The new value of the object. If the value of the object has not changed, this should return the same object it was passed.
        /// </returns>
        /// <exception cref="T:System.ComponentModel.Design.CheckoutException">
        /// An attempt to check out a file that is checked into a source code management program did not succeed.
        /// </exception>
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            return base.EditValue(context, provider, value);
        }
    }
}
