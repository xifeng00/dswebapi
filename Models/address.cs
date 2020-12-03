using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dswebapi.Models
{
    /// <summary>
    /// 地址类
    /// </summary>
    [SugarTable("public.address")]//指定数据库中的表名，要对应数据库的表名，否则会出错
    public class Address : DataBase
    {
        private Area _Area = null;
        public Address()
        {
            //Load();
        }
        public void Load()
        {
            if (fan.Fan.toStr(this.areaid) != "")
            {
                this._Area = db.dbdao.GetById<Area>(this.areaid);
            }
            else
            {
                this._Area = null;
            }
        }
        [SugarColumn(IsPrimaryKey = true)]//指定主键和自动增长
        public override string id { get; set; }
        public string name { get; set; }
        public string lat { get; set; }
        public string lng { get; set; }
        public string areaid { get; set; }
        public string areaname { get; set; }

        [SugarColumn(IsIgnore = true)]
        public Area Area
        {
            get
            {
                return _Area;
            }
            set
            {
                this._Area = value;
                if (this._Area == null) { return; }
                if (this._Area == null)
                {
                    this.areaid = "";
                    this.areaname = "";
                }
                else
                {
                    this.areaid = this._Area.id;
                    this.areaname = this._Area.name;
                }

            }
        }
        public DateTime creattime { get; set; }
        public DateTime updatetime { get; set; }
        public string creatuserid { get; set; }
        public int num { get; set; }
        public override string ToString()
        {
            if (name == null) { return ""; }
            else { return name; }

        }

    }
}
