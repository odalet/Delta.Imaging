using System;
using System.Text;
using System.Drawing;
using System.Collections;
using System.Globalization;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Windows.Forms.Design.Behavior;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;

using Delta.CertXplorer.Internals;
using Delta.CertXplorer.ComponentModel;

namespace Delta.CertXplorer.UI
{
    internal class BaseChromeDesigner : DocumentDesigner
    {
        private class BaseChromeActionsBehavior : Behavior
        {
            private BaseChromeDesigner designer = null;
            private bool ignoreNextMouseUp = false;

            /// <summary>
            /// Initializes a new instance of the <see cref="BaseChromeActionsBehavior"/> class.
            /// </summary>
            /// <param name="owner">The owner.</param>
            public BaseChromeActionsBehavior(BaseChromeDesigner owner)
                : base()
            {
                designer = owner;
                System.Diagnostics.Debug.Assert(designer != null);
                System.Diagnostics.Debug.Assert(designer.Control != null);
                System.Diagnostics.Debug.Assert(designer.BehaviorService != null);
            }

            /// <summary>
            /// Called when any double-click message enters the adorner window of the <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorService"/>.
            /// </summary>
            /// <param name="g">A <see cref="T:System.Windows.Forms.Design.Behavior.Glyph"/>.</param>
            /// <param name="button">A <see cref="T:System.Windows.Forms.MouseButtons"/> value indicating which button was clicked.</param>
            /// <param name="mouseLoc">The location at which the click occurred.</param>
            /// <returns>
            /// true if the message was handled; otherwise, false.
            /// </returns>
            public override bool OnMouseDoubleClick(Glyph g, MouseButtons button, Point mouseLoc)
            {
                if (!(g is BaseChromeActionsGlyph)) return false;
                ignoreNextMouseUp = true;
                return true;
            }

            /// <summary>
            /// Called when any mouse-down message enters the adorner window of the <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorService"/>.
            /// </summary>
            /// <param name="g">A <see cref="T:System.Windows.Forms.Design.Behavior.Glyph"/>.</param>
            /// <param name="button">A <see cref="T:System.Windows.Forms.MouseButtons"/> value indicating which button was clicked.</param>
            /// <param name="mouseLoc">The location at which the click occurred.</param>
            /// <returns>
            /// true if the message was handled; otherwise, false.
            /// </returns>
            public override bool OnMouseDown(Glyph g, MouseButtons button, Point mouseLoc)
            {
                if (!(g is BaseChromeActionsGlyph)) return false;
                else return base.OnMouseDown(g, button, mouseLoc);
            }

            /// <summary>
            /// Called when any mouse-up message enters the adorner window of the <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorService"/>.
            /// </summary>
            /// <param name="g">A <see cref="T:System.Windows.Forms.Design.Behavior.Glyph"/>.</param>
            /// <param name="button">A <see cref="T:System.Windows.Forms.MouseButtons"/> value indicating which button was clicked.</param>
            /// <returns>
            /// true if the message was handled; otherwise, false.
            /// </returns>
            public override bool OnMouseUp(Glyph g, MouseButtons button)
            {
                if (!(g is BaseChromeActionsGlyph)) return false;
                if (!ignoreNextMouseUp)
                {
                    ShowUI(g);
                    return true;
                }
                else return false;
            }

            private void ShowUI(Glyph g)
            {
                var glyph = g as BaseChromeActionsGlyph;
                if (glyph != null)
                {
                    var service = designer.BehaviorService;
                    var control = designer.Control;

                    var location = g.Bounds.Location;
                    location = service.ControlToAdornerWindow(control);
                    location.X += control.Width;
                    location.Y += glyph.GlyphImageSize.Width / 2;

                    designer.ShowContextMenu(service.AdornerWindowPointToScreen(location));
                }
            }
        }

