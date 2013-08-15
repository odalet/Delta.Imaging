using System;
using System.IO;
using System.Xml;
using System.Text;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

using WeifenLuo.WinFormsUI.Docking;

using Delta.CertXplorer.UI.ToolWindows;

namespace Delta.CertXplorer.ApplicationModel
{
    /// <summary>
    /// Default implementation of <see cref="ILayoutService"/>.
    /// </summary>
    public class LayoutService : ILayoutService
    {
        #region Events locking management

        private class EventsLock : IDisposable
        {
            private LayoutService parent = null;
            
            public EventsLock(LayoutService layoutService)
            {
                parent = layoutService;
                parent.eventsAreLocked = true;
            }

            #region IDisposable Members

            public void Dispose()
            {
                parent.eventsAreLocked = false;
            }

            #endregion
        }

        private IDisposable LockEvents() { return new EventsLock(this); }

        private bool EventsLocked { get { return eventsAreLocked; } }

        #endregion

        #region Layout store

        private class FormLayout
        {
            public FormLayout() { DockingInfo = string.Empty; }

            public Rectangle? Bounds { get; set; }
            public FormWindowState? WindowState { get; set; }
            public string DockingInfo { get; set; }
            public string AdditionalData { get; set; }
        }

        #endregion

        private bool eventsAreLocked = false;
        private Rectangle defaultBounds = Rectangle.Empty;
        private string layoutSettingsFileName = string.Empty;

        private List<string> loadedList = new List<string>();
        private Dictionary<string, Form> forms = new Dictionary<string, Form>();
        private Dictionary<string, FormLayout> layouts = new Dictionary<string, FormLayout>();
        private Dictionary<string, DockPanel> workspaces = new Dictionary<string, DockPanel>();
        private Dictionary<string, Dictionary<Guid, ToolWindow>> toolWindows = 
            new Dictionary<string, Dictionary<Guid, ToolWindow>>();

        /// <summary>
        /// Initializes a new instance of the <see cref="LayoutService"/> class.
        /// </summary>
        /// <param name="fileName">Name of the file containing the serialized layout information.</param>
        public LayoutService(string fileName)
        {
            layoutSettingsFileName = fileName;
            if (File.Exists(layoutSettingsFileName)) ReadLayouts();
        }

        #region ILayoutService Members

        /// <summary>
        /// Registers a form with the service.
        /// </summary>
        /// <param name="key">The key identifying the form class.</param>
        /// <param name="form">The form instance.</param>
        public void RegisterForm(string key, Form form) { RegisterForm(key, form, null); }

        /// <summary>
        /// Registers a form and its dock panel with the service.
        /// </summary>
        /// <param name="key">The key identifying the form class.</param>
        /// <param name="form">The form instance.</param>
        /// <param name="workspace">The workspace.</param>
        public void RegisterForm(string key, Form form, DockPanel workspace)
        {
            if (forms.ContainsKey(key)) forms.Remove(key);
            forms.Add(key, form);
            LoadForm(key);
            form.Load += (s, e) => LoadForm(key);

            if (workspace != null)
            {
                if (workspaces.ContainsKey(key)) workspaces.Remove(key);
                workspaces.Add(key, workspace);
            }
        }

        /// <summary>
        /// Registers a docking tool window with this service.
        /// </summary>
        /// <param name="key">The key identifying the containing form class.</param>
        /// <param name="window">The tool window instance.</param>
        public void RegisterToolWindow(string key, ToolWindow window)
        {
            if (window == null) throw new ArgumentNullException("window");
            if (!workspaces.ContainsKey(key)) throw new ApplicationException(
                "You can't register a tool window, if a workspace was not registered first.");

            Dictionary<Guid, ToolWindow> subDictionary = null;
            if (toolWindows.ContainsKey(key))
            {
                subDictionary = toolWindows[key];
                if (subDictionary.ContainsKey(window.Guid)) throw new ApplicationException(string.Format(
                    "Tool window with Guid {0} was already registered.", window.Guid));
            }
            else
            {
                subDictionary = new Dictionary<Guid, ToolWindow>();
                toolWindows.Add(key, subDictionary);
            }
            
            subDictionary.Add(window.Guid, window);
        }

