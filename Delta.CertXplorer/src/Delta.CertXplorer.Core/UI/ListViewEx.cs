using System;
using System.Linq;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using Delta.CertXplorer.Resources;
using Delta.CertXplorer.ComponentModel;

namespace Delta.CertXplorer.UI
{
    /// <summary>
    /// Contrôle dérivé de ListView : retaille
    /// automatiquement la dernière colonne + méthodes utilitaires
    /// </summary>
    /// <remarks>
    /// Ce contrôle permet de :
    /// <list type="bullet">
    /// 		<item>Retaillage automatique de la dernière colonne : la dernière colonne prend
    /// automatiquement toute la place disponible</item>
    /// 		<item><see cref="GetFirstSelectedIndex"/> et <see cref="GetFirstSelectedItem"/> :
    /// ces deux méthodes permettent de récupérer le premier élement sélectionné (ou son index)</item>
    /// 		<item>Tri des colonnes : toutes les colonnes sont triables (par clic) sur l'en-tête.
    /// NB : on peut sélectionner un type de tri (numérique ou chaîne) en appelant la méthode
    /// <see cref="SetColumnOrderType"/></item>
    /// 	</list>
    /// 	<u>Remarque</u> : la propriété <see cref="ListView.AllowColumnReorder"/> est
    /// masquée ; en effet, elle n'est pas compatible avec le redimensionnement auto de
    /// la dernière colonne (ce comportement sera peut-être revu dans une prochaine version.
    /// </remarks>
    public class ListViewEx : System.Windows.Forms.ListView
    {
        #region Enumerations and inner classes

        /// <summary>Defines sorting types.</summary>
        public enum SortType
        {
            /// <summary>Sorts based on the alphabetical order.</summary>
            String,

            /// <summary>Sorts based on a numerical order.</summary>
            Numeric
        }

        /// <summary>Defines sorting directions.</summary>
        public enum SortDirection
        {
            /// <summary>Ascending direction.</summary>
            Ascending,

            /// <summary>Descending direction.</summary>
            Descending
        }

        /// <summary>Ordering information class.</summary>
        public class SortInformation
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="SortInformation"/> class.
            /// </summary>
            public SortInformation() : this(SortType.String, SortDirection.Ascending) { }

            /// <summary>
            /// Initializes a new instance of the <see cref="SortInformation"/> class.
            /// </summary>
            /// <param name="type">The sort type.</param>
            /// <param name="direction">The sort direction.</param>
            public SortInformation(SortType type, SortDirection direction)
            {
                Type = type;
                Direction = direction;
            }

            /// <summary>Gets or sets the sort type.</summary>
            public SortType Type { get; set; }

            /// <summary>Gets or sets the sort direction.</summary>
            public SortDirection Direction { get; set; }
        }

        /// <summary>
        /// Allows for comparison between two <see cref="ListViewItem"/> instances.
        /// </summary>
        public class ListViewItemComparer : System.Collections.IComparer
        {
            private int columnIndex = 0;
            private bool asc = true;
            private SortType sortType = SortType.String;

            #region Construction

            /// <summary>
            /// Initializes a new instance of the <see cref="ListViewItemComparer"/> class.
            /// </summary>
            public ListViewItemComparer() : this(0, true, SortType.String) { }

            /// <summary>
            /// Initializes a new instance of the <see cref="ListViewItemComparer"/> class.
            /// </summary>
            /// <param name="colIndex">The column index.</param>
            /// <param name="sortInformation">The sort information.</param>
            public ListViewItemComparer(int colIndex, SortInformation sortInformation) : this(colIndex, sortInformation.Direction, sortInformation.Type) { }

