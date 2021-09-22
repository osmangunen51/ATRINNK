using MakinaTurkiye.Core.Configuration;
using MakinaTurkiye.Core.Infrastructure;
using MakinaTurkiye.Services.Catalog;
using MakinaTurkiye.Services.Media;
using MakinaTurkiye.Services.Members;
using MakinaTurkiye.Services.Stores;
using Quartz;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Hosting;

namespace MakinaTurkiye.Tasks.Pinterest.Tasks
{
    public class Pinterest : IJob
    {
        public string LogFile { get; set; } = "";

        private void LogEkle(string Mesaj)
        {
            if (LogFile=="")
            {
                LogFile = HostingEnvironment.MapPath("~/Log/Pinterest.txt");
            }
            Mesaj = $"{DateTime.Now.ToString()} ==> {Mesaj}{Environment.NewLine}";
            System.IO.File.AppendAllText(LogFile, Mesaj);
        }
        public Task Execute(IJobExecutionContext context)
        {
            
            Random Rnd = new Random();
            IProductService productService = EngineContext.Current.Resolve<IProductService>();
            IPictureService pictureService = EngineContext.Current.Resolve<IPictureService>();
            ICategoryService categoryService = EngineContext.Current.Resolve<ICategoryService>();
            MakinaTurkiyeConfig config = EngineContext.Current.Resolve<MakinaTurkiyeConfig>();
            LogEkle("Pinterest Görevi Başladı");
            if (config.PinDurum)
            {
                using (MakinaTurkiye.Pinterest.Istemci PinIstemci = new MakinaTurkiye.Pinterest.Istemci(config.PinUsername, config.PinPassword))
                {
                    var IslemDurum = PinIstemci.Login();
                    int IslemYapilanPin = 0;
                    if (IslemDurum.Status)
                    {
                        // IslemDurum = PinIstemci.ChangeBussines("623467279572699863", "380906218392263941");
                        int ZamanlamaPinAdet = 10;
                        if (config.PinZamanlamaPinAdet != "")
                        {
                            ZamanlamaPinAdet = Convert.ToInt32(config.PinZamanlamaPinAdet);
                        }
                        var ProductList = productService.GetProductsOnlyIdList();
                        foreach (var productitm in ProductList)
                        {
                            var product = productService.GetProductByProductId(productitm);
                            string ProductPageUrl = "";
                            ProductPageUrl = MakinaTurkiye.Utilities.HttpHelpers.UrlBuilder.GetProductUrl(product.ProductId, product.ProductName);
                            if (!ProductPageUrl.StartsWith("https://www.makinaturkiye.com"))
                            {
                                ProductPageUrl = "https://www.makinaturkiye.com" + ProductPageUrl;
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
                                List<MakinaTurkiye.Entities.Tables.Catalog.Category> productCategoryList = new List<Entities.Tables.Catalog.Category>();
                                foreach (var item in productCategoryIdList)
                                {
                                    var snc = categoryService.GetCategoryByCategoryId(item);
                                    if (snc.CategoryType == (byte)CategoryTypeEnum.Brand)
                                    {
                                        var MarkaKategori = categoryService.GetCategoryByCategoryId((int)snc.CategoryParentId);
                                        if (MarkaKategori!=null)
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
                                string SonAktifPanoPinId = "";
                                string Source = config.PinBasePano;
                                bool AllCategoryChainCreateStatus = true;
                                Source = "/makinaturkiyecom/_saved/";
                                foreach (var productCategory in productCategoryList)
                                {

                                    if (String.IsNullOrEmpty(productCategory.PinId))
                                    {
                                        int IstekTimeOut = Rnd.Next(500, 3000);
                                        Thread.Sleep(IstekTimeOut);
                                        var IslemCreateBoard = PinIstemci.CreateBoard(productCategory.CategoryContentTitle, productCategory.CategoryPath, "public", false, false, Source);
                                        if (IslemCreateBoard.Status)
                                        {
                                            MakinaTurkiye.Pinterest.Model.Result.BoardCreate.BoardCreate boardCreate = (MakinaTurkiye.Pinterest.Model.Result.BoardCreate.BoardCreate)IslemCreateBoard.Value;
                                            if (boardCreate != null)
                                            {
                                                SonAktifPanoPinId = boardCreate.resource_response.data.id;
                                                productCategory.PinId = SonAktifPanoPinId;
                                                categoryService.UpdateCategory(productCategory);
                                                //Source = boardCreate.resource_response.data.url;
                                                LogEkle($"{productCategory.CategoryContentTitle} panosu eklendi");
                                            }
                                        }
                                        else
                                        {
                                            AllCategoryChainCreateStatus = false;
                                            LogEkle($"{productCategory.CategoryContentTitle} panosu eklenemedi");
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
                                if (AllCategoryChainCreateStatus)
                                {
                                    var ProductPictures = pictureService.GetPicturesByProductId(product.ProductId).ToList();

                                    foreach (var ProductPicture in ProductPictures)
                                    {
                                        if (String.IsNullOrEmpty(ProductPicture.PinId))
                                        {
                                            string ProductKontrolUrl=HostingEnvironment.MapPath($"~/UserFiles/Product/{product.ProductId}/{ProductPicture.PicturePath}");
                                            System.IO.FileInfo FileInfoProductKontrolUrl = new FileInfo(ProductKontrolUrl);
                                            if (FileInfoProductKontrolUrl.Exists)
                                            {
                                                int IstekTimeOut = Rnd.Next(500, 3000);
                                                Thread.Sleep(IstekTimeOut);
                                                string ProductPictureUrl = "https://" + $"s.makinaturkiye.com/Product/{product.ProductId}/{ProductPicture.PicturePath}";
                                                var IslemCreateBoard = PinIstemci.CreatePin(ProductPictureUrl, SonAktifPanoPinId, productlastCategory.CategoryPath, ProductPageUrl, product.ProductName);
                                                if (IslemCreateBoard.Status)
                                                {
                                                    if (IslemCreateBoard.Value != null)
                                                    {
                                                        MakinaTurkiye.Pinterest.Model.Result.PinCreate.PinCreate PinCreate = (MakinaTurkiye.Pinterest.Model.Result.PinCreate.PinCreate)IslemCreateBoard.Value;
                                                        if (PinCreate != null)
                                                        {
                                                            ProductPicture.PinId = PinCreate.resource_response.data.id;
                                                            pictureService.UpdatePicture(ProductPicture);
                                                            IslemYapilanPin++;
                                                            LogEkle($"{product.ProductName} panosu eklendi ==>  {ProductPictureUrl} {ProductPageUrl} {IslemYapilanPin}");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        ProductPicture.PinId = "-";
                                                        pictureService.UpdatePicture(ProductPicture);
                                                        LogEkle($"{product.ProductName} panosu eklenemedi ==>  {ProductPictureUrl} {ProductPageUrl}");
                                                    }
                                                }
                                                else
                                                {
                                                    ProductPicture.PinId = "-";
                                                    pictureService.UpdatePicture(ProductPicture);
                                                    LogEkle($"{product.ProductName} panosu eklenemedi ==>  {ProductPictureUrl} {ProductPageUrl}");
                                                }                                                
                                            }
                                        }
                                        if (IslemYapilanPin > ZamanlamaPinAdet)
                                        {
                                            LogEkle($"İşlem Yapılan Pin = {IslemYapilanPin} | Zamanlama Pin Adet = {ZamanlamaPinAdet}");
                                            break;
                                        }
                                    }
                                }
                            }
                            if (IslemYapilanPin > ZamanlamaPinAdet)
                            {
                                LogEkle($"İşlem Yapılan Pin = {IslemYapilanPin} | Zamanlama Pin Adet = {ZamanlamaPinAdet}");
                                break;
                            }
                        }
                    }
                    else
                    {
                        LogEkle("Login Olunmadı. "+ IslemDurum.Value.ToString());
                    }
                }
            }
            else
            {
                LogEkle("Pinterest Görevi Kapalı.");
            }
            LogEkle("Pinterest Görevi Tamamlandı");
            return Task.CompletedTask;
        }
    }
}
