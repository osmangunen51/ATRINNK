using MakinaTurkiye.Core.Configuration;
using MakinaTurkiye.Core.Infrastructure;
using MakinaTurkiye.Services.Catalog;
using MakinaTurkiye.Services.Media;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pinterest.App
{
    public partial class FrmMain : Form
    {
        public MakinaTurkiye.Pinterest.Istemci PinterestIstemci { get; set; }
        public FrmMain()
        {
            InitializeComponent();
            PinterestIstemci = new MakinaTurkiye.Pinterest.Istemci("makinaturkiye@makinaturkiye.com", "haciosman!777");
            //PinterestIstemci = new MakinaTurkiye.Pinterest.Istemci("osmangunen51@gmail.com", "Hayat123??");
        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            var Sonuc = PinterestIstemci.Login();
            txtSonuc.Text = Newtonsoft.Json.JsonConvert.SerializeObject(Sonuc, Newtonsoft.Json.Formatting.Indented);
        }

        private void btnCreatePin_Click(object sender, EventArgs e)
        {
            var Sonuc = PinterestIstemci.CreatePin(txtREsimUrl.Text, txtBoardId.Text, txtAciklama.Text, txtLink.Text, txtBaslik.Text,"");
            txtSonuc.Text = Newtonsoft.Json.JsonConvert.SerializeObject(Sonuc, Newtonsoft.Json.Formatting.Indented);
        }

        private void btnBoardOlustur_Click(object sender, EventArgs e)
        {
            var Sonuc = PinterestIstemci.CreateBoard(txtBoardAd.Text, txtBoardAciklama.Text,"public",false,false, txtsource.Text);
            txtSonuc.Text = Newtonsoft.Json.JsonConvert.SerializeObject(Sonuc, Newtonsoft.Json.Formatting.Indented);
        }

        private void btnBoardDelete_Click(object sender, EventArgs e)
        {
            var Sonuc = PinterestIstemci.ArchiveBoard(txtBoardBoardId.Text,false,"");
            txtSonuc.Text = Newtonsoft.Json.JsonConvert.SerializeObject(Sonuc, Newtonsoft.Json.Formatting.Indented);
        }

        private void btnPinSil_Click(object sender, EventArgs e)
        {
            var Sonuc = PinterestIstemci.DeletePin(txtPinId.Text,false,"");
            txtSonuc.Text = Newtonsoft.Json.JsonConvert.SerializeObject(Sonuc, Newtonsoft.Json.Formatting.Indented);
        }

        private void BtnIslemYap_Click(object sender, EventArgs e)
        {
            
        }
    }
}
