using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;

namespace dswebapi.db
{
    /// <summary>
    /// 数据操作类
    /// </summary>
    public class dbdao
    {
       
        public static List<T> GetList<T>()
        {
            List<T> list1 = dbConnect.GetSqlSugarClient(0).Queryable<T>().ToList();
            cache.DbCache.CacheAdds<T>(list1);
            return list1;
        }
        //查询
        public static T GetById<T>(string id1) where T : class, new()
        {
            if (id1 == null) { return default; }
            T t1 = (T)cache.DbCache.CacheGet<T>(id1);
            if(t1==null)
            {
                t1= dbConnect.GetSqlSugarClient(0).Queryable<T>().InSingle(id1);
                if(t1!=null)
                    db.cache.DbCache.CacheAddOne(t1);
            }
            return t1;   
          
  
        }
       
       
        //新增
        public static bool DbInsert<T>(T t1) where T : class, new()
        {
            int lsrecn=dbConnect.GetSqlSugarClient(0).Insertable<T>(t1).ExecuteCommand();
            db.cache.DbCache.CacheAddOne(t1);
            return lsrecn > 0;
            

        }
        //更新
        public static bool DbUpdate<T>(T t1) where T : class, new()
        {
            int lsrecn = dbConnect.GetSqlSugarClient(0).Updateable<T>(t1).ExecuteCommand();
            db.cache.DbCache.CacheUpdate(t1);
            return lsrecn > 0;


        }
        //删除
        public static bool DbDelete<T>(T t1) where T : class, new()
        {
            int lsrecn = dbConnect.GetSqlSugarClient(0).Deleteable<T>(t1).ExecuteCommand();
            db.cache.DbCache.CacheDelete(t1);
            return lsrecn > 0;

        }
        //用SQL方式
        public static List<T> DbSql<T>(string sql)
        {
            List<T> t2=dbConnect.GetSqlSugarClient(0).Ado.SqlQuery<T>(sql);
            cache.DbCache.CacheAdds<T>(t2);
            return t2;
        }
        //批量更新参数 List<Class>
        public static bool DbUpdates<T>(List<T> tlist)
        {
            int lsrecn = dbConnect.GetSqlSugarClient(0).Updateable(tlist).ExecuteCommand();

            return lsrecn > 0;
        }
        public static DataTable AdoSql(string sql)
        {
            /*
            var dt = db.Ado.GetDataTable("select * from table where id=@id and name=@name", new List<SugarParameter>(){
  new SugarParameter("@id",1),
  new SugarParameter("@name",2)
            */
            DataTable dt = dbConnect.GetSqlSugarClient(0).Ado.GetDataTable(sql);
            return dt;
        }
        public static int AdoExecuteSqls(List<string> sqllist)
        {
            int hh = 0;
            SqlSugarClient c = dbConnect.GetSqlSugarClient(0);
            c.BeginTran();
            foreach (string s in sqllist)
            {
                hh+=c.Ado.ExecuteCommand(s);
            }
            c.CommitTran();
            return hh;

        }
        public static int AdoExecuteSql(string sql)
        {

            SqlSugarClient c = dbConnect.GetSqlSugarClient(0);
            c.BeginTran();
            int hh = c.Ado.ExecuteCommand(sql);
            c.CommitTran();
            return hh;

        }
    }
}
