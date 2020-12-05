using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dswebapi.Models;

namespace dswebapi.db
{
    public class DeviceDao
    {
        private log4net.ILog log = log4net.LogManager.GetLogger(Startup.repository.Name, typeof(DeviceDao));

        /// <summary>
        /// 根据设备编码检索设备信息
        /// </summary>
        /// <param name="sn"></param>
        /// <returns></returns>
        public Device getBySn(string sn)
        {
            string sql = "select * from \"device\" where device_sn='" + sn + "'";
            List<Device> devicels = dbdao.DbSql<Device>(sql);
            Device device = new Device();
            if (devicels.Count > 0)
            {
                device = devicels[0];
            }
            return device;
            
        }
    }
}
