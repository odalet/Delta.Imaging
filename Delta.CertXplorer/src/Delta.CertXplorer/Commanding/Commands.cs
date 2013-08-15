using System;
using System.Collections.Generic;

using Delta.CertXplorer.Asn1Decoder;

namespace Delta.CertXplorer.Commanding
{
    internal static partial class Commands
    {
        private class BindingDescriptor
        {
            private BindingDescriptor(IVerb verb, Type commandType, bool isDefaultBinding)
            {
                Verb = verb;
                CommandType = commandType;
                IsDefaultBinding = isDefaultBinding;
            }
            
            public static BindingDescriptor Create<C>(IVerb verb, bool isDefaultBinding) where C : ICommand
            {
                if (verb == null) verb = NullVerb.Instance;
                return new BindingDescriptor(verb, typeof(C), isDefaultBinding);
            }

            public Type CommandType { get; private set; }
            public IVerb Verb { get; private set; }
            public bool IsDefaultBinding { get; private set; }
        }

        private static Dictionary<Type, List<BindingDescriptor>> commandBindings = new Dictionary<Type, List<BindingDescriptor>>();

        //private static readonly ICommand ViewDetectionFlowsCommand;
        //private static readonly ICommand PurgeDetectionFlowsCommand;
        
        static Commands()
        {
            AddCommandBinding<string, OpenFileDocumentVerb, OpenFileDocumentCommand>();
            AddCommandBinding<X509Object, OpenCertificateDocumentVerb, OpenCertificateDocumentCommand>();
            AddCommandBinding<FileDocument, OpenExistingDocumentVerb, OpenExistingDocumentCommand>();
            AddCommandBinding<X509Document, OpenExistingDocumentVerb, OpenExistingDocumentCommand>();
            AddCommandBinding<FileDocument, CloseDocumentVerb, CloseDocumentCommand>();
            AddCommandBinding<X509Document, CloseDocumentVerb, CloseDocumentCommand>();

            //AddCommandBinding<BuildingDTO, CreateVerb, CreateBuildingCommand>();
            //AddCommandBinding<FloorDTO, CreateVerb, CreateFloorCommand>();
            //AddCommandBinding<RoomDTO, CreateVerb, CreateRoomCommand>();

            //// Bind Types/Verbs to commands and set default verb
            //AddCommandBinding<CameraDTO, CreateVerb, CreateCameraCommand>();
            //AddCommandBinding<CameraDTO, EditVerb, EditCommand>(true);
            //AddCommandBinding<CameraDTO, DeleteVerb, DeleteCameraCommand>();

            //AddCommandBinding<PersonDTO, CreateVerb, CreatePersonCommand>();
            //AddCommandBinding<PersonDTO, EditVerb, EditCommand>(true);
            //AddCommandBinding<PersonDTO, DeleteVerb, DeletePersonCommand>();
            //AddCommandBinding<PersonProxyDTO, EditVerb, EditCommand>(true);
            //AddCommandBinding<PersonProxyDTO, DeleteVerb, DeletePersonProxyCommand>();

            //if (Globals.CanEditDoors)
            //{
            //    AddCommandBinding<Door, CreateVerb, CreateDoorCommand>();
            //    AddCommandBinding<Door, EditVerb, EditCommand>(true);
            //    AddCommandBinding<Door, DeleteVerb, DeleteDoorCommand>();
            //}
            //else
            //{
            //    AddCommandBinding<Door, CreateVerb, NullCommand>();
            //    AddCommandBinding<Door, EditVerb, NullCommand>(true);
            //    AddCommandBinding<Door, DeleteVerb, NullCommand>();
            //}

            //// Other commands
            //ViewDetectionFlowsCommand = new ViewDetectionFlowsCommand();
            //PurgeDetectionFlowsCommand = new PurgeDetectionFlowsCommand();            
        }

        public static void RunVerb(object commandTarget, IVerb verb, params object[] otherArguments)
        {
            if (commandTarget == null)
            {
                This.Logger.Warning(
                    "RunCommand was invoked with a null target object.");
                return;
            }

            RunVerb(commandTarget, commandTarget.GetType(), verb, otherArguments);
        }

