using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlGen.Core.CSharp;

namespace SqlGen
{
    public class CodeGenerator : ITool<CodeGeneratorToolParameters>
    {
        public CodeGeneratorToolParameters CreateDefaultParameters()
        {
            return new CodeGeneratorToolParameters
            {
                Pattern = "select {0} from {1};"
            };
        }

        public SqlScript Generate(CodeGeneratorToolParameters parameters)
        {
            List<string> lines = new List<string>();

            lines.Add(parameters.Header);

            var data = parameters.Data;
            var pattern = parameters.Pattern;

            int rowCount = data.GetUpperBound(1) + 1;
            int colCount = data.GetUpperBound(0) + 1;

            for (int r = 0; r < rowCount; r++)
            {
                string[] values = new string[colCount];

                bool isAllNull = true;
                for (int c = 0; c < colCount; c++)
                {
                    var val = data[c, r];
                    if (val != null)
                    {
                        isAllNull = false;
                        break;
                    }
                }

                if (isAllNull)
                    continue;

                for (int c = 0; c < colCount; c++)
                {
                    values[c] = data[c, r];
                }

                //string line = string.Format(pattern, values);

                string line = pattern;
                for (int i = 0; i < values.Length; i++)
                {
                    string iPattern = $"{{{i}}}";
                    string trimmedValue = values[i].Trim();
                    line = line.Replace(iPattern, trimmedValue);
                }

                lines.Add(line);
            }

            lines.Add(parameters.Footer);

            return new SqlScript
            {
                Lines = lines
            };

        }
    
    }
}
