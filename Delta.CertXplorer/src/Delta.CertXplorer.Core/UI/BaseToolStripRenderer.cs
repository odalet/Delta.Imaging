using System;
using System.Windows.Forms;

namespace Delta.CertXplorer.UI
{
    /// <summary>
    /// A default toolstrip renderer that uses grayish colors 
    /// and fixes the disabled menu selection bug.
    /// </summary>
    public class BaseToolStripRenderer : ToolStripProfessionalRenderer
    {
        /// <summary>
        /// Color table for <see cref="BaseToolStripRenderer"/>.
        /// </summary>
        private class BaseColorTable : ProfessionalColorTable
        {
            /// <summary>
            /// References this class' unique instance.
            /// </summary>
            public static readonly BaseColorTable Instance = new BaseColorTable();

            /// <summary>
            /// Initializes a new instance of the <see cref="BaseColorTable"/> class.
            /// </summary>
            private BaseColorTable() : base() { base.UseSystemColors = true; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseToolStripRenderer"/> class.
        /// </summary>
        public BaseToolStripRenderer() : this(BaseColorTable.Instance) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseToolStripRenderer"/> class.
        /// </summary>
        public BaseToolStripRenderer(ProfessionalColorTable colorTable) : base(colorTable)
        {
            base.RoundedEdges = true;
        }

        /// <summary>
        /// We override this so that no border is drawn around disabled menu items.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemRenderEventArgs"/> that contains the event data.</param>
        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            if (e.Item.Enabled) base.OnRenderMenuItemBackground(e);
        }
    }
}