            /// <summary>
            /// Initializes a new instance of the <see cref="ListViewItemComparer"/> class.
            /// </summary>
            /// <param name="colIndex">Index of the column.</param>
            /// <param name="direction">The sort direction.</param>
            /// <param name="type">The sort type.</param>
            public ListViewItemComparer(int colIndex, SortDirection direction, SortType type)
            {
                columnIndex = colIndex;
                asc = (direction == SortDirection.Ascending);
                sortType = type;
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="ListViewItemComparer"/> class.
            /// </summary>
            /// <param name="colIndex">Index of the column.</param>
            /// <param name="ascending">if set to <c>true</c> the sort direction is ascending.</param>
            /// <param name="type">The sort type.</param>
            public ListViewItemComparer(int colIndex, bool ascending, SortType type)
            {
                columnIndex = colIndex;
                asc = ascending;
                sortType = type;
            }

            #endregion

            public string GetStringValue(object o)
            {
                string s = string.Empty;

                if (columnIndex == -1)
                    s = ((ListViewItem)o).Tag.ToString();
                else
                {
                    if (((ListViewItem)o).SubItems.Count > columnIndex)
                        s = ((ListViewItem)o).SubItems[columnIndex].Text;
                }

                return s;
            }

            #region IComparer Members

            public int Compare(object x, object y)
            {
                var sx = GetStringValue(x);
                var sy = GetStringValue(y);

                if (sortType == SortType.String) return CompareStrings(sx, sy);
                else
                {
                    double dx = double.NaN, dy = double.NaN;

                    if (string.IsNullOrEmpty(sx)) dx = double.NaN;
                    else
                    {
                        try { dx = Convert.ToDouble(sx); }
                        catch { }
                    }

                    if (string.IsNullOrEmpty(sy)) dy = double.NaN;
                    else
                    {
                        try { dy = Convert.ToDouble(sy); }
                        catch { }
                    }

                    return CompareDoubles(dx, dy);
                }
            }

            #endregion

            private int CompareStrings(string sx, string sy)
            {
                if (asc) return String.Compare(sx, sy);
                else return String.Compare(sy, sx);
            }

            private int CompareDoubles(double dx, double dy)
            {
                double result = 0.0;

                if (double.IsNaN(dx))
                {
                    if (asc) return int.MinValue; else return int.MaxValue;
                }

                if (double.IsNaN(dy))
                {
                    if (asc) return int.MaxValue; else return int.MinValue;
                }

                if (asc) result = dx - dy;
                else result = dy - dx;

                int i = 0;
                try { i = Convert.ToInt32(result); }
                catch { }
                return i;
            }
        }

        /// <summary>
        /// Stores a pair consisting of a control and its associated sub-item.
        /// </summary>
        private class EmbeddedControl
        {
            public Control Control { get; set; }

            public ListViewItem Item { get; set; }

            public int SubItemIndex { get; set; }

            public ListViewItem.ListViewSubItem SubItem
            {
                get
                {
                    if (Item == null) return null;
                    if ((SubItemIndex < 0) || (SubItemIndex >= Item.SubItems.Count)) return null;
                    return Item.SubItems[SubItemIndex];
                }
            }
        }

        #endregion

        #region Interop

        private const uint LVM_FIRST = 0x1000;
        private const uint LVM_SCROLL = (LVM_FIRST + 20);

        private const int WM_HSCROLL = 0x114;
        private const int WM_VSCROLL = 0x115;
        private const int WM_MOUSEWHEEL = 0x020A;
        private const int WM_PAINT = 0x000F;

        [DllImport("user32.dll")]
        private static extern bool SendMessage(IntPtr hWnd, UInt32 m, int wParam, int lParam);

        #endregion

        private List<EmbeddedControl> embeddedControls = new List<EmbeddedControl>();
        private Padding controlPadding = new Padding(4);

        private Brush sortColumnBrush = SystemBrushes.ControlLight;
        private Brush highlightBrush = SystemBrushes.Highlight;

        private Dictionary<int, SortInformation> sortProperties = new Dictionary<int, SortInformation>();
        private SortInformation sortTag = new SortInformation();

        private ImageList backupImageList = null;

        private int rowHeight = 16;

