using Autofac;
using SqlGen.Core;

namespace SqlGen.Presentation.Desktop
{
    public static class AutofacConfiguration
    {
        public static void ConfigureBuilder(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(
                    typeof(SqlQueryEngine).Assembly,
                    typeof(InsertScriptGenerator).Assembly,
                    typeof(SqlScript).Assembly,
                    typeof(SqlGenForm).Assembly)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            
            builder.RegisterGeneric(typeof(StandardCommand<>))
                .AsSelf()
                .InstancePerLifetimeScope();

            var parameterTypes = CommandFactory.GetParametersTypes();
            builder.RegisterTypes(parameterTypes).AsSelf().InstancePerLifetimeScope();

        }

        public static IContainer GetConfiguredContainer()
        {
            var builder = new ContainerBuilder();

            ConfigureBuilder(builder);

            var container = builder.Build();
            return container;
        }
    }
}
