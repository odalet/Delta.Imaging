using System;
using System.Linq;
using System.Drawing.Design;
using System.Windows.Forms;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections.Generic;

using WeifenLuo.WinFormsUI.Docking;

using Delta.CertXplorer;
using Delta.CertXplorer.UI.Actions;
using Delta.CertXplorer.UI.ToolWindows;
using Delta.CertXplorer.ApplicationModel;

namespace Delta.CertXplorer.UI
{
    /// <summary>
    /// Base class for main forms.
    /// </summary>
    [Designer(typeof(BaseChromeDesigner), typeof(IRootDesigner)), ProvideProperty("Action", typeof(Component))]
    public partial class BaseChrome : Form, IExtenderProvider //, IAdditionalLayoutDataSource
    {
        private bool toolstripPanelsFixed = false;
        private ILayoutService layoutService = null;
        private ToolStripRenderMode renderMode = ToolStripRenderMode.ManagerRenderMode;
        private Dictionary<Guid, ToolWindowInfo> toolWindowInfos = new Dictionary<Guid, ToolWindowInfo>();        

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseDockingMainForm"/> class.
        /// </summary>
        public BaseChrome() 
        {
            InitializeComponent();
        }

        #region Properties

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
        /// Gets this form's main menu strip.
        /// </summary>
        /// <value>The menu strip.</value>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public MenuStrip MenuStrip
        {
            get { return menuStrip; }
        }

        /// <summary>
        /// Gets the view menu item.
        /// </summary>
        /// <value>The view menu item.</value>
        protected ToolStripMenuItem ViewMenuItem { get; set; }

        /// <summary>
        /// Gets the top tool strip panel.
        /// </summary>
        /// <value>The top tool strip panel.</value>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ToolStripPanel TopToolStripPanel
        {
            get { return topToolStripPanel; }
        }

        /// <summary>
        /// Gets the left tool strip panel.
        /// </summary>
        /// <value>The left tool strip panel.</value>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ToolStripPanel LeftToolStripPanel
        {
            get { return leftToolStripPanel; }
        }

        /// <summary>
        /// Gets the right tool strip panel.
        /// </summary>
        /// <value>The right tool strip panel.</value>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]/// 
        public ToolStripPanel RightToolStripPanel
        {
            get { return rightToolStripPanel; }
        }

        /// <summary>
        /// Gets the bottom tool strip panel.
        /// </summary>
        /// <value>The bottom tool strip panel.</value>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ToolStripPanel BottomToolStripPanel
        {
            get { return bottomToolStripPanel; }
        }

