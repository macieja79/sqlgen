using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlGen
{
    public class CollectionBuilder : ITool<CollectionBuildersParameters>
    {
        public CollectionBuildersParameters CreateDefaultParameters()
        {
            return new CollectionBuildersParameters
            {
                ItemPattern = "{0}",
                Items = new string[0]
            };
        }

        public SqlScript Generate(CollectionBuildersParameters p)
        {
           var sb = new StringBuilder();
           sb.Append(p.Start);

            for (int i = 0; i < p.Items.Length; i++)
            {
                sb.Append(string.Format(p.ItemPattern, p.Items[i]));

                if (i < p.Items.Length - 1)
                    sb.Append(p.Separator);
            }

            sb.Append(p.End);

            var script = new SqlScript();
            script.Lines.Add(sb.ToString());

            return script;
        }
    }
}
