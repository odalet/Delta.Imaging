using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Collections;
using Delta.CertXplorer.UI.ToolboxModel;

namespace Delta.CertXplorer.UI.ToolboxImplementation
{
    internal class ToolboxServiceProxy : IToolboxService
    {
        private ISimpleToolboxService toolbox = null;

        public ToolboxServiceProxy(ISimpleToolboxService simpleToolboxService)
        {
            if (simpleToolboxService == null) throw new ArgumentNullException("simpleToolboxService");
            toolbox = simpleToolboxService;
        }

        #region IToolboxService Members

        public void AddCreator(ToolboxItemCreatorCallback creator, string format, IDesignerHost host) { toolbox.AddCreator(creator, format, host); }

        public void AddCreator(ToolboxItemCreatorCallback creator, string format) { AddCreator(creator, format, toolbox.GetDesignerHost()); }

        public void AddLinkedToolboxItem(ToolboxItem toolboxItem, string category, IDesignerHost host) { toolbox.AddLinkedToolboxItem(toolboxItem, category, host); }

        public void AddLinkedToolboxItem(ToolboxItem toolboxItem, IDesignerHost host) { AddLinkedToolboxItem(toolboxItem, toolbox.GetSelectedCategory(), host); }

        public void AddToolboxItem(ToolboxItem toolboxItem, string category) { toolbox.AddToolboxItem(toolboxItem, category); }

        public void AddToolboxItem(ToolboxItem toolboxItem) { AddToolboxItem(toolboxItem, toolbox.GetSelectedCategory()); }

        public CategoryNameCollection CategoryNames { get { return toolbox.CategoryNames; } }

        public ToolboxItem DeserializeToolboxItem(object serializedObject, IDesignerHost host) { return toolbox.DeserializeToolboxItem(serializedObject, host); }

        public ToolboxItem DeserializeToolboxItem(object serializedObject) { return DeserializeToolboxItem(serializedObject, toolbox.GetDesignerHost()); }

        public ToolboxItem GetSelectedToolboxItem(IDesignerHost host) { return toolbox.GetSelectedToolboxItem(host); } 

        public ToolboxItem GetSelectedToolboxItem() { return GetSelectedToolboxItem(toolbox.GetDesignerHost()); }

        public ToolboxItemCollection GetToolboxItems(string category, IDesignerHost host) { return toolbox.GetToolboxItems(category, host); }

        public ToolboxItemCollection GetToolboxItems(string category) { return GetToolboxItems(category, toolbox.GetDesignerHost()); }

        public ToolboxItemCollection GetToolboxItems(IDesignerHost host) { return GetToolboxItems(toolbox.GetSelectedCategory(), host); }

        public ToolboxItemCollection GetToolboxItems() { return GetToolboxItems(toolbox.GetSelectedCategory(), toolbox.GetDesignerHost()); }

        public bool IsSupported(object serializedObject, ICollection filterAttributes) { return toolbox.IsSupported(serializedObject, filterAttributes); }

        public bool IsSupported(object serializedObject, IDesignerHost host) { return toolbox.IsSupported(serializedObject, host); }

        public bool IsToolboxItem(object serializedObject, IDesignerHost host) { return toolbox.IsToolboxItem(serializedObject, host); }

        public bool IsToolboxItem(object serializedObject) { return IsToolboxItem(serializedObject, toolbox.GetDesignerHost()); }

        public void Refresh() { toolbox.Refresh(); }

        public void RemoveCreator(string format, IDesignerHost host) { toolbox.RemoveCreator(format, host); }

        public void RemoveCreator(string format) { RemoveCreator(format, toolbox.GetDesignerHost()); }

        public void RemoveToolboxItem(ToolboxItem toolboxItem, string category) { toolbox.RemoveToolboxItem(toolboxItem, category); }

        public void RemoveToolboxItem(ToolboxItem toolboxItem) { RemoveToolboxItem(toolboxItem, toolbox.GetSelectedCategory()); }

        public string SelectedCategory { get { return toolbox.GetSelectedCategory(); } set { toolbox.SetSelectedCategory(value); } }

        public void SelectedToolboxItemUsed() { toolbox.SelectedToolboxItemUsed(); }

        public object SerializeToolboxItem(ToolboxItem toolboxItem) { return toolbox.SerializeToolboxItem(toolboxItem); }

        public bool SetCursor() { return toolbox.SetCursor(); }

        public void SetSelectedToolboxItem(ToolboxItem toolboxItem) { toolbox.SetSelectedToolboxItem(toolboxItem); }

        #endregion        
    }
}
