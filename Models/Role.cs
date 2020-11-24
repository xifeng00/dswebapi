using System;
using NpgsqlTypes;
using SqlSugar;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dswebapi.Models
{
    [SugarTable("public.Role")]//指定数据库中的表名，要对应数据库的表名，否则会出错
    public class Role:DataBase
    {
        [SugarColumn(IsPrimaryKey = true)]//指定主键和自动增长
        public override string id { get; set; }

        public string name { get; set; }
        public string  interfaceids { get; set; }
        public string interfacenames { get; set; }
        public DateTime  createtime { get; set; }
        public DateTime  updatetime { get; set; }
        public string createuserid { get; set; }


    }



}