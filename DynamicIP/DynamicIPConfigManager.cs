using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DynamicIP
{
    public class DynamicIPConfig
    {
        public string DomainProvider { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string HostName { get; set; }

        
    }
    public static class DynamicIPConfigManager
    {
        private static string GetDefaultPath()
        {
            string configPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            configPath = System.IO.Path.Combine(configPath, "DynamicIP", "DynamicIP.xml");

            return configPath;
        }


        public static void SaveConfiguration(DynamicIPConfig config, string path = "")
        {
            XmlSerializer serializer = new XmlSerializer(typeof(DynamicIPConfig));
            string configPath = string.Empty;

            if (path == string.Empty)
                configPath = GetDefaultPath();

            int index = configPath.LastIndexOf('\\');
            string directoryPath = configPath.Substring(0, index);
            Directory.CreateDirectory(directoryPath);

            FileStream stream = new FileStream(configPath, FileMode.Create);
            serializer.Serialize(stream, config);
            stream.Close();
        }

        public static DynamicIPConfig LoadConfiguration(string path = "")
        {
            XmlSerializer serializer = new XmlSerializer(typeof(DynamicIPConfig));
            string configPath = string.Empty;

            if (path == string.Empty)
                configPath = GetDefaultPath();

            FileStream stream = new FileStream(configPath, FileMode.Open);
            var config = serializer.Deserialize(stream) as DynamicIPConfig;
            stream.Close();

            return config;
        }
    }

}
