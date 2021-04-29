using System;
using System.Collections.Generic;
using System.Text;
using VolTest.Core.Configuration;

namespace VolTest.Core.DBManager
{
    public class DBServerProvider
    {
        // 连接字符串池，可用于多数据库连接
        private static Dictionary<string, string> ConnectionPool = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        private static readonly string DefaultConnName = "default";

        public DBServerProvider()
        {
            SetConnection(DefaultConnName, AppSetting.DbConnectionString);
        }
        public static void SetConnection(string key, string val)
        {
            if (ConnectionPool.ContainsKey(key))
            {
                ConnectionPool[key] = val;
                return;
            }
            ConnectionPool.Add(key, val);
        }
    }
}
