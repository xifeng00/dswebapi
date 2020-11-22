using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace db
{
    [SugarTable("public.device_config")]//指定数据库中的表名，要对应数据库的表名，否则会出错
    public class DeviceConfig:DataBase
    {
        [SugarColumn(IsPrimaryKey = true)]//指定主键和自动增长
        public override string id { get; set; }
        public string pssid { get; set; }
        public string device_id { get; set; }
        public string ppw { get; set; }
        public int perrspace { get; set; }
        public int pbreathed { get; set; }
        public int pdelytime { get; set; }
        public int popentime { get; set; }
        public int pclosetime { get; set; }
        public int pautoopentime { get; set; }
        public int pisautoopen { get; set; }
        public int pljcs { get; set; }
        public string psrvip { get; set; }
        public int psrvport { get; set; }
        public int pdaytime { get; set; }
        public int psgtime { get; set; }
        public string pupdatedns { get; set; }
        public string pupdatefile { get; set; }
        public int pupdateport { get; set; }
        public string pdskey { get; set; }

    }
}
