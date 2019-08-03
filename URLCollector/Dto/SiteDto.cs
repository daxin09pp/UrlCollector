using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URLCollector.Dto
{
    public class SiteDto
    {
        public string URL { get; set; }
        public string Title { get; set; }
        //public bool Equals(SiteDto p)
        //{
        //    //按需求定制自己需要的比较方式
        //    return this.URL == p.URL;
        //}
        //public override int GetHashCode()
        //{
        //    return this.URL.GetHashCode();
        //}
    }
    public class SiteComparer : IEqualityComparer<SiteDto>
    {
        public static SiteComparer Default = new SiteComparer();
        public bool Equals(SiteDto x, SiteDto y)
        {
            return string.Equals(x.URL, y.URL);
        }

        public int GetHashCode(SiteDto obj)
        {
            return obj.URL.GetHashCode();
        }
    }

    internal sealed class SiteDtoMap : ClassMap<SiteDto>
    {
        public SiteDtoMap()
        {
            Map(x => x.URL).Name("Url");
            Map(x => x.Title).Name("Title");
        }
    }




}
