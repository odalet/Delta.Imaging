/* 
 * Grabbed from Marco De Sanctis' Actions
 * see http://blogs.ugidotnet.org/crad/articles/38329.aspx
 * Original namespace: Crad.Windows.Forms.Actions
 * License: Common Public License Version 1.0
 * 
 */ 

using System;
using System.Diagnostics;
using System.Reflection;
using System.ComponentModel;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace Delta.CertXplorer.UI.Actions
{
    [ToolboxBitmap(typeof(UIAction), "UIAction.bmp"), DefaultEvent("Run"), StandardAction]
    public class UIAction: Component
    {
        protected enum ActionWorkingState : byte { Listening, Driving }

        private ActionWorkingState workingState = ActionWorkingState.Listening;
        private UIActionsManager actionList = null;
        private List<Component> targets = null;
        
        private CheckState checkState = CheckState.Unchecked;
        private bool enabled = true;        
        private bool checkOnClick = false;
        private Keys shortcutKeys = Keys.None;
        private bool visible = true;        
        private bool interceptingCheckStateChanged = false;

        private EventHandler clickEventHandler;
        private EventHandler checkStateChangedEventHandler;

        public UIAction()
        {
            targets = new List<Component>();                       

            clickEventHandler = new EventHandler(target_Click);
            checkStateChangedEventHandler = new EventHandler(target_CheckStateChanged);
        }

        #region Définition des événements

        /// <summary>
        /// Occurs when this action's check state has changed.
        /// </summary>
        public event EventHandler CheckStateChanged;

        public event CancelEventHandler BeforeRun;
        public event EventHandler Run;
        public event EventHandler AfterRun;
        public event EventHandler Update;

        #endregion

        public void DoUpdate() { OnUpdate(EventArgs.Empty); }

        #region Propriétés
        
        [DefaultValue(false)]
        public bool Checked
        {
            get { return (this.checkState != CheckState.Unchecked); }
            set
            {
                if (value != Checked)
                    CheckState = value ? CheckState.Checked : CheckState.Unchecked;
            }
        }
        
        [DefaultValue(CheckState.Unchecked), UpdatableProperty]
        public CheckState CheckState
        {
            get { return checkState; }
            set
            {
                if (checkState != value)
                {
                    checkState = value;
                    UpdateAllTargets("CheckState", value);
                    if (CheckStateChanged != null) CheckStateChanged(this, EventArgs.Empty);
                }
            }
        }
        
        [DefaultValue(true), UpdatableProperty]
        public bool Enabled
        {
            get
            {
                if (ActionList != null) return enabled && ActionList.Enabled;
                else return enabled;
            }
            set
            {
                if (enabled != value)
                {
                    enabled = value;
                    UpdateAllTargets("Enabled", value);
                }
            }
        }
                
        [DefaultValue(false), UpdatablePropertyAttribute]
        public bool CheckOnClick
        {
            get { return checkOnClick; }
            set
            {
                if (checkOnClick != value)
                {
                    checkOnClick = value;
                    UpdateAllTargets("CheckOnClick", value);
                }
            }
        }

        [DefaultValue(Keys.None), UpdatablePropertyAttribute, Localizable(true)]
        public Keys ShortcutKeys
        {
            get { return shortcutKeys; }
            set
            {
                if (shortcutKeys != value)
                {
                    shortcutKeys = value;
                    KeysConverter kc = new KeysConverter();
                    string s = (string)kc.ConvertTo(value, typeof(string));
                    UpdateAllTargets("ShortcutKeyDisplayString", s);
                }
            }
        }
        
        [DefaultValue(true), UpdatablePropertyAttribute]
        public bool Visible
        {
            get { return visible; }
            set
            {
                if (visible != value)
                {
                    visible = value;
                    UpdateAllTargets("Visible", value);
                }
            }
        }

        protected ActionWorkingState WorkingState { get { return workingState; } set { workingState = value; } }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        protected internal UIActionsManager ActionList
        {
            get { return actionList; } set { if (actionList != value) actionList = value; }
        }

        internal bool InterceptingCheckStateChanged
        {
            get { return interceptingCheckStateChanged; }
            set { interceptingCheckStateChanged = value; }
        }
        
        #endregion

        #region Méthodes publiques

        public void DoRun()
        {
            if (!enabled) return;

            CancelEventArgs e = new CancelEventArgs();
            OnBeforeRun(e);
            if (e.Cancel) return;

            OnRun(EventArgs.Empty);
            OnAfterRun(EventArgs.Empty);
        }

        #endregion

        #region Méthodes virtuelles

        protected virtual void OnBeforeRun(CancelEventArgs e)
        {
            if (BeforeRun != null) BeforeRun(this, e);
        }
                
        protected virtual void OnRun(EventArgs e)
        {
            if (Run != null) Run(this, e);
        }
                
        protected virtual void OnAfterRun(EventArgs e)
        {
            if (AfterRun != null) AfterRun(this, e);
        }
        
        protected virtual void OnUpdate(EventArgs e)
        {
            if (Update != null) Update(this, e);
        }

        protected virtual void OnRemovingTarget(Component extendee) {}

        protected virtual void OnAddingTarget(Component extendee) {}

        protected virtual void AddHandler(Component extendee)
        {
            EventInfo clickEvent = extendee.GetType().GetEvent("Click");
            if (clickEvent != null)
                clickEvent.AddEventHandler(extendee, clickEventHandler);

            EventInfo checkStateChangedEvent = extendee.GetType().GetEvent("CheckStateChanged");
            if (checkStateChangedEvent != null)
                checkStateChangedEvent.AddEventHandler(extendee, checkStateChangedEventHandler);

            // Cas particulier : bouton de barre d'outils
            ToolBarButton button = extendee as ToolBarButton;
            if (button != null)
                button.Parent.ButtonClick += new ToolBarButtonClickEventHandler(toolbar_ButtonClick);
        }

        protected virtual void RemoveHandler(Component extendee)
        {
            EventInfo clickEvent = extendee.GetType().GetEvent("Click");
            if (clickEvent != null)
                clickEvent.RemoveEventHandler(extendee, clickEventHandler);

            EventInfo checkStateChangedEvent = extendee.GetType().GetEvent("CheckStateChanged");
            if (checkStateChangedEvent != null)
                checkStateChangedEvent.RemoveEventHandler(extendee, checkStateChangedEventHandler);

            // Cas particulier : bouton de barre d'outils
            ToolBarButton button = extendee as ToolBarButton;
            if (button != null)
                button.Parent.ButtonClick -= new ToolBarButtonClickEventHandler(toolbar_ButtonClick);
        }
        
        #endregion

        #region Méthodes internes
        
        internal void InternalRemoveTarget(Component extendee)
        {
            targets.Remove(extendee);
            RemoveHandler(extendee);
            OnRemovingTarget(extendee);            
        }

        internal void InternalAddTarget(Component extendee)
        {
            targets.Add(extendee);
            RefreshState(extendee);
            AddHandler(extendee);
            OnAddingTarget(extendee);
        }

        internal void RefreshEnabledCheckState()
        {
            UpdateAllTargets("Enabled", this.Enabled);
            UpdateAllTargets("CheckState", this.CheckState);
        }

        internal void RunShortcut()
        {
            if (!Enabled) return;
            if (CheckOnClick) Checked = !Checked;
            DoRun();
        }
        
        #endregion

        #region Méthodes protégées

        protected void UpdateAllTargets(string propertyName, object value)
        {
            foreach (Component c in targets)
                UpdateProperty(c, propertyName, value);
        }
        
        #endregion

        #region Méthodes privées

        private void UpdateProperty(Component target, string propertyName, object value)
        {
            workingState = ActionWorkingState.Driving;
            try
            {
                if (ActionList != null) ActionList.TypesDescription[target.GetType()].SetValue(
                            propertyName, target, value);
            }
            finally { workingState = ActionWorkingState.Listening; }
        }

        private void RefreshState(Component target)
        {
            PropertyDescriptorCollection properties =
            TypeDescriptor.GetProperties(this, new Attribute[] { new UpdatablePropertyAttribute() });

            foreach (PropertyDescriptor property in properties)
                UpdateProperty(target, property.Name, property.GetValue(this));
        }

        private void HandleClick(object sender, EventArgs e)
        {
            if (workingState == ActionWorkingState.Listening)
            {
                Component target = sender as Component;
                Debug.Assert(target != null, "Target isn't a component with HandleClick");
                Debug.Assert(targets.Contains(target), "Target non esiste su collection targets su handleClick");

                DoRun();
            }
        }

        private void HandleCheckStateChanged(object sender, EventArgs e)
        {
            if (workingState == ActionWorkingState.Listening)
            {
                Component target = sender as Component;
                CheckState = (CheckState)ActionList.
                    TypesDescription[sender.GetType()].GetValue("CheckState", sender);
            }
        }

        #endregion

        #region Evénements
        
        private void toolbar_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            if (targets.Contains(e.Button)) HandleClick(e.Button, e); // called if sender is ToolBarButton
        }

        private void target_Click(object sender, EventArgs e)
        {
            HandleClick(sender, e); // called if sender is Control
        }        

        private void target_CheckStateChanged(object sender, EventArgs e)
        {
            HandleCheckStateChanged(sender, e);
        }                
        
        #endregion
    }
}
