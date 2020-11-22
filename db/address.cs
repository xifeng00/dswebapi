using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace db
{
    [SugarTable("public.address")]//指定数据库中的表名，要对应数据库的表名，否则会出错
    public class Address:DataBase
    {
        [SugarColumn(IsPrimaryKey = true)]//指定主键和自动增长
        public override string  id { get; set; }
        public string name { get; set; }
        public string lat { get; set; }
        public string lng { get; set; }
        public string areaid { get; set; }
        public DateTime creattime { get; set; }
        public DateTime updatetime { get; set; }
        public string creatuserid { get; set; }

    }
}
