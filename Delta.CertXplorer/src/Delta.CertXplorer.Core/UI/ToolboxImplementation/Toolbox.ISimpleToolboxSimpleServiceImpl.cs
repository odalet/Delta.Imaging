using System;
using System.Collections;
using System.Drawing.Design;
using System.ComponentModel.Design;
using System.Windows.Forms;

using Delta.CertXplorer.UI.ToolboxModel;

namespace Delta.CertXplorer.UI.ToolboxImplementation
{
    partial class Toolbox : ISimpleToolboxService
    {
        private const string DEFAULT_CATEGORY = "General";

        #region ISimpleToolboxService Members

        public CategoryNameCollection CategoryNames { get { return new CategoryNameCollection(new string[] { DEFAULT_CATEGORY }); } }

        public void AddCreator(ToolboxItemCreatorCallback creator, string format, IDesignerHost host) { }

        public void AddLinkedToolboxItem(ToolboxItem toolboxItem, string category, IDesignerHost host) { }

        public void AddToolboxItem(ToolboxItem toolboxItem, string category) { }

        public ToolboxItem DeserializeToolboxItem(object serializedObject, IDesignerHost host)
        {
            DataObject dataObject = serializedObject as DataObject;
            if (dataObject == null) return null;

            ToolboxTool tool = dataObject.GetData(typeof(ToolboxTool)) as ToolboxTool;
            if (tool == null)
            {
                ToolboxItem item = dataObject.GetData(typeof(ToolboxItem)) as ToolboxItem;
                return item;
            }
            else return tool;
        }

        public ToolboxItem GetSelectedToolboxItem(IDesignerHost host)
        {
            ToolboxButton button = SelectedButton;
            if (button != null)
            {
                ToolboxTool tool = button.Tool;
                if (tool is ToolboxPointer) return null;
                else return tool;
            }
            else return null;
        }

        public ToolboxItemCollection GetToolboxItems(string category, IDesignerHost host)
        {
            //TODO: renvoyer une collection de toolbox items
            return null;
        }

        public bool IsSupported(object serializedObject, ICollection filterAttributes) { return false; }

        public bool IsSupported(object serializedObject, IDesignerHost host) { return false; }

        public bool IsToolboxItem(object serializedObject, IDesignerHost host)
        {
            //TODO: implémenter plus efficacement !
            ToolboxItem item = null;
            try { item = DeserializeToolboxItem(serializedObject, host); } catch { }
            return (item != null);
        }

        public void RemoveCreator(string format, IDesignerHost host) { }

        public void RemoveToolboxItem(ToolboxItem toolboxItem, string category) { }

        public void SelectedToolboxItemUsed() { SelectPointerTool(); }

        public object SerializeToolboxItem(ToolboxItem toolboxItem) { return new DataObject (toolboxItem); }

        public bool SetCursor() { return false; }

        public void SetSelectedToolboxItem(ToolboxItem toolboxItem)
        {
            ToolboxTool tool = toolboxItem as ToolboxTool;
            if (tool != null) SelectTool(tool);
        }

        public IDesignerHost GetDesignerHost() { return null; }

        public string GetSelectedCategory() { return DEFAULT_CATEGORY; }

        public void SetSelectedCategory(string category) { }

        #endregion
    }
}
