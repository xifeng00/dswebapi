using System;
using NpgsqlTypes;
using SqlSugar;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace db
{
    [SugarTable("public.area")]//指定数据库中的表名，要对应数据库的表名，否则会出错
    public class Area:DataBase
    {
        [SugarColumn(IsPrimaryKey = true,ColumnName ="id")]//指定主键和自动增长
        public override string id { get; set; }
      
        public string parent_id { get; set; }
        public string name { get; set; }
        public string merger_name { get; set; }
        public string short_name { get; set; }
        public string level_type { get; set; }
        public string  city_code { get; set; }
        public string spell { get; set; }
        public string simple_spell { get; set; }
        public string first_char { get; set; }
        public string lng { get; set; }
        public string lat { get; set; }
        public string remark { get; set; }
        

    }
   


}