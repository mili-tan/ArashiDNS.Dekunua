using System;
using System.Windows.Forms;
using Microsoft.Win32;

namespace ArashiDNS.Win11WellKnownDoHServersManager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var key =
                Registry.LocalMachine.OpenSubKey(
                    "SYSTEM\\CurrentControlSet\\Services\\Dnscache\\Parameters\\DohWellKnownServers");

            foreach (var item in key.GetSubKeyNames())
            {
                var li = new ListViewItem {Text = item};
                li.SubItems.Add(key.OpenSubKey(item).GetValue("Template").ToString());
                listView.Items.Add(li);
            }
        }
    }
}
