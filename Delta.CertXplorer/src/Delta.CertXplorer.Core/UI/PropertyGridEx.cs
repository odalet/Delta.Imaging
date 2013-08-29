using System;
using System.Windows.Forms;
using System.ComponentModel;

using Delta.CertXplorer.UI.Theming;

namespace Delta.CertXplorer.UI
{
    /// <summary>
    /// Cette <see cref="System.Windows.Forms.PropertyGrid"/> utilise
    /// <see cref="System.Windows.Forms.ToolStripManager"/> pour dessiner sa barre d'outils
    /// </summary>
    public partial class PropertyGridEx : PropertyGrid
    {
        private object[] wrappedObjects = null;
        private object selectedObject = null;
        private object[] selectedObjects = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyGridEx"/> class.
        /// </summary>
        public PropertyGridEx() : base()
        {
            // Properties' default values
            CanOverrideRenderer = true;
            ProtectConnectionStrings = true;
            
            InitializeComponentModel();
            
            ThemesManager.RegisterThemeAwareControl(this, (renderer) =>
            {
                if (CanOverrideRenderer)
                {
                    if (renderer is ToolStripProfessionalRenderer)
                        ((ToolStripProfessionalRenderer)renderer).RoundedEdges = false;

                    base.ToolStripRenderer = renderer;
                }
            });
        }

        /// <summary>
        /// Gets or sets the renderer used by this control's toolstrip and menu.
        /// </summary>
        /// <value>The renderer.</value>
        public ToolStripRenderer Renderer
        {
            get { return base.ToolStripRenderer; }
            set { base.ToolStripRenderer = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to protect connection strings.
        /// </summary>
        /// <remarks>
        /// If set to <c>true</c>, we search for a property named <c>ConnectionString</c>
        /// in the selected objects, if it is found, then it is displayed read-only and 
        /// possible password inside the connection string is hidden (replaced with stars).
        /// </remarks>
        /// <value>
        /// 	<c>true</c> if connection strings should be protected; otherwise, <c>false</c>.
        /// </value>
        [DefaultValue(true)]
        public bool ProtectConnectionStrings { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the global toolstrip renderer 
        /// can be overriden in this control.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this control can override renderer; otherwise, <c>false</c>.
        /// </value>
        [DefaultValue(true)]
        public bool CanOverrideRenderer { get; set; }

        /// <summary>
        /// Gets or sets the object for which the grid displays properties.
        /// </summary>
        /// <value></value>
        /// <returns>The first object in the object list. If there is no currently selected object the return is null.</returns>
        public new object SelectedObject { get { return selectedObject; } set { SelectObject(value); } }

        /// <summary>
        /// Gets or sets the currently selected objects.
        /// </summary>
        /// <value></value>
        /// <returns>An array of type <see cref="T:System.Object"/>. The default is an empty array.</returns>
        /// <exception cref="T:System.ArgumentException">One of the items in the array of objects had a null value. </exception>
        public new object[] SelectedObjects { get { return selectedObjects; } set { SelectObjects(value); } }

        /// <summary>
        /// Creates an object wrapper around the original object.
        /// </summary>
        /// <param name="o">The original object.</param>
        /// <returns>Wrapped object.</returns>
        protected virtual ObjectWrapper CreateObjectWrapper(object o) 
        {
            return new ObjectWrapper(this, o); 
        }

        /// <summary>
        /// Creates the property descriptor used to describe the wrapped object.
        /// </summary>
        /// <param name="toWrap">The wrapped object.</param>
        /// <param name="originalDescriptor">The original property descriptor.</param>
        /// <returns>A Property descriptor.</returns>
        protected internal virtual PropertyDescriptor CreatePropertyDescriptor(object toWrap, PropertyDescriptor originalDescriptor)
        {
            return new InnerPropertyDescriptor(toWrap, originalDescriptor, ProtectConnectionStrings);
        }

        private void SelectObject(object o)
        {
            if (o == null) selectedObject = base.SelectedObject = null;
            else
            {
                selectedObject = o;

                wrappedObjects = new object[1];
                wrappedObjects[0] = CreateObjectWrapper(o);
                OnSelectionAboutToChange();
                base.SelectedObject = wrappedObjects[0];                
            }
        }

        private void SelectObjects(object[] objects)
        {
            if (objects == null) selectedObjects = base.SelectedObjects = null;
            else
            {
                selectedObjects = objects;
                wrappedObjects = new object[objects.Length];

                for (int i = 0; i < objects.Length; i++)
                    wrappedObjects[i] = CreateObjectWrapper(objects[i]);

                OnSelectionAboutToChange();
                base.SelectedObjects = wrappedObjects;
            }
        }
    }
}
