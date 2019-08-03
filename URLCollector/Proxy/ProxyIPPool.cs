using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using URLCollector.Dto;

namespace URLCollector.Proxy
{
    public class ProxyIPPool
    {
        private static ProxyIPPool singleton = null;
        private static readonly object lockS = new object();
        private static readonly object proxyCollectorLock = new object();
        public static ProxyIPPool GetProxyIPPool()
        {
            lock (lockS)
            {
                if (singleton == null)
                {
                    singleton = new ProxyIPPool();
                }
            }
            return singleton;
        }
        private HttpClient client;
        log4net.ILog log = log4net.LogManager.GetLogger(typeof(ProxyIPPool));
        private int page_num = 20;
        private List<ProxyDto> _proxies;
        private List<ProxyDto> _bad_proxies;
        private int cursor = 0;
        public List<ProxyDto> Proxies
        {
            get
            {
                return _proxies;
            }
        }
        public HttpClient GetProxyClient()
        {
            lock (_proxies)
            {
                var proxy = _proxies[cursor % _proxies.Count];
                var httpClientHandler = new HttpClientHandler
                {
                    Proxy = new WebProxy(string.Format("http://{0}:{1}", proxy.IP, proxy.Port), false),
                    UseProxy = true
                };
                var proxyclient = new HttpClient(httpClientHandler);
                proxyclient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.3; Trident/7.0; rv:11.0) like Gecko");
                return proxyclient;
            }
        }
        public ProxyIPPool()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.3; Trident/7.0; rv:11.0) like Gecko");
        }
        public void RefreshLocalProxy()
        {
            var json = System.IO.File.ReadAllText("proxies.json", Encoding.UTF8);
            var bad_json = System.IO.File.ReadAllText("bad_proxies.json", Encoding.UTF8);
            _proxies = JsonConvert.DeserializeObject<List<ProxyDto>>(json);
            _bad_proxies = JsonConvert.DeserializeObject<List<ProxyDto>>(bad_json);
            this.ValidAllProxies(this._proxies, null, true);
        }
        public void CollectProxyIP(Action<int> updateAction)
        {
            this.RefreshLocalProxy();
            log.Debug("Collect Proxy Ip start...");
            var allProxy = new List<ProxyDto>();
            for (int i = 0; i < page_num; i++)
            {
                try
                {
                    //如果是503，用代理访问。
                    var url = "https://www.xicidaili.com/wt/" + string.Format("{0}", i + 1);
                    var ipxpath = "//table[@id=\"ip_list\"]//tr[position()>1]/td[position()=2]/text()";
                    var portxpath = "//table[@id=\"ip_list\"]//tr[position()>1]/td[position()=3]/text()";
                    var results = this.GetContent(url, ipxpath, portxpath);
                    allProxy.AddRange(results);
                }
                catch (Exception e)
                {
                    log.ErrorFormat("Collect xicidaili.com proxy failed.{0}", e.Message);
                }
                try
                {
                    var url2 = "https://www.kuaidaili.com/free/inha/" + string.Format("{0}/", i + 1);
                    var url_xpath_kuaidaili = "//td[@data-title=\"IP\"]/text()";
                    var port_xpath_kuaidaili = "//td[@data-title=\"PORT\"]/text()";
                    var results2 = this.GetContent(url2, url_xpath_kuaidaili, port_xpath_kuaidaili);
                    allProxy.AddRange(results2);
                }
                catch (Exception e)
                {
                    log.ErrorFormat("Collect kuaidaili.com proxy failed.{0}", e.Message);
                }
                Thread.Sleep(1000);
            }
            this.ValidAllProxies(allProxy, updateAction);
        }
        public List<ProxyDto> GetContent(string url, string ipxpath, string portxpath)
        {
            var proxyList = new List<ProxyDto>();
            var html = client.GetStringAsync(url).Result;
            HtmlDocument hd = new HtmlDocument();
            hd.LoadHtml(html);
            HtmlNode root = hd.DocumentNode;
            HtmlNodeCollection ipNodes = root.SelectNodes(ipxpath);
            HtmlNodeCollection portNodes = root.SelectNodes(portxpath);
            for (int j = 0; j < ipNodes.Count; j++)
            {
                var proxy = new ProxyDto();
                proxy.IP = ipNodes[j].InnerText;
                proxy.Port = portNodes[j].InnerText;
                proxyList.Add(proxy);
            }
            return proxyList;
        }
        public void ValidAllProxies(List<ProxyDto> proxiesDto, Action<int> updateAction, bool oldProxy = false)
        {
            var option = new ParallelOptions() { MaxDegreeOfParallelism = 20 };
            var pendingRemoveDtos = new List<ProxyDto>();
            Parallel.ForEach(proxiesDto, option, item =>
            {
                if (!oldProxy)
                {
                    lock (proxyCollectorLock)
                    {
                        if (this.CheckProxyExist(this._proxies, item) || this.CheckProxyExist(this._bad_proxies, item))
                        {
                            return;
                        }
                    }
                    if (this.ValidProxy(item))
                    {
                        lock (proxyCollectorLock)
                        {
                            if (updateAction != null)
                            {
                                updateAction(this._proxies.Count);
                            }
                            this._proxies.Add(item);
                        }
                    }
                    else
                    {
                        lock (proxyCollectorLock)
                        {
                            this._bad_proxies.Add(item);
                        }
                    }
                }
                else
                {
                    if (!this.ValidProxy(item))
                    {
                        pendingRemoveDtos.Add(item);
                    }
                }
            });
            if (oldProxy)
            {
                foreach (var dto in pendingRemoveDtos)
                {
                    lock (proxyCollectorLock)
                    {
                        this._proxies.Remove(dto);
                        this._bad_proxies.Add(dto);
                    }
                }
            }
            var json = JsonConvert.SerializeObject(_proxies);
            var badjson = JsonConvert.SerializeObject(_bad_proxies);
            System.IO.File.WriteAllText("proxies.json", json, Encoding.UTF8);
            System.IO.File.WriteAllText("bad_proxies.json", badjson, Encoding.UTF8);
        }
        public bool ValidProxy(ProxyDto proxy)
        {
            var httpClientHandler = new HttpClientHandler
            {
                Proxy = new WebProxy(string.Format("http://{0}:{1}", proxy.IP, proxy.Port), false),
                UseProxy = true
            };
            var proxyclient = new HttpClient(httpClientHandler);
            proxyclient.Timeout = new TimeSpan(0, 0, 40);
            proxyclient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3770.142 Safari/537.36");
            var testurl = "https://www.baidu.com";
            try
            {
                //var html = proxyclient.GetStringAsync(testurl).Result;
                var task = proxyclient.GetStringAsync(testurl);
                var html = task.Result;
                if (string.IsNullOrEmpty(html))
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                log.WarnFormat("Valid Proxy Faied. Exception {0}", e.ToString());
                return false;
            }
            return true;
        }
        private bool CheckProxyExist(List<ProxyDto> list, ProxyDto dto)
        {
            foreach (var item in list)
            {
                if (string.Equals(item.IP, dto.IP) && string.Equals(item.Port, dto.Port))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
