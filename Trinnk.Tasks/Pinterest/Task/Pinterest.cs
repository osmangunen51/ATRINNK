using Trinnk.Core.Configuration;
using Trinnk.Core.Infrastructure;
using Trinnk.Services.Catalog;
using Trinnk.Services.Media;
using Quartz;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Hosting;

namespace Trinnk.Tasks.Pinterest.Tasks
{
    public class Pinterest : IJob
    {
        public string LogFile { get; set; } = "";

        private void LogEkle(string Mesaj)
        {
            if (LogFile == "")
            {
                LogFile = HostingEnvironment.MapPath("~/Log/Pinterest.txt");
            }
            Mesaj = $"{DateTime.Now.ToString()} ==> {Mesaj}{Environment.NewLine}";
            System.IO.File.AppendAllText(LogFile, Mesaj);
        }
        public Task Execute(IJobExecutionContext context)
        {
            IProductService productService = EngineContext.Current.Resolve<IProductService>();
            IPictureService pictureService = EngineContext.Current.Resolve<IPictureService>();
            ICategoryService categoryService = EngineContext.Current.Resolve<ICategoryService>();
            TrinnkConfig config = EngineContext.Current.Resolve<TrinnkConfig>();
            string Proxy = "";
            Random Rnd = new Random();
        Basla:
            try
            {
                // LogEkle("Pinterest Görevi Başladı");
                if (config.PinDurum)
                {
                    int commonTime = Convert.ToInt32(config.PinZaman);
                    DateTime GorevBitisZaman = DateTime.Now.AddMinutes(commonTime);
                    //// LogEkle("Pinterest Görevi Enable Durumda");
                    //var ProxyServerListesi = config.ProxyServerListesi.Split(',');
                    //int RndIndex = Rnd.Next(0, ProxyServerListesi.Length - 1);
                    //string Proxy = ProxyServerListesi[RndIndex];
                    //// LogEkle("Seçilen Proxy ==> "+ Proxy);

                    using (Trinnk.Pinterest.Istemci PinIstemci = new Trinnk.Pinterest.Istemci(config.PinUsername, config.PinPassword, Proxy))
                    {
                        if (!string.IsNullOrEmpty(Proxy))
                        {
                            LogEkle("Seçilen Proxy ==> " + Proxy);
                        }

                        var IslemDurum = PinIstemci.Login();
                        int IslemYapilanPin = 0;
                        if (IslemDurum.Status)
                        {
                            string Vpn = "";
                            LogEkle("Login Olundu...");
                            int ZamanlamaPinAdet = 10;
                            if (config.PinZamanlamaPinAdet != "")
                            {
                                ZamanlamaPinAdet = Convert.ToInt32(config.PinZamanlamaPinAdet);
                            }
                            // LogEkle("ZamanlamaPinAdet...===> "+ ZamanlamaPinAdet);
                            var ProductList = productService.GetProductsOnlyIdList();
                            foreach (var productitm in ProductList)
                            {
                                if (DateTime.Now > GorevBitisZaman)
                                {
                                    break;
                                }
                                try
                                {
                                    var product = productService.GetProductByProductId(productitm);
                                    if (product == null)
                                    {
                                        continue;
                                    }
                                    // LogEkle("ProductName...===> " + product.ProductName);
                                    string ProductPageUrl = "";
                                    ProductPageUrl = Trinnk.Utilities.HttpHelpers.UrlBuilder.GetProductUrl(product.ProductId, product.ProductName);
                                    if (!ProductPageUrl.Contains("www.trinnk.com"))
                                    {
                                        ProductPageUrl = "https://www.trinnk.com" + ProductPageUrl;
                                    }
                                    var productlastCategory = categoryService.GetCategoryByCategoryId((int)product.CategoryId);
                                    if (productlastCategory != null)
                                    {
                                        List<int> productCategoryIdList = new List<int>();
                                        var productCategoryTxtIdList = product.CategoryTreeName.Split('.').ToArray();
                                        for (int Don = productCategoryTxtIdList.Length - 1; Don > -1; Don--)
                                        {
                                            int Snc = 0;
                                            bool Islem = Int32.TryParse(productCategoryTxtIdList[Don], out Snc);
                                            if (Islem)
                                            {
                                                productCategoryIdList.Add(Snc);
                                            }
                                        }
                                        List<Trinnk.Entities.Tables.Catalog.Category> productCategoryList = new List<Entities.Tables.Catalog.Category>();
                                        foreach (var item in productCategoryIdList)
                                        {
                                            var snc = categoryService.GetCategoryByCategoryId(item);
                                            if (snc != null)
                                            {
                                                if (snc.CategoryType == (byte)CategoryTypeEnum.Brand)
                                                {
                                                    var MarkaKategori = categoryService.GetCategoryByCategoryId((int)snc.CategoryParentId);
                                                    if (MarkaKategori != null)
                                                    {
                                                        productCategoryList.Add(MarkaKategori);
                                                        break;
                                                    }
                                                }
                                                ////if (snc.CategoryType == (byte)CategoryTypeEnum.Sector)
                                                ////{
                                                ////    productCategoryList.Add(snc);
                                                ////}
                                                ////if (snc.CategoryType == (byte)CategoryTypeEnum.ProductGroup)
                                                ////{
                                                ////    productCategoryList.Add(snc);
                                                ////}
                                                ////if (snc.CategoryType == (byte)CategoryTypeEnum.Category)
                                                ////{
                                                ////    productCategoryList.Add(snc);
                                                ////}
                                            }

                                        }
                                        string SonAktifPanoPinId = "";
                                        string Source = config.PinBasePano;
                                        bool AllCategoryChainCreateStatus = true;
                                        Source = "/trinnkcom/_saved/";
                                        foreach (var productCategory in productCategoryList)
                                        {
                                            // LogEkle("ProductName...===> " + productCategory.CategoryName);
                                            if (String.IsNullOrEmpty(productCategory.PinId))
                                            {
                                                int IstekTimeOut = Rnd.Next(500, 3000);
                                                Thread.Sleep(IstekTimeOut);
                                                var IslemCreateBoard = PinIstemci.CreateBoard(productCategory.CategoryContentTitle, productCategory.CategoryPath, "public", false, false, Source);
                                                if (IslemCreateBoard.Status)
                                                {
                                                    Trinnk.Pinterest.Model.Result.BoardCreate.BoardCreate boardCreate = (Trinnk.Pinterest.Model.Result.BoardCreate.BoardCreate)IslemCreateBoard.Value;
                                                    if (boardCreate != null)
                                                    {
                                                        SonAktifPanoPinId = boardCreate.resource_response.data.id;
                                                        productCategory.PinId = SonAktifPanoPinId;
                                                        categoryService.UpdateCategory(productCategory);
                                                        //Source = boardCreate.resource_response.data.url;
                                                        // LogEkle($"{productCategory.CategoryContentTitle} panosu eklendi");
                                                    }
                                                }
                                                else
                                                {
                                                    AllCategoryChainCreateStatus = false;
                                                    // LogEkle($"{productCategory.CategoryContentTitle} panosu eklenemedi");
                                                }
                                            }
                                            else
                                            {
                                                SonAktifPanoPinId = productCategory.PinId;
                                            }
                                            productlastCategory = productCategory;
                                        }
                                        if (SonAktifPanoPinId == "")
                                        {
                                            AllCategoryChainCreateStatus = false;
                                        }
                                        // LogEkle("Ürün Resimleri Eklenmeye Başladı...===> " + AllCategoryChainCreateStatus);
                                        if (AllCategoryChainCreateStatus)
                                        {
                                            var ProductPictures = pictureService.GetPicturesByProductId(product.ProductId).ToList();
                                            foreach (var ProductPicture in ProductPictures)
                                            {
                                                if (string.IsNullOrEmpty(ProductPicture.PicturePath))
                                                {
                                                    ProductPicture.PinId = "-";
                                                    pictureService.UpdatePicture(ProductPicture);
                                                    LogEkle($"{product.ProductName} Ürünü için Pin Resim Path Boş  ==> {ProductPicture.PictureId} Resim İdsi.");
                                                    continue;
                                                }
                                                // LogEkle("Ürün Resimleri Yolu ===> " + ProductPicture.PicturePath);
                                                // LogEkle("ProductPicture ID ===> " + ProductPicture.PictureId);
                                                // LogEkle("Product ID ===> " + ProductPicture.ProductId);
                                                if (String.IsNullOrEmpty(ProductPicture.PinId))
                                                {
                                                    string ProductKontrolUrl = HostingEnvironment.MapPath($"~/UserFiles/Product/{product.ProductId}/{ProductPicture.PicturePath}");
                                                    // LogEkle("Product ID ===> " + ProductKontrolUrl);
                                                    System.IO.FileInfo FileInfoProductKontrolUrl = new FileInfo(ProductKontrolUrl);
                                                    bool FileKontrol = FileInfoProductKontrolUrl.Exists;

#if DEBUG
                                                    FileKontrol = true;
#endif

                                                    //if (FileInfoProductKontrolUrl.Exists | FileKontrol)
                                                    if (FileKontrol)
                                                    {
                                                        //// LogEkle("R%esim Diskte Var   ===> " + FileInfoProductKontrolUrl.FullName);
                                                        int IstekTimeOut = Rnd.Next(500, 3000);
                                                        Thread.Sleep(IstekTimeOut);
                                                        string ProductPictureUrl = "https://" + $"s.trinnk.com/Product/{product.ProductId}/{ProductPicture.PicturePath}";
                                                        // LogEkle("Pin Ekleme Resim Urlsi ===> " + ProductPictureUrl);
                                                        // LogEkle("SonAktifPanoPinId ===> " + SonAktifPanoPinId);
                                                        var IslemCreateBoard = PinIstemci.CreatePin(ProductPictureUrl, SonAktifPanoPinId, productlastCategory.CategoryPath, ProductPageUrl, product.ProductName);
                                                        // LogEkle("Pin Ekleme Sonucu ===> " + IslemCreateBoard.Message);
                                                        if (IslemCreateBoard.Status)
                                                        {
                                                            if (IslemCreateBoard.Value != null)
                                                            {
                                                                Trinnk.Pinterest.Model.Result.PinCreate.PinCreate PinCreate = (Trinnk.Pinterest.Model.Result.PinCreate.PinCreate)IslemCreateBoard.Value;
                                                                if (PinCreate != null)
                                                                {
                                                                    ProductPicture.PinId = PinCreate.resource_response.data.id;
                                                                    pictureService.UpdatePicture(ProductPicture);
                                                                    IslemYapilanPin++;
                                                                    LogEkle($"{product.ProductName} Ürünü için Pin Eklendi ==>  {ProductPictureUrl} | {ProductPageUrl}");
                                                                }
                                                            }
                                                            else
                                                            {
                                                                ProductPicture.PinId = "-";
                                                                pictureService.UpdatePicture(ProductPicture);
                                                                LogEkle($"{product.ProductName} Ürünü için Pin Eklenemedi  1 ==>  {ProductPictureUrl} | {ProductPageUrl} {IslemCreateBoard.Error.Message} {IslemCreateBoard.Error.StackTrace}");
                                                            }
                                                        }
                                                        else
                                                        {
                                                            ProductPicture.PinId = "-";
                                                            pictureService.UpdatePicture(ProductPicture);
                                                            LogEkle($"{product.ProductName} Ürünü için Pin Eklenemedi  2 ==>  {ProductPictureUrl} | {ProductPageUrl} {IslemCreateBoard.Error.Message} {IslemCreateBoard.Error.StackTrace}");
                                                            // LogEkle($"{product.ProductName} panosu eklenemedi ==>  {ProductPictureUrl} {ProductPageUrl}");
                                                        }
                                                    }

                                                }
                                                if (IslemYapilanPin > ZamanlamaPinAdet)
                                                {
                                                    // LogEkle($"İşlem Yapılan Pin = {IslemYapilanPin} | Zamanlama Pin Adet = {ZamanlamaPinAdet}");
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                    if (IslemYapilanPin > ZamanlamaPinAdet)
                                    {
                                        // LogEkle($"İşlem Yapılan Pin = {IslemYapilanPin} | Zamanlama Pin Adet = {ZamanlamaPinAdet}");
                                        break;
                                    }
                                }
                                catch (Exception Hata)
                                {
                                    continue;
                                }
                            }
                        }
                        else
                        {
                            LogEkle("Login Olunmadı." + IslemDurum.Message.ToString());
                            var ProxyServerListesi = config.ProxyServerListesi.Split(',');
                            int RndIndex = Rnd.Next(0, ProxyServerListesi.Length - 1);
                            Proxy = ProxyServerListesi[RndIndex];
                            goto Basla;
                            //// LogEkle("Seçilen Proxy ==> "+ Proxy);
                        }
                    }
                }
                else
                {
                    // LogEkle("Pinterest Görevi Kapalı.");
                }
            }
            catch (Exception Hata)
            {
                LogEkle("Genel Hata. " + Hata.StackTrace.ToString());
                //var ProxyServerListesi = config.ProxyServerListesi.Split(',');
                //int RndIndex = Rnd.Next(0, ProxyServerListesi.Length - 1);
                //Proxy = ProxyServerListesi[RndIndex];
                //goto Basla;
            }
            // LogEkle("Pinterest Görevi Tamamlandı");
            return Task.CompletedTask;
        }
    }
}