        private class BaseChromeActionsGlyph : Glyph
        {
            private BaseChromeDesigner designer = null;
            private Bitmap glyphImage = null;
            private bool mouseOver = false;
            private Adorner componentAdorner = null;
            private Rectangle bounds = Rectangle.Empty;

            /// <summary>
            /// Initializes a new instance of the <see cref="BaseChromeActionsGlyph"/> class.
            /// </summary>
            /// <param name="owner">The owner.</param>
            /// <param name="behavior">The <see cref="T:System.Windows.Forms.Design.Behavior.Behavior"/> associated with the <see cref="T:System.Windows.Forms.Design.Behavior.Glyph"/>. Can be null.</param>
            /// <param name="adorner">The adorner.</param>
            public BaseChromeActionsGlyph(BaseChromeDesigner owner, Behavior behavior, Adorner adorner)
                : base(behavior)
            {
                designer = owner;
                System.Diagnostics.Debug.Assert(designer != null);
                System.Diagnostics.Debug.Assert(behavior != null);
                System.Diagnostics.Debug.Assert(adorner != null);
                System.Diagnostics.Debug.Assert(designer.Control != null);

                componentAdorner = adorner;
                glyphImage = Properties.Resources.Glyph;

                Invalidate();
            }

            /// <summary>
            /// Gets the size of the glyph image.
            /// </summary>
            /// <value>The size of the glyph image.</value>
            internal Size GlyphImageSize
            {
                get { return glyphImage.Size; }
            }

            /// <summary>
            /// Provides hit test logic.
            /// </summary>
            /// <param name="p">A point to hit-test.</param>
            /// <returns>
            /// A <see cref="T:System.Windows.Forms.Cursor"/> if the <see cref="T:System.Windows.Forms.Design.Behavior.Glyph"/> is associated with <paramref name="p"/>; otherwise, null.
            /// </returns>
            public override Cursor GetHitTest(Point p)
            {
                if (bounds.Contains(p))
                {
                    MouseOver = true;
                    return Cursors.Default;
                }

                MouseOver = false;
                return null;
            }

            /// <summary>
            /// Provides paint logic.
            /// </summary>
            /// <param name="pe">A <see cref="T:System.Windows.Forms.PaintEventArgs"/> that contains the event data.</param>
            public override void Paint(PaintEventArgs pe)
            {
                pe.Graphics.DrawImage(glyphImage, bounds.Left, bounds.Top);
            }

            private bool MouseOver
            {
                get { return mouseOver; }
                set
                {
                    if (mouseOver != value)
                    {
                        mouseOver = value;
                        InvalidateOwnerLocation();
                    }
                }
            }

            private void InvalidateOwnerLocation()
            {
                componentAdorner.Invalidate(bounds);
            }

            internal void Invalidate()
            {
                var control = designer.Control;
                Point location = Point.Empty;

                if (componentAdorner != null)
                    location = componentAdorner.BehaviorService.ControlToAdornerWindow(control);
                else location = new Point(15, 15);

                location.X += control.Width - GlyphImageSize.Width / 2;
                bounds = new Rectangle(location.X, location.Y, glyphImage.Width, glyphImage.Height);
            }
        }

        private class BaseChromeActionList : DesignerActionList
        {
            private BaseChromeDesigner designer = null;

            /// <summary>
            /// Initializes a new instance of the <see cref="BaseChromeActionList"/> class.
            /// </summary>
            /// <param name="owner">The owner.</param>
            public BaseChromeActionList(BaseChromeDesigner owner)
                : base(owner.Component)
            {
                designer = owner;
                System.Diagnostics.Debug.Assert(designer != null);
            }

            /// <summary>
            /// Adds the standard menu items.
            /// </summary>
            public void AddStandardMenuItems()
            {
                InsertStandardMenuItemsAction.CreateStandardMenuStrip(designer, designer.MenuStrip);
            }

            /// <summary>
            /// Expands the or collapse top panel.
            /// </summary>
            public void ExpandOrCollapseTopPanel()
            {
                designer.ProcessToolStripPanelVerb(designer.topToolStripPanel);
            }

