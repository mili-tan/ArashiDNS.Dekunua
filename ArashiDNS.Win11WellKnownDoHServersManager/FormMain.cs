using System;
using System.Windows.Forms;
using Microsoft.Win32;

namespace ArashiDNS.Win11WellKnownDoHServersManager
{
    public partial class FormMain : Form
    {
        RegistryKey registryKey =
            Registry.LocalMachine.OpenSubKey(
                "SYSTEM\\CurrentControlSet\\Services\\Dnscache\\Parameters\\DohWellKnownServers"); 
        public FormMain()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listFlash();
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要删除吗？","Delete", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                var rKey = registryKey =
                    Registry.LocalMachine.OpenSubKey(
                        "SYSTEM\\CurrentControlSet\\Services\\Dnscache\\Parameters\\DohWellKnownServers", true);
                foreach (ListViewItem item in listView.SelectedItems) rKey.DeleteSubKeyTree(item.Text);
                listFlash();
            }
        }

        private void listFlash()
        {
            listView.Items.Clear();
            foreach (var item in registryKey.GetSubKeyNames())
            {
                var li = new ListViewItem { Text = item };
                li.SubItems.Add(registryKey.OpenSubKey(item).GetValue("Template").ToString());
                listView.Items.Add(li);
            }
        }
    }
}
