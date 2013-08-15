
namespace Delta.CertXplorer.Commanding
{
    /// <summary>
    /// Base class for verbs.
    /// </summary>
    public abstract class BaseVerb : IVerb
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseVerb"/> class.
        /// </summary>
        /// <param name="verbName">Name of the verb.</param>
        public BaseVerb(string verbName)
        {
            Name = verbName;
        }

        #region IVerb Members

        /// <summary>
        /// Gets the verb name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; private set; }

        #endregion
    }
}
