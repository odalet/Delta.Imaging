using System;
using System.Drawing;
using System.Collections;
using System.Globalization;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Windows.Forms.Design.Behavior;
using System.ComponentModel;
using System.ComponentModel.Design;

using Delta.CertXplorer.Internals;
using Delta.CertXplorer.ComponentModel;


namespace Delta.CertXplorer.ApplicationModel.Design
{
    internal class BaseDockingMainFormDesigner : DocumentDesigner
    {
        private const int DEFAULTFORMPADDING = 9;

        private static string[] preFilteredProperties = new string[] 
        { 
            "Opacity", 
            "Menu", 
            "IsMdiContainer", 
            "Size", 
            "ShowInTaskBar", 
            "WindowState", 
            "AutoSize", 
            "AcceptButton", 
            "CancelButton" 
        };

        private BaseDockingMainForm baseDockingMainForm = null;
        private DesignerVerbCollection verbs = null;

        private ToolStripPanel topToolStripPanel = null;
        private ToolStripPanel leftToolStripPanel = null;
        private ToolStripPanel rightToolStripPanel = null;
        private ToolStripPanel bottomToolStripPanel = null;

        private Size autoScaleBaseSize = Size.Empty;
        private bool autoSize = false;
        private bool hasMenu = false;
        private int heightDelta = 0;
        private bool inAutoscale = false;
        private InheritanceAttribute inheritanceAttribute = null;
        private bool initializing = false;
        private bool isMenuInherited = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseDockingMainFormDesigner"/> class.
        /// </summary>
        public BaseDockingMainFormDesigner() : base() { }

        /// <summary>
        /// Gets the design-time verbs supported by the component that is associated with the designer.
        /// </summary>
        /// <value></value>
        /// <returns>
        /// A <see cref="T:System.ComponentModel.Design.DesignerVerbCollection"/> 
        /// of <see cref="T:System.ComponentModel.Design.DesignerVerb"/> objects, or null 
        /// if no designer verbs are available. This default implementation always returns null.
        /// </returns>
        public override DesignerVerbCollection Verbs
        {
            get
            {
                if (verbs == null)
                {                    
                    verbs = new DesignerVerbCollection();
                    verbs.AddRange(new DesignerVerb[] 
                    {
                        new DesignerVerb("Expand/Collapse Top Panel", (s, e) => ProcessToolStripPanelVerb(topToolStripPanel)),
                        new DesignerVerb("Expand/Collapse Left Panel", (s, e) => ProcessToolStripPanelVerb(leftToolStripPanel)),
                        new DesignerVerb("Expand/Collapse Right Panel", (s, e) => ProcessToolStripPanelVerb(rightToolStripPanel)),
                        new DesignerVerb("Expand/Collapse Bottom Panel", (s, e) => ProcessToolStripPanelVerb(bottomToolStripPanel))
                    });
                }

                return verbs;
            }
        }

        #region Properties

        public override IList SnapLines
        {
            get
            {
                ArrayList snapLines = null;
                base.AddPaddingSnapLines(ref snapLines);
                if (snapLines == null) snapLines = new ArrayList(4);

                if ((Control.Padding == Padding.Empty) && (snapLines != null))
                {
                    int count = 0;
                    for (int i = 0; i < snapLines.Count; i++)
                    {
                        SnapLine line = snapLines[i] as SnapLine;
                        if ((line != null) && (line.Filter != null) && line.Filter.StartsWith("Padding"))
                        {
                            if (line.Filter.Equals("Padding.Left") || line.Filter.Equals("Padding.Top"))
                            {
                                line.AdjustOffset(DEFAULTFORMPADDING);
                                count++;
                            }

                            if (line.Filter.Equals("Padding.Right") || line.Filter.Equals("Padding.Bottom"))
                            {
                                line.AdjustOffset(-DEFAULTFORMPADDING);
                                count++;
                            }

                            if (count == 4) return snapLines;
                        }
                    }
                }

                return snapLines;
            }
        }