            /// <summary>
            /// Expands the or collapse bottom panel.
            /// </summary>
            public void ExpandOrCollapseBottomPanel()
            {
                designer.ProcessToolStripPanelVerb(designer.bottomToolStripPanel);
            }

            /// <summary>
            /// Expands the or collapse left panel.
            /// </summary>
            public void ExpandOrCollapseLeftPanel()
            {
                designer.ProcessToolStripPanelVerb(designer.leftToolStripPanel);
            }

            /// <summary>
            /// Expands the or collapse right panel.
            /// </summary>
            public void ExpandOrCollapseRightPanel()
            {
                designer.ProcessToolStripPanelVerb(designer.rightToolStripPanel);
            }

            /// <summary>
            /// Returns the collection of <see cref="T:System.ComponentModel.Design.DesignerActionItem"/> objects contained in the list.
            /// </summary>
            /// <returns>
            /// A <see cref="T:System.ComponentModel.Design.DesignerActionItem"/> array that contains the items in this list.
            /// </returns>
            public override DesignerActionItemCollection GetSortedActionItems()
            {
                var items = new DesignerActionItemCollection();
                items.Add(new DesignerActionMethodItem(this,
                    "AddStandardMenuItems", "&Add Standard Menu Items", "Menu", "Adds Standard menu items to the main menustrip.", true));
                items.Add(new DesignerActionMethodItem(this,
                    "ExpandOrCollapseTopPanel", "Expand/Collapse &Top Panel", "ToolStrip", "Expands or collapses the top toolstrip container panel.", true));
                items.Add(new DesignerActionMethodItem(this,
                    "ExpandOrCollapseLeftPanel", "Expand/Collapse &Left Panel", "ToolStrip", "Expands or collapses the left toolstrip container panel.", true));
                items.Add(new DesignerActionMethodItem(this,
                    "ExpandOrCollapseRightPanel", "Expand/Collapse &Right Panel", "ToolStrip", "Expands or collapses the right toolstrip container panel.", true));
                items.Add(new DesignerActionMethodItem(this,
                    "ExpandOrCollapseBottomPanel", "Expand/Collapse &Bottom Panel", "ToolStrip", "Expands or collapses the bottom toolstrip container panel.", true));
                return items;

            }
        }

        private static class InsertStandardMenuItemsAction
        {
            #region Constants

            private static readonly string[] fileMenu;
            private static readonly string[] editMenu;
            private static readonly string[] toolsMenu;
            private static readonly string[] helpMenu;

            private static readonly string[][] menuTexts;
            private static readonly string[][] menuImages;
            private static readonly Keys[][] menuKeys;

            #endregion

