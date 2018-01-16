using System;
using System.Windows;
using System.IO;
using System.Windows.Controls;
using System.Diagnostics;
using System.Xml.Serialization;

namespace DynamicIP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private XmlSerializer serializer;
        private string configPath;
        private string appName;

        public MainWindow()
        {
            InitializeComponent();
            InitConfiguration();
        }

        private void InitConfiguration()
        {
            DynamicIPConfig config = new DynamicIPConfig();
            try
            {
                config = DynamicIPConfigManager.LoadConfiguration();
            }
            catch (Exception ex) { }

            UpdateView(config);
        }

        private void UpdateView(DynamicIPConfig config)
        {
            for (int i = 0; i < domainProviders.Items.Count; i++)
            {
                if ((string)(domainProviders.Items[i] as ComboBoxItem).Content == config.DomainProvider)
                {
                    domainProviders.SelectedIndex = i;
                    break;
                }
            }

            login.Text = config.Login;
            password.Text = config.Password;
            hostName.Text = config.HostName;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            DynamicIPConfig config = new DynamicIPConfig
            {
                DomainProvider = (string)((ComboBoxItem)domainProviders.SelectedItem).Content,
                Login = login.Text,
                Password = password.Text,
                HostName = hostName.Text
            };

            DynamicIPConfigManager.SaveConfiguration(config);
        }
    }
}