        private IButtonControl AcceptButton
        {
            get { return (base.ShadowProperties["AcceptButton"] as IButtonControl); }
            set
            {
                ((Form)base.Component).AcceptButton = value;
                base.ShadowProperties["AcceptButton"] = value;
            }
        }

        private IButtonControl CancelButton
        {
            get { return (base.ShadowProperties["CancelButton"] as IButtonControl); }
            set
            {
                ((Form)base.Component).CancelButton = value;
                base.ShadowProperties["CancelButton"] = value;
            }
        }

        private MainMenu Menu
        {
            get { return (MainMenu)base.ShadowProperties["Menu"]; }
            set
            {
                if (value != base.ShadowProperties["Menu"])
                {
                    base.ShadowProperties["Menu"] = value;
                    
                    var service = GetService<IDesignerHost>();
                    if ((service != null) && !service.Loading)
                    {
                        base.EnsureMenuEditorService(value);
                        if (base.menuEditorService != null)
                            base.menuEditorService.SetMenu(value);
                    }
                    if (heightDelta == 0) heightDelta = GetMenuHeight();
                }
            }
        }

        private bool IsMenuInherited
        {
            get
            {
                if ((inheritanceAttribute == null) && (Menu != null))
                {
                    inheritanceAttribute = (InheritanceAttribute)
                        TypeDescriptor.GetAttributes(Menu)[typeof(InheritanceAttribute)];
                    
                    if (inheritanceAttribute.Equals(InheritanceAttribute.NotInherited))
                        isMenuInherited = false;
                    else isMenuInherited = true;
                }
                return isMenuInherited;
            }
        }

        private Size ClientSize
        {
            get
            {
                Size clientSize = new Size(-1, -1);
                if (initializing) return clientSize;
                
                Form component = base.Component as Form;
                if (component != null)
                {
                    clientSize = component.ClientSize;
                    if (component.HorizontalScroll.Visible)
                        clientSize.Height += SystemInformation.HorizontalScrollBarHeight;
                    if (component.VerticalScroll.Visible)
                        clientSize.Width += SystemInformation.VerticalScrollBarWidth;
                }
                return clientSize;
            }
            set
            {
                var service = GetService<IDesignerHost>();
                if ((service != null) && service.Loading)
                    heightDelta = GetMenuHeight();

                ((Form)base.Component).ClientSize = value;
            }
        }

        private FormWindowState WindowState
        {
            get { return (FormWindowState)base.ShadowProperties["WindowState"]; }
            set { base.ShadowProperties["WindowState"] = value; }
        }

        private bool AutoSize
        {
            get { return autoSize; }
            set { autoSize = value; }
        }

        private bool IsMdiContainer
        {
            get { return ((Form)Control).IsMdiContainer; }
            set
            {
                if (!value) base.UnhookChildControls(Control);

                ((Form)Control).IsMdiContainer = value;
                if (value) base.HookChildControls(Control);
            }
        }

        private double Opacity
        {
            get { return (double)base.ShadowProperties["Opacity"]; }
            set
            {
                if ((value < 0.0) || (value > 1.0))
                {
                    object[] args = new object[] 
                    { 
                        "value", 
                        value.ToString(CultureInfo.CurrentCulture), 
                        0f.ToString(CultureInfo.CurrentCulture), 
                        1f.ToString(CultureInfo.CurrentCulture) };
                    throw new ArgumentException(string.Format(
                        "Invalid Bound Argument: {0}; actual value is {1}; and should be > {2} and < {3}.", args),
                        "value");
                }
                base.ShadowProperties["Opacity"] = value;
            }
        }

        private bool ShowInTaskbar
        {
            get { return (bool)base.ShadowProperties["ShowInTaskbar"]; }
            set { base.ShadowProperties["ShowInTaskbar"] = value; }
        }

        private Size Size
        {
            get { return base.Control.Size; }
            set
            {
                var service = GetService<IComponentChangeService>();
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(base.Component);
                if (service != null)
                    service.OnComponentChanging(base.Component, properties["ClientSize"]);

                Control.Size = value;
                if (service != null)
                    service.OnComponentChanged(base.Component, properties["ClientSize"], null, null);
            }
        }

