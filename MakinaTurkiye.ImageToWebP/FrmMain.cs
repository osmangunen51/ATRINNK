using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MakinaTurkiye.ImageToWebP
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void tlstrpbtnTemizle_Click(object sender, EventArgs e)
        {
            txtLog.Text = string.Empty;
        }

        private void btnDizinSec_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog AcKutu = new FolderBrowserDialog())
            {
                if (AcKutu.ShowDialog() ==DialogResult.OK)
                {
                    txtPath.Text = AcKutu.SelectedPath;
                }
            }
        }
    }
}
