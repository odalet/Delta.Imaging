using System.ComponentModel;
using System.Windows.Forms;

using Delta.CertXplorer.UI;

namespace Delta.CertXplorer.UI
{
    public class FolderTreeNode : TreeNodeEx
    {
        private int collapsedImageIndex = -1;
        private int selectedCollapsedImageIndex = -1;

        /// <summary>
        /// Initializes a new instance of the <see cref="FolderTreeNode"/> class.
        /// </summary>
        public FolderTreeNode() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="FolderTreeNode"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        public FolderTreeNode(string text) : base(text) { }

        /// <summary>
        /// Gets or sets the image index for the collapsed state.
        /// </summary>
        /// <value>The image index for the collapsed state.</value>
        [DefaultValue(-1)]
        public int CollapsedImageIndex 
        {
            get { return collapsedImageIndex; }
            set
            {
                collapsedImageIndex = value;
                if (base.ImageIndex == -1) base.ImageIndex = value;
            }
        }

        /// <summary>
        /// Gets or sets the image index for the collapsed expanded state.
        /// </summary>
        /// <value>The image index for the selected collapsed state.</value>
        [DefaultValue(-1)]
        public int SelectedCollapsedImageIndex
        {
            get { return selectedCollapsedImageIndex; }
            set
            {
                selectedCollapsedImageIndex = value;
                if (base.SelectedImageIndex == -1) base.SelectedImageIndex = value;
            }
        }

        /// <summary>
        /// Gets or sets the image index for the expanded state.
        /// </summary>
        /// <value>The image index for the expanded state.</value>
        [DefaultValue(-1)]
        public int ExpandedImageIndex { get; set; }

        /// <summary>
        /// Gets or sets the image index for the selected expanded state.
        /// </summary>
        /// <value>The image index for the selected expanded state.</value>
        [DefaultValue(-1)]
        public int SelectedExpandedImageIndex { get; set; }

        /// <summary>
        /// Expanding the current node.
        /// </summary>
        /// <param name="e">The <see cref="System.Windows.Forms.TreeViewCancelEventArgs"/> instance containing the event data.</param>
        protected override void Expanding(TreeViewCancelEventArgs e)
        {
            base.Expanding(e);
            SetExpandedState(true);
        }

        /// <summary>
        /// Collapsing the current node.
        /// </summary>
        /// <param name="e">The <see cref="System.Windows.Forms.TreeViewCancelEventArgs"/> instance containing the event data.</param>
        protected override void Collapsing(TreeViewCancelEventArgs e)
        {
            base.Collapsing(e);
            SetExpandedState(false);
        }

        /// <summary>
        /// Sets the expanded state of this node.
        /// </summary>
        /// <param name="expanded">if set to <c>true</c> the node is expanded; otherwise (<c>false</c>), it is collapsed.</param>
        private void SetExpandedState(bool expanded)
        {
            if (expanded)
            {
                base.ImageIndex = ExpandedImageIndex;
                base.SelectedImageIndex = SelectedExpandedImageIndex;
            }
            else 
            {
                base.ImageIndex = CollapsedImageIndex;
                base.SelectedImageIndex = SelectedCollapsedImageIndex;
            }
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        private void Initialize()
        {
            // default values
            ExpandedImageIndex = -1;
            SelectedExpandedImageIndex = -1;
            CollapsedImageIndex = -1;
            SelectedCollapsedImageIndex = -1;
        }
    }
}
