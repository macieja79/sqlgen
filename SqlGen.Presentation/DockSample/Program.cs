using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Autofac;
using SqlGen;
using SqlGen.Core.Presentation;

namespace DockSample
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

            using (var container = AutofacConfigurationForWinFormsUI.GetConfiguredContainer())
            {
                using (var scope = container.BeginLifetimeScope())
                {
                    var ui = scope.Resolve<IUIOutput>();
                    var form = (Form) ui;
                    Application.Run(form);
                }
            }

        }
    }
}