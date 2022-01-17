using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace ArashiDNS.Win11WellKnownDoHServersManager
{
    public partial class FormAdd : Form
    {
        RegistryKey registryKey =
            Registry.LocalMachine.OpenSubKey(
                "SYSTEM\\CurrentControlSet\\Services\\Dnscache\\Parameters\\DohWellKnownServers",true);

        public FormAdd()
        {
            InitializeComponent();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (Uri.TryCreate(textBoxURL.Text, UriKind.Absolute, out var uri) &&
                IPAddress.TryParse(textBoxIP.Text, out var ipAddress))
            {
                registryKey.CreateSubKey(ipAddress.ToString());
                registryKey.OpenSubKey(ipAddress.ToString(), true).SetValue("Template", uri.ToString());
                Close();
            }
            else
            {
                MessageBox.Show("Invalid IP address or URL template. ");
            }
        }
    }
}
