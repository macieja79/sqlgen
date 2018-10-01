using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlGen.Core.EF
{
    public class PropertyNameExtractor : IPropertyNamesExtractor
    {
        public PropertyNameExtractorParameters CreateDefaultParameters()
        {
            return new PropertyNameExtractorParameters();
        }

        public SqlScript Generate(PropertyNameExtractorParameters parameters)
        {
            SqlScript result = new SqlScript();
            foreach (string property in parameters.Properties)
            {
                if (string.IsNullOrEmpty(property)) continue;

                string trimmed = property.Trim();
                var separatorArray = new [] {" "};
                string[] splitted = trimmed.Split(separatorArray, StringSplitOptions.RemoveEmptyEntries);
                string name = splitted[2];
                result.Lines.Add(name);
            }

            return result;
        }
    }
}
