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
using System.Threading.Tasks;
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
            txtBaseDizin.Text = @"C:\Test\Product";
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

            //var List = _productService.GetProductsByCategoryId(93636);
            //List= List.Where(x=>x.ProductId== 193250).ToList();
            try
            {

                int productId = 153278;
                var pictureList = _pictureService.GetPicturesByProductId(productId);
                var List = new List<MakinaTurkiye.Entities.Tables.Catalog.Product>();
                List.Add(_productService.GetProductByProductId(productId));
                int KayitSayisi = List.Count;
                int IslemYapilanKayitSayisi = 0;


                //var pictureList = _pictureService.GetPictures();
                //var List = _productService.GetProductsAll();
                //int KayitSayisi = List.Count;
                //int IslemYapilanKayitSayisi = 0;

                Object _lock = new Object();
                //List=List.Where(x => x.ProductId==187174).ToList();
                Parallel.ForEach(List, item =>
                {
                    // <add key="ProductImageFolder" value="/UserFiles/Product/" />
                    var pictures = pictureList.Where(x=>x.ProductId== item.ProductId);
                    Parallel.ForEach(pictures, picture =>
                    {
                        string mainPicture = $"{BaseFolder}\\{item.ProductId.ToString()}\\{picture.PicturePath}";
                        var mainPictureFileBilgi = new FileInfo(mainPicture);
                        if (mainPictureFileBilgi.Exists)
                        {
                            string destinationfile = mainPicture.Replace(item.ProductId.ToString(), item.ProductId.ToString() + "\\thumbs").Replace(".jpg", "");
                            var FileBilgi = new FileInfo(destinationfile);
                            if (!FileBilgi.Directory.Exists)
                            {
                                try
                                {
                                    FileBilgi.Directory.Create();
                                    //LogEkle($"{FileBilgi.Directory.FullName} Oluşturuldu.");
                                }
                                catch (Exception Hata)
                                {
                                    //LogEkle($"{Hata.Message}");
                                }
                            }
                            else
                            {
                                try
                                {
                                    FileBilgi.Directory.Delete();
                                    FileBilgi.Directory.Create();
                                }
                                catch (Exception Hata)
                                {
                                    //LogEkle($"{Hata.Message}");
                                }
                            }
                            bool thumbResult = false;
                            while (!thumbResult)
                            {
                                thumbResult=ImageProcessHelper.ImageResize(mainPicture, destinationfile, thumbSizes);
                                if (!thumbResult)
                                {
                                    LogEkle($"{item.ProductId} - {picture.Id} - {mainPicture} Oluşturulamadı...");
                                }
                            }
                        }
                    });

                    lock(_lock)
                    {
                        IslemYapilanKayitSayisi++;
                    }
                    if (ChLogDurum.Checked)
                    {
                        LogEkle($"{item.ProductId} İşlemi Tamamlandı");
                    }
                    string IfadeText = $"Toplam : {KayitSayisi} - İşlem Yapılan : {IslemYapilanKayitSayisi}";
                    DurumBilgisiGuncelle(IfadeText);
                });
            }
            catch (Exception Hata)
            {

                LogEkle(Hata.Message+" "+Hata.StackTrace);
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
            if (txtLog.Lines.Count()>20)
            {
                txtLog.Clear();
            }
            txtLog.AppendText($"{DateTime.Now.ToString()} :==> {value}{Environment.NewLine}");
        }


        public void DurumBilgisiGuncelle(string value)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(DurumBilgisiGuncelle), new object[] { value });
                return;
            }
            lblDurum.Text = value;
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
