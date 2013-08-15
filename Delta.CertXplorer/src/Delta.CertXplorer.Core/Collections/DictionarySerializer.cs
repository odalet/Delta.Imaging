using System;
using System.IO;
using System.Xml;
using System.Linq;
using System.Reflection;
using System.ComponentModel;
using System.Collections.Generic;

namespace Delta.CertXplorer.Collections
{
    /// <summary>
    /// This class helps in deserializing an XML file (containing key/value pairs) into a dictionary.
    /// </summary>
    public class DictionarySerializer
    {
        private const BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

        private const string rootNodeName = "settings";
        private const string itemNodeName = "setting";
        private const string defaultKeyAttributeName = "name";
        private const string defaultValueAttributeName = "value";
        private static readonly string[] allowedKeyAttributeNames = { defaultKeyAttributeName, "key" };
        private static readonly string[] allowedValueAttributeNames = { defaultValueAttributeName };

        private string currentFileName = string.Empty;

        /// <summary>
        /// Deserializes an xml file into the specified target.
        /// </summary>
        /// <param name="target">The target dictionary.</param>
        /// <param name="fileName">Name of the XML file.</param>
        public void Deserialize(IDictionary<string, string> target, string fileName)
        {
            if (target == null) throw new ArgumentNullException("target");
            if (string.IsNullOrEmpty(fileName)) throw new ArgumentNullException("fileName");
            if (!File.Exists(fileName)) throw new FileNotFoundException(
                string.Format(SR.FileNotFound, fileName));

            currentFileName = fileName;

            var doc = new XmlDocument();
            doc.Load(fileName);
            DeserializeFrom(target, doc);
        }

        /// <summary>
        /// Deserializes an xml file into the specified target.
        /// </summary>
        /// <param name="target">The target dictionary.</param>
        /// <param name="doc">Xml document.</param>
        public void Deserialize(IDictionary<string, string> target, XmlDocument doc)
        {
            if (target == null) throw new ArgumentNullException("target");
            if (doc == null) throw new ArgumentNullException("doc");

            currentFileName = string.Empty;
            DeserializeFrom(target, doc);
        }

        /// <summary>
        /// Serializes the specified source data to a file.
        /// </summary>
        /// <param name="source">The source data.</param>
        /// <param name="fileName">Name of the file.</param>
        public void Serialize(IDictionary<string, string> source, string fileName)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (string.IsNullOrEmpty(fileName)) throw new ArgumentNullException("fileName");
            if (File.Exists(fileName))
            {
                try { File.Delete(fileName); }
                catch (Exception ex)
                {
                    throw new ApplicationException(string.Format(SR.FileAlreadyExistsAndCantBeDeleted, fileName), ex);
                }
            }

            currentFileName = fileName;

            var doc = new XmlDocument();
            SerializeTo(source, doc);
            doc.Save(fileName);
        }

        /// <summary>
        /// Serializes the specified source data to an Xml document.
        /// </summary>
        /// <param name="source">The source data.</param>
        /// <param name="doc">The Xml document.</param>
        public void Serialize(IDictionary<string, string> source, XmlDocument doc)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (doc == null) throw new ArgumentNullException("doc");