        private Size AutoScaleBaseSize
        {
            get
            {                
                // This is obsolete:
                //SizeF autoScaleSize = Form.GetAutoScaleSize(((Form)base.Component).Font);
                SizeF autoScaleSize = ((Form)base.Component).AutoScaleDimensions;
                return new Size(
                    (int)Math.Round((double)autoScaleSize.Width), 
                    (int)Math.Round((double)autoScaleSize.Height));
            }
            set
            {
                autoScaleBaseSize = value;
                base.ShadowProperties["AutoScaleBaseSize"] = value;
            }
        }

        #endregion
        
        public override void Initialize(IComponent component)
        {
            PropertyDescriptor descriptor = TypeDescriptor.GetProperties(
                component.GetType())["WindowState"];
            if ((descriptor != null) && 
                (descriptor.PropertyType == typeof(FormWindowState)))
                WindowState = (FormWindowState)descriptor.GetValue(component);

            initializing = true;
            base.Initialize(component);
            initializing = false;

            base.AutoResizeHandles = true;
            
            var host = GetService<IDesignerHost>();
            if (host != null)
            {
                host.LoadComplete += new EventHandler(OnLoadComplete);
                host.Activated += new EventHandler(OnDesignerActivate);
                host.Deactivated += new EventHandler(OnDesignerDeactivate);
            }

            Form control = (Form)Control;
            control.WindowState = FormWindowState.Normal;
            base.ShadowProperties["AcceptButton"] = control.AcceptButton;
            base.ShadowProperties["CancelButton"] = control.CancelButton;

            var service = GetService<IComponentChangeService>();
            if (service != null)
            {
                service.ComponentAdded += new ComponentEventHandler(OnComponentAdded);
                service.ComponentRemoved += new ComponentEventHandler(OnComponentRemoved);
            }

            baseDockingMainForm = (BaseDockingMainForm)component;
            topToolStripPanel = baseDockingMainForm.TopToolStripPanel;
            leftToolStripPanel = baseDockingMainForm.LeftToolStripPanel;
            rightToolStripPanel = baseDockingMainForm.RightToolStripPanel;
            bottomToolStripPanel = baseDockingMainForm.BottomToolStripPanel;
        }

        protected override void PreFilterProperties(IDictionary properties)
        {
            PropertyDescriptor descriptor = null;
            base.PreFilterProperties(properties);
            
            Attribute[] attributes = new Attribute[0];
            for (int i = 0; i < preFilteredProperties.Length; i++)
            {
                descriptor = (PropertyDescriptor)properties[preFilteredProperties[i]];
                if (descriptor != null) properties[preFilteredProperties[i]] = TypeDescriptor.CreateProperty(
                    typeof(BaseDockingMainFormDesigner), descriptor, attributes);
            }

            descriptor = (PropertyDescriptor)properties["AutoScaleBaseSize"];
            if (descriptor != null) properties["AutoScaleBaseSize"] = TypeDescriptor.CreateProperty(
                typeof(BaseDockingMainFormDesigner), descriptor, new Attribute[] 
                { 
                    DesignerSerializationVisibilityAttribute.Visible 
                });

            descriptor = (PropertyDescriptor)properties["ClientSize"];
            if (descriptor != null) properties["ClientSize"] = TypeDescriptor.CreateProperty(
                typeof(BaseDockingMainFormDesigner), descriptor, new Attribute[] 
                { 
                    new DefaultValueAttribute(new Size(-1, -1)) 
                });
        }

