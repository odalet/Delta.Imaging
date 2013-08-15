using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;

namespace Delta.CertXplorer.UI
{
    /// <summary>
    /// A TreeView allowing more control over the way it is represented and the way it behaves.
    /// </summary>
    public partial class TreeViewEx : TreeView
    {
        #region Private members

        private const int CTRL_KEYSTATE = 8;

        private ITreeNodeResolver expandedStateNodeResolver = null;
        private TreeViewExpandedStateSerializer treeViewExpandedStateSerializer = null;
        private string expandedState = string.Empty;
        private bool canClearSelection = false;
        private bool editingLabel = false;
        private bool expanding = false;
        private bool collapsing = false;
        private bool allowDrag = false;
        private ITreeViewExContextMenuStripProvider contextMenuStripProvider = null;

        private TreeNode mouseDownSelectedNode = null;
        private TreeNodeEx lastDragOverNode = null;
        private IComparer currentTreeViewNodeSorter = null;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeViewEx"/> class.
        /// </summary>
        public TreeViewEx() : base()
        {
            currentTreeViewNodeSorter = (IComparer)new Sorter();
            base.TreeViewNodeSorter = currentTreeViewNodeSorter;
            base.FullRowSelect = true;
            base.HideSelection = false;
            base.AllowDrop = true;

            treeViewExpandedStateSerializer = new TreeViewExpandedStateSerializer(this, BuiltinTreeNodeResolver.Text);
            expandedStateNodeResolver = treeViewExpandedStateSerializer.TreeNodeResolver;
            contextMenuStripProvider = new DefaultTreeViewExContextMenuStripProvider(this);
        }

        #endregion

        #region Events definition

        /// <summary>
        /// Occurs when the selected node changed.
        /// </summary>
        public event TreeNodeExEventHandler SelectedNodeChanged;
        
        /// <summary>
        /// Occurs when a node was activated.
        /// </summary>
        public event TreeNodeExEventHandler NodeActivated;

        // TODO : TreeView with MultiSelect !
        //public event TreeNodeCollectionEventHandler SelectedNodesChanged;

        #endregion

        #region Sorting