        public static void RunVerb<T>(IVerb verb, params object[] otherArguments)
        {
            RunVerb(null, typeof(T), verb, otherArguments);
        }

        public static void RunVerb(Type commandTargetType, IVerb verb, params object[] otherArguments)
        {
            if (commandTargetType == null)
            {
                This.Logger.Warning(
                    "RunCommand was invoked with a null target object type.");
                return;
            }

            RunVerb(null, commandTargetType, verb, otherArguments);
        }

        public static void RunDefaultVerb(object commandTarget, params object[] otherArguments)
        {
            if (commandTarget == null)
            {
                This.Logger.Warning(
                    "RunDefaultCommand was invoked with a null target object.");
                return;
            }

            Type targetType = commandTarget.GetType();
            BindingDescriptor descriptor = FindDefaultDescriptor(targetType);

            if (descriptor != null) ExecuteCommand(
                descriptor.CommandType, commandTarget, descriptor.Verb, otherArguments);
        }
        
        #region Implementation

        private static void RunVerb(object commandTarget, Type commandTargetType, IVerb verb, params object[] otherArguments)
        {
            if (verb == null) verb = NullVerb.Instance;

            var descriptor = FindDescriptor(commandTargetType, verb);
            Type commandType = descriptor == null ? null : descriptor.CommandType;

            if (commandType == null)
            {
                This.Logger.Error(string.Format(
                    "Could not find a command associated to Objects of type {0} with Verb {1}.",
                    commandTargetType, verb.Name));
                return;
            }

            ExecuteCommand(commandType, commandTarget, verb, otherArguments);
        }

        private static BindingDescriptor FindDescriptor(Type targetType, IVerb verb)
        {
            if (!commandBindings.ContainsKey(targetType)) return null;
            if (verb == null) verb = NullVerb.Instance;

            BindingDescriptor found = null;
            foreach (var descriptor in commandBindings[targetType])
            {
                if (descriptor.Verb != null && descriptor.Verb.Name == verb.Name)
                {
                    found = descriptor;
                    break;
                }
            }

            return found;
        }

        private static BindingDescriptor FindDefaultDescriptor(Type targetType)
        {
            if (!commandBindings.ContainsKey(targetType)) return null;

            BindingDescriptor found = null;
            foreach (var descriptor in commandBindings[targetType])
            {
                if (descriptor.IsDefaultBinding)
                {
                    found = descriptor;
                    break;
                }
            }

            return found;
        }
        
        private static void ExecuteCommand(Type commandType, object commandTarget, IVerb verb, params object[] otherArguments)
        {
            // We have a commandType. Construct it and invoke it.
            ICommand command = null;
            try { command = (ICommand)Activator.CreateInstance(commandType); }
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
            if (otherArguments != null && otherArguments.Length > 0)
            {
                var args = new object[otherArguments.Length + 2];
                args[0] = commandTarget;
                args[1] = verb;
                for (int i = 0; i < otherArguments.Length; i++)
                    args[i + 2] = otherArguments[i];
                command.Run(args);
            }        
            else command.Run(commandTarget, verb);
        }

        private static void AddCommandBinding<T, V, C>() 
            where V : IVerb, new()
            where C : ICommand
        {
            AddCommandBinding<T, V, C>(false);
        }

        private static void AddCommandBinding<T, V, C>(bool isDefaultBinding) 
            where V : IVerb, new()
            where C : ICommand
        {
            AddCommandBinding<T, C>(new V(), isDefaultBinding);
        }

        private static void AddCommandBinding<T, C>(IVerb verb) 
            where C : ICommand
        {
            AddCommandBinding<T, C>(verb, false);
        }

        private static void AddCommandBinding<T, C>(IVerb verb, bool isDefaultBinding) 
            where C : ICommand
        {
            var type = typeof(T);
            var commandType = typeof(C);

            if (verb == null) verb = NullVerb.Instance;            
            
            List<BindingDescriptor> descriptors = null;
            if (commandBindings.ContainsKey(type)) descriptors = commandBindings[type];
            else
            {
                descriptors = new List<BindingDescriptor>();
                commandBindings.Add(type, descriptors);
            }

            descriptors.Add(BindingDescriptor.Create<C>(verb, isDefaultBinding));
        }

        #endregion
    }
}
