using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SqlGen.Presentation.Desktop;

namespace SqlGen.Test.IntegrationTests
{
    [TestClass]
    public class Class1
    {
        [TestMethod]
        public void Test()
        {
            using (var container = AutofacConfiguration.GetConfiguredContainer())
            {
                using (var scope = container.BeginLifetimeScope())
                {

                    var createTableGenerator = scope.Resolve<ISqlCreateScriptGenerator>();

                    var script = createTableGenerator.GetCreateTableScript(Globals.IdefixConnectionString,
                        "ritanl.ReportScheduleTimeZone".ToSingleElementArray());
                }
            }

        }

        [TestMethod]
        public void SqlService_Should_Return_Schemas()
        {
            string connectionString = Global.IDEFIX_CONNECTION_STRING;

            using (var container = AutofacConfiguration.GetConfiguredContainer())
            {
                using (var scope = container.BeginLifetimeScope())
                {

                    var sqlService = scope.Resolve<ISqlService>();
                    var schemas = sqlService.GetSchemas(connectionString);

                }
            }




        }

    }
}
