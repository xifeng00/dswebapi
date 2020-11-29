using System;
using NpgsqlTypes;
using SqlSugar;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dswebapi.Models
{
    [SugarTable("public.Station")]//指定数据库中的表名，要对应数据库的表名，否则会出错
    public class Station:DataBase
    {
        private Address _Address = null;
        public Station()
        {
            Load();
        }
        public void Load()
        {
            if(fan.Fan.toStr(this.addressid)!="")
            {
                this._Address = db.dbdao.GetById<Address>(this.addressid);
            }
            else
            {
                this._Address = null;
            }
        }
        [SugarColumn(IsPrimaryKey = true)]//指定主键和自动增长
        public override string id { get; set; }
        public string name { get; set; }
        public string fzr { get; set; }
        public string tel { get; set; }
        public string addressid { get; set; }
        public string addressname{ get; set; }
        [SugarColumn(IsIgnore = true)]
        public Address Adress
        {
            get
            {
                return _Address;
            }
            set
            {
                _Address = value;
                if(this._Address!=null)
                {
                    this.addressname = this._Address.name;
                }
            }
        }
        public string remark { get; set; }
        public DateTime creattime { get; set; }
        public DateTime updatetime { get; set; }
        public string creatuserid { get; set; }
        public int num { get; set; }
        public override string ToString()
        {
            return name;
        }
        public override bool Equals(object obj)
        {
            if (obj == null) { return false; }
            else
            {
                return ((DataBase)obj).id == this.id;
            }
        }

    }

}