using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using URLCollector.Dto;

namespace URLCollector.Client
{
    class SogouClient : BaseClient
    {
        log4net.ILog log;
        public SogouClient(int threadNum, bool useProxy)
        {
            this.threadNum = threadNum;
            log = log4net.LogManager.GetLogger(typeof(SogouClient));
        }

        public override List<SiteDto> CollectHost(string keyword, int pageNum)
        {
            var result = new List<SiteDto>();
            var search_url = string.Format("https://www.sogou.com/websearch/sogou.jsp?query={0}&page={1}&ie=utf8&_ast=1557664512&_asf=null&w=01029901&cid=&s_from=result_up", keyword, pageNum);
            //client.BaseAddress = new Uri(QnASetting.Host);
            var html = client.GetStringAsync(search_url).Result;
            if (!html.Contains(string.Format(@"<span>{0}</span>", pageNum)))
            {
                log.DebugFormat("[SOUGOU] the page not exist.PageNum:{0} HTML:{1}", pageNum, html);
                return null;
            }
            //Regex regex_url = new Regex(@"<h3 class=""pt"">.*?href=""(?<url>.+?)"".*?>(?<title>.+?)</a>.*?</h3>");
            Regex regex_url = new Regex(@"<h3 class="".*?href=""(?<url>.+?)"".*?>(?<title>.+?)</a>.*?</h3>");
            var mc = regex_url.Matches(html);
            foreach (var m in mc)
            {
                Thread.Sleep(100);
                var value = m.ToString();
                var title = Regex.Replace(value, "<[^>]+>", "");
                var url = this.SubString(value, "href=\"", "\"");
                var realUrl = this.GetRealURL(url);
                var host = this.GetHost(realUrl);
                var site = new SiteDto();
                site.URL = host;
                site.Title = title;
                result.Add(site);
            }
            return result;
        }
        public override string GetRealURL(string url)
        {
            if (url.StartsWith("http"))
            {
                return url;
            }
            var a = client.GetStringAsync("https://www.sogou.com" + url).Result;
            var realUrl = this.SubString(a, "window.location.replace(\"", "\"");
            return realUrl;
        }


    }
}
