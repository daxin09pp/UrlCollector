using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using URLCollector.Dto;

namespace URLCollector.Client
{
    class BaiduClient : BaseClient
    {
        log4net.ILog log;
        public BaiduClient(int threadNum, bool useProxy)
        {
            this.threadNum = threadNum;
            log = log4net.LogManager.GetLogger(typeof(BaiduClient));
        }
        public override List<SiteDto> CollectHost(string keyword, int pageNum)
        {
            var result = new List<SiteDto>();
            var search_url = string.Format("http://www.baidu.com/s?wd={0}&pn={1}0", keyword, pageNum - 1);
            var html = client.GetStringAsync(search_url).Result;
            if (!html.Contains(string.Format(@"<span class=""pc"">{0}</span>", pageNum)))
            {
                log.DebugFormat("[BAIDU] the page not exist.PageNum:{0} HTML:{1}", pageNum, html);
                return null;
            }
            Regex regex_titleurl = new Regex(@"<div class=""result c-container "".*<h3 class="".*""><a(?:[^\<]*\n[^\<]*)href = ""(?<url>.+?)""(?:[^\<]*\n[^\<]*)target=""_blank""(?:[^\<]*\n[^\<]*)>(?<title>.+?)</a></h3>");
            var mc = regex_titleurl.Matches(html);
            foreach (var m in mc)
            {
                Thread.Sleep(500);
                var value = m.ToString();
                var title = Regex.Replace(value, "<[^>]+>", "");
                var url = this.SubString(value, "href = \"", "\"");
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
            if (url.StartsWith("http") && !url.Contains("www.baidu.com"))
            {
                return url;
            }
            var a = client.GetAsync(url).Result;
            return a.RequestMessage.RequestUri.ToString();
            //var realUrl = this.SubString(a, "{window.location.replace(\"", "\"");
            //return realUrl;
        }
    }
}
