using Trinnk.Core;
using NeoSistem.Trinnk.Core.Web.Helpers;
using NeoSistem.Trinnk.Management.Models;
using NeoSistem.Trinnk.Management.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace NeoSistem.Trinnk.Management.Controllers
{
    public class BannerController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            var bannerModel = new BannerModel();
            bannerModel.BannerItems = entities.Banners.Where(c => c.CategoryId == id).ToList();

            return View(bannerModel);
        }

        [HttpPost]
        public ActionResult Edit(int id, BannerModel model)
        {
            var bannerModel = new BannerModel();
            foreach (var inputTagName in Request.Files)
            {
                if (Request.Files[inputTagName.ToString()].ContentLength > 0)
                {
                    if (inputTagName.ToString() == "Banner6Rsc")
                    {
                        var oldBanner = entities.Banners.SingleOrDefault(c => c.CategoryId == id && c.BannerType == (byte)BannerType.CategoryBanner6);
                        if (oldBanner != null)
                        {
                            entities.Banners.DeleteObject(oldBanner);
                            FileHelpers.Delete(AppSettings.BannerImagesThumbFolder + oldBanner.BannerResource);
                            FileHelpers.Delete(AppSettings.BannerImagesFolder + oldBanner.BannerResource);
                            FileHelpers.Delete(AppSettings.BannerFlashFolder + oldBanner.BannerResource);
                            FileHelpers.Delete(AppSettings.BannerGifFolder + oldBanner.BannerResource);
                            entities.SaveChanges();
                        }


                        if (Request.Files[inputTagName.ToString()].ContentType == "application/x-shockwave-flash")
                        {
                            string fileName = FileHelpers.Upload(AppSettings.BannerFlashFolder, Request.Files[inputTagName.ToString()]);
                            var banner = new Banner
                            {
                                BannerResource = fileName,
                                BannerType = (byte)BannerType.CategoryBanner6,
                                BannerLink = model.Banner6Link,
                                BannerDescription = model.Banner6Desc,
                                CategoryId = id
                            };
                            entities.Banners.AddObject(banner);
                            entities.SaveChanges();
                        }
                        else if (Request.Files[inputTagName.ToString()].ContentType == "image/gif")
                        {
                            string fileName = FileHelpers.Upload(AppSettings.BannerGifFolder, Request.Files[inputTagName.ToString()]);
                            var banner = new Banner
                            {
                                BannerResource = fileName,
                                BannerType = (byte)BannerType.CategoryBanner6,
                                BannerLink = model.Banner6Link,
                                BannerDescription = model.Banner6Desc,
                                CategoryId = id
                            };
                            entities.Banners.AddObject(banner);
                            entities.SaveChanges();
                        }
                        else
                        {
                            var thumns = new Dictionary<string, string>();
                            thumns.Add(AppSettings.BannerImagesThumbFolder, "170x390");
                            string fileName = FileHelpers.ImageResize(AppSettings.BannerImagesFolder, Request.Files[inputTagName.ToString()], thumns);

                            var banner = new Banner
                            {
                                BannerResource = fileName,
                                BannerType = (byte)BannerType.CategoryBanner6,
                                BannerLink = model.Banner6Link,
                                BannerDescription = model.Banner6Desc,
                                CategoryId = id
                            };
                            entities.Banners.AddObject(banner);
                            entities.SaveChanges();
                        }

                    }
                    else if (inputTagName.ToString() == "Banner5Rsc")
                    {
                        var oldBanner = entities.Banners.SingleOrDefault(c => c.CategoryId == id && c.BannerType == (byte)BannerType.CategoryBanner5);
                        if (oldBanner != null)
                        {
                            entities.Banners.DeleteObject(oldBanner);
                            FileHelpers.Delete(AppSettings.BannerImagesThumbFolder + oldBanner.BannerResource);
                            FileHelpers.Delete(AppSettings.BannerImagesFolder + oldBanner.BannerResource);
                            FileHelpers.Delete(AppSettings.BannerFlashFolder + oldBanner.BannerResource);
                            FileHelpers.Delete(AppSettings.BannerGifFolder + oldBanner.BannerResource);
                            entities.SaveChanges();
                        }

                        if (Request.Files[inputTagName.ToString()].ContentType == "application/x-shockwave-flash")
                        {
                            string fileName = FileHelpers.Upload(AppSettings.BannerFlashFolder, Request.Files[inputTagName.ToString()]);
                            var banner = new Banner
                            {
                                BannerResource = fileName,
                                BannerType = (byte)BannerType.CategoryBanner5,
                                BannerLink = model.Banner5Link,
                                BannerDescription = model.Banner5Desc,
                                CategoryId = id
                            };
                            entities.Banners.AddObject(banner);
                            entities.SaveChanges();
                        }
                        else if (Request.Files[inputTagName.ToString()].ContentType == "image/gif")
                        {
                            string fileName = FileHelpers.Upload(AppSettings.BannerGifFolder, Request.Files[inputTagName.ToString()]);
                            var banner = new Banner
                            {
                                BannerResource = fileName,
                                BannerType = (byte)BannerType.CategoryBanner5,
                                BannerLink = model.Banner5Link,
                                BannerDescription = model.Banner5Desc,
                                CategoryId = id
                            };
                            entities.Banners.AddObject(banner);
                            entities.SaveChanges();
                        }
                        else
                        {
                            var thumns = new Dictionary<string, string>();
                            thumns.Add(AppSettings.BannerImagesThumbFolder, "170x390");
                            string fileName = FileHelpers.ImageResize(AppSettings.BannerImagesFolder, Request.Files[inputTagName.ToString()], thumns);

                            var banner = new Banner
                            {
                                BannerResource = fileName,
                                BannerType = (byte)BannerType.CategoryBanner5,
                                BannerLink = model.Banner5Link,
                                BannerDescription = model.Banner5Desc,
                                CategoryId = id
                            };
                            entities.Banners.AddObject(banner);
                            entities.SaveChanges();
                        }

                    }
                    else if (inputTagName.ToString() == "Banner4Rsc")
                    {
                        var oldBanner = entities.Banners.SingleOrDefault(c => c.CategoryId == id && c.BannerType == (byte)BannerType.CategoryBanner4);
                        if (oldBanner != null)
                        {
                            entities.Banners.DeleteObject(oldBanner);
                            FileHelpers.Delete(AppSettings.BannerImagesThumbFolder + oldBanner.BannerResource);
                            FileHelpers.Delete(AppSettings.BannerImagesFolder + oldBanner.BannerResource);
                            FileHelpers.Delete(AppSettings.BannerFlashFolder + oldBanner.BannerResource);
                            FileHelpers.Delete(AppSettings.BannerGifFolder + oldBanner.BannerResource);
                            entities.SaveChanges();
                        }

                        if (Request.Files[inputTagName.ToString()].ContentType == "application/x-shockwave-flash")
                        {
                            string fileName = FileHelpers.Upload(AppSettings.BannerFlashFolder, Request.Files[inputTagName.ToString()]);
                            var banner = new Banner
                            {
                                BannerResource = fileName,
                                BannerType = (byte)BannerType.CategoryBanner4,
                                BannerLink = model.Banner4Link,
                                BannerDescription = model.Banner4Desc,
                                CategoryId = id
                            };
                            entities.Banners.AddObject(banner);
                            entities.SaveChanges();
                        }
                        else if (Request.Files[inputTagName.ToString()].ContentType == "image/gif")
                        {
                            string fileName = FileHelpers.Upload(AppSettings.BannerGifFolder, Request.Files[inputTagName.ToString()]);
                            var banner = new Banner
                            {
                                BannerResource = fileName,
                                BannerType = (byte)BannerType.CategoryBanner4,
                                BannerLink = model.Banner4Link,
                                BannerDescription = model.Banner4Desc,
                                CategoryId = id
                            };
                            entities.Banners.AddObject(banner);
                            entities.SaveChanges();
                        }
                        else
                        {
                            var thumns = new Dictionary<string, string>();
                            thumns.Add(AppSettings.BannerImagesThumbFolder, "728x90");
                            string fileName = FileHelpers.ImageResize(AppSettings.BannerImagesFolder, Request.Files[inputTagName.ToString()], thumns);

                            var banner = new Banner
                            {
                                BannerResource = fileName,
                                BannerType = (byte)BannerType.CategoryBanner4,
                                BannerLink = model.Banner4Link,
                                BannerDescription = model.Banner4Desc,
                                CategoryId = id
                            };

                            entities.Banners.AddObject(banner);
                            entities.SaveChanges();
                        }

                    }
                    else if (inputTagName.ToString() == "Banner3Rsc")
                    {
                        var oldBanner = entities.Banners.SingleOrDefault(c => c.CategoryId == id && c.BannerType == (byte)BannerType.CategoryBanner3);
                        if (oldBanner != null)
                        {
                            entities.Banners.DeleteObject(oldBanner);
                            FileHelpers.Delete(AppSettings.BannerImagesThumbFolder + oldBanner.BannerResource);
                            FileHelpers.Delete(AppSettings.BannerImagesFolder + oldBanner.BannerResource);
                            FileHelpers.Delete(AppSettings.BannerFlashFolder + oldBanner.BannerResource);
                            FileHelpers.Delete(AppSettings.BannerGifFolder + oldBanner.BannerResource);
                            entities.SaveChanges();
                        }

                        if (Request.Files[inputTagName.ToString()].ContentType == "application/x-shockwave-flash")
                        {
                            string fileName = FileHelpers.Upload(AppSettings.BannerFlashFolder, Request.Files[inputTagName.ToString()]);
                            var banner = new Banner
                            {
                                BannerResource = fileName,
                                BannerType = (byte)BannerType.CategoryBanner3,
                                BannerLink = model.Banner3Link,
                                BannerDescription = model.Banner3Desc,
                                CategoryId = id
                            };
                            entities.Banners.AddObject(banner);
                            entities.SaveChanges();
                        }
                        else if (Request.Files[inputTagName.ToString()].ContentType == "image/gif")
                        {
                            string fileName = FileHelpers.Upload(AppSettings.BannerGifFolder, Request.Files[inputTagName.ToString()]);
                            var banner = new Banner
                            {
                                BannerResource = fileName,
                                BannerType = (byte)BannerType.CategoryBanner3,
                                BannerLink = model.Banner3Link,
                                BannerDescription = model.Banner3Desc,
                                CategoryId = id
                            };
                            entities.Banners.AddObject(banner);
                            entities.SaveChanges();
                        }
                        else
                        {
                            var thumns = new Dictionary<string, string>();
                            thumns.Add(AppSettings.BannerImagesThumbFolder, "728x90");
                            string fileName = FileHelpers.ImageResize(AppSettings.BannerImagesFolder, Request.Files[inputTagName.ToString()], thumns);

                            var banner = new Banner
                            {
                                BannerResource = fileName,
                                BannerType = (byte)BannerType.CategoryBanner3,
                                BannerLink = model.Banner3Link,
                                BannerDescription = model.Banner3Desc,
                                CategoryId = id
                            };
                            entities.Banners.AddObject(banner);
                            entities.SaveChanges();
                        }

                    }
                    else if (inputTagName.ToString() == "Banner2Rsc")
                    {
                        var oldBanner = entities.Banners.SingleOrDefault(c => c.CategoryId == id && c.BannerType == (byte)BannerType.CategoryBanner2);
                        if (oldBanner != null)
                        {
                            entities.Banners.DeleteObject(oldBanner);
                            FileHelpers.Delete(AppSettings.BannerImagesThumbFolder + oldBanner.BannerResource);
                            FileHelpers.Delete(AppSettings.BannerImagesFolder + oldBanner.BannerResource);
                            FileHelpers.Delete(AppSettings.BannerFlashFolder + oldBanner.BannerResource);
                            FileHelpers.Delete(AppSettings.BannerGifFolder + oldBanner.BannerResource);
                            entities.SaveChanges();
                        }

                        if (Request.Files[inputTagName.ToString()].ContentType == "application/x-shockwave-flash")
                        {
                            string fileName = FileHelpers.Upload(AppSettings.BannerFlashFolder, Request.Files[inputTagName.ToString()]);
                            var banner = new Banner
                            {
                                BannerResource = fileName,
                                BannerType = (byte)BannerType.CategoryBanner2,
                                BannerLink = model.Banner2Link,
                                BannerDescription = model.Banner2Desc,
                                CategoryId = id
                            };
                            entities.Banners.AddObject(banner);
                            entities.SaveChanges();
                        }
                        else if (Request.Files[inputTagName.ToString()].ContentType == "image/gif")
                        {
                            string fileName = FileHelpers.Upload(AppSettings.BannerGifFolder, Request.Files[inputTagName.ToString()]);
                            var banner = new Banner
                            {
                                BannerResource = fileName,
                                BannerType = (byte)BannerType.CategoryBanner2,
                                BannerLink = model.Banner2Link,
                                BannerDescription = model.Banner2Desc,
                                CategoryId = id
                            };
                            entities.Banners.AddObject(banner);
                            entities.SaveChanges();
                        }
                        else
                        {
                            var thumns = new Dictionary<string, string>();
                            thumns.Add(AppSettings.BannerImagesThumbFolder, "728x90");
                            string fileName = FileHelpers.ImageResize(AppSettings.BannerImagesFolder, Request.Files[inputTagName.ToString()], thumns);

                            var banner = new Banner
                            {
                                BannerResource = fileName,
                                BannerType = (byte)BannerType.CategoryBanner2,
                                BannerLink = model.Banner2Link,
                                BannerDescription = model.Banner2Desc,
                                CategoryId = id
                            };
                            entities.Banners.AddObject(banner);
                            entities.SaveChanges();
                        }
                    }
                    else if (inputTagName.ToString() == "Banner1Rsc")
                    {
                        var oldBanner = entities.Banners.SingleOrDefault(c => c.CategoryId == id && c.BannerType == (byte)BannerType.CategoryBanner1);
                        if (oldBanner != null)
                        {
                            entities.Banners.DeleteObject(oldBanner);
                            FileHelpers.Delete(AppSettings.BannerImagesThumbFolder + oldBanner.BannerResource);
                            FileHelpers.Delete(AppSettings.BannerImagesFolder + oldBanner.BannerResource);
                            FileHelpers.Delete(AppSettings.BannerFlashFolder + oldBanner.BannerResource);
                            FileHelpers.Delete(AppSettings.BannerGifFolder + oldBanner.BannerResource);
                            entities.SaveChanges();
                        }


                        if (Request.Files[inputTagName.ToString()].ContentType == "application/x-shockwave-flash")
                        {
                            string fileName = FileHelpers.Upload(AppSettings.BannerFlashFolder, Request.Files[inputTagName.ToString()]);
                            var banner = new Banner
                            {
                                BannerResource = fileName,
                                BannerType = (byte)BannerType.CategoryBanner1,
                                BannerLink = model.Banner1Link,
                                BannerDescription = model.Banner1Desc,
                                CategoryId = id
                            };
                            entities.Banners.AddObject(banner);
                            entities.SaveChanges();
                        }
                        else if (Request.Files[inputTagName.ToString()].ContentType == "image/gif")
                        {
                            string fileName = FileHelpers.Upload(AppSettings.BannerGifFolder, Request.Files[inputTagName.ToString()]);
                            var banner = new Banner
                            {
                                BannerResource = fileName,
                                BannerType = (byte)BannerType.CategoryBanner1,
                                BannerLink = model.Banner1Link,
                                BannerDescription = model.Banner1Desc,
                                CategoryId = id
                            };
                            entities.Banners.AddObject(banner);
                            entities.SaveChanges();
                        }
                        else
                        {
                            var thumns = new Dictionary<string, string>();
                            thumns.Add(AppSettings.BannerImagesThumbFolder, "728x90");
                            string fileName = FileHelpers.ImageResize(AppSettings.BannerImagesFolder, Request.Files[inputTagName.ToString()], thumns);

                            var banner = new Banner
                            {
                                BannerResource = fileName,
                                BannerType = (byte)BannerType.CategoryBanner1,
                                BannerLink = model.Banner1Link,
                                BannerDescription = model.Banner1Desc,
                                CategoryId = id
                            };
                            entities.Banners.AddObject(banner);
                            entities.SaveChanges();
                        }

                    }

                    var bannerItems = entities.Banners.Where(c => c.CategoryId == id).ToList();

                    bannerModel.BannerItems = bannerItems;
                    if (model.SaveMessage == true)
                    {
                        bannerModel.SaveMessage = true;
                    }
                }
            }
            return View(bannerModel);
        }

        public ActionResult EditHomePage()
        {
            PAGEID = PermissionPage.AnaSayfaBanner;

            var bannerModel = new BannerModel();
            bannerModel.BannerItems = entities.Banners.Where(c => c.BannerType > (byte)BannerType.CategoryBanner6).ToList();

            return View(bannerModel);
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            var banner = entities.Banners.SingleOrDefault(c => c.BannerId == id);
            if (banner != null)
            {
                entities.Banners.DeleteObject(banner);
                entities.SaveChanges();
                return Json(true);
            }
            else
            {
                return Json(false);
            }
        }

        public JsonResult EditDelete(int id)
        {
            var editBanner = entities.Banners.SingleOrDefault(x => x.CategoryId == id);

            if (editBanner != null)
            {
                entities.Banners.DeleteObject(editBanner);
                entities.SaveChanges();
                return Json(true);
            }
            else
            {
                return Json(false);
            }
        }

        [HttpPost]
        public ActionResult EditHomePage(BannerModel model)
        {
            var bannerModel = new BannerModel();
            foreach (var inputTagName in Request.Files)
            {
                if (Request.Files[inputTagName.ToString()].ContentLength > 0)
                {
                    if (inputTagName.ToString() == "Banner7Rsc")
                    {
                        var oldBanner = entities.Banners.SingleOrDefault(c => c.CategoryId == null && c.BannerType == (byte)BannerType.ProductSideLeft);
                        //if (oldBanner != null)
                        //{
                        //    entities.Banners.DeleteObject(oldBanner);
                        //    FileHelpers.Delete(AppSettings.BannerImagesThumbFolder + oldBanner.BannerResource);
                        //    FileHelpers.Delete(AppSettings.BannerImagesFolder + oldBanner.BannerResource);
                        //    FileHelpers.Delete(AppSettings.BannerFlashFolder + oldBanner.BannerResource);
                        //    FileHelpers.Delete(AppSettings.BannerGifFolder + oldBanner.BannerResource);
                        //    entities.SaveChanges();
                        //}

                        var thumns = new Dictionary<string, string>();
                        thumns.Add(AppSettings.BannerImagesThumbFolder, "630x228");
                        string fileName = FileHelpers.ImageResize(AppSettings.BannerImagesFolder, Request.Files[inputTagName.ToString()], thumns, true);

                        var banner = new Banner
                        {
                            BannerResource = fileName,
                            BannerType = (byte)BannerType.MainSlider,
                            BannerLink = model.Banner7Link,
                            BannerDescription = model.Banner7Desc,
                            BannerOrder = model.Banner7Order,
                            CategoryId = null,
                            BannerAltTag = Request.Form["BannerAltTag"],
                            BannerImageType = Convert.ToInt16(Request.Form.Get("ImageType"))
                        };

                        entities.Banners.AddObject(banner);
                        entities.SaveChanges();
                    }
                    else if (inputTagName.ToString() == "Banner6Rsc")
                    {
                        var oldBanner = entities.Banners.SingleOrDefault(c => c.CategoryId == null && c.BannerType == (byte)BannerType.ProductSideLeft);
                        if (oldBanner != null)
                        {
                            entities.Banners.DeleteObject(oldBanner);
                            FileHelpers.Delete(AppSettings.BannerImagesThumbFolder + oldBanner.BannerResource);
                            FileHelpers.Delete(AppSettings.BannerImagesFolder + oldBanner.BannerResource);
                            FileHelpers.Delete(AppSettings.BannerFlashFolder + oldBanner.BannerResource);
                            FileHelpers.Delete(AppSettings.BannerGifFolder + oldBanner.BannerResource);
                            entities.SaveChanges();
                        }

                        if (Request.Files[inputTagName.ToString()].ContentType == "application/x-shockwave-flash")
                        {
                            string fileName = FileHelpers.Upload(AppSettings.BannerFlashFolder, Request.Files[inputTagName.ToString()]);
                            var banner = new Banner
                            {
                                BannerResource = fileName,
                                BannerType = (byte)BannerType.ProductSideLeft,
                                BannerLink = model.Banner6Link,
                                BannerDescription = model.Banner6Desc,
                                CategoryId = null
                            };
                            entities.Banners.AddObject(banner);
                            entities.SaveChanges();
                        }
                        else if (Request.Files[inputTagName.ToString()].ContentType == "image/gif")
                        {
                            string fileName = FileHelpers.Upload(AppSettings.BannerGifFolder, Request.Files[inputTagName.ToString()]);
                            var banner = new Banner
                            {
                                BannerResource = fileName,
                                BannerType = (byte)BannerType.ProductSideLeft,
                                BannerLink = model.Banner6Link,
                                BannerDescription = model.Banner6Desc,
                                CategoryId = null
                            };
                            entities.Banners.AddObject(banner);
                            entities.SaveChanges();
                        }
                        else
                        {
                            var thumns = new Dictionary<string, string>();
                            thumns.Add(AppSettings.BannerImagesThumbFolder, "252x213");
                            string fileName = FileHelpers.ImageResize(AppSettings.BannerImagesFolder, Request.Files[inputTagName.ToString()], thumns);

                            var banner = new Banner
                            {
                                BannerResource = fileName,
                                BannerType = (byte)BannerType.ProductSideLeft,
                                BannerLink = model.Banner6Link,
                                BannerDescription = model.Banner6Desc,
                                CategoryId = null
                            };
                            entities.Banners.AddObject(banner);
                            entities.SaveChanges();
                        }

                    }
                    else if (inputTagName.ToString() == "Banner5Rsc")
                    {
                        var oldBanner = entities.Banners.SingleOrDefault(c => c.CategoryId == null && c.BannerType == (byte)BannerType.CategorySideLeft);
                        if (oldBanner != null)
                        {
                            entities.Banners.DeleteObject(oldBanner);
                            FileHelpers.Delete(AppSettings.BannerImagesThumbFolder + oldBanner.BannerResource);
                            FileHelpers.Delete(AppSettings.BannerImagesFolder + oldBanner.BannerResource);
                            FileHelpers.Delete(AppSettings.BannerFlashFolder + oldBanner.BannerResource);
                            FileHelpers.Delete(AppSettings.BannerGifFolder + oldBanner.BannerResource);
                            entities.SaveChanges();
                        }

                        if (Request.Files[inputTagName.ToString()].ContentType == "application/x-shockwave-flash")
                        {
                            string fileName = FileHelpers.Upload(AppSettings.BannerFlashFolder, Request.Files[inputTagName.ToString()]);
                            var banner = new Banner
                            {
                                BannerResource = fileName,
                                BannerType = (byte)BannerType.CategorySideLeft,
                                BannerLink = model.Banner5Link,
                                BannerDescription = model.Banner5Desc,
                                CategoryId = null
                            };
                            entities.Banners.AddObject(banner);
                            entities.SaveChanges();
                        }
                        else if (Request.Files[inputTagName.ToString()].ContentType == "image/gif")
                        {
                            string fileName = FileHelpers.Upload(AppSettings.BannerGifFolder, Request.Files[inputTagName.ToString()]);
                            var banner = new Banner
                            {
                                BannerResource = fileName,
                                BannerType = (byte)BannerType.CategorySideLeft,
                                BannerLink = model.Banner5Link,
                                BannerDescription = model.Banner5Desc,
                                CategoryId = null
                            };
                            entities.Banners.AddObject(banner);
                            entities.SaveChanges();
                        }
                        else
                        {
                            var thumns = new Dictionary<string, string>();
                            thumns.Add(AppSettings.BannerImagesThumbFolder, "252x371");
                            string fileName = FileHelpers.ImageResize(AppSettings.BannerImagesFolder, Request.Files[inputTagName.ToString()], thumns);

                            var banner = new Banner
                            {
                                BannerResource = fileName,
                                BannerType = (byte)BannerType.CategorySideLeft,
                                BannerLink = model.Banner5Link,
                                BannerDescription = model.Banner5Desc,
                                CategoryId = null
                            };
                            entities.Banners.AddObject(banner);
                            entities.SaveChanges();
                        }

                    }
                    else if (inputTagName.ToString() == "Banner4Rsc")
                    {
                        var oldBanner = entities.Banners.SingleOrDefault(c => c.CategoryId == null && c.BannerType == (byte)BannerType.MainPageBottom3);
                        if (oldBanner != null)
                        {
                            entities.Banners.DeleteObject(oldBanner);
                            FileHelpers.Delete(AppSettings.BannerImagesThumbFolder + oldBanner.BannerResource);
                            FileHelpers.Delete(AppSettings.BannerImagesFolder + oldBanner.BannerResource);
                            FileHelpers.Delete(AppSettings.BannerFlashFolder + oldBanner.BannerResource);
                            FileHelpers.Delete(AppSettings.BannerGifFolder + oldBanner.BannerResource);
                            entities.SaveChanges();
                        }

                        if (Request.Files[inputTagName.ToString()].ContentType == "application/x-shockwave-flash")
                        {
                            string fileName = FileHelpers.Upload(AppSettings.BannerFlashFolder, Request.Files[inputTagName.ToString()]);
                            var banner = new Banner
                            {
                                BannerResource = fileName,
                                BannerType = (byte)BannerType.MainPageBottom3,
                                BannerLink = model.Banner4Link,
                                BannerDescription = model.Banner4Desc,
                                CategoryId = null
                            };
                            entities.Banners.AddObject(banner);
                            entities.SaveChanges();
                        }
                        else if (Request.Files[inputTagName.ToString()].ContentType == "image/gif")
                        {
                            string fileName = FileHelpers.Upload(AppSettings.BannerGifFolder, Request.Files[inputTagName.ToString()]);
                            var banner = new Banner
                            {
                                BannerResource = fileName,
                                BannerType = (byte)BannerType.MainPageBottom3,
                                BannerLink = model.Banner4Link,
                                BannerDescription = model.Banner4Desc,
                                CategoryId = null
                            };
                            entities.Banners.AddObject(banner);
                            entities.SaveChanges();
                        }
                        else
                        {
                            var thumns = new Dictionary<string, string>();
                            thumns.Add(AppSettings.BannerImagesThumbFolder, "230x70");
                            string fileName = FileHelpers.ImageResize(AppSettings.BannerImagesFolder, Request.Files[inputTagName.ToString()], thumns);

                            var banner = new Banner
                            {
                                BannerResource = fileName,
                                BannerType = (byte)BannerType.MainPageBottom3,
                                BannerLink = model.Banner4Link,
                                BannerDescription = model.Banner4Desc,
                                CategoryId = null
                            };
                            entities.Banners.AddObject(banner);
                            entities.SaveChanges();
                        }

                    }
                    else if (inputTagName.ToString() == "Banner3Rsc")
                    {
                        var oldBanner = entities.Banners.SingleOrDefault(c => c.CategoryId == null && c.BannerType == (byte)BannerType.MainPageBottom2);
                        if (oldBanner != null)
                        {
                            entities.Banners.DeleteObject(oldBanner);
                            FileHelpers.Delete(AppSettings.BannerImagesThumbFolder + oldBanner.BannerResource);
                            FileHelpers.Delete(AppSettings.BannerImagesFolder + oldBanner.BannerResource);
                            FileHelpers.Delete(AppSettings.BannerFlashFolder + oldBanner.BannerResource);
                            FileHelpers.Delete(AppSettings.BannerGifFolder + oldBanner.BannerResource);
                            entities.SaveChanges();
                        }

                        if (Request.Files[inputTagName.ToString()].ContentType == "application/x-shockwave-flash")
                        {
                            string fileName = FileHelpers.Upload(AppSettings.BannerFlashFolder, Request.Files[inputTagName.ToString()]);
                            var banner = new Banner
                            {
                                BannerResource = fileName,
                                BannerType = (byte)BannerType.MainPageBottom2,
                                BannerLink = model.Banner3Link,
                                BannerDescription = model.Banner3Desc,
                                CategoryId = null
                            };
                            entities.Banners.AddObject(banner);
                            entities.SaveChanges();
                        }
                        else if (Request.Files[inputTagName.ToString()].ContentType == "image/gif")
                        {
                            string fileName = FileHelpers.Upload(AppSettings.BannerGifFolder, Request.Files[inputTagName.ToString()]);
                            var banner = new Banner
                            {
                                BannerResource = fileName,
                                BannerType = (byte)BannerType.MainPageBottom2,
                                BannerLink = model.Banner3Link,
                                BannerDescription = model.Banner3Desc,
                                CategoryId = null
                            };
                            entities.Banners.AddObject(banner);
                            entities.SaveChanges();
                        }
                        else
                        {
                            var thumns = new Dictionary<string, string>();
                            thumns.Add(AppSettings.BannerImagesThumbFolder, "450x25");
                            string fileName = FileHelpers.ImageResize(AppSettings.BannerImagesFolder, Request.Files[inputTagName.ToString()], thumns);

                            var banner = new Banner
                            {
                                BannerResource = fileName,
                                BannerType = (byte)BannerType.MainPageBottom2,
                                BannerLink = model.Banner3Link,
                                BannerDescription = model.Banner3Desc,
                                CategoryId = null
                            };
                            entities.Banners.AddObject(banner);
                            entities.SaveChanges();
                        }

                    }
                    else if (inputTagName.ToString() == "Banner2Rsc")
                    {
                        var oldBanner = entities.Banners.SingleOrDefault(c => c.CategoryId == null && c.BannerType == (byte)BannerType.MainPageBottom1);
                        if (oldBanner != null)
                        {
                            entities.Banners.DeleteObject(oldBanner);
                            FileHelpers.Delete(AppSettings.BannerImagesThumbFolder + oldBanner.BannerResource);
                            FileHelpers.Delete(AppSettings.BannerImagesFolder + oldBanner.BannerResource);
                            FileHelpers.Delete(AppSettings.BannerFlashFolder + oldBanner.BannerResource);
                            FileHelpers.Delete(AppSettings.BannerGifFolder + oldBanner.BannerResource);
                            entities.SaveChanges();
                        }

                        if (Request.Files[inputTagName.ToString()].ContentType == "application/x-shockwave-flash")
                        {
                            string fileName = FileHelpers.Upload(AppSettings.BannerFlashFolder, Request.Files[inputTagName.ToString()]);
                            var banner = new Banner
                            {
                                BannerResource = fileName,
                                BannerType = (byte)BannerType.MainPageBottom1,
                                BannerLink = model.Banner2Link,
                                BannerDescription = model.Banner2Desc,
                                CategoryId = null
                            };
                            entities.Banners.AddObject(banner);
                            entities.SaveChanges();
                        }
                        else if (Request.Files[inputTagName.ToString()].ContentType == "image/gif")
                        {
                            string fileName = FileHelpers.Upload(AppSettings.BannerGifFolder, Request.Files[inputTagName.ToString()]);
                            var banner = new Banner
                            {
                                BannerResource = fileName,
                                BannerType = (byte)BannerType.MainPageBottom1,
                                BannerLink = model.Banner2Link,
                                BannerDescription = model.Banner2Desc,
                                CategoryId = null
                            };
                            entities.Banners.AddObject(banner);
                            entities.SaveChanges();
                        }
                        else
                        {
                            var thumns = new Dictionary<string, string>();
                            thumns.Add(AppSettings.BannerImagesThumbFolder, "235x110");
                            string fileName = FileHelpers.ImageResize(AppSettings.BannerImagesFolder, Request.Files[inputTagName.ToString()], thumns);

                            var banner = new Banner
                            {
                                BannerResource = fileName,
                                BannerType = (byte)BannerType.MainPageBottom1,
                                BannerLink = model.Banner2Link,
                                BannerDescription = model.Banner2Desc,
                                CategoryId = null
                            };
                            entities.Banners.AddObject(banner);
                            entities.SaveChanges();
                        }
                    }
                    else if (inputTagName.ToString() == "Banner1Rsc")
                    {
                        var oldBanner = entities.Banners.SingleOrDefault(c => c.CategoryId == null && c.BannerType == (byte)BannerType.MainPageRight);
                        if (oldBanner != null)
                        {
                            entities.Banners.DeleteObject(oldBanner);
                            FileHelpers.Delete(AppSettings.BannerImagesThumbFolder + oldBanner.BannerResource);
                            FileHelpers.Delete(AppSettings.BannerImagesFolder + oldBanner.BannerResource);
                            FileHelpers.Delete(AppSettings.BannerFlashFolder + oldBanner.BannerResource);
                            FileHelpers.Delete(AppSettings.BannerGifFolder + oldBanner.BannerResource);
                            entities.SaveChanges();
                        }


                        if (Request.Files[inputTagName.ToString()].ContentType == "application/x-shockwave-flash")
                        {
                            string fileName = FileHelpers.Upload(AppSettings.BannerFlashFolder, Request.Files[inputTagName.ToString()]);
                            var banner = new Banner
                            {
                                BannerResource = fileName,
                                BannerType = (byte)BannerType.MainPageRight,
                                BannerLink = model.Banner1Link,
                                BannerDescription = model.Banner1Desc,
                                CategoryId = null
                            };
                            entities.Banners.AddObject(banner);
                            entities.SaveChanges();
                        }
                        else if (Request.Files[inputTagName.ToString()].ContentType == "image/gif")
                        {
                            string fileName = FileHelpers.Upload(AppSettings.BannerGifFolder, Request.Files[inputTagName.ToString()]);
                            var banner = new Banner
                            {
                                BannerResource = fileName,
                                BannerType = (byte)BannerType.MainPageRight,
                                BannerLink = model.Banner1Link,
                                BannerDescription = model.Banner1Desc,
                                CategoryId = null
                            };
                            entities.Banners.AddObject(banner);
                            entities.SaveChanges();
                        }
                        else
                        {
                            var thumns = new Dictionary<string, string>();
                            thumns.Add(AppSettings.BannerImagesThumbFolder, "230x230");
                            string fileName = FileHelpers.ImageResize(AppSettings.BannerImagesFolder, Request.Files[inputTagName.ToString()], thumns);

                            var banner = new Banner
                            {
                                BannerResource = fileName,
                                BannerType = (byte)BannerType.MainPageRight,
                                BannerLink = model.Banner1Link,
                                BannerDescription = model.Banner1Desc,
                                CategoryId = null
                            };
                            entities.Banners.AddObject(banner);
                            entities.SaveChanges();
                        }

                    }

                }
                bannerModel.BannerItems = entities.Banners.Where(c => c.BannerType > (byte)BannerType.CategoryBanner6).ToList();
                if (model.SaveMessage == true)
                {
                    bannerModel.SaveMessage = true;
                }
            }
            return View(bannerModel);
        }

        public ActionResult SliderPost()
        {
            return View();
        }
    }
}