        /// <summary>
        /// Restores the docking state of a <see cref="WeifenLuo.WinFormsUI.Docking.DockPanel"/>
        /// and its tool windows.
        /// </summary>
        /// <param name="key">The key identifying the containing form class.</param>
        /// <returns>
        /// 	<c>true</c> if the deserialization was successful; otherwise, <c>false</c>.
        /// </returns>
        public bool RestoreDockingState(string key)
        {
            if (!layouts.ContainsKey(key)) return false;
            if (!workspaces.ContainsKey(key)) return false;

            var dockingXml = layouts[key].DockingInfo;
            try
            {
                using (var stream = new MemoryStream())
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write(dockingXml);
                    writer.Flush();
                    stream.Seek(0L, SeekOrigin.Begin);
                    workspaces[key].LoadFromXml(stream, target =>
                    {
                        Delta.CertXplorer.Logging.ILogService log = This.Logger;

                        if (string.IsNullOrEmpty(target))
                        {
                            log.Info("Unable to load window: no guid was provided");
                            return null;
                        }

                        Guid guid = Guid.Empty;
                        try { guid = new Guid(target); }
                        catch (Exception ex)
                        {
                            var debugException = ex;
                        }

                        if (guid == Guid.Empty)
                        {
                            log.Info(string.Format("Unable to load window with guid {0}.", target));
                            return null;
                        }

                        ToolWindow window = null;
                        if (toolWindows.ContainsKey(key))
                        {
                            var dict = toolWindows[key];
                            if (dict.ContainsKey(guid)) window = dict[guid];
                        }

                        if (window == null)
                        {
                            log.Info(string.Format("Unable to load window with guid {0}.", target));
                            return null;
                        }
                        else return window.ToDockableWindow();

                    }, false);

                    writer.Close();
                }
            }
            catch (Exception ex)
            {
                This.Logger.Error(string.Format(
                    "Unable to restore docking state from the layout key {0}.", key), ex);
                return false;
            }

            return true;
        }

        #endregion

        #region Xml deserialization

