using System;
using System.Linq;
using System.Collections.Generic;

using Delta.CertXplorer.Asn1Decoder;
using Delta.CertXplorer.DocumentModel;

namespace Delta.CertXplorer.Commanding
{
    internal static partial class Commands
    {
        private class CommandBindingDescriptor
        {
            private CommandBindingDescriptor(IVerb verb, Type commandType, Type targetType, bool isDefaultBinding)
            {
                Verb = verb;
                CommandType = commandType;
                TargetType = targetType;
                IsDefaultBinding = isDefaultBinding;
            }

            public static CommandBindingDescriptor Create<V, C>(Type targetType = null, bool isDefaultBinding = false)
                where V : IVerb
                where C : ICommand
            {
                if (targetType == null && isDefaultBinding)
                    throw new ArgumentException("Can't have both a null target type and default binding set.", "targetType, isDefaultBinding");

                var verb = CreateVerb<V>();
                return new CommandBindingDescriptor(verb, typeof(C), targetType, isDefaultBinding);
            }

            private static IVerb CreateVerb<V>() where V : IVerb
            {
                IVerb verb = null;
                var verbType = typeof(V);
                try
                {
                    verb = (IVerb)Activator.CreateInstance(verbType);
                }
                catch (Exception ex)
                {
                    This.Logger.Error(string.Format(
                        "Could not create an instance of verb type {0}: {1}",
                        verbType, ex.Message), ex);
                    throw;
                }

                if (verb == null)
                {
                    var message = string.Format(
                        "Could not convert the verb type {0} to IVerb", verbType);
                    This.Logger.Error(message);
                    throw new InvalidCastException(message);
                }

                return verb;
            }

            public Type CommandType { get; private set; }
            public Type TargetType { get; private set; }
            public IVerb Verb { get; private set; }
            public bool IsDefaultBinding { get; private set; }
        }

        private static Dictionary<Type, List<CommandBindingDescriptor>> commandBindings = new Dictionary<Type, List<CommandBindingDescriptor>>();

        static Commands()
        {
            AddCommandBinding<OpenFileVerb, OpenFileCommand>(typeof(string));
            AddCommandBinding<OpenCertificateVerb, OpenCertificateCommand>(typeof(X509Object));
            AddCommandBinding<OpenExistingDocumentVerb, OpenExistingDocumentCommand>(typeof(IDocument));
            AddCommandBinding<CloseDocumentVerb, CloseDocumentCommand>(typeof(IDocument));        
        }

        public static void RunVerb(IVerb verb, params object[] arguments)
        {
            RunVerb<object>(verb, arguments);
        }

        public static void RunVerb<T>(IVerb verb, params T[] arguments)
        {
            RunVerbImpl(verb, arguments == null ? new object[0] : arguments.Cast<object>().ToArray(), typeof(T));
        }

        #region Implementation

        private static void RunVerbImpl(IVerb verb, object[] arguments, Type firstArgumentType = null)
        {
            if (verb == null) verb = NullVerb.Instance;
            if (firstArgumentType == null)
            {
                if (arguments != null && arguments.Length > 0)
                    firstArgumentType = arguments[0].GetType();
            }

            var descriptor = FindDescriptor(verb, firstArgumentType);
            var commandType = descriptor == null ? null : descriptor.CommandType;

            if (commandType == null)
            {
                This.Logger.Error(string.Format(
                    "Could not find a command associated to Objects of type {0} with Verb {1}.",
                    firstArgumentType == null ? "<null>" : firstArgumentType.ToString(), verb.Name));
                return;
            }

            ExecuteCommand(commandType, verb, arguments);
        }

        private static CommandBindingDescriptor FindDescriptor(IVerb verb, Type targetType)
        {
            if (verb == null) verb = NullVerb.Instance;
            var verbType = verb.GetType();

            if (!commandBindings.ContainsKey(verbType)) return null;

            var list = commandBindings[verbType];

            CommandBindingDescriptor result = null;
            if (targetType != null)
            {
                result = list.FirstOrDefault(b => b.TargetType != null && b.TargetType == targetType); // exact matching
                if (result != null) return result;

                result = list.FirstOrDefault(b => b.TargetType != null && b.TargetType.IsA(targetType)); // inheritance matching
                if (result != null) return result;
            }

            return list.FirstOrDefault(b => b.TargetType == null); // non-typed commands
        }

        private static void ExecuteCommand(Type commandType, IVerb verb, object[] arguments)
        {
            // We have a commandType. Construct it and invoke it.
            ICommand command = null;
            try
            {
                command = (ICommand)Activator.CreateInstance(commandType);
            }
            catch (Exception ex)
            {
                This.Logger.Error(string.Format(
                    "The command type {0} is not convertible to ICommand: {1}",
                    commandType, ex.Message), ex);
                return;
            }

            if (command == null)
            {
                This.Logger.Error(string.Format(
                    "Could not convert the command type {0} to ICommand", commandType));
                return;
            }

            // We have a command object. Invoke it
            else command.Run(verb, arguments);
        }

        private static void AddCommandBinding<V, C>(params Type[] acceptedTargetTypes)
            where V : IVerb
            where C : ICommand
        {
            foreach (var tt in acceptedTargetTypes)
            {
                var acceptedTargetType = tt;
                AddCommandBinding<V, C>(acceptedTargetType, false);
            }
        }

        private static void AddCommandBinding<V, C>(Type acceptedTargetType = null, bool isDefaultBinding = false)
            where V : IVerb
            where C : ICommand
        {
            var verbType = typeof(V);

            List<CommandBindingDescriptor> list = null;
            if (commandBindings.ContainsKey(verbType)) list = commandBindings[verbType];
            else
            {
                list = new List<CommandBindingDescriptor>();
                commandBindings.Add(verbType, list);
            }

            var descriptor = CommandBindingDescriptor.Create<V, C>(acceptedTargetType, isDefaultBinding);
            list.Add(descriptor);
        }

        #endregion
    }
}
