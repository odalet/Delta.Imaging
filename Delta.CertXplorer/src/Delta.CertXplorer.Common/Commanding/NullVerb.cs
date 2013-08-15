
namespace Delta.CertXplorer.Commanding
{
    /// <summary>
    /// A verb with a NULL action.
    /// </summary>
    public class NullVerb : BaseVerb
    {
        /// <summary>
        /// This is the singleton instance.
        /// </summary>
        public static readonly NullVerb Instance = new NullVerb();

        /// <summary>
        /// Initializes a new instance of the <see cref="NullVerb"/> class.
        /// </summary>
        private NullVerb() : base("NULL") { }
    }
}