            /// <summary>
            /// Initializes the <see cref="InsertStandardMenuItemsAction"/> class.
            /// </summary>
            static InsertStandardMenuItemsAction()
            {
                fileMenu = new[] 
                { 
                    "&File", 
                    "&New",
                    "&Open...",
                    "-",
                    "&Save",
                    "Save &As...",
                    "-",
                    "&Print...",
                    "Print Pre&view...",
                    "-",
                    "E&xit"
                };

                editMenu = new[]
                {
                    "&Edit",
                    "&Undo", 
                    "&Redo", 
                    "-", 
                    "Cu&t", 
                    "&Copy", 
                    "&Paste", 
                    "-", 
                    "Select &All"
                };

                toolsMenu = new[]
                {
                    "&Tools",
                    "&Customize",
                    "&Options"
                };

                helpMenu = new[]
                {
                    "&Help",
                    "&Contents",
                    "&Index",
                    "&Search",
                    "-",
                    "&About..."
                };

                menuTexts = new[] 
                { 
                    fileMenu,
                    editMenu,
                    toolsMenu,
                    helpMenu
                };

                menuImages = new string[][] 
                { 
                    new string[] { "", "new", "open", "-", "save", "", "-", "print", "printPreview", "-", "" }, 
                    new string[] { "", "", "", "-", "cut", "copy", "paste", "-", "" }, 
                    new string[] { "", "", "" }, 
                    new string[] { "", "", "", "", "-", "" } 
                };

                var fileMenuKeys = new Keys[fileMenu.Length];
                fileMenuKeys[1] = Keys.Control | Keys.N;
                fileMenuKeys[2] = Keys.Control | Keys.O;
                fileMenuKeys[4] = Keys.Control | Keys.S;
                fileMenuKeys[7] = Keys.Control | Keys.P;

                var editMenuKeys = new Keys[editMenu.Length];
                editMenuKeys[1] = Keys.Control | Keys.Z;
                editMenuKeys[2] = Keys.Control | Keys.Y;
                editMenuKeys[4] = Keys.Control | Keys.X;
                editMenuKeys[5] = Keys.Control | Keys.C;
                editMenuKeys[6] = Keys.Control | Keys.V;

                menuKeys = new[]
                {
                    fileMenuKeys,
                    editMenuKeys,
                    new Keys[toolsMenu.Length],
                    new Keys[helpMenu.Length]
                };
            }

            internal static void CreateStandardMenuStrip(BaseChromeDesigner designer, MenuStrip menuStripControl)
            {
                System.Diagnostics.Debug.Assert(designer != null);

                var serviceProvider = designer.Component.Site;
                var designerHost = (IDesignerHost)serviceProvider.GetService(typeof(IDesignerHost));
                var componentChangeService = serviceProvider.GetService<IComponentChangeService>();
                   

                

                if (designerHost != null)
                {
                    menuStripControl.SuspendLayout();
                    DesignerTransaction transaction = designerHost.CreateTransaction("CreateStandardMenu");
                    try
                    {
                        var nameCreationService = serviceProvider.GetService<INameCreationService>(true);
                        string str = "standardMainMenuStrip";
                        string name = str;
                        int num = 1;
                        if (designerHost != null)
                        {
                            while (designerHost.Container.Components[name] != null)
                                name = str + num++.ToString(CultureInfo.InvariantCulture);
                        }

                        for (int i = 0; i < menuTexts.Length; i++)
                        {
                            string[] currentMenuTexts = menuTexts[i];
                            ToolStripMenuItem component = null;

                            for (int j = 0; j < currentMenuTexts.Length; j++)
                            {
                                name = null;
                                string text = currentMenuTexts[j];
                                name = NameFromText(designer, text, typeof(ToolStripMenuItem), nameCreationService, true);

                                ToolStripItem currentMenuItem = null;
                                if (name.Contains("Separator"))
                                {
                                    currentMenuItem = (ToolStripSeparator)designerHost.CreateComponent(typeof(ToolStripSeparator), name);
                                    var separatorDesigner = designerHost.GetDesigner(currentMenuItem);
                                    if (separatorDesigner is ComponentDesigner)
                                        ((ComponentDesigner)separatorDesigner).InitializeNewComponent(null);

                                    currentMenuItem.Text = text;
                                }
                                else
                                {
                                    currentMenuItem = (ToolStripMenuItem)designerHost.CreateComponent(typeof(ToolStripMenuItem), name);
                                    var menuItemDesigner = designerHost.GetDesigner(currentMenuItem);
                                    if (menuItemDesigner is ComponentDesigner)
                                        ((ComponentDesigner)menuItemDesigner).InitializeNewComponent(null);

                                    currentMenuItem.Text = text;
                                    Keys shortcut = menuKeys[i][j];
                                    if (((currentMenuItem is ToolStripMenuItem) &&
                                        (shortcut != Keys.None)) &&
                                        (!ToolStripManager.IsShortcutDefined(shortcut) &&
                                        ToolStripManager.IsValidShortcut(shortcut)))
                                        ((ToolStripMenuItem)currentMenuItem).ShortcutKeys = shortcut;

                                    Bitmap image = null;
                                    try
                                    {
                                        image = GetImage(menuImages[i][j]);
                                    }
                                    catch (Exception ex)
                                    {
                                        System.Diagnostics.Debug.WriteLine(string.Format(
                                            "Could not get a menu image: {0}", ex.Message));
                                    }

                                    if (image != null)
                                    {
                                        PropertyDescriptor descriptor = TypeDescriptor.GetProperties(currentMenuItem)["Image"];
                                        if (descriptor != null) descriptor.SetValue(currentMenuItem, image);

                                        currentMenuItem.ImageTransparentColor = Color.Magenta;
                                    }
                                }

                                if (j == 0) // top menu
                                {
                                    component = (ToolStripMenuItem)currentMenuItem;
                                    component.DropDown.SuspendLayout();
                                }
                                else component.DropDownItems.Add(currentMenuItem);

                                if (j == (currentMenuTexts.Length - 1))
                                {
                                    var memberDescriptor = TypeDescriptor.GetProperties(component)["DropDownItems"];
                                    componentChangeService.OnComponentChanging(component, memberDescriptor);
                                    componentChangeService.OnComponentChanged(component, memberDescriptor, null, null);
                                }
                            }

                            component.DropDown.ResumeLayout(false);

                            menuStripControl.Items.Add(component);

                            if (i == (menuTexts.Length - 1))
                            {
                                var memberDescriptor = TypeDescriptor.GetProperties(menuStripControl)["Items"];
                                componentChangeService.OnComponentChanging(menuStripControl, memberDescriptor);
                                componentChangeService.OnComponentChanged(menuStripControl, memberDescriptor, null, null);
                            }
                        }
                    }
                    catch (Exception exception)
                    {
                        if (exception is InvalidOperationException)
                            ((IUIService)serviceProvider.GetService(typeof(IUIService))).ShowError(exception.Message);

                        if (transaction != null)
                        {
                            transaction.Cancel();
                            transaction = null;
                        }
                    }
                    finally
                    {
                        if (transaction != null)
                        {
                            transaction.Commit();
                            transaction = null;
                        }

                        menuStripControl.ResumeLayout();
                        var selectionService = serviceProvider.GetService<ISelectionService>();
                        if (selectionService != null)
                            selectionService.SetSelectedComponents(new object[] { designer.Component });

                        var uiService = serviceProvider.GetService<DesignerActionUIService>();
                        if (uiService != null) uiService.Refresh(designer.Component);
                    }
                }
            }