        protected override void OnCreateHandle()
        {
            if ((Menu != null) && (base.menuEditorService != null))
            {
                base.menuEditorService.SetMenu(null);
                base.menuEditorService.SetMenu(Menu);
            }

            if (heightDelta != 0)
            {
                Form component = (Form)base.Component;
                component.Height += heightDelta;
                heightDelta = 0;
            }
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                var host = GetService<IDesignerHost>();
                if (host != null)
                {
                    host.LoadComplete -= new EventHandler(OnLoadComplete);
                    host.Activated -= new EventHandler(OnDesignerActivate);
                    host.Deactivated -= new EventHandler(OnDesignerDeactivate);
                }

                var service = GetService<IComponentChangeService>();
                if (service != null)
                {
                    service.ComponentAdded -= new ComponentEventHandler(OnComponentAdded);
                    service.ComponentRemoved -= new ComponentEventHandler(OnComponentRemoved);
                }
            }
            base.Dispose(disposing);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == NativeMethods.WM_WINDOWPOSCHANGING)
                WmWindowPosChanging(ref m);

            base.WndProc(ref m);
        }

        private unsafe void WmWindowPosChanging(ref Message m)
        {
            NativeMethods.WINDOWPOS* lParam = (NativeMethods.WINDOWPOS*)m.LParam;
            bool autoScaling = inAutoscale;
            if (!inAutoscale)
            {
                var service = GetService<IDesignerHost>();
                if (service != null) autoScaling = service.Loading;
            }

            if (((autoScaling && (Menu != null)) && ((lParam->flags & 1) == 0)) && (IsMenuInherited || this.inAutoscale))
                heightDelta = GetMenuHeight();
        }

        private int GetMenuHeight()
        {
            if ((Menu == null) || (IsMenuInherited && initializing))
                return 0;

            if (base.menuEditorService != null)
            {
                PropertyDescriptor descriptor = TypeDescriptor.GetProperties(
                    base.menuEditorService)["MenuHeight"];

                if (descriptor != null)
                    return (int)descriptor.GetValue(base.menuEditorService);
            }
            return SystemInformation.MenuHeight;
        }

        private void ApplyAutoScaling(SizeF baseVar, Form form)
        {
            if (!baseVar.IsEmpty)
            {
                // This is obsolete:
                //SizeF autoScaleSize = Form.GetAutoScaleSize(form.Font);
                SizeF autoScaleSize = form.AutoScaleDimensions;                
                Size size = new Size((int)Math.Round((double)autoScaleSize.Width), (int)Math.Round((double)autoScaleSize.Height));
                if (!baseVar.Equals(size))
                {
                    var ratio = new SizeF(
                        ((float)size.Height) / baseVar.Height,
                        ((float)size.Width) / baseVar.Width);
                    try
                    {
                        inAutoscale = true;
                        form.Scale(ratio);
                    }
                    finally { inAutoscale = false; }
                }
            }
        }

        private bool ShouldSerializeAutoScaleBaseSize()
        {
            if (initializing) return false;

            // This is obsolete:
            //bool autoScale = ((Form)base.Component).AutoScale;
            bool autoScale = ((ContainerControl)base.Component).AutoScaleMode != AutoScaleMode.None;
            return autoScale && base.ShadowProperties.Contains("AutoScaleBaseSize");
        }
        
        #region Event Handlers

        private void OnComponentAdded(object source, ComponentEventArgs ce)
        {
            var service = GetService<IDesignerHost>();
            if (service == null) return;

            if (ce.Component is Menu)
            {
                if ((service != null) && !service.Loading && (ce.Component is MainMenu) && !hasMenu)
                {
                    TypeDescriptor.GetProperties(base.Component)["Menu"].SetValue(
                        base.Component, ce.Component);
                    hasMenu = true;
                }
            }
            //else
            //{
            //    PropertyDescriptor pd = TypeDescriptor.
            //    TypeDescriptor.
            //}
        }

        private void OnComponentRemoved(object source, ComponentEventArgs ce)
        {
            if (ce.Component is Menu)
            {
                if (ce.Component == Menu)
                {
                    TypeDescriptor.GetProperties(base.Component)["Menu"].SetValue(
                        base.Component, null);
                    hasMenu = false;
                }
                else if ((base.menuEditorService != null) && (ce.Component == base.menuEditorService.GetMenu()))
                    base.menuEditorService.SetMenu(Menu);
            }
            
            if (ce.Component is IButtonControl)
            {
                if (ce.Component == base.ShadowProperties["AcceptButton"])
                    AcceptButton = null;

                if (ce.Component == base.ShadowProperties["CancelButton"])
                    CancelButton = null;
            }
        }
         
        private void OnLoadComplete(object source, EventArgs evevent)
        {
            Form form = base.Control as Form;
            if (form != null)
            {
                int width = form.ClientSize.Width;
                int height = form.ClientSize.Height;
                if (form.HorizontalScroll.Visible && form.AutoScroll)
                    height += SystemInformation.HorizontalScrollBarHeight;
                if (form.VerticalScroll.Visible && form.AutoScroll)
                    width += SystemInformation.VerticalScrollBarWidth;

                ApplyAutoScaling((SizeF)this.autoScaleBaseSize, form);
                ClientSize = new Size(width, height);

                var service = GetService<BehaviorService>();
                if (service != null) service.SyncSelection();

                if (heightDelta == 0) heightDelta = GetMenuHeight();
                if (heightDelta != 0)
                {
                    form.Height += heightDelta;
                    heightDelta = 0;
                }

                if (!form.ControlBox && 
                    !form.ShowInTaskbar && 
                    !string.IsNullOrEmpty(form.Text) && 
                    (Menu != null) && 
                    !IsMenuInherited) 
                    form.Height += SystemInformation.CaptionHeight + 1;

                form.PerformLayout();
            }
        }

        private void OnDesignerActivate(object source, EventArgs evevent)
        {
            Control control = base.Control;
            if ((control != null) && control.IsHandleCreated)
            {
                NativeMethods.SendMessage(control.Handle, NativeMethods.WM_NCACTIVATE, 1, 0);
                NativeMethods.RedrawWindow(control.Handle, null, IntPtr.Zero, NativeMethods.RDW_FRAME);
            }
        }

        private void OnDesignerDeactivate(object sender, EventArgs e)
        {
            Control control = base.Control;
            if ((control != null) && control.IsHandleCreated)
            {
                NativeMethods.SendMessage(control.Handle, NativeMethods.WM_NCACTIVATE, 0, 0);
                NativeMethods.RedrawWindow(control.Handle, null, IntPtr.Zero, NativeMethods.RDW_FRAME);
            }
        }

        #endregion

        #region ToolStrip Panel Verbs Handlers

        private void ProcessToolStripPanelVerb(ToolStripPanel panel)
        {
            if (panel.Padding != Padding.Empty) CollapsePanel(panel);
            else ExpandPanel(panel, false);
        }
                
        private void CollapsePanel(ToolStripPanel panel)
        {
            panel.Padding = Padding.Empty;
        }

        private void ExpandPanel(ToolStripPanel panel, bool select)
        {
            const int panelHeight = 25;
            switch (panel.Dock)
            {
                case DockStyle.Top:
                    panel.Padding = new Padding(0, 0, 0, panelHeight);
                    break;

                case DockStyle.Bottom:
                    panel.Padding = new Padding(0, panelHeight, 0, 0);
                    break;

                case DockStyle.Left:
                    panel.Padding = new Padding(0, 0, panelHeight, 0);
                    break;

                case DockStyle.Right:
                    panel.Padding = new Padding(panelHeight, 0, 0, 0);
                    break;
            }

            if (select)
            {
                ISelectionService service = GetService<ISelectionService>();
                if (service != null)
                    service.SetSelectedComponents(new object[] { panel }, SelectionTypes.Replace);
            }
        }

        #endregion

        #region Service helpers

        protected T GetService<T>() where T : class 
        {
            return base.GetService(typeof(T)) as T;
        }

        protected T GetService<T>(bool mandatory) where T : class 
        {
            T t = GetService<T>();
            if (t == null)
            {
                if (mandatory) throw new ServiceNotFoundException<T>();
                else return null;
            }

            return t;
        }

        #endregion
    }
}