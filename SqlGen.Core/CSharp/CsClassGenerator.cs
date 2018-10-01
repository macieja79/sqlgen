using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Metaproject.Common;

namespace SqlGen
{

    public class CsClassGenerator : ITool<CsClassGeneratorParameters>
    {
        List<string> _lines = new List<string>();

        public SqlScript Generate(CsClassGeneratorParameters csClassGeneratorParameters)
        {
           
            Add($"public class {csClassGeneratorParameters.TargetClassName}");
            Add("{");

            foreach (var propertyName in csClassGeneratorParameters.PropertyNames)
            {
                string type = ResolveTypeOutOfName(propertyName);
                Add(CodeBuilder.CreateProperty(type, propertyName));
            }

            Add("}");

            return new SqlScript { Lines = _lines.ToList() };
        }

        private string ResolveTypeOutOfName(string propertyName)
        {

            if (propertyName.StartsWith("Is") || propertyName.StartsWith("Can"))
            {
                return "bool";
            }

            if (propertyName.ContainsAny("Size", "Number", "Count", "Id"))
            {
                return "int";
            }

            if (propertyName.ContainsAny("Name", "String", "Caption", "Text", "Sql", "Description"))
            {
                return "string";
            }

            if (propertyName.ContainsAny("Date", "Time"))
            {
                return "DateTime";
            }
           
            
            return "object";

        }



        void Add(string line)
        {
            _lines.Add(line);
        }

        public CsClassGeneratorParameters CreateDefaultParameters()
        {
            return new CsClassGeneratorParameters {TargetClassName = "ClassName", PropertyNames = new string[0]};
        }
    }
}
