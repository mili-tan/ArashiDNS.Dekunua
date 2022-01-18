using System;
using System.Threading;
using System.Windows.Forms;

namespace ArashiDNS.Win11WellKnownDoHServersManager
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

        }
    }
}
