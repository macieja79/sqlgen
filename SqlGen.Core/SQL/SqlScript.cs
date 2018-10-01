using System.Collections.Generic;

namespace SqlGen
{
    public class SqlScript
    {
        public SqlScript()
        {
            
        }

        public SqlScript(List<string> lines)
        {
            Lines = lines;
        }

        public string Name { get; set; }
        public string TargetTable { get; set; }
        public List<string> Lines { get; set; } = new List<string>();

        public void Append(string line)
        {
            Lines.Add(line);
        }
        

        public void Attach(SqlScript other)
        {
            Lines.AddRange(other.Lines);
        }

        public override string ToString()
        {
            return TargetTable;
        }
    }
}