        /// <summary>
        /// Reads the layouts.
        /// </summary>
        private void ReadLayouts()
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(layoutSettingsFileName);
                foreach (XmlNode xn in doc.ChildNodes)
                {
                    if (xn.Name == "layouts")
                    {
                        foreach (XmlNode xnRoot in xn.ChildNodes)
                        {
                            if (xnRoot.Name == "layout")
                            {
                                string key = GetNodeValue(xnRoot, "key");
                                if (key != null)
                                {
                                    try { ReadLayout(xnRoot, key); }
                                    catch (Exception ex)
                                    {
                                        This.Logger.Error(string.Format(
                                            "Unable to deserialize layout information from file {0} for key {1}.", 
                                            layoutSettingsFileName, key), ex);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                This.Logger.Error(string.Format(
                    "Unable to deserialize layout information from file {0}.", layoutSettingsFileName), ex);
            }
        }

        /// <summary>
        /// Reads a layout.
        /// </summary>
        /// <param name="xnRoot">The xml root node.</param>
        /// <param name="key">The layout key.</param>
        private void ReadLayout(XmlNode xnRoot, string key)
        {
            if (layouts.ContainsKey(key)) return;

            var fl = new FormLayout();
            foreach (XmlNode xn in xnRoot.ChildNodes)
            {
                if (xn.Name == "bounds")
                {
                    string bounds = GetNodeValue(xn);
                    if (bounds != null)
                    {
                        try { fl.Bounds = bounds.ConvertToType<Rectangle>(); }
                        catch (Exception ex)
                        {
                            This.Logger.Warning(string.Format(
                                "Invalid 'bounds' value for key {0} in layout file {1}: {2}",
                                key, layoutSettingsFileName, bounds), ex);
                            fl.Bounds = null;
                        }
                    }
                    else
                    {
                        This.Logger.Warning(string.Format(
                            "'bounds' value for key {0} in layout file {1} was not found.",
                            key, layoutSettingsFileName));
                        fl.Bounds = null;
                    }
                }
                else if (xn.Name == "state")
                {
                    string state = GetNodeValue(xn);
                    if (state != null)
                    {
                        try { fl.WindowState = state.ConvertToType<FormWindowState>(); }
                        catch (Exception ex)
                        {
                            This.Logger.Warning(string.Format(
                                "Invalid 'state' value for key {0} in layout file {1}: {2}",
                                key, layoutSettingsFileName, state), ex);
                            fl.WindowState = null;
                        }
                    }
                    else
                    {
                        This.Logger.Warning(string.Format(
                            "'state' value for key {0} in layout file {1} was not found.",
                            key, layoutSettingsFileName));
                        fl.WindowState = null;
                    }
                }
                else if (xn.Name == "docking") fl.DockingInfo = xn.InnerXml;
                else if ((xn.Name == "additionalData") && (xn is XmlElement)) 
                    fl.AdditionalData = xn.InnerText;
            }

            layouts.Add(key, fl);
        }

        private string GetNodeValue(XmlNode xn) { return GetNodeValue(xn, "value"); }
        private string GetNodeValue(XmlNode xn, string xaName)
        {
            var xaValue = xn.Attributes.Cast<XmlAttribute>().FirstOrDefault(
                        xa => xa.Name == xaName);
            if (xaValue != null)
                return xaValue.Value;
            else return null;
        }

        #endregion

        #region Xml serialization

        /// <summary>
        /// Saves the layouts.
        /// </summary>
        private void SaveLayouts()
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.AppendChild(doc.CreateXmlDeclaration("1.0", "utf-8", string.Empty));
                var xnRoot = doc.AppendChild(doc.CreateElement("layouts"));
                foreach (string key in layouts.Keys)
                {
                    var xn = doc.CreateElement("layout");
                        
                    var xa = doc.CreateAttribute("key");
                    xa.Value = key;
                    xn.Attributes.Append(xa);

                    // mandatory, if not present, skip this node
                    var layout = layouts[key];
                    if (layout == null) continue;

                    if (layout.Bounds.HasValue)
                    {
                        var xnBounds = xn.AppendChild(doc.CreateElement("bounds"));
                        var xaBoundsValue = doc.CreateAttribute("value");
                        xaBoundsValue.Value = layout.Bounds.ConvertToString();
                        xnBounds.Attributes.Append(xaBoundsValue);
                    }

                    if (layout.WindowState.HasValue)
                    {
                        var xnState = xn.AppendChild(doc.CreateElement("state"));
                        var xaStateValue = doc.CreateAttribute("value");
                        xaStateValue.Value = layout.WindowState.ConvertToString();
                        xnState.Attributes.Append(xaStateValue);
                    }

                    if (!string.IsNullOrEmpty(layout.DockingInfo))
                    {
                        var xnDocking = xn.AppendChild(doc.CreateElement("docking"));
                        xnDocking.InnerXml = layout.DockingInfo;
                    }

                    if (layout.AdditionalData != null)
                    {
                        var xnAdditionalData = xn.AppendChild(doc.CreateElement("additionalData"));
                        xnAdditionalData.InnerText = layout.AdditionalData;
                    }

                    xnRoot.AppendChild(xn);
                }

                doc.Save(layoutSettingsFileName);
            }
            catch (Exception ex)
            {
                This.Logger.Error(string.Format(
                    "Unable to serialize layout information to file {0}.", layoutSettingsFileName), ex);
            }
        }

        #endregion

        #region Registered forms events

        private bool IsLoaded(string key)
        {
            return loadedList.Contains(key);
        }

        /// <summary>
        /// Restores the state and bounds of the form identified by the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        private void LoadForm(string key)
        {
            if (IsLoaded(key)) return;

            using (LockEvents())
            {
                if (!forms.ContainsKey(key)) return;

                var form = forms[key];
                
                if (layouts.ContainsKey(key))
                {
                    var layout = layouts[key];
                    if (layout.Bounds.HasValue)
                    {
                        form.StartPosition = FormStartPosition.Manual;
                        var bounds = layout.Bounds.Value;
                        if (!This.Computer.Screen.WorkingArea.Contains(bounds))
                            bounds = DefaultBounds;
                        form.Bounds = bounds;                        
                    }
                    else This.Logger.Verbose(string.Format("Could not find a 'bounds' value for layout key {0}", key));

                    if (layout.WindowState.HasValue)
                    {
                        var state = layout.WindowState.Value;
                        if (state == FormWindowState.Minimized)
                            form.WindowState = FormWindowState.Normal;
                        else form.WindowState = state;
                    }
                    else
                    {
                        // default to Normal
                        form.WindowState = FormWindowState.Normal;
                        This.Logger.Verbose(string.Format("Could not find a 'state' value for layout key {0}", key));                        
                    }

                    // If state is Normal and we have no bounds, use DefaultBounds and center on screen
                    if ((!layout.Bounds.HasValue) && (form.WindowState == FormWindowState.Normal))
                    {
                        form.Size = DefaultBounds.Size;
                        if (form.ParentForm == null)
                            form.StartPosition = FormStartPosition.CenterScreen;
                        else form.StartPosition = FormStartPosition.CenterParent;
                    }
                }
                else // Creation and initialization
                {                    
                    var newLayout = new FormLayout();
                    if (form.WindowState != FormWindowState.Normal)
                        newLayout.Bounds = DefaultBounds;
                    layouts.Add(key, newLayout);
                }

                form.SizeChanged += (s, e) => UpdateForm(key);
                form.LocationChanged += (s, e) => UpdateForm(key);
                form.FormClosed += (s, e) => UnloadForm(key);

                loadedList.Add(key);

                UpdateForm(key);

                // Additional layout data
                if (form is IAdditionalLayoutDataSource)
                {
                    // Even if no additional data was saved, call SetData.
                    ((IAdditionalLayoutDataSource)form).SetAdditionalLayoutData(layouts[key].AdditionalData);
                }
            }
        }

        /// <summary>
        /// Updates the bounds and state information of the form identified by the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        private void UpdateForm(string key)
        {
            if (EventsLocked) return;
            if (!IsLoaded(key)) return;
                        
            using (LockEvents())
            {
                if (!forms.ContainsKey(key)) return;
                var form = forms[key];
                var layout = layouts[key];

                layout.WindowState = form.WindowState;
                layout.Bounds = form.Bounds;
            }
        }

        /// <summary>
        /// Saves the bounds and state information of the form identified by the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        private void UnloadForm(string key)
        {
            UpdateForm(key);

            // save workspace docking state
            if (layouts.ContainsKey(key))
            {
                var layout = layouts[key];
                if (workspaces.ContainsKey(key))
                {
                    var workspace = workspaces[key];
                    using (Stream stream = new MemoryStream())
                    {
                        workspace.SaveAsXml(stream, Encoding.UTF8, true);
                        stream.Flush();
                        stream.Seek(0L, SeekOrigin.Begin);
                        using (var reader = new StreamReader(stream))
                        {
                            layout.DockingInfo = reader.ReadToEnd();
                            reader.Close();
                        }

                        stream.Close();
                    }
                }

                var form = forms[key];
                if (form is IAdditionalLayoutDataSource)
                    layout.AdditionalData = ((IAdditionalLayoutDataSource)form).GetAdditionalLayoutData();
            }

            SaveLayouts();

            loadedList.Remove(key);
        }
        
        #endregion

        #region Utilities

        /// <summary>
        /// Gets the default bounds for a form.
        /// </summary>
        /// <value>The default bounds.</value>
        private Rectangle DefaultBounds
        {
            get
            {
                if (defaultBounds.IsEmpty)
                    defaultBounds = GetDefaultBounds();
                return defaultBounds;
            }
        }

        /// <summary>
        /// Gets the default bounds for a form.
        /// </summary>
        /// <returns>A rectangle centered in the screen and 4/5 as big.</returns>
        private Rectangle GetDefaultBounds()
        {
            var screen = This.Computer.Screen.WorkingArea;
            var wmargin = screen.Width / 10;
            var hmargin = screen.Height / 10;

            return new Rectangle(
                screen.Left + wmargin,
                screen.Top + hmargin,
                screen.Width - 2 * wmargin,
                screen.Height - 2 * hmargin);

        }

        #endregion
    }
}

