namespace SqlGen
{
    public interface ISqlSnippets
    {
        SqlScript CreateDropCreateDatabase(string databaseName);
        SqlScript CreateUseDatabase(string databaseName);
    }
}
