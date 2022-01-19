using System;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;

namespace ArashiDNS.Dekunua
{
    public partial class FormMain : Form
    {
        bool isZh = Thread.CurrentThread.CurrentCulture.Name.Contains("zh");

        public FormMain() { InitializeComponent(); }
        private void listView_KeyDown(object sender, KeyEventArgs e) { if (e.KeyCode == Keys.Delete) listDelete(); }

        private void Form1_Load(object sender, EventArgs e)
        {
            listFlash();
            if (isZh)
            {
                Text = "ArashiDNS.Dekunua - 适用于 Windows 11 的内置 DoH 服务器管理工具";
                buttonAdd.Text = "添加";
                buttonDel.Text = "删除";
                buttonImport.Text = "从在线列表导入";
                listView.Columns[0].Text = "IP 地址";
                listView.Columns[1].Text = "查询 URL 模板";
            }
        }

        private void buttonDel_Click(object sender, EventArgs e) => listDelete();

        private void listDelete()
        {
            if (listView.SelectedItems.Count <= 0) return;
            if (MessageBox.Show(isZh ? "确定要删除吗？" : "You sure you want to delete it?", "Delete",
                    MessageBoxButtons.OKCancel) != DialogResult.OK) return;
            var rKey = Registry.LocalMachine.OpenSubKey(
                    "SYSTEM\\CurrentControlSet\\Services\\Dnscache\\Parameters\\DohWellKnownServers", true);
            foreach (ListViewItem item in listView.SelectedItems) rKey.DeleteSubKeyTree(item.Text);
            listFlash();
        }

        private void listFlash()
        {
            var registryKey =
                Registry.LocalMachine.OpenSubKey(
                    "SYSTEM\\CurrentControlSet\\Services\\Dnscache\\Parameters\\DohWellKnownServers");

            listView.Items.Clear();
            foreach (var item in registryKey.GetSubKeyNames())
            {
                var li = new ListViewItem { Text = item };
                li.SubItems.Add(registryKey.OpenSubKey(item).GetValue("Template").ToString());
                listView.Items.Add(li);
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            new FormAdd().ShowDialog();
            listFlash();
        }

        private void buttonImport_Click(object sender, EventArgs e)
        {
            new FormImport().ShowDialog();
            listFlash();
        }
    }
}