            private static Bitmap GetImage(string name)
            {
                Bitmap bitmap = null;
                if (name.StartsWith("new"))
                {
                    return new Bitmap(typeof(ToolStripMenuItem), "new.bmp");
                }
                if (name.StartsWith("open"))
                {
                    return new Bitmap(typeof(ToolStripMenuItem), "open.bmp");
                }
                if (name.StartsWith("save"))
                {
                    return new Bitmap(typeof(ToolStripMenuItem), "save.bmp");
                }
                if (name.StartsWith("printPreview"))
                {
                    return new Bitmap(typeof(ToolStripMenuItem), "printPreview.bmp");
                }
                if (name.StartsWith("print"))
                {
                    return new Bitmap(typeof(ToolStripMenuItem), "print.bmp");
                }
                if (name.StartsWith("cut"))
                {
                    return new Bitmap(typeof(ToolStripMenuItem), "cut.bmp");
                }
                if (name.StartsWith("copy"))
                {
                    return new Bitmap(typeof(ToolStripMenuItem), "copy.bmp");
                }
                if (name.StartsWith("paste"))
                {
                    return new Bitmap(typeof(ToolStripMenuItem), "paste.bmp");
                }
                if (name.StartsWith("help"))
                {
                    bitmap = new Bitmap(typeof(ToolStripMenuItem), "help.bmp");
                }
                return bitmap;
            }

