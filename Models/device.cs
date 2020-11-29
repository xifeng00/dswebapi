using fan;
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
      
        private Station _station = null;
        private List<DeviceDebug> _deviceDebugs = null;
        public Device()
        { 
        }
        public void Load()
        {
            if (this.stationid != null && this.stationid != "")
                this._station = db.dbdao.GetById<Station>(this.stationid);
            if (fan.Fan.toStr(this.id) != "")
            {
                this._deviceDebugs = db.dbdao.DbSql<DeviceDebug>("select * from device_debug where device_id='" + this.id + "'");
            }
        }
        [SugarColumn(IsPrimaryKey = true)]//指定主键和自动增长
        public override string id { get; set; }
        public string name { get; set; }
        public string remark { get; set; }
        public string device_sn { get; set; }
        public string device_num { get; set; }
        public string device_mac { get; set; }
        public bool device_online { get; set; }
        
        public bool device_state { get; set; }
        public string stationid { get; set; }
        public string stationname { get; set; }
        [SugarColumn(IsIgnore = true)]
        public Station station
        {
            get{return _station;}
            set {this._station = value;}
        }
        public string device_ver { get; set; }
        public string device_type { get; set; }
        public string device_usernum { get; set; }
        public DateTime createtime { get; set; }
        public DateTime updatetime { get; set; }
        public string createuserid { get; set; }
        public string device_config { get; set; }
        public string device_debug { get; set; }
        public string pssid { get; set; }
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
      
      
        [SugarColumn(IsIgnore = true)]
        public List<DeviceDebug> deviceDebugs 
        {
            get { return _deviceDebugs; }

            set { this._deviceDebugs = value; }
          
        }
        public string SetConfigToStr()
        {
            string str1 = "dskey:" + Fan.toStr(this.device_sn);
            str1 += ",pssid:" + Fan.toStr(this.pssid);
            str1 += ",ppw:" + Fan.toStr(this.ppw);
            str1 += ",psrvip:" + Fan.toStr(this.psrvip);
            str1 += ",psrvport:" + Fan.toStr(this.psrvport);
            str1 += ",pljcs:" + Fan.toStr(this.pljcs);
            str1 += ",pbreathed:" + Fan.toStr(this.pbreathed);
            str1 += ",pdelytime:" + Fan.toStr(this.pdelytime);
            str1 += ",perrspace:" + Fan.toStr(this.perrspace);
            str1 += ",pclosetime:" + Fan.toStr(this.pclosetime);
            str1 += ",pautoopentime:" + Fan.toStr(this.pautoopentime);
            str1 += ",pisautoopen:" + Fan.toStr(this.pisautoopen);
            str1 += ",pdaytime:" + Fan.toStr(this.pdaytime);
            str1 += ",psgtime:" + Fan.toStr(this.psgtime);
            str1 += ",pupdatedns:" + Fan.toStr(this.pupdatedns);
            str1 += ",pupdatefile:" + Fan.toStr(this.pupdatefile);
            str1 += ",pupdateport:" + Fan.toStr(this.pupdateport);
            this.device_config = str1;
            return str1;
        }


    }
}
