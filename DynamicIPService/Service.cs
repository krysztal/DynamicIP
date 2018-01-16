using System;
using System.Linq;
using System.Net.Http;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;

namespace DynamicIPService
{
    public partial class DynamicIPService : ServiceBase
    {
        Task task;
        CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
        CancellationToken token;

        public DynamicIPService()
        {
        }

        protected override void OnStart(string[] args)
        {
            cancelTokenSource = new CancellationTokenSource();
            token = cancelTokenSource.Token;
            task = Task.Factory.StartNew(UpdateIP, token);
            task.Start();
        }

        protected override void OnStop()
        {
            cancelTokenSource.Cancel();
        }

        void UpdateIP()
        {
            IDomainProvider idp;
            OVHDomainProvider ovh = new OVHDomainProvider();
            idp = ovh;
            string ip = String.Empty;
            while (true)
            {
                var config = DynamicIP.DynamicIPConfigManager.LoadConfiguration();
                ip = GetPublicIP();
                idp.UpdateIP(config);

                Thread.Sleep(1000 * 60 * 10); // 10 min
            }
            
        }

        string GetPublicIP()
        {
            string ipPage = "http://checkip.dyndns.org/";
            string ip = String.Empty;

            using (HttpClient client = new HttpClient())
            {
                string response = client.GetStringAsync(ipPage).Result;
                ip = response.Split(new string[] { "Current IP Address: " }, StringSplitOptions.None).Last();
                ip = ip.Split('<').First();
            }

            return ip;
        }
    }
}
