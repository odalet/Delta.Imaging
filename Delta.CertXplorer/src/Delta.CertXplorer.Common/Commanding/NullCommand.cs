
namespace Delta.CertXplorer.Commanding
{
    /// <summary>
    /// Null command: does nothing.
    /// </summary>
    public class NullCommand : ICommand
    {
        #region ICommand Members

        public IVerb Verb { get { return null; } }

        public void Run(params object[] arguments) { }

        #endregion
    }
}
