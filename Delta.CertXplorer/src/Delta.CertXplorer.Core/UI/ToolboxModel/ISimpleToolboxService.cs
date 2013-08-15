using System;
using System.Collections;
using System.Drawing.Design;
using System.ComponentModel.Design;

namespace Delta.CertXplorer.UI.ToolboxModel
{
    internal interface ISimpleToolboxService
    {
        CategoryNameCollection CategoryNames { get; }

        void AddCreator(ToolboxItemCreatorCallback creator, string format, IDesignerHost host);
        void AddLinkedToolboxItem(ToolboxItem toolboxItem, string category, IDesignerHost host);
        void AddToolboxItem(ToolboxItem toolboxItem, string category);
        ToolboxItem DeserializeToolboxItem(object serializedObject, IDesignerHost host);
        ToolboxItem GetSelectedToolboxItem(IDesignerHost host);
        ToolboxItemCollection GetToolboxItems(string category, IDesignerHost host);
        bool IsSupported(object serializedObject, ICollection filterAttributes);
        bool IsSupported(object serializedObject, IDesignerHost host);
        bool IsToolboxItem(object serializedObject, IDesignerHost host);
        void Refresh();
        void RemoveCreator(string format, IDesignerHost host);
        void RemoveToolboxItem(ToolboxItem toolboxItem, string category);
        void SelectedToolboxItemUsed();
        object SerializeToolboxItem(ToolboxItem toolboxItem);
        bool SetCursor();
        void SetSelectedToolboxItem(ToolboxItem toolboxItem);

        IDesignerHost GetDesignerHost();
        string GetSelectedCategory();
        void SetSelectedCategory(string category);
    }
}
