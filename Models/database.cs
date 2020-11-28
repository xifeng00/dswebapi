using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dswebapi.Models
{
    public class DataBase
    {
        public virtual string id { get; set; }
        public override bool Equals(object obj)
        {
            if (obj == null) { return false; }
            else
            {
                return ((DataBase)obj).id == this.id;
            }
        }
        public override int GetHashCode()
        {
            return this.id.GetHashCode();
        }

    }
}
