
namespace Delta.CertXplorer.Commanding
{
    /// <summary>
    /// Represents an UI command
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Runs the command with the specified arguments.
        /// </summary>
        /// <param name="verb">The verb this command was invoked from (informative).</param>
        /// <param name="arguments">The command arguments.</param>
        void Run(IVerb verb, params object[] arguments);
    }
}