        // Stores data allowing adding automatically objects.
        private Type currentType = null;
        private Dictionary<PropertyInfo, ColumnHeader> columnsDictionary = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="ListViewEx"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public ListViewEx(System.ComponentModel.IContainer container)
        {
            container.Add(this);
            InitializeControl();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ListViewEx"/> class.
        /// </summary>
        public ListViewEx() { InitializeControl(); }


        /// <summary>
        /// Gets or sets a value indicating whether clicking an item selects all its subitems.
        /// </summary>
        /// <value></value>
        /// <returns>true if clicking an item selects the item and all its subitems; false if clicking an item selects only the item itself. The default is false.
        /// </returns>
        /// <PermissionSet>
        /// 	<IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
        /// 	<IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
        /// 	<IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/>
        /// 	<IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
        /// </PermissionSet>
        [DefaultValue(true)]
        public new bool FullRowSelect
        {
            get { return base.FullRowSelect; }
            set { base.FullRowSelect = value; }
        }

        /// <summary>
        /// Gets or sets the height of each row of this list view.
        /// </summary>
        /// <value>The height of the row.</value>
        public int RowHeight
        {
            get { return rowHeight; }
            set
            {
                if (rowHeight != value)
                {
                    rowHeight = value;
                    UpdateRowHeight();
                }
            }
        }

        // TODO: should test that RowHeight and SmallImageList work well together.
        public new ImageList SmallImageList
        {
            get
            {
                if (backupImageList == null) return base.SmallImageList;
                return backupImageList;
            }
            set
            {
                base.SmallImageList = value;
                UpdateRowHeight();
            }
        }

        /// <summary>
        /// Renvoie l'index du premier élément sélectionné.
        /// </summary>
        /// <remarks>
        /// Si aucun élément n'est sélectionné, renvoie -1.
        /// </remarks>
        /// <returns>Index du premier élément sélectionné ou -1</returns>
        public int GetFirstSelectedIndex()
        {
            if (SelectedIndices.Count > 0)
                return SelectedIndices[0];
            else return -1;
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public new bool AllowColumnReorder
        {
            get { return base.AllowColumnReorder; }
            set { base.AllowColumnReorder = false; }
        }

        [Browsable(true), DefaultValue(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool SortColumnOnClick { get; set; }

        public Padding ControlPadding
        {
            get { return controlPadding; }
            set { controlPadding = value; }
        }

        /// <summary>
        /// Redéfinit le comportement sur WM_PAINT afin
        /// de dimensionner la dernière colonne au maximum
        /// </summary>
        /// <param name="m">Message Windows</param>
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            if (base.DesignMode)
            {
                base.WndProc(ref m);
                return;
            }

            switch (m.Msg)
            {
                case WM_PAINT:
                    if (base.View != View.Details) break;
                    // Last column is full width
                    if (base.Columns.Count > 0) base.Columns[base.Columns.Count - 1].Width = -2;

                    foreach (var ec in embeddedControls)
                    {
                        var bounds = GetSubItemBounds(ec.Item, ec.SubItemIndex);

                        // Does control overlap the column header?
                        if ((base.HeaderStyle != ColumnHeaderStyle.None) && (bounds.Top < base.Font.Height))
                            ec.Control.Visible = false;
                        else if ((bounds.Y > 0) && (bounds.Y < ClientRectangle.Height))
                        {
                            ec.Control.Visible = true;
                            ec.Control.Bounds = new Rectangle(
                                bounds.X + controlPadding.Left,
                                bounds.Y + controlPadding.Top,
                                bounds.Width - controlPadding.Left - controlPadding.Right,
                                bounds.Height - controlPadding.Top - controlPadding.Bottom);
                        }
                        else ec.Control.Visible = false;
                    }
                    break;

                case WM_HSCROLL:
                case WM_VSCROLL:
                case WM_MOUSEWHEEL:
                    base.Focus();
                    break;
            }

            base.WndProc(ref m);
        }

        /// <summary>Adds a control to a given sub-item.</summary>
        /// <param name="control">The control to add.</param>
        /// <param name="item">The item parent of the sub-item we want the control be added.</param>
        /// <param name="index">The index of the sub-item inside the item.</param>
        public void AddControlToSubItem(Control control, ListViewItem item, int index)
        {
            var ec = new EmbeddedControl()
            {
                Control = control,
                Item = item,
                SubItemIndex = index
            };

            var subItem = ec.SubItem;
            if (subItem != null)
            {
                // It should follow the subItem size, and not be docked to 
                // the listview itself. But the subItem is not a control!
                // TODO: use the specified dock & anchor properties do determine
                // the way the control should be layed out into the sub item.
                if (control.Dock == DockStyle.Fill) control.Dock = DockStyle.None;

                base.Controls.Add(control);
                //subItem.Control = control;
                embeddedControls.Add(ec);
                control.Click += new EventHandler(OnEmbeddedControlClick);
            }
            else throw new ArgumentException(string.Format("No ListViewExControlSubitem at index {0}", index), "index");
        }

        /// <summary>Removes the control from a given sub-item.</summary>
        /// <param name="item">The item parent of the sub-item we want the control be removed.</param>
        /// <param name="index">The index of the sub-item inside the item.</param>
        public void RemoveControlFromSubItem(ListViewItem item, int index)
        {
            int ecIndex = -1;
            EmbeddedControl ec = null;

            for (int i = 0; i < embeddedControls.Count; i++)
            {
                ec = embeddedControls[i];
                if ((ec.Item == item) & (ec.SubItemIndex == index))
                {
                    ecIndex = i;
                    break;
                }
            }

            if (index != -1)
            {
                var control = ec.Control;
                if (control != null)
                {
                    control.Click -= new EventHandler(OnEmbeddedControlClick);
                    base.Controls.Remove(control);
                    control.Dispose();
                }

                embeddedControls.RemoveAt(ecIndex);
            }
        }

        /// <summary>
        /// Renvoie le premier élément sélectionné.
        /// </summary>
        /// <remarks>
        /// Si aucun élément n'est sélectionné, renvoie null.
        /// </remarks>
        /// <returns>Premier élément sélectionné ou null</returns>
        public ListViewItem GetFirstSelectedItem()
        {
            if (SelectedItems.Count > 0) return SelectedItems[0];
            else return null;
        }

        /// <summary>
        /// Cette méthode permet de spécifier un type d'ordre pour une colonne
        /// (Numérique ou Chaîne)
        /// </summary>
        /// <param name="columnIndex">Numéro de la colonne à paramétrer</param>
        /// <param name="sortType">Type of the sort.</param>
        /// <returns></returns>
        /// <remarks>
        /// Si le numéro de colonne est -1, alors, on spécifie les
        /// paramètres de tri pour la propriété Tag.
        /// </remarks>
        public bool SetColumnOrderType(int columnIndex, SortType sortType)
        {
            UpdateSortInfos();

            if (columnIndex == -1) sortTag.Type = sortType;
            else
            {
                if (Columns.Count <= columnIndex) return false;
                else
                {
                    SortInformation info = sortProperties[columnIndex];
                    if (info == null) return false;
                    else info.Type = sortType;
                }
            }

            return true;
        }

        /// <summary>
        /// Sorts the list according to the specified column index.
        /// </summary>
        /// <param name="columnIndex">Index of the column.</param>
        public void Sort(int columnIndex)
        {
            SortInformation info = null;

            UpdateSortInfos();

            bool ok = true;

            if (columnIndex == -1)
            {
                base.ListViewItemSorter = new ListViewItemComparer(-1, sortTag);
                info = sortTag;
            }
            else
            {
                if (columnIndex < 0) ok = false;
                if (Columns.Count <= columnIndex) ok = false;

                info = sortProperties[columnIndex];
                if (info == null) ok = false;

                if (ok) base.ListViewItemSorter = new ListViewItemComparer(columnIndex, info);
            }

            if (ok)
            {
                base.Sort();
                // Enfin, on inverse l'ordre de tri pour la prochaine fois.
                if (info != null) info.Direction = (
                    info.Direction == SortDirection.Ascending ? SortDirection.Descending : SortDirection.Ascending);
            }
        }

        #region Direct objects management

        /// <summary>
        /// Adds an object to the list.
        /// </summary>
        /// <param name="o">The object.</param>
        public void Add(object o)
        {
            if (o == null) return;

            if ((currentType != null) && (!currentType.IsAssignableFrom(o.GetType())))
                throw new ApplicationException(string.Format(
                    "You must add objects of the same type (or derived): {0}, or call the CleanObjects method.", currentType));

            if (currentType == null)
            {
                // First determine the column headers, based on the object public properties & fields
                currentType = o.GetType();
                columnsDictionary = new Dictionary<PropertyInfo, ColumnHeader>();
                var properties = currentType.GetProperties();
                foreach (var pi in properties)
                {
                    if (!columnsDictionary.ContainsKey(pi))
                    {
                        string text = GetHeaderTextFromPropertyInfo(o, pi);
                        ColumnHeader header = null;
                        if (base.Columns.ContainsKey(pi.Name))
                            header = base.Columns[pi.Name];
                        else header = base.Columns.Add(pi.Name, text);

                        columnsDictionary.Add(pi, header);
                    }
                }
            }

            // Create a corresponding ListViewItem
            var item = new ListViewItem();
            item.Tag = o;

            var subItems = new Dictionary<int, ListViewItem.ListViewSubItem>();

            for (int i = 0; i < base.Columns.Count; i++)
            {
                var q = from k in columnsDictionary.Keys
                        where columnsDictionary[k].Index == i
                        select k;
                var pi = q.FirstOrDefault();

                if (pi == null) subItems.Add(i, new ListViewItem.ListViewSubItem() { Text = string.Empty });
                else subItems.Add(i, new ListViewItem.ListViewSubItem() { Text = GetValueFromPropertyInfo(o, pi) });
            }

            for (int i = 0; i < base.Columns.Count; i++)
            {
                if (i == 0) item.Text = subItems[i].Text;
                else item.SubItems.Add(subItems[i]);
            }


            base.Items.Add(item); // Add it
        }

        /// <summary>
        /// Removes a previously added object.
        /// </summary>
        /// <param name="o">The object to remove.</param>
        public void Remove(object o)
        {
            var itemsToRemove = new List<ListViewItem>();
            foreach (ListViewItem lvi in base.Items)
            {
                if (lvi.Tag == o) itemsToRemove.Add(lvi);
            }

            foreach (var lvi in itemsToRemove) base.Items.Remove(lvi);
        }

        /// <summary>
        /// Clears the objects associated with items.
        /// </summary>
        public void ClearObjects()
        {
            base.Clear();
            columnsDictionary = null;
            currentType = null;
        }

        private string GetHeaderTextFromPropertyInfo(object o, PropertyInfo property)
        {
            var name = property.Name;

            foreach (Attribute attribute in property.GetCustomAttributes(true))
            {
                // If we find a SRName attribute, we don't want to continue searching. 
                // We also override an existing Name attribute.
                if (attribute is SRNameAttribute)
                {
                    string resName = ((SRNameAttribute)attribute).ResourceName;
                    string result = string.Empty;
                    try { result = ResourceLocator.GetString(resName, o.GetType().Assembly); }
                    catch (Exception ex)
                    {
                        var debugEx = ex;
                    }
                    if (!string.IsNullOrEmpty(result))
                    {
                        name = result;
                        break;
                    }
                }

                if (attribute is NameAttribute)
                    name = ((NameAttribute)attribute).Name;
            }

            return name;
        }

        private string GetValueFromPropertyInfo(object o, PropertyInfo pi)
        {
            var get = pi.GetGetMethod();
            if (get != null)
            {
                var result = get.Invoke(o, new object[] { });
                if (result != null) return result.ConvertToString();
                else return string.Empty;
            }
            else return string.Empty;
        }

        #endregion

        /// <summary>
        /// Called when an embedded control is clicked.
        /// </summary>
        /// <param name="sender">The sender (the embedded control).</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected virtual void OnEmbeddedControlClick(object sender, EventArgs e)
        {
            // When a control is clicked the ListViewItem holding it is selected
            foreach (EmbeddedControl ec in embeddedControls)
            {
                if (ec.Control == (Control)sender)
                {
                    base.SelectedItems.Clear();
                    ec.Item.Selected = true;
                    break;
                }
            }
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.ListView.ColumnClick"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.ColumnClickEventArgs"/> that contains the event data.</param>
        protected override void OnColumnClick(ColumnClickEventArgs e)
        {
            //TODO: call Sort(columnIndex);

            base.OnColumnClick(e);
            if (!SortColumnOnClick) return;

            UpdateSortInfos();

            bool asc = true;
            SortType sortType = SortType.String;

            SortInformation info = sortProperties[e.Column];
            if (info != null)
            {
                asc = (info.Direction == SortDirection.Ascending);
                sortType = info.Type;
            }

            // On trie
            base.ListViewItemSorter = new ListViewItemComparer(e.Column, asc, sortType);
            base.Sort();

            // Enfin, on inverse l'ordre de tri pour la prochaine fois.
            if (info != null) info.Direction = (
                    info.Direction == SortDirection.Ascending ? SortDirection.Descending : SortDirection.Ascending);

        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.ListView.DrawColumnHeader"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.DrawListViewColumnHeaderEventArgs"/> that contains the event data.</param>
        protected override void OnDrawColumnHeader(DrawListViewColumnHeaderEventArgs e)
        {
            e.DrawDefault = true;
            base.OnDrawColumnHeader(e);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.ListView.DrawSubItem"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.DrawListViewSubItemEventArgs"/> that contains the event data.</param>
        protected override void OnDrawSubItem(DrawListViewSubItemEventArgs e)
        {
            e.DrawDefault = true;
            base.OnDrawSubItem(e);
        }

        /// <summary>
        /// Gets the bounds for a given sub-item
        /// </summary>
        /// <param name="item">The item parent of the sub-item for which we request the bounds.</param>
        /// <param name="index">The index of the sub-item.</param>
        /// <returns>The bounding rectangle of the sub-item.</returns>
        private Rectangle GetSubItemBounds(ListViewItem item, int index)
        {
            if (item == null) throw new ArgumentNullException("item");
            if ((index < 0) || (index >= item.SubItems.Count))
                throw new ArgumentOutOfRangeException("index");

            // Retrieve the bounds of the entire ListViewItem (all sub-items)
            Rectangle lviBounds = item.GetBounds(ItemBoundsPortion.Entire);
            int left = lviBounds.Left;

            // Compute the X position of the sub-item.
            ColumnHeader col = base.Columns[index];
            for (int i = 0; i < index; i++)
                left += base.Columns[i].Width;

            return new Rectangle(left, lviBounds.Top, base.Columns[index].Width, lviBounds.Height);
        }

        /// <summary>
        /// Initializes the control.
        /// </summary>
        private void InitializeControl()
        {
            base.OwnerDraw = true;
            base.FullRowSelect = true;
            base.View = View.Details;

            SortColumnOnClick = true; // default value
        }

        private void UpdateRowHeight()
        {
            if ((base.SmallImageList != null) && (base.SmallImageList.ImageSize.Height == rowHeight)) return;

            var il = new ImageList() { ImageSize = new Size(1, rowHeight) };
            if (base.SmallImageList != null)
            {
                backupImageList = base.SmallImageList;
                il.TransparentColor = base.SmallImageList.TransparentColor;
                il.Images.AddRange(base.SmallImageList.Images.Cast<Image>().ToArray());
            }

            base.SmallImageList = il;
        }

        /// <summary>
        /// Recherche et met à jour la table interne gardant trace
        /// des types de tri (et de leur ordre) pour chaque colonne.
        /// </summary>
        private void UpdateSortInfos()
        {
            if (sortProperties.Count < Columns.Count)
            {
                foreach (ColumnHeader ch in Columns)
                {
                    if (!sortProperties.ContainsKey(ch.Index))
                        sortProperties.Add(ch.Index, new SortInformation());
                }
            }
        }

        /// <summary>Sends a scroll message to this control.</summary>
        /// <param name="x">The amount of pixels to scroll horizontally.</param>
        /// <param name="y">The amount of pixels to scroll vertically.</param>
        private void SendScrollMessage(int x, int y)
        {
            SendMessage(base.Handle, LVM_SCROLL, x, y);
        }
    }
}
