using System;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;

namespace ArashiDNS.Win11WellKnownDoHServersManager
{
    public partial class FormAdd : Form
    {
        bool isZh = Thread.CurrentThread.CurrentCulture.Name.Contains("zh");

        public FormAdd()
        {
            InitializeComponent();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var registryKey =
                Registry.LocalMachine.OpenSubKey(
                    "SYSTEM\\CurrentControlSet\\Services\\Dnscache\\Parameters\\DohWellKnownServers", true);

            if (Uri.TryCreate(textBoxURL.Text, UriKind.Absolute, out var uri) &&
                IPAddress.TryParse(textBoxIP.Text, out var ipAddress))
            {
                registryKey.CreateSubKey(ipAddress.ToString());
                registryKey.OpenSubKey(ipAddress.ToString(), true).SetValue("Template", uri.ToString());
                Close();
            }
            else
            {
                MessageBox.Show(isZh ? "无效的 IP 或 URL 模板。" : "Invalid IP address or URL template. ");
            }
        }

        private void textBoxURL_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) buttonAdd_Click(sender, e);
        }

        private void FormAdd_Load(object sender, EventArgs e)
        {
            if (isZh)
            {
                buttonAdd.Text = "添加";
                labelIP.Text = "IP 地址：";
                labelUrl.Text = "查询 URL 模板：";
            }
        }
    }
}
