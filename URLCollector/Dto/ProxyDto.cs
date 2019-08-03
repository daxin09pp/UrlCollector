using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URLCollector.Dto
{
    public class ProxyDto
    {
        public string IP { get; set; }
        public string Port { get; set; }
    }

    public class ProxyComparer : IEqualityComparer<ProxyDto>
    {
        public static ProxyComparer Default = new ProxyComparer();
        public bool Equals(ProxyDto x, ProxyDto y)
        {
            return string.Equals(x.IP, y.IP) && string.Equals(x.Port, y.Port);
        }
        public int GetHashCode(ProxyDto obj)
        {
            return (obj.IP + obj.Port).GetHashCode();
        }
    }
}
