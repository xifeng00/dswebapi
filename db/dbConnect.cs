using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dswebapi.db
{
    public class dbConnect
    {
        private static string dbstr = @"PORT=6432;DATABASE=disuo;HOST=61.158.167.166;PASSWORD=rnSdAGeC;USER ID=plmcs;Pooling=true;";

        public static SqlSugarClient GetSqlSugarClient(int threadIndex) 
        {
            SqlSugarClient s = null;
           
            if (dbConnects.ContainsKey(threadIndex))
            {
                s = dbConnects[threadIndex];
            }
            if (s == null)
            {
                ConnectionConfig f = new ConnectionConfig();
                f.ConnectionString = dbstr;
                f.DbType = DbType.PostgreSQL;
                f.IsAutoCloseConnection = false;
                f.InitKeyType = InitKeyType.Attribute;
                s = new SqlSugarClient(f);
                dbConnects.Add(threadIndex, s);
            }
            return s;       
        }
        private static Dictionary<int, SqlSugarClient> dbConnects = new Dictionary<int, SqlSugarClient>();

    }
}
