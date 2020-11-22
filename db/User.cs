using NpgsqlTypes;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace db
{
    [SugarTable("public.User")]//指定数据库中的表名，要对应数据库的表名，否则会出错
    public class User:DataBase
    {
        [SugarColumn(IsPrimaryKey = true)]//指定主键和自动增长
        public override string id { get; set; }
        public string name { get; set; }
        public string tel { get; set; }
        public DateTime account { get; set; }
        public string pwd { get; set; }
        public string roleids { get; set; }
        public string rolenames { get; set; }
        public DateTime createtime { get; set; }
        public DateTime updatetime { get; set; }
        public string creatuserid { get; set; }

    }

}