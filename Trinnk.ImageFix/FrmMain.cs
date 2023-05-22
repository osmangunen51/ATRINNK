using Trinnk.Core;
using Trinnk.Core.Infrastructure;
using Trinnk.Entities.Tables.Catalog;
using Trinnk.Services;
using Trinnk.Services.Catalog;
using Trinnk.Services.Media;
using NeoSistem.Trinnk.Core.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;

namespace Trinnk.ImageFix
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
            txtBaseDizin.Text = @"C:\Web\s.trinnk.com\Product";
        }

        private void tlstrpbtnTemizle_Click(object sender, EventArgs e)
        {
            txtLog.Text = string.Empty;

        }


        int Islem = 0;
        Thread KanalIslem = null;
        int ToplamDosyaSayisi = 0;

        private static object _lockObject = new object();
        private void IslemYap()
        {
            
            BaseFolder = txtBaseDizin.Text.Trim();
            List<string> thumbSizes = new List<string>();
            thumbSizes.AddRange(AppSettings.ProductThumbSizes.Split(';'));

            //var List = _productService.GetProductsByCategoryId(93636);
            //List= List.Where(x=>x.ProductId== 193250).ToList();
            try
            {

                //int productId = 186099;
                //int productId = 78243;
                //var pictureList = _pictureService.GetPicturesByProductId(productId);
                //var List = new List<int>();
                //List.Add(_productService.GetProductByProductId(productId).ProductId);

                //int KayitSayisi = List.Count;
                //int IslemYapilanKayitSayisi = 0;

                LogEkle($"Başladım..");
                // var Dizin = new DirectoryInfo(this.BaseFolder);
                // LogEkle($"İşlem Yapılacak Dizin ==>{Dizin.FullName}");
                // List<string> FileListesi = Dizin.EnumerateDirectories()
                //.AsParallel()
                //.SelectMany(di => di.EnumerateFiles("*.*", SearchOption.AllDirectories)).Select(x=>x.FullName).ToList();

                // //FileListesi = FileListesi.Take(1000).ToList();
                // LogEkle($"{FileListesi.Count()} Resim Dosyası Bulundu.");


                var pictureList = _pictureService.GetPictures().ToList();
                var List = _productService.GetProductsAll().Select(x => x.ProductId).ToList();

                LogEkle($"{List.Count()} Ürün Var...");
                int KayitSayisi = List.Count;
                int IslemYapilanKayitSayisi = 0;


                LogEkle($"Son Durum {List.Count()} Ürün Var...");
                KayitSayisi = List.Count;
                IslemYapilanKayitSayisi = 0;
                var result = Parallel.For(0, KayitSayisi, (Don, state) =>
                {
                    var item = List[Don];
                    try
                    {
                        var DirectoryInfothumbs = new DirectoryInfo($"{BaseFolder}\\{item.ToString()}\\thumbs");
                        try
                        {
                            if (DirectoryInfothumbs.Exists)
                            {
                                DirectoryInfothumbs.Delete(true);
                            }
                            DirectoryInfothumbs.Create();

                        }
                        catch (Exception Hata)
                        {
                            lock (_lockObject)
                            {
                                LogEkle($"{item} {Hata.Message}");
                            }
                        }

                        var pictures = pictureList.Where(x => x.ProductId == item).Select(x => x.PicturePath).ToList();
                        var result1 = Parallel.For(0, pictures.Count(), (Don2, state2) =>
                        {
                            var picture = pictures[Don2];
                            string mainPicture = $"{BaseFolder}\\{item.ToString()}\\{picture}";

                            var mainPictureFileBilgi = new FileInfo(mainPicture);
                            if (mainPictureFileBilgi.Exists)
                            {
                                string destinationfile = mainPicture.Replace(item.ToString(), item.ToString() + "\\thumbs").Replace(".jpg", "");
                                //LogEkle($"destinationfile==>> {destinationfile}");
                                bool thumbResult = false;
                                int DenemeSayisi = 0;
                                while (!thumbResult && DenemeSayisi < 1)
                                {
                                    thumbResult = ImageProcessHelper.ImageResize(mainPicture, destinationfile, thumbSizes);
                                    if (!thumbResult)
                                    {
                                        lock (_lockObject)
                                        {
                                            LogEkle($"{destinationfile} Oluşturulamadı...");
                                        }
                                    }
                                    DenemeSayisi++;
                                }
                                if (thumbResult)
                                {
                                    foreach (var thumbSize in thumbSizes)
                                    {
                                        if (thumbSize == "900x675" || thumbSize == "500x375")
                                        {
                                            var FileBilgi = new FileInfo(destinationfile);
                                            var yol = DirectoryInfothumbs.FullName + "\\" + FileBilgi.Name + "-" + thumbSize.Replace("*", "") + ".jpg";
                                            ImageProcessHelper.AddWaterMarkNew(yol, thumbSize);
                                        }
                                    }
                                }
                            }
                        });
                    }
                    catch (Exception Hata)
                    {
                        lock (_lockObject)
                        {
                            LogEkle($"{item} Hata Oluştu {Hata.Message}");
                        }
                    }
                    finally
                    {
                        lock (_lockObject)
                        {
                            IslemYapilanKayitSayisi++;
                            if (IslemYapilanKayitSayisi % 50 == 0)
                            {
                                FlushMemory();
                            }
                            string IfadeText = $"Toplam : {KayitSayisi} - İşlem Yapılan : {IslemYapilanKayitSayisi}";
                            DurumBilgisiGuncelle(IfadeText);
                            // LogEkle($"{item} Tamamlandı.");
                        }
                    }
                });
            }
            catch (Exception Hata)
            {

                LogEkle(Hata.Message+" "+Hata.StackTrace);
            }
            LogEkle($"Bitti.");
            DurdurmaIslemiYap();
        }

        public void FlushMemory()
        {
            Process prs = Process.GetCurrentProcess();
            try
            {
                prs.MinWorkingSet = (IntPtr)(300000);
            }
            catch (Exception exception)
            {
                throw new Exception();
            }
        }


        public void LogEkle(string value)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(LogEkle), new object[] { value });
                return;
            }
            //if (txtLog.Lines.Count()>20)
            //{
            //    txtLog.Clear();
            //}
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
