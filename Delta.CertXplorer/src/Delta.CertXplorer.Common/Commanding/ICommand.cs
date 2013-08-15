
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
        /// <param name="arguments">The arguments.</param>
        void Run(params object[] arguments);
    }
}
