using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Metaproject.Common.Practices;
using Autofac;
using SqlGen;
using TreasuryIS.Practices;

namespace DockSample
{
    public class AutofacConfigurationForWinFormsUI
    {
        public static void ConfigureBuilder(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(
                    typeof(SqlQueryEngine).Assembly,
                    typeof(InsertScriptGenerator).Assembly,
                    typeof(SqlScript).Assembly,
                    typeof(MainForm).Assembly)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            var commandTypes = CommandFactory.GetCommandTypes();
            builder.RegisterTypes(commandTypes)
                .AsSelf();

            builder.RegisterType<EventAggregator>()
                .As<IEventAggregator>()
                .SingleInstance();

            builder.RegisterType<MainForm>()
                .As<IUIOutput>()
                .SingleInstance();

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