            currentFileName = string.Empty;
            SerializeTo(source, doc);
        }

        /// <summary>
        /// Converts the dictionary passed here into a new object of type <typeparamref name="T"/>.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If the requested type is a <see cref="IDictionary{T, T}"/> and <c>T</c> is <see cref="System.String"/>, 
        /// we just return a copy of our internal dictionary.
        /// </para>
        /// <para>
        /// We try to map the keys found in the source dictionary with properties of the target
        /// object.
        /// </para>
        /// <para>
        /// <b>Important: the target object type must provide a parameterless constructor.</b>
        /// </para>
        /// </remarks>
        /// <typeparam name="T">Type of the target object.</typeparam>
        /// <param name="dictionary">The source dictionary.</param>
        /// <returns>A filled instance of the target object.</returns>
        public T CreateObject<T>(IDictionary<string, string> dictionary) where T : class, new()
        {
            var targetType = typeof(T);
            var target = (T)targetType.CreateInstance();

            FillObject(dictionary, target);
            return target;
        }

        /// <summary>
        /// Read the settings dictionary<paramref name="dictionary"/> and 
        /// fills the object <paramref name="target"/>. by mapping the 
        /// dictionary keys with the target object properties.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If the requested type is a <see cref="IDictionary{T, T}"/> and <c>T</c> is <see cref="System.String"/>, 
        /// we just copy our internal dictionary into the target dictionary.
        /// </para>
        /// <para>
        /// We try to map the keys found in the source dictionary with properties of the target
        /// object.
        /// </para>
        /// </remarks>
        /// <typeparam name="T">Type of the target object.</typeparam>
        /// <param name="dictionary">The source dictionary.</param>
        /// <param name="target">The target object to fill.</param>
        public void FillObject<T>(IDictionary<string, string> dictionary, T target) where T : class
        {
            var targetType = typeof(T);

            // A null dictionary is equivalent to an empty dictionary;
            // this way, we can manage default values.
            if (dictionary == null) dictionary = new Dictionary<string, string>();

            // If the requested type is a Dictionary<string, string>, just return
            // a copy of our internal dictionary
            if (target is IDictionary<string, string>)
            {
                var targetDictionary = (IDictionary<string, string>)target;
                foreach (var key in dictionary.Keys)
                    targetDictionary.Add(key, dictionary[key]);
            }
            else foreach (var pi in targetType.GetProperties(bindingFlags))
                {
                    var set = pi.GetSetMethod(true);
                    if (set != null) // we ignore read-only properties.
                    {
                        object value = null;
                        if (dictionary.ContainsKey(pi.Name))
                            value = dictionary[pi.Name];
                        else // Do we have a default value?
                        {
                            var attribute = pi.GetCustomAttributes(typeof(DefaultValueAttribute), false)
                                .FirstOrDefault() as DefaultValueAttribute;
                            if (attribute != null) value = attribute.Value;
                        }

                        var targetValue = value.ConvertToType(pi.PropertyType);
                        if (targetValue != null) set.Invoke(target, new object[] { targetValue });
                    }
                }
        }

        public void UpdateDictionary<T>(IDictionary<string, string> dictionary, T source) where T : class
        {
            if (dictionary == null) throw new ArgumentNullException("dictionary");
            if (source == null) return;

            var sourceType = typeof(T);

            // if the source object is a Dictionary<string, string>, just copy 
            // it into our internal dictionary.
            if (source is IDictionary<string, string>)
            {
                var sourceDictionary = (IDictionary<string, string>)source;
                foreach (var key in sourceDictionary.Keys)
                {
                    if (dictionary.ContainsKey(key))
                        dictionary[key] = sourceDictionary[key];
                    else dictionary.Add(key, sourceDictionary[key]);
                }
            }
            else foreach (var pi in sourceType.GetProperties(bindingFlags))
                {
                    var get = pi.GetGetMethod(true);
                    if (get != null) // we ignore write-only properties.
                    {
                        var objectValue = get.Invoke(source, new object[] { });
                        string stringValue = objectValue.ConvertToString();
                        var key = pi.Name;
                        if (dictionary.ContainsKey(key)) dictionary[key] = stringValue;
                        else dictionary.Add(key, stringValue);
                    }
                }
        }

        #region Overridable Xml tag names

        protected virtual StringComparison StringComparison
        {
            get { return StringComparison.Ordinal; }
        }

        protected virtual string RootNodeName { get { return rootNodeName; } }

        protected virtual string ItemNodeName { get { return itemNodeName; } }

        protected virtual string DefaultKeyAttributeName { get { return defaultKeyAttributeName; } }

        protected virtual string DefaultValueAttributeName { get { return defaultValueAttributeName; } }

        protected virtual string[] AllowedKeyAttributeNames
        {
            get { return allowedKeyAttributeNames; }
        }

        protected virtual string[] AllowedValueAttributeNames
        {
            get { return allowedValueAttributeNames; }
        }

        #endregion

        private string CurrentFileName
        {
            get
            {
                if (string.IsNullOrEmpty(currentFileName)) return "?";
                else return currentFileName;
            }
        }

        #region XML serialization

        private void SerializeTo(IDictionary<string, string> source, XmlDocument doc)
        {
            XmlNode xnRoot = null;

            if (!doc.HasChildNodes)
            {
                doc.AppendChild(doc.CreateXmlDeclaration("1.0", "utf-8", string.Empty));
                xnRoot = doc.CreateElement(RootNodeName);
                doc.AppendChild(xnRoot);
            }
            else
            {
                foreach (XmlNode xn in doc.ChildNodes)
                {
                    if (xn.Name.Equals(RootNodeName, StringComparison))
                    {
                        xnRoot = xn;
                        break;
                    }
                }

                if (xnRoot == null)
                {
                    xnRoot = doc.CreateElement(RootNodeName);
                    doc.AppendChild(xnRoot);
                }
            }

            //We store the existing nodes for future use.
            var existingNodes = new Dictionary<string, XmlNode>();
            if (xnRoot.HasChildNodes)
            {
                foreach (XmlNode xn in xnRoot.ChildNodes)
                {
                    var key = string.Empty;
                    var attributes = xn.Attributes;
                    if ((attributes == null) || (attributes.Count == 0))
                        continue;

                    foreach (XmlAttribute xa in xn.Attributes)
                    {
                        if (AllowedKeyAttributeNames.Contains(xa.Name, StringComparison.ToStringComparer()))
                        {
                            key = xa.Value;
                            break;
                        }
                    }

                    if (!existingNodes.ContainsKey(key) && !string.IsNullOrEmpty(key))
                        existingNodes.Add(key, xn);
                }
            }

            foreach (string key in source.Keys)
            {
                if (existingNodes.ContainsKey(key))
                {
                    var xn = existingNodes[key];
                    XmlAttribute xaValue = null;
                    foreach (XmlAttribute xa in xn.Attributes)
                    {
                        if (AllowedValueAttributeNames.Contains(xa.Name, StringComparison.ToStringComparer()))
                            xaValue = xa;
                    }

                    if (xaValue == null)
                    {
                        xaValue = doc.CreateAttribute(DefaultValueAttributeName);
                        xn.Attributes.Append(xaValue);
                    }

                    xaValue.Value = source[key];
                }
                else
                {
                    XmlElement item = doc.CreateElement(ItemNodeName);
                    XmlAttribute xaKey = doc.CreateAttribute(DefaultKeyAttributeName);
                    XmlAttribute xaValue = doc.CreateAttribute(DefaultValueAttributeName);

                    xaKey.Value = key;
                    xaValue.Value = source[key];

                    item.Attributes.Append(xaKey);
                    item.Attributes.Append(xaValue);

                    xnRoot.AppendChild(item);
                }
            }
        }

        #endregion

        #region XML deserialization

        /// <summary>
        /// Deserializes the Xml document <paramref name="doc"/> into the specified target.
        /// </summary>
        /// <remarks>
        /// The only reason we pass the Xml file name to this method is to be able to trace
        /// failures.
        /// </remarks>
        /// <param name="target">The target dictionary.</param>
        /// <param name="doc">The Xml document.</param>
        private void DeserializeFrom(IDictionary<string, string> target, XmlDocument doc)
        {
            foreach (XmlNode xnRoot in doc.ChildNodes)
            {
                if (xnRoot.Name.Equals(RootNodeName, StringComparison))
                {
                    foreach (XmlNode xn in xnRoot.ChildNodes)
                    {
                        if (xn.Name.Equals(ItemNodeName, StringComparison))
                            ParseKeyValuePair(target, xn);
                    }
                }
            }
        }

        /// <summary>
        /// Parses a key value pair from an Xml node into the specified target.
        /// </summary>
        /// <remarks>
        /// The only reason we pass the Xml file name to this method is to be able to trace
        /// failures.
        /// </remarks>
        /// <param name="target">The target dictionary.</param>
        /// <param name="xn">The Xml node.</param>
        private void ParseKeyValuePair(IDictionary<string, string> target, XmlNode xn)
        {
            var key = string.Empty;
            var value = string.Empty;
            foreach (XmlAttribute xa in xn.Attributes)
            {
                if (AllowedKeyAttributeNames.Contains(xa.Name, StringComparison.ToStringComparer()))
                    key = xa.Value;

                if (AllowedValueAttributeNames.Contains(xa.Name, StringComparison.ToStringComparer()))
                    value = xa.Value;
            }

            if (string.IsNullOrEmpty(value))
            {
                value = xn.InnerXml;
            }

            if (!string.IsNullOrEmpty(key))
            {
                if (target.ContainsKey(key)) This.Logger.Warning(string.Format(
                    SR.DuplicateKeyInSettingsFile, key, CurrentFileName));
                else target.Add(key, value);
            }
        }

        #endregion
    }
}
