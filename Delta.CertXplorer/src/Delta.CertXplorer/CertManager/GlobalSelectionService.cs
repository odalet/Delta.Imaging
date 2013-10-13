using System;
using System.Collections.Generic;
using SCD = System.ComponentModel.Design;

using Delta.CertXplorer.Services;

namespace Delta.CertXplorer.CertManager
{
    /// <summary>
    /// Global selection service.
    /// </summary>
    internal class GlobalSelectionService : ISelectionService
    {
        private Dictionary<ISelectionSource, EventHandler> sources =
            new Dictionary<ISelectionSource, EventHandler>();

        /// <summary>
        /// Gets or create the global selection service.
        /// </summary>
        /// <param name="serviceContainer">The service container in which an instance of the selection service will be added (or retrieved).</param>
        /// <returns>An instance of a class implemenaing <see cref="Delta.CertXplorer.Common.Services.ISelectionService"/>.</returns>
        public static ISelectionService GetOrCreateSelectionService(SCD.IServiceContainer serviceContainer)
        {
            if (serviceContainer == null) throw new ArgumentNullException("serviceContainer");

            var selectionService = serviceContainer.GetService<ISelectionService>();
            if (selectionService == null)
            {
                selectionService = new GlobalSelectionService();
                serviceContainer.AddService<ISelectionService>(selectionService);
            }

            return selectionService;
        }

        #region ISelectionService Members

        /// <summary>
        /// Gets the current selection source.
        /// </summary>
        /// <value>The current source.</value>
        public object CurrentSource { get; private set; }

        /// <summary>
        /// Adds a selection source to the observed sources list.
        /// </summary>
        /// <param name="selectionSource">The selection source.</param>
        public void AddSource(ISelectionSource selectionSource)
        {
            if (sources.ContainsKey(selectionSource)) return;

            EventHandler handler = (s, e) =>
            {
                var source = s as ISelectionSource;
                if (source != null) OnSelectionChanged(source, source.SelectedObject);
            };

            selectionSource.SelectionChanged += handler;
            sources.Add(selectionSource, handler);
        }

        /// <summary>
        /// Removes a selection source from the observed sources list.
        /// </summary>
        /// <param name="selectionSource">The selection source.</param>
        public void RemoveSource(ISelectionSource selectionSource)
        {
            if (sources.ContainsKey(selectionSource))
            {
                var handler = sources[selectionSource];
                selectionSource.SelectionChanged -= handler;
                sources.Remove(selectionSource);
            }
        }

        #endregion

        #region ISelectionSource Members

        /// <summary>
        /// Occurs when the currently selected object has changed.
        /// </summary>
        public event EventHandler SelectionChanged;

        /// <summary>
        /// Gets the currently selected object.
        /// </summary>
        /// <value>The selected object.</value>
        public object SelectedObject { get; protected set; }

        #endregion

        /// <summary>
        /// Called when the current selection has changed.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="selectedObject">The selected object.</param>
        protected virtual void OnSelectionChanged(object source, object selectedObject)
        {
            CurrentSource = source;
            SelectedObject = selectedObject;
            if (SelectionChanged != null) SelectionChanged(this, EventArgs.Empty);
        }
    }
}
