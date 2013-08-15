/* 
 * Grabbed from Marco De Sanctis' Actions
 * see http://blogs.ugidotnet.org/crad/articles/38329.aspx
 * Original namespace: Crad.Windows.Forms.Actions
 * License: Common Public License Version 1.0
 * 
 */ 

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing.Design;

namespace Delta.CertXplorer.UI.Actions
{
    [Editor(typeof(Delta.CertXplorer.UI.Design.UIActionCollectionEditor), typeof(UITypeEditor))]
    public class UIActionCollection: Collection<UIAction>
    {
        private UIActionsManager parent = null;

        public UIActionCollection(UIActionsManager parentList) { parent = parentList; }
        
        public UIActionsManager Parent { get { return parent; } }

        protected override void ClearItems()
        {
            foreach (UIAction action in this) action.ActionList = null;
            base.ClearItems();
        }

        protected override void InsertItem(int index, UIAction item)
        {
            // This check is needed because Delta.CertXplorer.ApplicationModel.BaseDockingForm may add the
            // same item multiple times...
            if (base.Contains(item)) return;

            base.InsertItem(index, item);
            item.ActionList = Parent;
        }

        protected override void RemoveItem(int index)
        {
            this[index].ActionList = null;
            base.RemoveItem(index);
        }

        protected override void SetItem(int index, UIAction item)
        {
            if (base.Count > index) this[index].ActionList = null;
            base.SetItem(index, item);

            item.ActionList = Parent;
        }
    }
}
