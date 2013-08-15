using System;
using System.Drawing.Design;
using System.Windows.Forms;
using System.ComponentModel;
using System.ComponentModel.Design;

using Delta.CertXplorer.UI.Design;
using Delta.CertXplorer.UI.Actions;
using Delta.CertXplorer.ApplicationModel.Design;

using WeifenLuo.WinFormsUI.Docking;

namespace Delta.CertXplorer.ApplicationModel
{
    [Designer(typeof(BaseDockingMainFormDesigner), typeof(IRootDesigner)),
    ProvideProperty("Action", typeof(Component))]
    public partial class BaseDockingMainForm : Form, IExtenderProvider
    {
        private bool toolstripPanelsFixed = false;
        private ToolStripRenderMode renderMode = ToolStripRenderMode.ManagerRenderMode;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseDockingMainForm"/> class.
        /// </summary>
        public BaseDockingMainForm() 
        {
            InitializeComponent();             
        }

        /// <summary>
        /// Gets or sets the render mode.
        /// </summary>
        /// <value>The render mode.</value>
        public ToolStripRenderMode RenderMode
        {
            get { return renderMode; }
            set
            {
                if (renderMode != value)
                {
                    renderMode = value;
                    menuStrip.RenderMode = renderMode;
                    topToolStripPanel.RenderMode = renderMode;
                    bottomToolStripPanel.RenderMode = renderMode;
                    leftToolStripPanel.RenderMode = renderMode;
                    rightToolStripPanel.RenderMode = renderMode;
                }
            }
        }

        /// <summary>
        /// Gets the top tool strip panel.
        /// </summary>
        /// <value>The top tool strip panel.</value>
        public ToolStripPanel TopToolStripPanel
        {
            get { return topToolStripPanel; }
        }

        /// <summary>
        /// Gets the left tool strip panel.
        /// </summary>
        /// <value>The left tool strip panel.</value>
        public ToolStripPanel LeftToolStripPanel
        {
            get { return leftToolStripPanel; }
        }

        /// <summary>
        /// Gets the right tool strip panel.
        /// </summary>
        /// <value>The right tool strip panel.</value>
        public ToolStripPanel RightToolStripPanel
        {
            get { return rightToolStripPanel; }
        }

        /// <summary>
        /// Gets the bottom tool strip panel.
        /// </summary>
        /// <value>The bottom tool strip panel.</value>
        public ToolStripPanel BottomToolStripPanel
        {
            get { return bottomToolStripPanel; }
        }

        /// <summary>
        /// Gets the workspace (the docking manager).
        /// </summary>
        /// <value>The workspace.</value>
        protected DockPanel Workspace
        {
            get { return workspace; }
        }

        [Editor(typeof(UIActionCollectionEditor), typeof(UITypeEditor)),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public UIActionCollection Actions
        {
            get { return actionsManager.Actions; }
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);
            if (!toolstripPanelsFixed)
            {
                FixToolStripPanelsChildIndex();
                toolstripPanelsFixed = true;
            }
        }

        /// <summary>
        /// Fixes the child index of the toolstrip panels.
        /// </summary>
        private void FixToolStripPanelsChildIndex()
        {
            /*
             * This fixes the appearance of the vertical toolstrip panels:
             * If we don't do run the code below, the toolstrip panels may appear
             * layed out like the figure on the left, and we want them like 
             * the figure on the right:
             *
             * |-------------|            ---------------
             * |             |            |             |
             * |             |            |             |
             * |             |            |             |
             * ---------------            ---------------
             * 
             * To achieve this, we reorder the child index so that the vertical panels
             * are created first. This ensures that the latter (the horizontal panels) 
             * will expand to their full width.
             * 
             */

            Controls.SetChildIndex(rightToolStripPanel, 0);
            Controls.SetChildIndex(leftToolStripPanel, 1);
            Controls.SetChildIndex(topToolStripPanel, 2);
            Controls.SetChildIndex(bottomToolStripPanel, 3);
        }

        [DefaultValue(null)]
        public UIAction GetAction(Component extendee)
        {
            return actionsManager.GetAction(extendee);
        }

        public void SetAction(Component extendee, UIAction action)
        {
            // There may be a problem: an action can be set to an extendee before it is part 
            // of the manager actions list...
            // So we add it here to the collection (if it doesn't exist)
            // This means each action will probably be added twice to the collection.
            // This is not really an issue as the UIActionCollection checks for duplicates 
            // before adding actions.
            if (!actionsManager.Actions.Contains(action)) actionsManager.Actions.Add(action);

            actionsManager.SetAction(extendee, action);
        }

        #region IExtenderProvider Members

        /// <summary>
        /// Specifies whether this object can provide its extender properties to the specified object.
        /// </summary>
        /// <param name="extendee">The <see cref="T:System.Object"/> to receive the extender properties.</param>
        /// <returns>
        /// true if this object can provide extender properties to the specified object; otherwise, false.
        /// </returns>
        bool IExtenderProvider.CanExtend(object extendee)
        {
            return ((IExtenderProvider)actionsManager).CanExtend(extendee);
        }

        #endregion
    }
}
