using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dswebapi.Models
{
    [SugarTable("public.Device")]//指定数据库中的表名，要对应数据库的表名，否则会出错
    public class Device:DataBase
    {
        [SugarColumn(IsPrimaryKey = true)]//指定主键和自动增长
        public override string id { get; set; }
        public string name { get; set; }
        public string device_sn { get; set; }
        public string device_num { get; set; }
        public string device_mac { get; set; }
        public bool device_online { get; set; }
        public bool device_state { get; set; }
        public string stationid { get; set; }
        public string device_ver { get; set; }
        public string device_type { get; set; }
        public string device_usernum { get; set; }
        public DateTime createtime { get; set; }
        public DateTime updatetime { get; set; }
        public string createuserid { get; set; }


    }
}
