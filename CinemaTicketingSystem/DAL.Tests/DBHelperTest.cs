using System;
using Xunit;
using DAL;
using MySql.Data.MySqlClient;

namespace CTS_DAL_XUnit
{
    public class DBHelperUnitTest
    {
        [Fact]
        public void OpenConnectionTest()
        {
            Assert.NotNull(DBHelper.OpenConnection());
        }
    }
}