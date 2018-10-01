using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Metaproject;
using Metaproject.Common;

namespace SqlGen
{
    public class MenuFactory : IMenuFactory
    {
        public MenuItem CreateMenu()
        {
            var menu = new MenuItem
            {
                Children = MenuItem.SubItems(
                    MenuItem.Item("SQL",  
                        MenuItem.SubItems(
                            MenuItem.Item("Create Seed Script", typeof(StandardCommand<SqlSeedScriptParams>).GetFriendlyName()),
                            MenuItem.Item("Code generator", typeof(GenerateCodeCommand).GetFriendlyName())
                            )),
                    MenuItem.Item("CS",
                        MenuItem.SubItems(
                            MenuItem.Item("Build collection", typeof(StandardCommand<CollectionBuildersParameters>).GetFriendlyName()),
                            MenuItem.Item("Generate class", typeof(StandardCommand<CsClassGeneratorParameters>).GetFriendlyName()),
                            MenuItem.Item("Extract property names", typeof(StandardCommand<PropertyNameExtractorParameters>).GetFriendlyName())

                        )),
                    MenuItem.Item("TXT",
                        MenuItem.SubItems(
                            MenuItem.Item("Reverse lines", typeof(StandardCommand<ReverseLinesToolsParameters>).GetFriendlyName())
                        )),
                    MenuItem.Item("EF",
                        MenuItem.SubItems(
                            MenuItem.Item("Extract EDMX from migrations", typeof(StandardCommand<EfEdmxExtractorParameters>).GetFriendlyName())
                        )),
                    MenuItem.Item("Admin",
                        MenuItem.SubItems(
                            MenuItem.Item("Clear Event Aggregator", typeof(ClearAggregatorCommand).GetFriendlyName())
                        ))
                    )
            };

            return menu;
        }
    }
}
