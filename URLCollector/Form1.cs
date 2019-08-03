using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using URLCollector.Client;
using URLCollector.Dto;
using URLCollector.Proxy;

namespace URLCollector
{
    public partial class Form1 : Form
    {
        log4net.ILog log = log4net.LogManager.GetLogger(typeof(Form1));
        public Form1()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            var keyword = SearchKeyWord.Text;
            if (string.IsNullOrEmpty(keyword))
            {
                this.StatusLabel.Text = @"请输入关键词。";
                return;
            }
            new Thread((ThreadStart)(delegate ()
            {
                try
                {
                    var pool = ProxyIPPool.GetProxyIPPool();
                    if (proxyCheckBox.Checked && (pool.Proxies == null || pool.Proxies.Count == 0))
                    {
                        this.StatusLabel.Text = @"请在代理页面收集代理或刷新本地代理，或关闭使用代理功能。";
                        return;
                    }
                    this.StatusLabel.Text = @"正在爬网，请稍后。";
                    this.DisableAllButton();
                    int num, timeInterval, startPage, pageNumber;
                    int.TryParse(ThreadNumber.Text, out num);
                    int.TryParse(TimeInterval.Text, out timeInterval);
                    int.TryParse(StartPage.Text, out startPage);
                    int.TryParse(PageNumber.Text, out pageNumber);

                    log.DebugFormat("ThreadNumber:{0},TimeInterval:{1},StartPage:{2},PageNumber:{3},KeyWord:{4}", ThreadNumber, TimeInterval, startPage, PageNumber, keyword);
                    log.DebugFormat("Baidu:{0},Sogou:{1},Use proxy:{2}", baiduCheckBox.Checked, sogouCheckBox.Checked, proxyCheckBox.Checked);

                    BaseClient.InitCollectResult(keyword);
                    var clientList = new List<BaseClient>();
                    if (baiduCheckBox.Checked)
                    {
                        clientList.Add(new BaiduClient(num, proxyCheckBox.Checked));
                    }
                    if (sogouCheckBox.Checked)
                    {
                        clientList.Add(new SogouClient(num, proxyCheckBox.Checked));
                    }

                    Parallel.ForEach(clientList, client =>
                    {
                        client.StartCollect(keyword, timeInterval, startPage, pageNumber, (int count) =>
                        {
                            StatusLabel.Text = string.Format("正在爬网，现在共搜索到结果数：{0}", count);
                            log.DebugFormat("Crawling.. result count:{0},", count);
                        });
                    });
                    log.Debug("Generate File start.");
                    BaseClient.GenerateFile(keyword);
                    log.Debug("Generate File done.");
                    var path = System.Windows.Forms.Application.StartupPath;
                    var fileName = BaseClient.GetCSVFileName(keyword);
                    StatusLabel.Text = string.Format("完成。爬网结果：{0}\\{1}", path, fileName);
                    this.EnableAllButton();
                }
                catch (Exception ex)
                {
                    log.Warn(string.Format("collect url failed. {0}", ex));
                    this.StatusLabel.Text = @"爬网失败，请查看日志。";
                }
            })).Start();
        }
        private void DisableAllButton()
        {
            this.CollectIPButton.Enabled = false;
            this.StartButton.Enabled = false;
            this.RefreshButton.Enabled = false;
        }
        private void EnableAllButton()
        {
            this.CollectIPButton.Enabled = true;
            this.StartButton.Enabled = true;
            this.RefreshButton.Enabled = true;
        }
        private void RefreshButton_Click_1(object sender, EventArgs e)
        {
            new Thread((ThreadStart)(delegate ()
            {
                try
                {
                    log.Debug(string.Format("Start Refresh Proxy."));
                    this.StatusLabel.Text = @"正在刷新测试本地代理，请稍后。";
                    this.DisableAllButton();
                    var pool = ProxyIPPool.GetProxyIPPool();
                    pool.RefreshLocalProxy();
                    this.UsableProxyCount.Text = pool.Proxies.Count.ToString();
                }
                catch (Exception ex)
                {
                    log.Warn(string.Format("Get all ip failed. {0}", ex));
                    this.StatusLabel.Text = @"刷新测试本地代理失败，请查看日志。";
                    this.EnableAllButton();
                    return;
                }
                this.StatusLabel.Text = @"刷新测试本地代理完成。";
                this.EnableAllButton();
            })).Start();
        }

        private void CollectIPButton_Click(object sender, EventArgs e)
        {
            new Thread((ThreadStart)(delegate ()
            {
                try
                {
                    log.Debug(string.Format("Start Collect Proxy."));
                    this.StatusLabel.Text = @"正在收集代理，请稍后。";
                    this.DisableAllButton();
                    var pool = ProxyIPPool.GetProxyIPPool();
                    pool.CollectProxyIP((int count) =>
                    {
                        this.StatusLabel.Text = @"正在收集代理,当前收集代理数：" + count;
                        log.Debug("Collect proxy count:" + count);
                    });
                    this.UsableProxyCount.Text = pool.Proxies.Count.ToString();
                }
                catch (Exception ex)
                {
                    log.Warn(string.Format("Get all ip failed. {0}", ex));
                    this.StatusLabel.Text = @"收集代理失败，请查看日志。";
                    this.EnableAllButton();
                    return;
                }
                this.StatusLabel.Text = @"收集代理完成。";
                this.EnableAllButton();
            })).Start();
        }
    }
}
