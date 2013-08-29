using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;

namespace Delta.CertXplorer.UI
{
    /// <summary>
    /// Reprents a specialized Tree node (to be used with <see cref="TreeViewEx"/>).
    /// </summary>
    public class TreeNodeEx : TreeNode, IComparable<TreeNodeEx>, IEditActionsHandler
    {
        private bool allowLabelEdit = true;        
        
        private DateTime dragOverTimer = DateTime.MaxValue;
        private bool delayedDragOverAction = true;
        private TimeSpan dragOverActionDelay = new TimeSpan(0, 0, 1);
        private EditActionState editActionState = EditActionState.None;

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeNodeEx"/> class.
        /// </summary>
        public TreeNodeEx() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeNodeEx"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        public TreeNodeEx(string text) : base(text) { }

        #region IComparable<TreeNodeEx> Members

        public virtual int CompareTo(TreeNodeEx other)
        {
            if (NodeOrder != other.NodeOrder) return NodeOrder - other.NodeOrder;
            return base.Text.CompareTo(other.Text);
        }

        #endregion

        #region Search

        /// <summary>
        /// Gets recursively all the child nodes of this tree node.
        /// </summary>
        /// <value>All this tree node's child nodes.</value>
        public IEnumerable<TreeNode> AllNodes
        {
            get { return this.Find(null); }
        }

        #endregion

        #region Label Edit
        public bool LabelEdit { get { return allowLabelEdit; } set { allowLabelEdit = value; } }

        protected internal virtual void OnBeforeLabelEdit() { }
        protected internal virtual void OnAfterLabelEdit(string newLabel) 
        {
            base.Text = newLabel;
        }

        #endregion

        public TreeViewEx TreeViewEx { get { return base.TreeView as TreeViewEx; } }

        public bool DelayedDragOverAction { get { return delayedDragOverAction; } set { delayedDragOverAction = value; } }
        public TimeSpan DragOverActionDelay { get { return dragOverActionDelay; } set { dragOverActionDelay = value; } }
        
        protected virtual int NodeOrder { get { return 0; } }

        #region Expand / Collapse

        protected internal virtual void Expanding(TreeViewCancelEventArgs e) { }
        protected internal virtual void Collapsing(TreeViewCancelEventArgs e) { }   

        #endregion

        #region Node activation

        public void Activate() { TreeViewEx.ActivateNode(this); }

        internal void ActivateInternal(CancelEventArgs e) { OnActivate(e); }
        
        protected virtual void OnActivate(CancelEventArgs e) { }

        #endregion

        #region drag'n drop

        protected internal virtual DataObject GetDataObject()
        {
#if DEBUG
            return new DataObject(ToString());
#else
            return null;
#endif
        }

        protected internal virtual DragDropEffects FilterEffect(DragDropEffects defaultEffect)
        {
#if DEBUG
            return defaultEffect;
#else
            return DragDropEffects.None;
#endif
        }

        protected virtual void OnDragOver(DragEventArgs drgevent)
        {
#if DEBUG
            Expand();
#endif
        }

        protected virtual void OnDragDrop(DragEventArgs drgevent)
        {
#if DEBUG
            Expand();
#endif
        }

        internal void DoDragOver(DragEventArgs drgevent)
        {
            if (delayedDragOverAction)
            {
                if (IsDragOverTimerReset) InitializeDragOverTimer();
                else
                {
                    if (IsDragOverTimeElapsed)
                    {
                        ResetDragOverTimer();
                        OnDragOver(drgevent);
                    }
                }
            }
            else OnDragOver(drgevent);
        }

        internal void DoDragLeave(DragEventArgs drgevent) { ResetDragOverTimer(); }

        protected internal virtual void DoDragDrop(DragEventArgs drgevent) { OnDragDrop(drgevent); }

        private void ResetDragOverTimer() { dragOverTimer = DateTime.MaxValue; }
        private void InitializeDragOverTimer() { dragOverTimer = DateTime.Now; }
        private bool IsDragOverTimerReset { get { return (dragOverTimer == DateTime.MaxValue); } }
        private bool IsDragOverTimeElapsed { get { return ((DateTime.Now - dragOverTimer) >= dragOverActionDelay); } }

        #endregion

        #region Edit actions

        protected virtual void OnEditActionStateChanged(EditActionState state) { }

        #region IEditActionsHandler Members

        public EditActionState EditActionState
        {
            get { return editActionState; }
            set
            {
                if (value != editActionState)
                {
                    editActionState = value;
                    OnEditActionStateChanged(editActionState);

                    foreach (TreeNode tn in base.Nodes)
                    {
                        if (tn is TreeNodeEx) ((TreeNodeEx)tn).EditActionState = value;
                    }
                }
            }
        }

        public virtual bool CanCut { get { return false; } }
        public virtual bool CanCopy { get { return false; } }
        public virtual bool CanPaste { get { return false; } }
        public virtual bool CanDelete { get { return false; } }
        public virtual bool CanSelectAll { get { return false; } }
        public virtual void Cut() { }
        public virtual void Copy() { }
        public virtual void Paste() { }
        public virtual void Delete() { }
        public virtual void SelectAll() { }

        #endregion

        #endregion
    }
}
