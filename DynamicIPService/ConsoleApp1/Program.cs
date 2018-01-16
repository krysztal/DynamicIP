using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string ipPage = "http://checkip.dyndns.org/";
            using (HttpClient client = new HttpClient())
            {
                string response = client.GetStringAsync(ipPage).Result;
                string ip = response.Split(new string[] { "Current IP Address: " }, StringSplitOptions.None).Last();
                ip = ip.Split('<').First();
                Console.WriteLine(ip);
            }
                

            Console.ReadKey();
        }
    }
}
