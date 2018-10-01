using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autofac;

namespace SqlGen.Presentation.Desktop
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (var container = AutofacConfiguration.GetConfiguredContainer())
            {
                using (var scope = container.BeginLifetimeScope())
                {
                    
                    var seedGenerator = scope.Resolve<ITool<SqlSeedScriptParams>>();
                    var classGenerator = scope.Resolve<ITool<CsClassGeneratorParameters>>();
                    var edmxGenerator = scope.Resolve<ITool<EfEdmxExtractorParameters>>();
                    Application.Run(new SqlGenForm(seedGenerator, classGenerator, edmxGenerator));
                }
            }
        }
    }
}
