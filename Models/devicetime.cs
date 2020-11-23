using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dswebapi.Models
{
    [SugarTable("public.Device_time")]//指定数据库中的表名，要对应数据库的表名，否则会出错
    public class DeviceTime:DataBase
    {
        [SugarColumn(IsPrimaryKey = true)]//指定主键和自动增长
        public override string id { get; set; }

        public string device_id { get; set; }
        public string device_sn { get; set; }
        public DateTime actiontime { get; set; }
        public string device_action { get; set; }
        public DateTime createtime { get; set; }
        public DateTime updatetime { get; set; }
        public DateTime createuserid { get; set; }

    }
}
