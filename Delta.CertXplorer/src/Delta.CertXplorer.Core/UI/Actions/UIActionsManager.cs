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
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace Delta.CertXplorer.UI.Actions
{    
    [ProvideProperty("Action", typeof(Component)), ToolboxItemFilter("System.Windows.Forms"),
    ToolboxBitmap(typeof(UIAction), "UIActionsManager.bmp")]
    public class UIActionsManager: Component, IExtenderProvider, ISupportInitialize
    {
        private ContainerControl containerControl = null;
        private Dictionary<Type, UIActionTargetDescriptor> typesDescription = null;
        private Dictionary<Component, UIAction> targets = null;
        private UIActionCollection actions = null;        
        private bool enabled = true;
        private bool initializing = false;

        public UIActionsManager()
        {
            actions = new UIActionCollection(this);
            targets = new Dictionary<Component, UIAction>();
            typesDescription = new Dictionary<Type, UIActionTargetDescriptor>();            
            
            if (!DesignMode) Application.Idle += new EventHandler(Application_Idle);
        }

        public event EventHandler Update;

        #region Properties

        [DefaultValue(true)]
        public bool Enabled
        {
            get { return enabled; }
            set
            {
                if (enabled != value)
                {
                    enabled = value;
                    RefreshActions();
                }
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public UIActionCollection Actions { get { return actions; } }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Dictionary<Type, UIActionTargetDescriptor> TypesDescription { get { return typesDescription; } }

        public ContainerControl ContainerControl
        {
            get { return containerControl; } set { SetContainerControl(value); }
        }

        [Browsable(false)]
        public Control ActiveControl { get { return GetActiveControl(); } }

        public override ISite Site
        {
            get { return base.Site; }
            set
            {
                base.Site = value;
                if (value != null)
                {
                    IDesignerHost host = value.GetService(typeof(IDesignerHost)) as IDesignerHost;
                    if (host != null)
                    {
                        IComponent component = host.RootComponent;
                        if (component is ContainerControl)
                            SetContainerControl((ContainerControl)component);
                    }
                }
            }
        }

        #endregion                
        
        #region Public methods

        [DefaultValue(null)]
        public UIAction GetAction(Component extendee)
        {
            if (targets.ContainsKey(extendee)) return targets[extendee];
            else return null;
        }

        public void SetAction(Component extendee, UIAction action)
        {
            if (!initializing)
            {
                if (extendee == null) throw new ArgumentNullException("extendee");
                if (action != null && action.ActionList != this) 
                    throw new ArgumentException("The Action you selected is owned by another ActionList");
            }

            if (targets.ContainsKey(extendee))
            {
                targets[extendee].InternalRemoveTarget(extendee);
                targets.Remove(extendee);
            }

            if (action != null)
            {
                if (!typesDescription.ContainsKey(extendee.GetType())) typesDescription.Add(
                    extendee.GetType(), new UIActionTargetDescriptor(extendee.GetType()));

                targets.Add(extendee, action);
                action.InternalAddTarget(extendee);
            }
        } 

        #endregion

        #region Méthodes virtuelles
        protected virtual void OnUpdate(EventArgs eventArgs)
        {
            if (Update != null) Update(this, eventArgs);
            foreach (UIAction action in actions) action.DoUpdate();
        }

        protected virtual Type[] GetSupportedTypes()
        {
            return new Type[] {typeof(ButtonBase), typeof(ToolStripButton),
                typeof(ToolStripMenuItem), typeof(ToolBarButton), typeof(MenuItem)};
        }
        #endregion

        #region Méthodes privées

        private void RefreshActions()
        {
            if (!DesignMode)
            {
                foreach (UIAction action in actions) action.RefreshEnabledCheckState();
            }
        }

        private void CheckInternalCollections()
        {
            foreach (UIAction action in targets.Values)
            {
                if (!actions.Contains(action) || (action.ActionList != this))
                    throw new InvalidOperationException(
                        "Action owned by another action list or invalid Action.ActionList");
            }
        }

        private Control GetActiveControl() { return GetActiveControl(containerControl); }
        private Control GetActiveControl(ContainerControl container)
        {
            if (container == null) return null;
            else if (container.ActiveControl is ContainerControl)
                return GetActiveControl((ContainerControl)container.ActiveControl);
            else return container.ActiveControl;
        }

        private void SetContainerControl(ContainerControl container)
        {
            if (containerControl != container)
            {
                containerControl = container;
                if (!DesignMode)
                {
                    Form f = containerControl as Form;
                    if (f != null)
                    {
                        f.KeyPreview = true;
                        f.KeyDown += new KeyEventHandler(form_KeyDown);
                    }
                }
            }
        }

        #endregion

        #region Evénements

        private void Application_Idle(object sender, EventArgs e) { OnUpdate(EventArgs.Empty); }

        private void form_KeyDown(object sender, KeyEventArgs e)
        {
            foreach (UIAction action in actions)
            {
                if (action.ShortcutKeys == (Keys)e.KeyData)
                    action.RunShortcut();
            }
        }

        #endregion

        #region IExtenderProvider Members

        bool IExtenderProvider.CanExtend(object extendee)
        {
            Type targetType = extendee.GetType();

            foreach (Type t in GetSupportedTypes())
            {
                if (t.IsAssignableFrom(targetType)) return true;
            }

            return false;
        }

        
        #endregion

        #region ISupportInitialize Members
        
        public void BeginInit() { initializing = true; }

        public void EndInit() 
        {
            initializing = false;
            CheckInternalCollections();
            RefreshActions();
        }        

        #endregion
    }
}