        [Editor(typeof(UIActionCollectionEditor), typeof(UITypeEditor)),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public UIActionCollection Actions
        {
            get { return actionsManager.Actions; }
        }

        [Editor(typeof(ToolStripCollectionEditor), typeof(UITypeEditor)),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ToolStripItemCollection MenuItems
        {
            get { return menuStrip.Items; }
        }

        /// <summary>
        /// Gets the workspace (the docking manager).
        /// </summary>
        /// <value>The workspace.</value>
        protected DockPanel Workspace
        {
            get { return workspace; }
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Raises the <see cref="E:Layout"/> event.
        /// </summary>
        /// <param name="levent">The <see cref="System.Windows.Forms.LayoutEventArgs"/> instance containing the event data.</param>
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
        /// Raises the <see cref="E:System.Windows.Forms.Form.Load"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            layoutService = This.GetService<ILayoutService>();
            workspace.ActiveDocumentChanged += (s, ev) => OnActiveDocumentChanged();
            RegisterForm(); // registers this form for layout serialization

            base.OnLoad(e);
            if (DesignMode) return;

            InitializeStatusStrip();
            CreateToolWindows();

            InitializeWorkspace(workspace);
            // It is very important the DockPanel be added to its parent before adding tool windows;
            // otherwise, floating windows might be hidden by the parent form!
            //tstripContainer.ContentPanel.Controls.Add(workspace);

            InitializeDocking();
            CreateToolWindowMenus();

        }

        #endregion

        #region Layout, Docking, Documents and Tool Windows Management

        /// <summary>
        /// Gets this form id.
        /// </summary>
        /// <remarks>
        /// The id is used to store this form's bounds and state in the layout settings file.
        /// </remarks>
        /// <value>This form id.</value>
        protected virtual string FormId { get { return "Chrome"; } }

        /// <summary>
        /// Gets the layout service.
        /// </summary>
        /// <value>The layout service.</value>
        protected ILayoutService LayoutService
        {
            get { return layoutService; }
        }

        /// <summary>
        /// Registers this form with the layout service.
        /// </summary>
        protected virtual void RegisterForm()
        {
            if (LayoutService == null) return;
            LayoutService.RegisterForm(FormId, this, workspace);
        }

        #region IAdditionalLayoutDataSource Members

        /// <summary>
        /// This method is called by a <see cref="T:Delta.CertXplorer.ApplicationModel.ILayoutService"/> when the form is closed. It must
        /// return the layout data that will be serialized.
        /// </summary>
        /// <remarks>
        /// When overriden, allows defining custom layout data to be saved on form close.
        /// </remarks>
        /// <returns>The layout data to serialize.</returns>
        public virtual string GetAdditionalLayoutData()
        {
            return string.Empty;
        }

        /// <summary>
        /// This method is called by a <see cref="T:Delta.CertXplorer.ApplicationModel.ILayoutService"/> when the form is created. Its
        /// previously serialized layout data is provided in the <paramref name="data"/> parameter.
        /// </summary>
        /// <remarks>
        /// When overriden, gives previously saved custom layout data (on form load).
        /// </remarks>
        /// <param name="data">The previously serialized layout data.</param>
        public virtual void SetAdditionalLayoutData(string data)
        {
            var readData = data;
        }

        #endregion

        /// <summary>
        /// Registers a tool window that has just been created with the Layout service.
        /// </summary>
        /// <param name="windowInfo">The tool window info.</param>
        protected virtual void RegisterToolWindow(ToolWindowInfo windowInfo)
        {
            CheckToolWindow(windowInfo);

            if (LayoutService != null)
                LayoutService.RegisterToolWindow(FormId, windowInfo.ToolWindow);
            toolWindowInfos.Add(windowInfo.ToolWindow.Guid, windowInfo);

            OnToolWindowInfoRegistered(windowInfo);
        }

        /// <summary>
        /// Restores the docking state.
        /// </summary>
        /// <returns><c>true</c> if the deserialization was successful; otherwise, <c>false</c>.</returns>
        protected virtual bool RestoreDockingState()
        {
            if (LayoutService == null) return false;
            return LayoutService.RestoreDockingState(FormId);
        }

        protected void ShowToolWindow(ToolWindowInfo windowInfo)
        {
            ShowToolWindow(windowInfo, false);
        }

        protected void ShowToolWindow(ToolWindowInfo windowInfo, bool dockDefault)
        {
            CheckToolWindow(windowInfo);
            windowInfo.ToolWindow.Show(workspace);
            if (dockDefault) windowInfo.ToolWindow.DockDefault();
        }

        protected void HideToolWindow(ToolWindowInfo windowInfo)
        {
            CheckToolWindow(windowInfo);
            windowInfo.ToolWindow.Hide();
        }

        protected virtual bool IsToolWindowEnabled(ToolWindowInfo windowInfo)
        {
            if (windowInfo == null) return false;
            return windowInfo.IsEnabled;
        }

        protected virtual void CreateToolWindows()
        {
            //logWindowInfo = new LogWindowInfo(); // specific creation
            //logWindowInfo.Window = new LogWindow();
            //RegisterToolWindow(logWindowInfo);

            //selectorWindowInfo = CreateToolWindowInfo<ModulesSelectorWindow>();
            //selectorWindowInfo.Window = new ModulesSelectorWindow();
            //RegisterToolWindow(selectorWindowInfo);
        }

        protected ToolWindowInfo<T> CreateToolWindowInfo<T>() where T : ToolWindow
        {
            return CreateToolWindowInfo<T>(true);
        }

        protected virtual ToolWindowInfo<T> CreateToolWindowInfo<T>(bool enabled) where T : ToolWindow
        {
            return new ToolWindowInfo<T>(enabled);
        }

        /// <summary>
        /// Called when a tool window info is registered.
        /// </summary>
        /// <param name="windowInfo">The window info.</param>
        protected virtual void OnToolWindowInfoRegistered(ToolWindowInfo windowInfo)
        {
        }

        protected virtual void InitializeDocking()
        {
            if (RestoreDockingState())
            {
                foreach (var info in toolWindowInfos.Values)
                {
                    if (!IsToolWindowEnabled(info)) HideToolWindow(info);
                }
            }
            else foreach (var info in toolWindowInfos.Values)
                {
                    var toolWindow = info.ToolWindow;
                    if (IsToolWindowEnabled(info)) ShowToolWindow(info, true);
                    else HideToolWindow(info);
                }
        }

        private void CreateToolWindowMenus()
        {
            foreach (var info in toolWindowInfos.Values)
                CreateToolWindowMenu(info);
        }

        protected virtual void CreateToolWindowMenu(ToolWindowInfo windowInfo)
        {
            if (windowInfo.ToolWindow == null) return;

            ToolStripMenuItem menu = new ToolStripMenuItem(
                windowInfo.MenuText,
                windowInfo.MenuImage);

            windowInfo.ToolWindow.DockStateChanged += (s, e) =>
                menu.Checked = (windowInfo.ToolWindow.DockState != DockState.Hidden);
            menu.Checked = (windowInfo.ToolWindow.DockState != DockState.Hidden);
            menu.Visible = windowInfo.IsEnabled;
            menu.Tag = windowInfo;
            windowInfo.EnabledChanged += (s, e) =>
            {
                menu.Visible = windowInfo.IsEnabled;
                UpdateViewMenu();
            };

            menu.Click += (s, e) => ShowToolWindow(windowInfo);

            if (ViewMenuItem != null)
            {
                ViewMenuItem.DropDownItems.Add(menu);
                UpdateViewMenu();
            }
        }

        private void UpdateViewMenu()
        {
            if (ViewMenuItem == null) return;

            ViewMenuItem.Visible = ViewMenuItem
                .DropDownItems
                .Cast<ToolStripMenuItem>()
                .Where(item =>
                {
                    var windowInfo = item.Tag as ToolWindowInfo;
                    if ((windowInfo != null) && windowInfo.IsEnabled)
                        return true;
                    else return false;
                })
                .Count() > 0;
        }

        protected virtual void InitializeWorkspace(DockPanel workspace)
        {
            workspace.ShowDocumentIcon = true;
            workspace.ActiveAutoHideContent = null;
            workspace.Dock = DockStyle.Fill;
            //workspace.DocumentStyle = DocumentStyle.DockingWindow;
            workspace.Name = "workspace";
        }

        private void CheckToolWindow(ToolWindowInfo windowInfo)
        {
            if (windowInfo == null)
                throw new ArgumentNullException("windowInfo");
            if (windowInfo.ToolWindow == null)
                throw new ArgumentNullException("windowInfo.ToolWindow");
        }

        protected virtual void InitializeStatusStrip()
        {
        }

        /// <summary>
        /// Called when the currently active document has changed.
        /// </summary>
        protected virtual void OnActiveDocumentChanged() { }
        
        #endregion

        #region Actions management

        /// <summary>
        /// Gets the action.
        /// </summary>
        /// <param name="extendee">The extendee.</param>
        /// <returns></returns>
        [DefaultValue(null)]
        public UIAction GetAction(Component extendee)
        {
            return actionsManager.GetAction(extendee);
        }

        /// <summary>
        /// Sets the action.
        /// </summary>
        /// <param name="extendee">The extendee.</param>
        /// <param name="action">The action.</param>
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

        #endregion

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

        #region Helpers

        /// <summary>
        /// Fixes the child index of the toolstrip panels.
        /// </summary>
        protected void FixToolStripPanelsChildIndex()
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

            var index = 0;
            var controls = new Control[]
            {
                workspace,
                rightToolStripPanel,
                leftToolStripPanel,
                topToolStripPanel,
                bottomToolStripPanel,                
            };

            while (index < controls.Length)
            {
                Controls.SetChildIndex(controls[index], index);
                index++;
            }
        }

        #endregion
    }
}
