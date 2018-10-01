using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Metaproject
{
    public class TSqlBuilder
    {

        List<string> _lines = new List<string>();

        public void Comment (string content)
        {
            string line = $"-- {content}";
            Add(line);
        }

        public void Blank()
        {
            string line = "";
            Add(line);
        }

        public void Message(string content)
        {
            string line = $"RAISERROR ('{content}', 0, 1, 56) WITH NOWAIT;";
            Add(line);
        }

        public void Go()
        {
            Add("GO");
        }

        public void Raw(string line)
        {
            Add(line);
        }

        public void SetIdentity(string tableName, bool isOn)
        {
            string parameter = isOn ? "ON" : "OFF";
            string line = $"SET IDENTITY_INSERT {tableName} {parameter};";
            Add(line);
            Go();
        }

        public void CreateSchema(string schema)
        {
            Add($"CREATE SCHEMA {schema};");
            Go();
        }

        public void DropAndRecreateDb(string databaseName)
        {
            Add(@"USE master;");
            Add($"IF EXISTS(select * from sys.databases where name='{databaseName}') DROP DATABASE {databaseName};");
            Add($"CREATE DATABASE {databaseName};");
            Go();
        }

        public void UseDatabase(string databaseName)
        {
            Add($"USE [{databaseName}];");
            Go();
        }

        public void Select(string tableName)
        {
            Add($"SELECT * FROM {tableName};");
        }

        public List<string> GetContent()
        {
            return _lines;
        }

        void Add(string line)
        {
            _lines.Add(line);
        }

        
    }
}
