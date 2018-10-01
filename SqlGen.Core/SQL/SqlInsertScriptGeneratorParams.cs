namespace SqlGen
{
    public class SqlInsertScriptGeneratorParams
    {
        public string ConnectionString { get; set; }
        public string[] SqlSelectStatement { get; set; }
        public int ChunkSize { get; set; }
        public string IsMultistatement { get; set; }
        public string MultiStatementSeparator { get; set; }
    
    }
}