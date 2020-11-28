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
        private DeviceConfig _deviceConfig = null;
        private List<DeviceDebug> _deviceDebugs = null;
        public Device()
        {
            Load();
        }
        public void Load()
        {
            if (this.stationid != null && this.stationid != "")
                this._station = db.dbdao.GetById<Station>(this.stationid);
            if (fan.Fan.toStr(this.id) != "")
            {
                this._deviceConfig = db.dbdao.GetById<DeviceConfig>(this.id);
                this._deviceDebugs = db.dbdao.DbSql<DeviceDebug>("select * from device_debug where device_id='" + this.id + "'");
            }
        }
        [SugarColumn(IsPrimaryKey = true)]//指定主键和自动增长
        public override string id { get; set; }
        public string name { get; set; }
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
            get
            {
                return _station;
            }
            set 
            {
                this._station = value;
                if (this._station != null)
                {
                    this.stationid = this._station.id;
                    this.stationname = this._station.name;
                }
                else
                {
                    this.stationname = "";
                    this.stationid = "";
                }

            }
        }
        public string device_ver { get; set; }
        public string device_type { get; set; }
        public string device_usernum { get; set; }
        public DateTime createtime { get; set; }
        public DateTime updatetime { get; set; }
        public string createuserid { get; set; }
        public string device_config { get; set; }
        public string device_debug { get; set; }
        [SugarColumn(IsIgnore = true)]
        public DeviceConfig deviceConfig
        {
            get {
                return _deviceConfig;
            }
            set 
            {
                this._deviceConfig = value;
                if (_deviceConfig != null)
                {
                    this.device_config = _deviceConfig.ToString();
                }
                else
                {
                    this.device_config = "";
                }
            }
        }
        [SugarColumn(IsIgnore = true)]
        public List<DeviceDebug> deviceDebugs 
        { 
            get
            {
                return _deviceDebugs;
                   
            }
            set 
            {
                this._deviceDebugs = value;
                if (this._deviceDebugs != null)
                {
                    string lsd = "";
                    foreach(DeviceDebug d in this._deviceDebugs)
                    {
                        lsd += d.debuginfo+"\r\n";
                    }
                    this.device_debug = lsd;
                }
                else
                {
                    this.device_config = "";
                }
            }
        }


    }
}
