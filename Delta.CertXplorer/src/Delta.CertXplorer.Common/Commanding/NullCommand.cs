
namespace Delta.CertXplorer.Commanding
{
    /// <summary>
    /// Null command: does nothing.
    /// </summary>
    public class NullCommand : ICommand
    {
        #region ICommand Members

        /// <summary>
        /// Runs the command with the specified arguments.
        /// </summary>
        /// <param name="verb">The verb this command was invoked from (informative).</param>
        /// <param name="arguments">The command arguments.</param>
        public void Run(IVerb verb, params object[] arguments) { }

        #endregion
    }
}
