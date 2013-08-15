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

        #region ICommand Members

        /// <summary>
        /// Runs the command with the specified arguments.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        public virtual void Run(params object[] arguments)
        {
            string targetAsString = string.Empty;
            string verbAsString = string.Empty;

            if (arguments.Length == 0)
            {
                targetAsString = "NO TARGET";
                verbAsString = "NO VERB";
            }
            
            if (arguments.Length >= 1)
                targetAsString = arguments[0] == null ? "NULL" : arguments[0].ToString();
            if (arguments.Length >= 2)
            {
                if (arguments[1] is IVerb) verbAsString = ((IVerb)arguments[1]).Name;
                else verbAsString = string.Format("NOT A VERB: {0}", arguments[1]);
            }

            This.Logger.Verbose(string.Format("Command [{0}] invoked on target [{1}] with verb [{2}]", Name, targetAsString, verbAsString));
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
        public BaseCommand() : this(false) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseCommand&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="allowNull">if set to <c>true</c> [allow null].</param>
        public BaseCommand(bool allowNull) : base()
        {
            AllowNull = allowNull;
        }

        /// <summary>
        /// Gets or sets the target of the command (this is the 1st parameter of the Run method).
        /// </summary>
        /// <value>The target.</value>
        protected T Target { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the target of the command can be null.
        /// </summary>
        /// <value><c>true</c> if [allow null]; otherwise, <c>false</c>.</value>
        protected bool AllowNull { get; set; }

        /// <summary>
        /// Runs the command with the specified arguments.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        public override void Run(params object[] arguments)
        {
            base.Run(arguments);
            ParseArguments(arguments);
        }

        /// <summary>
        /// Parses the specified arguments.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        protected virtual void ParseArguments(object[] arguments)
        {
            if (arguments.Length == 0) throw new ApplicationException(string.Format(
                "No arguments were provided to this command ({0}).", Name));

            var target = arguments[0];
            if (target == null)
            {
                if (AllowNull) Target = default(T); 
                else throw new ApplicationException(string.Format(
                    "The argument provided to this command ({0}) is null.", Name));                
            }
            else if (target is T) Target = (T)target;
            else throw new ApplicationException(string.Format(
                    "The type of the object targetted by the command is invalid: {0}", target.GetType()));
        }
    }
}
