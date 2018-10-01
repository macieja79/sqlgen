using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using SqlGen.Core.Presentation.Commands;

namespace SqlGen
{
    public class CommandFactory : ICommandFactory
    {
        private readonly ILifetimeScope _lifetimeScope;

        public static Type[] GetCommandTypes()
        {
            var type = typeof(ICommand);
            var commands = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && !p.IsInterface && !p.IsAbstract)
                .ToArray();

            return commands;
        }
        
        public static Type[] GetParametersTypes()
        {
            var type = typeof(ToolParameters);
            var parameterTypes = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && !p.IsInterface && !p.IsAbstract)
                .ToArray();

            return parameterTypes;
        }




        public CommandFactory(ILifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope;
        }

        public ICommand CreateCommandByName(string name)
        {
            var chunks = name.Split('<');

            if (chunks.Length == 1)
            {
                var commands = GetCommandTypes();
                var invokedCommandType = commands.FirstOrDefault(t => t.Name == name);
                var command = (ICommand)_lifetimeScope.Resolve(invokedCommandType);
                return command;
            }
            else if (chunks.Length == 2)
            {
                var parameterTypeName = chunks[1].Replace(">", "");
                var parametersTypes = GetParametersTypes();
                var invokedParameterType = parametersTypes.FirstOrDefault(t => t.Name == parameterTypeName);
                var standardCommand = typeof(StandardCommand<>).MakeGenericType(invokedParameterType);
                var command = (ICommand) _lifetimeScope.Resolve(standardCommand);
                return command;
            }

            return null;

        }
    }
}
