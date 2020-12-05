using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dswebapi.Models
{
    [SugarTable("public.organ")]//指定数据库中的表名，要对应数据库的表名，否则会出错
    public class Organ:DataBase
    {

        [SugarColumn(IsPrimaryKey = true)]//指定主键和自动增长
        public override string id { get; set; }
        public string parentid { get; set; }
        public string name { get; set; }
        public string spellname { get; set; }
        public int state { get; set; }
        public  string kind { get; set; }
        public DateTime createtime { get; set; }
        public DateTime updatetime { get; set; }
        public string createuserid { get; set; }
        public string addressid { get; set; }
        public string addressname { get; set; }
        public string areaid { get; set; }
        public string areaname { get; set; }
        public string otherinfo { get; set; }
        public int num { get; set; }
        public override string ToString()
        {
            return fan.Fan.toStr(name);
        }
        public override int GetHashCode()
        {
            return id.GetHashCode();
        }
    }
}