            private static string NameFromText(BaseChromeDesigner designer,
                string text, Type itemType, INameCreationService nameCreationService, bool adjustCapitalization)
            {
                System.Diagnostics.Debug.Assert(designer != null);

                var serviceProvider = designer.Component.Site;
                var designerHost = serviceProvider.GetService<IDesignerHost>(true);
                string name = null;

                if (text == "-") name = "toolStripSeparator";
                else
                {
                    var itemTypeName = itemType.Name;
                    var builder = new StringBuilder(text.Length + itemTypeName.Length);
                    bool flag = false;

                    for (int j = 0; j < text.Length; j++)
                    {
                        char c = text[j];
                        if (char.IsLetterOrDigit(c))
                        {
                            if (!flag)
                            {
                                c = char.ToLower(c, CultureInfo.CurrentCulture);
                                flag = true;
                            }
                            builder.Append(c);
                        }
                    }

                    builder.Append(itemTypeName);
                    name = builder.ToString();

                    if (adjustCapitalization)
                    {
                        var newName = ToolStripDesignerNameFromText(null, typeof(ToolStripMenuItem), serviceProvider);
                        if (!string.IsNullOrEmpty(newName) && char.IsUpper(newName[0]))
                            name = char.ToUpper(name[0], CultureInfo.InvariantCulture) + name.Substring(1);
                    }
                }

                if (designerHost.Container.Components[name] == null)
                {
                    if (!nameCreationService.IsValidName(name))
                        return nameCreationService.CreateName(designerHost.Container, itemType);
                    return name;
                }

                string result = name;
                for (int i = 1; !nameCreationService.IsValidName(result); i++)
                    result = name + i.ToString(CultureInfo.InvariantCulture);

                return result;
            }

            private static string ToolStripDesignerNameFromText(string text, Type componentType, IServiceProvider serviceProvider)
            {
                if (serviceProvider == null) return null;

                string result = null;
                var service = serviceProvider.GetService<INameCreationService>();
                var container = serviceProvider.GetService<IContainer>();

                if (service == null || container == null) return null;

                result = service.CreateName(container, componentType);
                if (string.IsNullOrEmpty(text) || text == "-") return result;

                string name = componentType.Name;
                var builder = new StringBuilder(text.Length + name.Length);                
                bool whiteSpace = false;
                
                for (int i = 0; i < text.Length; i++)
                {
                    char c = text[i];
                    if (whiteSpace)
                    {
                        if (char.IsLower(c))
                            c = char.ToUpper(c, CultureInfo.CurrentCulture);
                        whiteSpace = false;
                    }

                    if (char.IsLetterOrDigit(c))
                    {
                        if (builder.Length == 0)
                        {
                            if (char.IsDigit(c)) continue;

                            if (char.IsLower(c) != char.IsLower(result[0]))
                            {
                                if (char.IsLower(c))
                                    c = char.ToUpper(c, CultureInfo.CurrentCulture);
                                else c = char.ToLower(c, CultureInfo.CurrentCulture);
                            }
                        }
                        builder.Append(c);
                    }
                    else if (char.IsWhiteSpace(c)) whiteSpace = true;
                }

                if (builder.Length == 0) return result;

                builder.Append(name);
                string res = builder.ToString();
                if (container.Components[res] == null)
                {
                    if (!service.IsValidName(res))
                        return result;
                    return res;
                }

                string temp = res;
                for (int j = 1; !service.IsValidName(temp) || (container.Components[temp] != null); j++)
                    temp = res + j.ToString(CultureInfo.InvariantCulture);
                return temp;
            }
        }

        private const int defaultFormPadding = 9;

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

