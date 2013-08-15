
namespace Delta.CertXplorer.Services
{
    /// <summary>
    /// Defines a basic selection service.
    /// </summary>
    public interface ISelectionService : ISelectionSource
    {
        /// <summary>Adds a selection source to the observed sources list.</summary>
        /// <param name="selectionSource">The selection source.</param>
        void AddSource(ISelectionSource selectionSource);

        /// <summary>
        /// Removes a selection source from the observed sources list.
        /// </summary>
        /// <param name="selectionSource">The selection source.</param>
        void RemoveSource(ISelectionSource selectionSource);

        /// <summary>
        /// Gets the current selection source.
        /// </summary>
        /// <value>The current source.</value>
        object CurrentSource { get; }
    }
}
