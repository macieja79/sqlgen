using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SqlGen
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void SqlTableNameResolver_Should_Resolve_Table_Name()
        {
            var sql = "select * from rita_Clients";
            var expectedTableName = "rita_Clients";
            
            var tableNameResolver = new SqlTableNameResolver();
            var tableName = tableNameResolver.ResolveTableName(sql);
            
            Assert.AreEqual(tableName, expectedTableName);
        }
    }
}
