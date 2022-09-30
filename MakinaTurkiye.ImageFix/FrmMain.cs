using MakinaTurkiye.Core;
using MakinaTurkiye.Core.Infrastructure;
using MakinaTurkiye.Entities.Tables.Catalog;
using MakinaTurkiye.Services;
using MakinaTurkiye.Services.Catalog;
using MakinaTurkiye.Services.Media;
using NeoSistem.MakinaTurkiye.Core.Web.Helpers;
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
            txtBaseDizin.Text = @"C:\Web\s.makinaturkiye.com\Product";
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
            
            BaseFolder = txtBaseDizin.Text.Trim();
            List<string> thumbSizes = new List<string>();
            thumbSizes.AddRange(AppSettings.ProductThumbSizes.Split(';'));

            //var List = _productService.GetProductsByCategoryId(93636);
            //List= List.Where(x=>x.ProductId== 193250).ToList();
            try
            {

                //int productId = 186099;
                //int productId = 193250;
                //var pictureList = _pictureService.GetPicturesByProductId(productId);
                //var List = new List<MakinaTurkiye.Entities.Tables.Catalog.Product>();
                //List.Add(_productService.GetProductByProductId(productId));

                //int KayitSayisi = List.Count;
                //int IslemYapilanKayitSayisi = 0;

                LogEkle($"Başladım..");
                var Dizin = new DirectoryInfo(this.BaseFolder);
                LogEkle($"İşlem Yapılacak Dizin ==>{Dizin.FullName}");
                List<string> FileListesi = Dizin.EnumerateDirectories()
               .AsParallel()
               .SelectMany(di => di.EnumerateFiles("*.*", SearchOption.AllDirectories)).Select(x=>x.FullName).ToList();

                //FileListesi = FileListesi.Take(1000).ToList();
                LogEkle($"{FileListesi.Count()} Resim Dosyası Bulundu.");
                var pictureList = _pictureService.GetPictures().ToList();
                var List = _productService.GetProductsAll().Select(x=>x.ProductId).ToList();
                LogEkle($"{List.Count()} Ürün Var...");
                if (!ChkOlmayanKontrol.Checked)
                {
                    if (!string.IsNullOrEmpty(LblProductDosya.Text.Trim()))
                    {
                        List<int> IslemListesi = new List<int>();
                        try
                        {
                            string txt = System.IO.File.ReadAllText(LblProductDosya.Text.Trim());
                            IslemListesi = txt.Trim().Replace(Environment.NewLine, "é").Replace(",", "é").Split('é').Select(x => Convert.ToInt32(x.Trim())).ToList();
                            if (IslemListesi.Count > 0)
                            {
                                List = List.Where(x => IslemListesi.Contains(x)).ToList();
                            }
                        }
                        catch (Exception Hata)
                        {
                            MessageBox.Show(Hata.Message);
                        }
                    }
                }

                int KayitSayisi = List.Count;
                int IslemYapilanKayitSayisi = 0;

                if (ChkOlmayanKontrol.Checked)
                {
                    int IslemUrunKayitSayisi = 0;
                    List<int> IslemUrunListesi = new List<int>();
                    for (int Don = 0; Don < List.Count; Don++)
                    {
                        try
                        {
                            var product = List[Don];
                            //LogEkle($"{product.ToString()} kontrol Ediliyor.");
                            var TmpFileListesi = FileListesi.AsParallel()?.Where(x => x.Contains(product.ToString()));
                            var pictures = pictureList.AsParallel()?.Where(x => x.ProductId == product)?.Select(x=>x.PicturePath)?.ToList();
                            for (int Don2 = 0; Don2 < pictures.Count; Don2++)
                            {
                                bool Eklendimi = false;
                                var picture = pictures[Don2];
                                for (int i = 0; i < thumbSizes.Count; i++)
                                {
                                    var size=thumbSizes[i];
                                    string file = @"\"+ product + @"\thumbs\"+picture.Replace(".jpg", "")+"-"+ size.Replace("*x980","980X").Replace("100x*","100X") + ".jpg";
                                    if (TmpFileListesi != null)
                                    {
                                        if (TmpFileListesi?.Where(x => x.Contains(file))?.Count() == 0)
                                        {
                                            //LogEkle($"{file}");
                                            // LogEkle($"{product} - {size} Eksikti Listeye Eklendi.");
                                            IslemUrunListesi.Add(product);
                                            IslemUrunKayitSayisi++;
                                            Eklendimi = true;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        //LogEkle($"{file}");
                                        // LogEkle($"{product} - {size} Eksikti Listeye Eklendi.");
                                        IslemUrunListesi.Add(product);
                                        IslemUrunKayitSayisi++;
                                        Eklendimi = true;
                                        break;
                                    }
                                }
                                if (Eklendimi)
                                {
                                    break;
                                }
                            }
                            IslemYapilanKayitSayisi++;
                            string IfadeText = $"Toplam : {KayitSayisi} - İşlem Yapılan : {IslemYapilanKayitSayisi} - Eklenen Kayıt => {IslemUrunKayitSayisi}";
                            DurumBilgisiGuncelle(IfadeText);
                        }
                        catch (Exception Hata)
                        {
                            LogEkle(Hata.Message + " " + Hata.StackTrace);
                            continue;
                        }
                    }
                    List = IslemUrunListesi;
                }
                LogEkle($"Son Durum {List.Count()} Ürün Var...");
                KayitSayisi = List.Count;
                IslemYapilanKayitSayisi = 0;
                foreach (var item in List)
                {
                    try
                    {
                        var pictures = pictureList.Where(x => x.ProductId == item).Select(x=>x.PicturePath);
                        foreach (var picture in pictures)
                        {
                            string mainPicture = $"{BaseFolder}\\{item.ToString()}\\{picture}";
                            var mainPictureFileBilgi = new FileInfo(mainPicture);
                            if (mainPictureFileBilgi.Exists)
                            {
                                string destinationfile = mainPicture.Replace(item.ToString(), item.ToString() + "\\thumbs").Replace(".jpg", "");
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
                                int DenemeSayisi = 0;
                                while (!thumbResult && DenemeSayisi<5)
                                {
                                    thumbResult = ImageProcessHelper.ImageResize(mainPicture, destinationfile, thumbSizes);
                                    if (!thumbResult)
                                    {
                                        LogEkle($"{item} - {picture} - {mainPicture} Oluşturulamadı...");
                                        Thread.Sleep(1000);
                                    }
                                    DenemeSayisi++;
                                }
                            }
                        }
                        
                    }
                    catch (Exception Hata)
                    {
                        LogEkle($"{item} Hata Oluştu {Hata.Message}");
                        continue;
                    }
                    finally
                    {
                        IslemYapilanKayitSayisi++;
                        if (ChLogDurum.Checked)
                        {
                            LogEkle($"{item} İşlemi Tamamlandı");
                        }
                        string IfadeText = $"Toplam : {KayitSayisi} - İşlem Yapılan : {IslemYapilanKayitSayisi}";
                        DurumBilgisiGuncelle(IfadeText);
                        if (IslemYapilanKayitSayisi % 10 == 0)
                        {
                            FlushMemory();
                        }
                    }
                }
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

        private void btnSec_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog Ac=new OpenFileDialog())
            {
                if (Ac.ShowDialog()==DialogResult.OK)
                {
                    TxtProductDosya.Text = Ac.FileName;
                }
            }
        }
    }
}
