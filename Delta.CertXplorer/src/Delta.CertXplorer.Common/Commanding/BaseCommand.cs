using System;

namespace Delta.CertXplorer.Commanding
{
    /// <summary>
    /// Base class for commands
    /// </summary>
    public abstract class BaseCommand : ICommand
    {
        /// <summary>
        /// Gets this command's name.
        /// </summary>
        /// <value>The command name.</value>
        public virtual string Name
        {
            get { return GetType().ToString(); }
        }

        /// <summary>
        /// Gets the verb this command was invoked with.
        /// </summary>
        protected IVerb Verb { get; private set; }

        #region ICommand Members

        /// <summary>
        /// Runs the command with the specified arguments.
        /// </summary>
        /// <param name="verb">The verb this command was invoked from (informative).</param>
        /// <param name="arguments">The command arguments.</param>
        public virtual void Run(IVerb verb, params object[] arguments)
        {
            Verb = verb ?? NullVerb.Instance;

            var targetAsString = string.Empty;
            if (arguments == null || arguments.Length == 0)
                targetAsString = "NO TARGET";
            else
                targetAsString = arguments[0] == null ? "NULL" : arguments[0].ToString();

            if (arguments != null && arguments.Length > 1) This.Logger.Verbose(string.Format(
                "Command [{0}] invoked on target [{1}] with verb [{2}]; additional arguments count={3}.", Name, targetAsString, verb.Name, arguments.Length - 1));
            else This.Logger.Verbose(string.Format(
                "Command [{0}] invoked on target [{1}] with verb [{2}].", Name, targetAsString, verb.Name));
        }

        #endregion
    }

    /// <summary>
    /// Generic Base class for commands
    /// </summary>
    /// <typeparam name="T">Type of the target object of the command.</typeparam>
    public abstract class BaseCommand<T> : BaseCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseCommand&lt;T&gt;"/> class.
        /// </summary>
        public BaseCommand() { }

        /// <summary>
        /// Gets the target of the command (this is the 1st argument in the list passed to the Run method).
        /// </summary>
        protected T Target { get; private set; }
                
        /// <summary>
        /// Gets all the arguments passed to this command (including the target as the first argument).
        /// </summary>
        protected object[] Arguments { get; private set; }
        
        /// <summary>
        /// Runs the command with the specified arguments.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        public override void Run(IVerb verb, params object[] arguments)
        {
            base.Run(verb, arguments); // for logging purpose
            ParseArguments(arguments);
            RunCommand();
        }

        protected abstract void RunCommand();

        /// <summary>
        /// Parses the arguments.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        protected virtual void ParseArguments(object[] arguments)
        {
            var target = default(T);

            if (arguments == null || arguments.Length == 0) return;

            var targetObject = arguments[0];
            if (targetObject == null) return;

            if (!(targetObject is T)) throw new ApplicationException(string.Format(
                "Invalid argument type ({0}) was provided to command [{1}].", targetObject.GetType(), Name));

            Arguments = arguments ?? new object[0];
            Target = (T)target;
        }
    }
}
