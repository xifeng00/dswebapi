using System;
using NpgsqlTypes;
using SqlSugar;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace db
{
    [SugarTable("public.test1")]//指定数据库中的表名，要对应数据库的表名，否则会出错
    public class Test:DataBase
    {
        [SugarColumn(IsPrimaryKey = true, ColumnName = "id")]//指定主键和自动增长
        public override string id { get; set; }

        public string name { get; set; }
       

    }



}