        private BaseChrome baseDockingMainForm = null;
        private MenuStrip menuStrip = null;
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
        /// Gets the design-time action lists supported by the component associated with the designer.
        /// </summary>
        /// <remarks>
        /// In this implementation, the action list is similar t othe verbs collection (<see cref="Verbs"/>).
        /// </remarks>
        /// <value></value>
        /// <returns>
        /// The design-time action lists supported by the component associated with the designer.
        /// </returns>
        public override DesignerActionListCollection ActionLists
        {
            get
            {
                var actions = base.ActionLists;

                if (actions == null) actions = new DesignerActionListCollection();
                actions.AddRange(new[] { new BaseChromeActionList(this) });
                return actions;
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
                                line.AdjustOffset(defaultFormPadding);
                                count++;
                            }

                            if (line.Filter.Equals("Padding.Right") || line.Filter.Equals("Padding.Bottom"))
                            {
                                line.AdjustOffset(-defaultFormPadding);
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

        private MenuStrip MenuStrip
        {
            get { return menuStrip; }
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

        private void ShowContextMenu(Point location)
        {
            base.OnContextMenu(location.X, location.Y);
        }

        /// <summary>
        /// Initializes the designer with the specified component.
        /// </summary>
        /// <param name="component">The <see cref="T:System.ComponentModel.IComponent"/> to associate with the designer.</param>
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

            baseDockingMainForm = (BaseChrome)component;
            menuStrip = baseDockingMainForm.MenuStrip;
            topToolStripPanel = baseDockingMainForm.TopToolStripPanel;
            leftToolStripPanel = baseDockingMainForm.LeftToolStripPanel;
            rightToolStripPanel = baseDockingMainForm.RightToolStripPanel;
            bottomToolStripPanel = baseDockingMainForm.BottomToolStripPanel;

            // Adding a designer glyph. Is this the right place for this?            
            var behaviorService = GetService<BehaviorService>();
            var adorner = new Adorner();
            behaviorService.Adorners.Add(adorner);
            var behavior = new BaseChromeActionsBehavior(this);
            var glyph = new BaseChromeActionsGlyph(this, behavior, adorner);

            Control.SizeChanged += (s, e) => glyph.Invalidate();

            adorner.Glyphs.Add(glyph);
        }

        /// <summary>
        /// Adjusts the set of properties the component exposes through a <see cref="T:System.ComponentModel.TypeDescriptor"/>.
        /// </summary>
        /// <param name="properties">An <see cref="T:System.Collections.IDictionary"/> that contains the properties for the class of the component.</param>
        protected override void PreFilterProperties(IDictionary properties)
        {
            PropertyDescriptor descriptor = null;
            base.PreFilterProperties(properties);

            Attribute[] attributes = new Attribute[0];
            for (int i = 0; i < preFilteredProperties.Length; i++)
            {
                descriptor = (PropertyDescriptor)properties[preFilteredProperties[i]];
                if (descriptor != null) properties[preFilteredProperties[i]] = TypeDescriptor.CreateProperty(
                    typeof(BaseChromeDesigner), descriptor, attributes);
            }

            descriptor = (PropertyDescriptor)properties["AutoScaleBaseSize"];
            if (descriptor != null) properties["AutoScaleBaseSize"] = TypeDescriptor.CreateProperty(
                typeof(BaseChromeDesigner), descriptor, new Attribute[] 
                { 
                    DesignerSerializationVisibilityAttribute.Visible 
                });

            descriptor = (PropertyDescriptor)properties["ClientSize"];
            if (descriptor != null) properties["ClientSize"] = TypeDescriptor.CreateProperty(
                typeof(BaseChromeDesigner), descriptor, new Attribute[] 
                { 
                    new DefaultValueAttribute(new Size(-1, -1)) 
                });
        }

        /// <summary>
        /// Called immediately after the handle for the designer has been created.
        /// </summary>
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

        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.Design.DocumentDesigner"/> and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
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

        /// <summary>
        /// Processes Windows messages.
        /// </summary>
        /// <param name="m">The <see cref="T:System.Windows.Forms.Message"/> to process.</param>
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