using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;

namespace ArashiDNS.Dekunua
{
    public partial class FormImport : Form
    {
        bool isZh = Thread.CurrentThread.CurrentCulture.Name.Contains("zh");

        public FormImport()
        {
            InitializeComponent();
        }

        private void FormImport_Load(object sender, EventArgs e)
        {
            if (isZh)
            {
                labelUrl.Text = "列表链接：";
                buttonImport.Text = "导入";
            }
        }

        private void buttonImport_Click(object sender, EventArgs e)
        {
            if (Uri.TryCreate(comboBox.Text, UriKind.Absolute, out var listUri))
            {
                Enabled = false;
                var strings = new WebClient().DownloadString(listUri).Split('\n');
                foreach (var item in strings)
                {
                    if (string.IsNullOrWhiteSpace(item)) continue;
                    var strs = item.Split(',');

                    var registryKey =
                        Registry.LocalMachine.OpenSubKey(
                            "SYSTEM\\CurrentControlSet\\Services\\Dnscache\\Parameters\\DohWellKnownServers", true);

                    if (Uri.TryCreate(strs.First().Trim(), UriKind.Absolute, out var uri) &&
                        IPAddress.TryParse(strs.Last().Trim(), out var ipAddress))
                    {
                        registryKey.CreateSubKey(ipAddress.ToString());
                        registryKey.OpenSubKey(ipAddress.ToString(), true).SetValue("Template", uri.ToString());
                        Close();
                    }
                    else
                    {
                        MessageBox.Show((isZh
                            ? "无效的 IP 或 URL 模板。"
                            : "Invalid IP address or URL template. ") + Environment.NewLine + item);
                    }
                }

                Close();
            }
            else
            {
                MessageBox.Show(isZh ? "无效的 URL 。" : "Invalid URL. ");
            }
        }
    }
}
