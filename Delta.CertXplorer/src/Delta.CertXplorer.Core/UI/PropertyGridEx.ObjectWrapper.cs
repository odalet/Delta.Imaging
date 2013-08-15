using System;
using System.Linq;
using System.Diagnostics;
using System.ComponentModel;

namespace Delta.CertXplorer.UI
{
    partial class PropertyGridEx
    {
        /// <summary>
        /// Wraps a real object so that we add custom behavior (mainly redefining properties)
        /// before the object is displayed in the property grid.
        /// </summary>
        protected class ObjectWrapper : ICustomTypeDescriptor, IComponent
        {
            private object wrappedObject = null;
            private PropertyDescriptorCollection properties = null;
            private PropertyGridEx grid = null;

            /// <summary>
            /// Initializes a new instance of the <see cref="ObjectWrapper"/> class.
            /// </summary>
            /// <param name="owner">The parent proerty grid.</param>
            /// <param name="objectToWrap">The object to wrap.</param>
            public ObjectWrapper(PropertyGridEx owner, object objectToWrap)
            {
                if (owner == null) throw new ArgumentNullException("owner");
                if (objectToWrap == null) throw new ArgumentNullException("objectToWrap");
                wrappedObject = objectToWrap;
                grid = owner;
            }

            private PropertyDescriptorCollection GetProps(Attribute[] attributes)
            {
                // Only do once
                if (properties == null)
                {
                    PropertyDescriptorCollection baseProps = null;
                    if (attributes == null) baseProps = TypeDescriptor.GetProperties(wrappedObject, true);
                    else baseProps = TypeDescriptor.GetProperties(wrappedObject, attributes, true);
                    properties = new PropertyDescriptorCollection(null);

                    // For each property use a property descriptor of our own that is able to be globalized
                    foreach (PropertyDescriptor property in baseProps)
                        properties.Add(CreatePropertyDescriptor(wrappedObject, property));
                }
                return properties;
            }

            /// <summary>
            /// Creates the custom inner property descriptor.
            /// </summary>
            /// <param name="objectToWrap">The object to wrap.</param>
            /// <param name="originalDescriptor">The original descriptor.</param>
            /// <returns>a <see cref="System.ComponentModel.PropertyDescriptor"/> instance.</returns>
            protected virtual PropertyDescriptor CreatePropertyDescriptor(object objectToWrap, PropertyDescriptor originalDescriptor)
            {
                return grid.CreatePropertyDescriptor(objectToWrap, originalDescriptor);
            }

            #region ICustomTypeDescriptor Members

            /// <summary>
            /// Returns the properties for this instance of a component using the attribute array as a filter.
            /// </summary>
            /// <param name="attributes">An array of type <see cref="T:System.Attribute"/> that is used as a filter.</param>
            /// <returns>
            /// A <see cref="T:System.ComponentModel.PropertyDescriptorCollection"/> that represents the filtered properties for this component instance.
            /// </returns>
            public PropertyDescriptorCollection GetProperties(Attribute[] attributes) { return GetProps(attributes); }

            /// <summary>
            /// Returns the properties for this instance of a component.
            /// </summary>
            /// <returns>
            /// A <see cref="T:System.ComponentModel.PropertyDescriptorCollection"/> that represents the properties for this component instance.
            /// </returns>
            public PropertyDescriptorCollection GetProperties() { return GetProps(null); }

            /// <summary>
            /// Returns a type converter for this instance of a component.
            /// </summary>
            /// <returns>
            /// A <see cref="T:System.ComponentModel.TypeConverter"/> that is the converter for this object, or null if there is no <see cref="T:System.ComponentModel.TypeConverter"/> for this object.
            /// </returns>
            public TypeConverter GetConverter() { return TypeDescriptor.GetConverter(wrappedObject, true); }

            /// <summary>
            /// Returns the events for this instance of a component using the specified attribute array as a filter.
            /// </summary>
            /// <param name="attributes">An array of type <see cref="T:System.Attribute"/> that is used as a filter.</param>
            /// <returns>
            /// An <see cref="T:System.ComponentModel.EventDescriptorCollection"/> that represents the filtered events for this component instance.
            /// </returns>
            public EventDescriptorCollection GetEvents(Attribute[] attributes) { return TypeDescriptor.GetEvents(wrappedObject, attributes, true); }

