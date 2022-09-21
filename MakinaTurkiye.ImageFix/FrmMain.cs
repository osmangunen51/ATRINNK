using MakinaTurkiye.Core;
using MakinaTurkiye.Core.Infrastructure;
using MakinaTurkiye.Services;
using MakinaTurkiye.Services.Catalog;
using MakinaTurkiye.Services.Media;
using NeoSistem.MakinaTurkiye.Core.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace MakinaTurkiye.ImageFix
{
    public partial class FrmMain : Form
    {
        public string BaseFolder { get; set; } = "";
        private readonly IProductService _productService;
        private readonly IPictureService _pictureService;
        public FrmMain()
        {
            InitializeComponent();
            _productService = EngineContext.Current.Resolve<IProductService>();
            _pictureService = EngineContext.Current.Resolve<IPictureService>();
        }

        
        private void FrmMain_Load(object sender, EventArgs e)
        {
            
        }

        private void tlstrpbtnTemizle_Click(object sender, EventArgs e)
        {
            txtLog.Text = string.Empty;

        }


        int Islem = 0;
        Thread KanalIslem = null;
        int ToplamDosyaSayisi = 0;

        private void IslemYap()
        {
            //var builder = new WebPEncoderBuilder();
            //var encoder = builder
            //    .LowMemory()
            //    .MultiThread()
            //    .Build();

            //string[] extensions = new[] { ".jpg", ".png" };
            //System.IO.DirectoryInfo Root = new DirectoryInfo(txtPath.Text);
            //List<System.IO.DirectoryInfo> DizinListesi = Root.GetDirectories().ToList();
            //for (int DizinIndex = 0; DizinIndex < DizinListesi.Count(); DizinIndex++)
            //{

            //    System.IO.DirectoryInfo AktifDirectory = DizinListesi[DizinIndex];
            //    LogEkle($"{AktifDirectory.FullName} İşlem Yapılıyor...");
            //    List<System.IO.FileInfo> DosyaListesi = AktifDirectory.GetFiles("*", SearchOption.AllDirectories).Where(snc => extensions.Contains(snc.Extension.ToLower())).ToList();


            //    for (int DosyaIndex = 0; DosyaIndex < DosyaListesi.Count(); DosyaIndex++)
            //    {
            //        System.IO.FileInfo Dosya = DosyaListesi[DosyaIndex];
            //        byte[] rawWebP;
            //        try
            //        {

            //            //Bitmap bmp = (Bitmap)Image.FromFile(Dosya.FullName);
            //            string simpleLosslessFileName = Dosya.FullName.Replace(Dosya.Extension, ".webp");
            //            System.IO.FileInfo DosyaKontrol = new FileInfo(simpleLosslessFileName);
            //            if (!DosyaKontrol.Exists)
            //            {
            //                using (var outputFile = File.Open(simpleLosslessFileName, FileMode.Create))
            //                using (var inputFile = File.Open(Dosya.FullName, FileMode.Open))
            //                {
            //                    encoder.Encode(inputFile, outputFile);
            //                }
            //                LogEkle($"{Dosya.FullName} İşlemi Tamamlandı");
            //            }
            //            else
            //            {
            //                LogEkle($"{simpleLosslessFileName} Zaten Var.");
            //            }

            //        }
            //        catch (Exception Hata)
            //        {
            //            LogEkle($"{Dosya.FullName} İşleminde Hata Oluştu..." + Hata.Message);
            //        }
            //        ToplamDosyaSayisi++;
            //        this.Text = $"Dizin : {DizinListesi.Count()} - {DizinIndex + 1} | {DosyaListesi.Count()} - {DosyaIndex + 1} ==> {ToplamDosyaSayisi} Dosyada Dönüştürüldü.";
            //    }
            //    LogEkle($"{AktifDirectory.FullName} İşlemi Tamamlandı");
            //}
            //DurdurmaIslemiYap();

            BaseFolder = txtBaseDizin.Text.Trim();
            List<string> thumbSizes = new List<string>();
            thumbSizes.AddRange(AppSettings.ProductThumbSizes.Split(';'));
            
            var List = _productService.GetProductsByCategoryId(164435);
            List= List.Where(x=>x.ProductId== 186099).ToList();
            foreach (var item in List)
            {
                // <add key="ProductImageFolder" value="/UserFiles/Product/" />
                var pictures = _pictureService.GetPicturesByProductId(item.ProductId);
                foreach (var picture in pictures)
                {
                    string mainPicture = $"{BaseFolder}\\{item.ProductId.ToString()}\\{picture.PicturePath}";
                    string destinationfile = mainPicture.Replace(item.ProductId.ToString(), item.ProductId.ToString()+ "\\thumbs").Replace(".jpg","");
                    var FileBilgi=new FileInfo(destinationfile);
                    if (!FileBilgi.Directory.Exists)
                    {
                        try
                        {
                            FileBilgi.Directory.Create();
                            LogEkle($"{FileBilgi.Directory.FullName} Oluşturuldu.");
                        }
                        catch (Exception Hata)
                        {
                            LogEkle($"{Hata.Message}");
                        }
                    }
                    bool thumbResult = ImageProcessHelper.ImageResize(mainPicture, destinationfile, thumbSizes);
                    if (thumbResult)
                    {
                        LogEkle($"{picture.PicturePath} İşlemi Tamamlandı");
                    }
                }
            }
            LogEkle($"Bitti.");
            DurdurmaIslemiYap();
        }

        public void LogEkle(string value)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(LogEkle), new object[] { value });
                return;
            }
            if (txtLog.Lines.Count()>1000)
            {
                txtLog.Clear();
            }
            txtLog.AppendText($"{DateTime.Now.ToString()} :==> {value}{Environment.NewLine}");
        }

        private void btnBaslatDurdur_Click(object sender, EventArgs e)
        {
            switch (Islem)
            {
                case 0:
                    {
                        ToplamDosyaSayisi = 0;
                        Control.CheckForIllegalCrossThreadCalls = false;
                        Islem = 1;
                        btnBaslatDurdur.Text = "Duraklat";
                        KanalIslem = new Thread(new ThreadStart(IslemYap));
                        KanalIslem.Start();
                        break;
                    }
                case 1:
                    {
                        KanalIslem.Suspend();
                        Islem = 2;
                        btnBaslatDurdur.Text = "Devam Ettir";
                        break;
                    }
                case 2:
                    {
                        KanalIslem.Resume();
                        Islem = 1;
                        btnBaslatDurdur.Text = "Duraklat";
                        break;
                    }
                default:
                    break;
            }
        }

        private void btnDurdur_Click(object sender, EventArgs e)
        {
            DurdurmaIslemiYap();
        }

        private void DurdurmaIslemiYap()
        {
            Islem = 0;
            btnBaslatDurdur.Text = "Başlat";
            if (KanalIslem.IsAlive)
            {
                KanalIslem.Abort();
            }
            KanalIslem = null;
            ToplamDosyaSayisi = 0;
        }

        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.ExitThread();
        }
    }
}
