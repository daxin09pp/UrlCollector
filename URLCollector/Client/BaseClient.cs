using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using URLCollector.Dto;
using URLCollector.Proxy;

namespace URLCollector.Client
{
    public abstract class BaseClient
    {
        private HttpClient _client;
        internal HttpClient client {
            get
            {
                if (!useProxy)
                {
                    if (_client == null)
                    {
                        _client = new HttpClient();
                        _client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3770.142 Safari/537.36");
                    }
                    return _client;
                }
                else
                {
                    var pool = ProxyIPPool.GetProxyIPPool();
                    var client = pool.GetProxyClient();
                    return client;
                }
            }
        }
        internal bool useProxy;
        internal int threadNum;
        internal int MaxPageNumber = 10000;
        internal static List<SiteDto> CollectResult;
        static log4net.ILog log = log4net.LogManager.GetLogger(typeof(BaiduClient));
        public abstract List<SiteDto> CollectHost(string keyword, int pageNum);
        public void StartCollect(string keyword, int timeInterval, int startPage, int pageNumber, Action<int> updateAction)
        {
            var option = new ParallelOptions() { MaxDegreeOfParallelism = threadNum };
            Parallel.For(startPage, startPage + pageNumber, option, num =>
            {
                try
                {
                    if (num > MaxPageNumber)
                    {
                        return;
                    }
                    var result = this.CollectHost(keyword, num);
                    if (result == null)
                    {
                        MaxPageNumber = num;
                    }
                    else
                    {
                        lock (CollectResult)
                        {
                            foreach (var s in result)
                            {
                                if (!CollectResult.Contains<SiteDto>(s, SiteComparer.Default))
                                {
                                    CollectResult.Add(s);
                                }
                            }
                            updateAction(CollectResult.Count);
                        }
                    }
                    Thread.Sleep(timeInterval * 1000);
                }
                catch (Exception e)
                {
                    log.ErrorFormat("CollectHost {0}", e.ToString());
                }
            });
        }
        public static void GenerateFile(string keyword)
        {
            var fileName = BaseClient.GetCSVFileName(keyword);
            if (CollectResult != null && CollectResult.Count != 0)
            {
                var lines = new List<string>();
                foreach (var s in CollectResult)
                {
                    var title = s.Title;
                    if (title.Contains(","))
                    {
                        title = title.Replace("\"", "\"\"");
                        title = string.Format("\"{0}\"", title);
                    }
                    lines.Add(string.Format("{0},{1}", s.URL, title));
                }
                if (lines.Count != 0)
                {
                    if (File.Exists(fileName))
                    {
                        File.Delete(fileName);
                    }
                    BaseClient.WriteCSV(fileName, new List<string>() { "Url,Title" });
                    BaseClient.WriteCSV(fileName, lines);
                }
            }
        }
        private static void WriteCSV(string fileName, List<string> lines)
        {
            System.IO.File.AppendAllLines(fileName, lines, Encoding.UTF8);
        }
        public static void InitCollectResult(string keyword)
        {
            var fileName = BaseClient.GetCSVFileName(keyword);
            if (File.Exists(fileName))
            {
                BaseClient.CollectResult = BaseClient.ReadOldCSV(fileName);
                log.DebugFormat("Read old {0} number:{1}", fileName, BaseClient.CollectResult.Count);
            }
            else
            {
                log.DebugFormat("No {0} found.", fileName);
                BaseClient.CollectResult = new List<SiteDto>();
            }
        }
        private static List<SiteDto> ReadOldCSV(string fileName)
        {
            using (var sr = new StreamReader(fileName))
            {
                using (var csvReader = new CsvReader(sr))
                {
                    csvReader.Configuration.RegisterClassMap<SiteDtoMap>();
                    return csvReader.GetRecords<SiteDto>().ToList();
                }
            }
        }
        public static string GetCSVFileName(string keyword)
        {
            keyword = keyword.Replace('<', '-').Replace('>', '-').Replace(':', '-').Replace('*', '-').Replace('?', '-').Replace('/', '-').Replace('\\', '-').Replace('|', '-');
            var fileName = string.Format("{0}.csv", keyword);
            return fileName;
        }

        public string GetHost(string realUrl)
        {
            //https://
            var index1 = realUrl.IndexOf("/", 9);
            if (index1 == -1)
            {
                return realUrl;
            }
            var host = realUrl.Remove(index1, realUrl.Length - index1);
            return host;
        }
        public abstract string GetRealURL(string url);
        public string SubString(string input, string firstString, string nextString)
        {
            var index1 = input.IndexOf(firstString);
            var tmp = input.Substring(index1 + firstString.Length);
            var index2 = tmp.IndexOf(nextString);
            var url = tmp.Remove(index2, tmp.Length - index2);
            return url;
        }
    }
}
