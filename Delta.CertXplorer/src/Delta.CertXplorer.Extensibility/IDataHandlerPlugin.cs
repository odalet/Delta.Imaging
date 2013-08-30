using System.ComponentModel.Composition;

namespace Delta.CertXplorer.Extensibility
{
    /// <summary>
    /// Basic interface for document data handling plugins.
    /// </summary>
    [InheritedExport]
    public interface IDataHandlerPlugin : IPlugin
    {
        IDataHandler CreateHandler();
    }

    public interface IDataHandler
    {
        /// <summary>
        /// Determines whether this instance the specified file can be processed by this plugin.
        /// </summary>
        /// <param name="filename">The file name.</param>
        /// <returns>
        ///   <c>true</c> if the file can be processed; otherwise, <c>false</c>.
        /// </returns>
        bool CanHandleFile(string filename);

        /// <summary>
        /// Processes the file and returns its data.
        /// </summary>
        /// <returns>Data read from the file and potentially transformed.</returns>
        IData ProcessFile();
    }

    public interface IData
    {
        byte[] MainData { get; }
        object AdditionalData { get; }
    }
}
