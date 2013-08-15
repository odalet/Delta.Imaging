using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

using Delta.CertXplorer.UI.ToolboxModel;

namespace Delta.CertXplorer.UI.ToolboxImplementation
{
    internal partial class ToolboxTab : UserControl, IToolboxTab
    {
        private Dictionary<ToolboxTool, ToolboxButton> tools = new Dictionary<ToolboxTool, ToolboxButton>();

        private Bitmap bmpMinus = null;
        private Bitmap bmpPlus = null;

        private int minHeight = 21;
        private int maxHeight = 21;
        private int buttonGap = 3; // 3 pixels between 2 buttons
        private bool opened = true;
        private bool refreshLayout = false;
        private ToolboxButton selectedButton = null;
        private ToolboxPointer pointerItem = null;
        private int pointersCount = 0;
        
        public ToolboxTab()
        {
            InitializeComponent();

            InitGlyphs(VisualStyleRenderer.IsSupported && Application.RenderWithVisualStyles);

            toolStrip.Renderer = new ToolboxTabRenderer();            

            Height = minHeight;
            pb.Image = bmpMinus;
            opened = true;
        }

        public event ToolboxButtonEventHandler ButtonClick;
        public event ToolboxButtonEventHandler ButtonDoubleClick;
        public event ToolboxButtonEventHandler SelectedButtonChanged;

        public event EventHandler CollapsedChanged;

        public ToolboxButton SelectedButton { get { return selectedButton; } }

        public ToolboxTool[] Tools
        {
            get
            {
                ToolboxTool[] toolsArray = new ToolboxTool[tools.Count];
                tools.Keys.CopyTo(toolsArray, 0);
                return toolsArray;
            }
        }

        public bool Collapsed 
        { 
            get { return !opened; }
            set
            {
                if (value) CloseTab();
                else OpenTab();
            }
        }

        public new string Text
        {
            get { return base.Text; }
            set { base.Text = value; SetText(value); }
        }

        public void RefreshLayout()
        {
            if (opened)
            {
                CloseTab(); OpenTab();
            }
        }

        public void AddPointer()
        {
            if (pointerItem == null) pointerItem = new ToolboxPointer();            
            AddTool(pointerItem, false);
        }

        public void AddTool(Type type)
        {
            if (type == null) throw new ArgumentNullException("type");
            AddTool(new ToolboxTool(type));
        }

        public void AddTool(ToolboxTool tool) { AddTool(tool, true); }
        public void AddTool(ToolboxTool tool, bool dragEnabled)
        {
            refreshLayout = false;
            ToolboxButton toolStripButton = new ToolboxButton();
            toolStripButton.Tool = tool;
            toolStripButton.DragEnabled = dragEnabled;
            toolStripButton.Text = tool.DisplayName;
            toolStripButton.Image = tool.Bitmap;
            toolStripButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            toolStripButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            toolStripButton.Alignment = ToolStripItemAlignment.Left;
            toolStripButton.DoubleClickEnabled = true;
            toolStripButton.Click += new EventHandler(OnButtonClick);
            toolStripButton.DoubleClick += new EventHandler(OnButtonDoubleClick);
            
            toolStrip.Items.Add(toolStripButton);            
            refreshLayout = true;
        }

        public void SelectTool(ToolboxTool tool)
        {
            if (tool == null) return;

            if (tools.ContainsKey(tool))
            {
                ToolboxButton button = tools[tool];
                button.PerformClick();
            }
        }

        public void OpenTab()
        {
            if (opened) return;
            toolStrip.Visible = true;
            RecalcHeight();
            Height = maxHeight;
            pb.Image = bmpMinus;
            opened = true;

            if (CollapsedChanged != null) CollapsedChanged(this, EventArgs.Empty);
        }

        public void CloseTab()
        {
            if (!opened) return;
            toolStrip.Visible = false;
            Height = minHeight;
            pb.Image = bmpPlus;
            opened = false;

            if (CollapsedChanged != null) CollapsedChanged(this, EventArgs.Empty);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (opened && (Height != maxHeight)) Height = maxHeight;
            else base.OnPaint(e);
        }        
        