            /// <summary>
            /// Returns the events for this instance of a component.
            /// </summary>
            /// <returns>
            /// An <see cref="T:System.ComponentModel.EventDescriptorCollection"/> that represents the events for this component instance.
            /// </returns>
            public EventDescriptorCollection GetEvents() { return TypeDescriptor.GetEvents(wrappedObject, true); }

            /// <summary>
            /// Returns the name of this instance of a component.
            /// </summary>
            /// <returns>
            /// The name of the object, or null if the object does not have a name.
            /// </returns>
            public string GetComponentName() { return TypeDescriptor.GetComponentName(wrappedObject, true); }

            /// <summary>
            /// Returns a collection of custom attributes for this instance of a component.
            /// </summary>
            /// <returns>
            /// An <see cref="T:System.ComponentModel.AttributeCollection"/> containing the attributes for this object.
            /// </returns>
            public AttributeCollection GetAttributes() { return TypeDescriptor.GetAttributes(wrappedObject, true); }

            /// <summary>
            /// Returns the default property for this instance of a component.
            /// </summary>
            /// <returns>
            /// A <see cref="T:System.ComponentModel.PropertyDescriptor"/> that represents the default property for this object, or null if this object does not have properties.
            /// </returns>
            public PropertyDescriptor GetDefaultProperty() { return TypeDescriptor.GetDefaultProperty(wrappedObject, true); }

            /// <summary>
            /// Returns the default event for this instance of a component.
            /// </summary>
            /// <returns>
            /// An <see cref="T:System.ComponentModel.EventDescriptor"/> that represents the default event for this object, or null if this object does not have events.
            /// </returns>
            public EventDescriptor GetDefaultEvent() { return TypeDescriptor.GetDefaultEvent(wrappedObject, true); }

            /// <summary>
            /// Returns the class name of this instance of a component.
            /// </summary>
            /// <returns>
            /// The class name of the object, or null if the class does not have a name.
            /// </returns>
            public string GetClassName() { return TypeDescriptor.GetClassName(wrappedObject, true); }

            /// <summary>
            /// Returns an editor of the specified type for this instance of a component.
            /// </summary>
            /// <param name="editorBaseType">A <see cref="T:System.Type"/> that represents the editor for this object.</param>
            /// <returns>
            /// An <see cref="T:System.Object"/> of the specified type that is the editor for this object, or null if the editor cannot be found.
            /// </returns>
            public object GetEditor(Type editorBaseType)
            {
                if (wrappedObject == null) return null;
                return TypeDescriptor.GetEditor(wrappedObject, editorBaseType, true);
            }

            /// <summary>
            /// Returns an object that contains the property described by the specified property descriptor.
            /// </summary>
            /// <param name="pd">A <see cref="T:System.ComponentModel.PropertyDescriptor"/> that represents the property whose owner is to be found.</param>
            /// <returns>
            /// An <see cref="T:System.Object"/> that represents the owner of the specified property.
            /// </returns>
            public object GetPropertyOwner(PropertyDescriptor pd) 
            {
                StackTrace trace = new StackTrace();
                var found = trace.GetFrames().FirstOrDefault(f => f.GetMethod().Name == "DisplayHotCommands");
                if (found == null) return wrappedObject; // normal call, we return the wrapped object
                else
                {
                    Console.WriteLine("Call emmitted from DisplayHotCommands");
                    return this;
                }
            }

            #endregion

            #region IComponent Members

            /// <summary>
            /// Represents the method that handles the <see cref="E:System.ComponentModel.IComponent.Disposed"/> event of a component.
            /// </summary>
            public event EventHandler Disposed;

            /// <summary>
            /// Gets or sets the <see cref="T:System.ComponentModel.ISite"/> associated with the <see cref="T:System.ComponentModel.IComponent"/>.
            /// </summary>
            /// <value></value>
            /// <returns>
            /// The <see cref="T:System.ComponentModel.ISite"/> object associated with the component; or null, if the component does not have a site.
            /// </returns>
            public ISite Site { get; set; }

            #endregion

            #region IDisposable Members

            /// <summary>
            /// Performs application-defined tasks associated with freeing, 
            /// releasing, or resetting unmanaged resources.
            /// </summary>
            public void Dispose()
            {
                // nothing to do here apart from invoking the event
                if (Disposed != null) Disposed(this, EventArgs.Empty);
            }

            #endregion
        }
    }
}