        /// <summary>
        /// Gets or sets a value indicating whether to sort items automatically.
        /// </summary>
        [DefaultValue(true), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public new bool Sorted
        {
            get { return base.Sorted; }
            set
            {
                base.Sorted = value;
                if (value) base.TreeViewNodeSorter = currentTreeViewNodeSorter;
                else base.TreeViewNodeSorter = null;
            }
        }

        /// <summary>
        /// Sets the current tree view node sorter and applies auto sort.
        /// </summary>
        /// <param name="comparer">The tree view node sorter.</param>
        public void SetCurrentTreeViewNodeSorter(IComparer comparer)
        {
            SetCurrentTreeViewNodeSorter(comparer, true);
        }

        /// <summary>
        /// Sets the current tree view node sorter.
        /// </summary>
        /// <param name="comparer">The comparer.</param>
        /// <param name="apply">if set to <c>true</c> then the sort is applied.</param>
        public void SetCurrentTreeViewNodeSorter(IComparer comparer, bool apply)
        {
            if (comparer == null) throw new ArgumentNullException("comparer");
            currentTreeViewNodeSorter = comparer;
            this.Sorted = apply;
        }

        /// <summary>
        /// Gets the current tree view node sorter.
        /// </summary>
        /// <returns>A tree view node sorter.</returns>
        public IComparer GetCurrentTreeViewNodeSorter()
        {
            return currentTreeViewNodeSorter;
        }

        /// <summary>
        /// Gets or sets the implementation of <see cref="T:System.Collections.IComparer"/> to perform a custom sort of the <see cref="T:System.Windows.Forms.TreeView"/> nodes.
        /// </summary>
        /// <value></value>
        /// <returns>
        /// The <see cref="T:System.Collections.IComparer"/> to perform the custom sort.
        /// </returns>
        /// <PermissionSet>
        /// 	<IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
        /// 	<IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
        /// 	<IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/>
        /// 	<IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
        /// </PermissionSet>
        [Browsable(false)]
        public new IComparer TreeViewNodeSorter
        {
            get { return base.TreeViewNodeSorter; }
            set { SetCurrentTreeViewNodeSorter(value, true); }
        }

        #endregion

        #region Search

        /// <summary>
        /// Gets recursively all the child nodes of this tree view.
        /// </summary>
        /// <value>All this tree view's nodes.</value>
        [Browsable(false)]
        public IEnumerable<TreeNode> AllNodes
        {
            get { return this.Find(null); }
        }

        #endregion

        #region Keyboard

        /// <summary>
        /// Processes a command key.
        /// </summary>
        /// <param name="msg">A <see cref="T:System.Windows.Forms.Message"/>, passed by reference, that represents the window message to process.</param>
        /// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys"/> values that represents the key to process.</param>
        /// <returns>
        /// true if the character was processed by the control; otherwise, false.
        /// </returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.F2:
                    BeginLabelEdit();
                    return true;

                case Keys.Return:
                case Keys.Space:
                    if (!editingLabel)
                    {
                        ActivateSelectedNode();
                        return true;
                    }
                    else break;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        #endregion

        #region ExpandedState management

        /// <summary>
        /// Gets or sets the expanded state node resolver.
        /// </summary>
        /// <value>The expanded state node resolver.</value>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public ITreeNodeResolver ExpandedStateNodeResolver
        {
            get { return expandedStateNodeResolver; }
            set
            {
                treeViewExpandedStateSerializer = new TreeViewExpandedStateSerializer(this, value);
                expandedStateNodeResolver = treeViewExpandedStateSerializer.TreeNodeResolver;
            }
        }

        /// <summary>Serializes the expanded state of the tree view.</summary>
        /// <returns>A string containing the serialized representation of the expanded state.</returns>
        public string SerializeExpandedState() { return treeViewExpandedStateSerializer.Serialize(); }

        /// <summary>Deserializes the expanded state of the tree view.</summary>
        /// <param name="state">A string containing the serialized representation of the expanded state.</param>
        public void DeserializeExpandedState(string state)
        {
            if (string.IsNullOrEmpty(state)) return;
            treeViewExpandedStateSerializer.Deserialize(state);
        }

        /// <summary>
        /// Saves the current expanded state of the tree view for future restore via <see cref="RestoreExpandedState"/>.
        /// </summary>
        public void SaveExpandedState()
        {
            OnBeforeSaveExpandedState();
            expandedState = SerializeExpandedState();
            OnAfterSaveExpandedState();
        }

        /// <summary>
        /// Restores a previously saved (<see cref="SaveExpandedState"/>) expanded state of the tree view.
        /// </summary>
        public void RestoreExpandedState()
        {
            OnBeforeRestoreExpandedState();
            DeserializeExpandedState(expandedState);
            OnAfterRestoreExpandedState();
        }

        /// <summary>
        /// Gets a string containing the serialized representation of the current expanded state.
        /// </summary>
        /// <value>The current expanded state.</value>
        protected string ExpandedState { get { return expandedState; } }

        /// <summary>Called before saving the expanded state.</summary>
        protected virtual void OnBeforeSaveExpandedState() { }

        /// <summary>Called after the expanded state has been saved.</summary>
        protected virtual void OnAfterSaveExpandedState() { }

        /// <summary>Called before restoring the expanded state.</summary>
        protected virtual void OnBeforeRestoreExpandedState() { }

        /// <summary>Called after the expanded state has been restored.</summary>
        protected virtual void OnAfterRestoreExpandedState() { }

        #endregion

        #region Selected node

        /// <summary>
        /// Gets/Sets whether the user can clear the selection by clicking in the empty area.
        /// </summary>
        public bool CanClearSelection { get { return canClearSelection; } set { canClearSelection = value; } }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseDown"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs"/> that contains the event data.</param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            var node = GetNodeAt(e.X, e.Y) as TreeNodeEx;
            if (node != null)
            {
                mouseDownSelectedNode = node;
                mouseDownSelectedNode.ContextMenuStrip = ContextMenuStripProvider.GetContextMenuStrip(node);
            }
            else if (canClearSelection) mouseDownSelectedNode = null;
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseUp"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs"/> that contains the event data.</param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (mouseDownSelectedNode != null)
            {
                if (e.Button == MouseButtons.Right) base.SelectedNode = mouseDownSelectedNode;
                else if (e.Button == MouseButtons.Left)
                {
                    if (base.SelectedNode == mouseDownSelectedNode)
                        BeginLabelEdit(mouseDownSelectedNode as TreeNodeEx, false);
                    else base.SelectedNode = mouseDownSelectedNode;
                }

                mouseDownSelectedNode = null;
            }
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.TreeView.BeforeSelect"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.TreeViewCancelEventArgs"/> that contains the event data.</param>
        protected override void OnBeforeSelect(TreeViewCancelEventArgs e) { base.OnBeforeSelect(e); }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.TreeView.AfterSelect"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.TreeViewEventArgs"/> that contains the event data.</param>
        protected override void OnAfterSelect(TreeViewEventArgs e)
        {
            base.OnAfterSelect(e);

            TreeNodeEx selectedNode = e.Node as TreeNodeEx;
            if ((selectedNode != null) && (SelectedNodeChanged != null) /*&& (selectedNode != base.SelectedNode)*/)
                SelectedNodeChanged(this, new TreeNodeExEventArgs(selectedNode));
        }

        #endregion

        #region Expand/Collapse

        /// <summary>
        /// Gets a value indicating whether this <see cref="TreeViewEx"/> is collapsing.
        /// </summary>
        /// <value><c>true</c> if collapsing; otherwise, <c>false</c>.</value>
        protected bool Collapsing { get { return collapsing; } }

        /// <summary>
        /// Gets a value indicating whether this <see cref="TreeViewEx"/> is expanding.
        /// </summary>
        /// <value><c>true</c> if expanding; otherwise, <c>false</c>.</value>
        protected bool Expanding { get { return expanding; } }

        /// <summary>
        /// Gets a value indicating whether this <see cref="TreeViewEx"/> is collapsing or expanding.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if collapsing or expanding; otherwise, <c>false</c>.
        /// </value>
        protected bool CollapsingOrExpanding { get { return collapsing || expanding; } }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.TreeView.BeforeCollapse"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.TreeViewCancelEventArgs"/> that contains the event data.</param>
        protected override void OnBeforeCollapse(TreeViewCancelEventArgs e)
        {
            collapsing = true;
            mouseDownSelectedNode = null;

            TreeNodeEx node = e.Node as TreeNodeEx;
            if (node != null) node.Collapsing(e);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.TreeView.AfterCollapse"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.TreeViewEventArgs"/> that contains the event data.</param>
        protected override void OnAfterCollapse(TreeViewEventArgs e) { collapsing = false; }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.TreeView.BeforeExpand"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.TreeViewCancelEventArgs"/> that contains the event data.</param>
        protected override void OnBeforeExpand(TreeViewCancelEventArgs e)
        {
            expanding = true;
            mouseDownSelectedNode = null;

            TreeNodeEx node = e.Node as TreeNodeEx;
            if (node != null) node.Expanding(e);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.TreeView.AfterExpand"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.TreeViewEventArgs"/> that contains the event data.</param>
        protected override void OnAfterExpand(TreeViewEventArgs e) { expanding = false; }

        #endregion

        #region Node activation

        /// <summary>Activates the specified node.</summary>
        /// <param name="node">The node to activate.</param>
        public void ActivateNode(TreeNodeEx node)
        {
            if (node == null) throw new ArgumentNullException("node");
            if (node.TreeView != this) throw new ArgumentException("This node doesn't belong to this treeview", "node");

            OnBeforeActivateNode(node);

            CancelEventArgs e = new CancelEventArgs(false);
            node.ActivateInternal(e);
            if (!e.Cancel) OnAfterActivateNode(node);
        }

        /// <summary>Called before activating a node.</summary>
        /// <param name="node">The node.</param>
        protected virtual void OnBeforeActivateNode(TreeNodeEx node) { }

        /// <summary>Called when after a node has been activated.</summary>
        /// <param name="node">The node.</param>
        protected virtual void OnAfterActivateNode(TreeNodeEx node)
        {
            if (NodeActivated != null) NodeActivated(this, new TreeNodeExEventArgs(node));
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseDoubleClick"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.Windows.Forms.MouseEventArgs"/> that contains the event data.</param>
        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);
            base.SelectedNode = base.GetNodeAt(e.Location);
            ActivateSelectedNode();
        }

        /// <summary>
        /// Activates the currently selected node.
        /// </summary>
        private void ActivateSelectedNode()
        {
            TreeNodeEx node = base.SelectedNode as TreeNodeEx;
            if (node != null) ActivateNode(node);
        }

        #endregion

        #region Context menu

        /// <summary>
        /// Gets or sets the context menu strip provider.
        /// </summary>
        /// <value>The context menu strip provider.</value>
        public ITreeViewExContextMenuStripProvider ContextMenuStripProvider
        {
            get { return contextMenuStripProvider; }
            set
            {
                if (value == null)
                    contextMenuStripProvider = new DefaultTreeViewExContextMenuStripProvider(this);
                else contextMenuStripProvider = value;
            }
        }

        #endregion

        #region Label edit

        /// <summary>
        /// Gets a value indicating whether this <see cref="TreeViewEx"/> is editing.
        /// </summary>
        /// <value><c>true</c> if editing; otherwise, <c>false</c>.</value>
        public bool Editing { get { return editingLabel; } }

        /// <summary>Begins the label edition.</summary>
        public void BeginLabelEdit() { BeginLabelEdit(SelectedNode as TreeNodeEx, true); }

        /// <summary>Begins the label edition.</summary>
        /// <param name="node">The node.</param>
        public void BeginLabelEdit(TreeNodeEx node) { BeginLabelEdit(node, true); }

        /// <summary>Begins the label edition.</summary>
        /// <param name="node">The node.</param>
        /// <param name="forceEdit">if set to <c>true</c>, force the edition.</param>
        public void BeginLabelEdit(TreeNodeEx node, bool forceEdit)
        {
            if (LabelEdit && (node != null) && (node.LabelEdit))
            {
                if (base.SelectedNode != node) base.SelectedNode = node;
                base.LabelEdit = true;
                if (forceEdit) node.BeginEdit();
            }
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.TreeView.BeforeLabelEdit"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.NodeLabelEditEventArgs"/> that contains the event data.</param>
        protected override void OnBeforeLabelEdit(NodeLabelEditEventArgs e)
        {
            editingLabel = true;

            e.Node.EnsureVisible();
            TreeNodeEx node = e.Node as TreeNodeEx;
            if (node != null) node.OnBeforeLabelEdit();

            base.OnBeforeLabelEdit(e);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.TreeView.AfterLabelEdit"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.NodeLabelEditEventArgs"/> that contains the event data.</param>
        protected override void OnAfterLabelEdit(NodeLabelEditEventArgs e)
        {            
            base.LabelEdit = false;

            e.CancelEdit = string.IsNullOrEmpty(e.Label);
            if (!e.CancelEdit)
            {
                TreeNodeEx node = e.Node as TreeNodeEx;
                if (node != null) node.OnAfterLabelEdit(e.Label);                
                SortParent(e.Node);
                RefreshParent(e.Node);
                // I don't know why we need this last instruction, 
                // but if we don"t apply a refresh, display bugs appear.
                // For example, when renaming a node, the first node is 
                // displayed with the other node's new label.
            }
            //e.CancelEdit = true;

            editingLabel = false;

            // We also need this, because RefreshParent (or SortParent?) loses the currently selected node:
            // The selected node isn't changed, but it displays the root node as selected
            base.SelectedNode = e.Node;

            base.OnAfterLabelEdit(e);
        }

        #endregion

        #region drag'n drop

        /// <summary>
        /// Gets or sets a value indicating whether to allow nodes dragging or not.
        /// </summary>
        /// <value><c>true</c> if [allow drag]; otherwise, <c>false</c>.</value>
        [DefaultValue(false)]
        public bool AllowDrag
        {
            get { return allowDrag; }
            set { allowDrag = value; }
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.TreeView.ItemDrag"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.Windows.Forms.ItemDragEventArgs"/> that contains the event data.</param>
        protected override void OnItemDrag(ItemDragEventArgs e)
        {
            base.OnItemDrag(e);
            if (!allowDrag) return;

            TreeNodeEx node = e.Item as TreeNodeEx;
            if (node != null)
            {
                DataObject data = node.GetDataObject();
                if (data != null)
                {
                    base.DoDragDrop(data, DragDropEffects.All);
                    SortParent(node);
                }
            }
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.DragEnter"/> event.
        /// </summary>
        /// <param name="drgevent">A <see cref="T:System.Windows.Forms.DragEventArgs"/> that contains the event data.</param>
        protected override void OnDragEnter(DragEventArgs drgevent)
        {
            base.OnDragEnter(drgevent);
            if (!allowDrag) return;

            drgevent.Effect = DragDropEffects.Move | DragDropEffects.Copy | DragDropEffects.None;
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.DragOver"/> event.
        /// </summary>
        /// <param name="drgevent">A <see cref="T:System.Windows.Forms.DragEventArgs"/> that contains the event data.</param>
        protected override void OnDragOver(DragEventArgs drgevent)
        {
            base.OnDragOver(drgevent);
            if (!allowDrag) return;

            Point pt = base.PointToClient(new Point(drgevent.X, drgevent.Y));
            TreeNodeEx node = base.GetNodeAt(pt) as TreeNodeEx;

            if (node != null)
            {
                DragDropEffects effect = DragDropEffects.None;
                if ((drgevent.KeyState & CTRL_KEYSTATE) == CTRL_KEYSTATE)
                    effect = DragDropEffects.Copy;
                else effect = DragDropEffects.Move;
                drgevent.Effect = node.FilterEffect(effect);

                if (drgevent.Effect != DragDropEffects.None)
                {
                    if ((lastDragOverNode != null) && (lastDragOverNode != node))
                        lastDragOverNode.DoDragLeave(drgevent);

                    node.DoDragOver(drgevent);
                    base.SelectedNode = lastDragOverNode = node;
                }
            }
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.DragDrop"/> event.
        /// </summary>
        /// <param name="drgevent">A <see cref="T:System.Windows.Forms.DragEventArgs"/> that contains the event data.</param>
        protected override void OnDragDrop(DragEventArgs drgevent)
        {
            base.OnDragDrop(drgevent);
            if (!allowDrag) return;

            Point pt = base.PointToClient(new Point(drgevent.X, drgevent.Y));
            TreeNodeEx node = base.GetNodeAt(pt) as TreeNodeEx;

            if (node != null)
            {
                node.DoDragDrop(drgevent);
                SortParent(node);
            }
        }

        #endregion

        #region Sort & Refresh

        /// <summary>Refreshes the 1st level nodes.</summary>
        private void RefreshNodes() { RefreshNodes(base.Nodes); }

        /// <summary>Refreshes the specified nodes.</summary>
        /// <param name="nodes">The nodes.</param>
        private void RefreshNodes(TreeNodeCollection nodes) 
        {
            TreeNode[] dest = new TreeNode[nodes.Count];
            nodes.CopyTo(dest, 0);
            nodes.Clear();
            nodes.AddRange(dest);
        }

        /// <summary>
        /// Refreshes the children nodes of the parent of the specified node.
        /// </summary>
        /// <param name="node">The node.</param>
        private void RefreshParent(TreeNode node)
        {
            TreeNode parent = node.Parent;
            RefreshNodes((parent == null) ? base.Nodes : parent.Nodes);
        }

        /// <summary>Sorts the 1st level nodes.</summary>
        protected void SortNodes() { SortNodes(base.Nodes, true); }

        /// <summary>Sorts the specified nodes.</summary>
        /// <param name="nodes">The nodes.</param>
        /// <param name="recursive">if set to <c>true</c> recursively sorts the children nodes.</param>
        protected void SortNodes(TreeNodeCollection nodes, bool recursive)
        {
            TreeNode[] array = new TreeNode[nodes.Count];
            nodes.CopyTo(array, 0);
            Array.Sort(array, base.TreeViewNodeSorter);
            nodes.Clear();
            nodes.AddRange(array);

            if (recursive)
            {
                foreach (TreeNode child in array) SortNodes(child.Nodes, true);
            }
        }

        /// <summary>
        /// Sorts the children nodes of the parent of the specified node.
        /// </summary>
        /// <param name="node">The node.</param>
        private void SortParent(TreeNode node)
        {
            TreeNode parent = node.Parent;
            SortNodes((parent == null) ? base.Nodes : parent.Nodes, false);
        }

        #endregion
    }
}