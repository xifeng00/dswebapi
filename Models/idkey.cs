using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dswebapi.Models
{
    public class IdKey
    {
        public string id { get; set; }
        public override string ToString()
        {
            return id.ToString();
        }
        public override bool Equals(object obj)
        {
            if (obj != null)
            {
                return ((IdKey)obj).id == this.id;
            }
            else
                return false;
        }
        public override int GetHashCode()
        {
            return id.GetHashCode();
        }
    }
}