        private void InitGlyphs(bool useVisualStyle)
        {
            if (useVisualStyle)
            {
                Graphics g = null;
                VisualStyleRenderer renderer = null;

                bmpMinus = new Bitmap(9, 9);
                g = Graphics.FromImage(bmpMinus);
                renderer = new VisualStyleRenderer(VisualStyleElement.TreeView.Glyph.Opened);
                renderer.DrawBackground(g, new Rectangle(0, 0, 9, 9));
                g.Dispose();

                bmpPlus = new Bitmap(9, 9);
                g = Graphics.FromImage(bmpPlus);
                renderer = new VisualStyleRenderer(VisualStyleElement.TreeView.Glyph.Closed);
                renderer.DrawBackground(g, new Rectangle(0, 0, 9, 9));
                g.Dispose();
            }
            else
            {
                bmpMinus = Properties.Resources.minus;
                bmpPlus = Properties.Resources.plus;
            }
        }

        private void RecalcHeight()
        {            
            if (toolStrip.Items.Count == 0) maxHeight = minHeight;
            else maxHeight = toolStrip.Items.Count * (toolStrip.Items[0].Height + buttonGap) + minHeight;            
        }

        private void SetText(string text) { lblHeader.Text = text; }

        private void OnItemAdded(ToolStripItem item)
        {
            ToolboxButton toolboxButton = null;

            if (!(item is ToolboxButton))
            {
                // We deny addition of non ToolboxButton buttons
                toolStrip.Items.Remove(item);
                return;
            }
            else toolboxButton = (ToolboxButton)item;

            ToolboxTool tool = toolboxButton.Tool;

            if (tool == null) toolStrip.Items.Remove(item);
            else if (tool is ToolboxPointer)
            {
                // We allow only one pointer per tab
                if (pointersCount > 1) toolStrip.Items.Remove(item);
                else
                {
                    tools.Add(tool, toolboxButton);
                    pointersCount++;
                }
            }
            else if (tool is ToolboxTool)
            {
                if (!tools.ContainsKey(tool)) tools.Add(tool, toolboxButton);
            }
            else toolStrip.Items.Remove(item);
        }

        private void OnItemRemoved(ToolStripItem item)
        {
            ToolboxButton toolboxButton = null;
            if (!(item is ToolboxButton)) return;
            else toolboxButton = (ToolboxButton)item;

            ToolboxTool tool = toolboxButton.Tool;

            if (tool == null) { } // It's ok, the item is removed    
            else if (tool is ToolboxPointer)
            {
                // We don't allow the removal of the pointer tool                
                if (pointersCount > 1)
                {
                    toolStrip.Items.Remove(item);
                    pointersCount--;
                }
                else AddPointer();
            }
            else if (tool is ToolboxTool)
            {
                if (tools.ContainsKey(tool)) tools.Remove(tool);
            }
            else { } // It's ok, the item is removed
        }

        private void OnHeaderClick() 
        { 
            if (opened) CloseTab(); else OpenTab();
        }

        private void OnButtonSelected(object sender, EventArgs e)
        {
            selectedButton = sender as ToolboxButton;
            if (SelectedButtonChanged != null) SelectedButtonChanged(this, new ToolboxButtonEventArgs(selectedButton));
        }

        private void OnButtonClick(object sender, EventArgs e) 
        {
            OnButtonSelected(sender, e);
            if (ButtonClick != null) ButtonClick(this, new ToolboxButtonEventArgs(sender as ToolboxButton)); 
        }

        private void OnButtonDoubleClick(object sender, EventArgs e) 
        {
            OnButtonSelected(sender, e);
            if (ButtonDoubleClick != null) ButtonDoubleClick(this, new ToolboxButtonEventArgs(sender as ToolboxButton)); 
        }

        private void lblHeader_Click(object sender, EventArgs e) { OnHeaderClick(); }

        private void pb_Click(object sender, EventArgs e) { OnHeaderClick(); } 

        private void toolStrip_ItemAdded(object sender, ToolStripItemEventArgs e)
        {
            OnItemAdded(e.Item); 
            if (refreshLayout) RefreshLayout();
        }

        private void toolStrip_ItemRemoved(object sender, ToolStripItemEventArgs e)
        {
            OnItemRemoved(e.Item);
            if (refreshLayout) RefreshLayout();
        }        
    }
}
