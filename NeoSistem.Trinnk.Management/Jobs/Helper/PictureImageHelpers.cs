using NeoSistem.Trinnk.Core.Web.Helpers;
using NeoSistem.Trinnk.Management.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace NeoSistem.Trinnk.Management.Helper
{
    public class ProductImageHelpers
    {
        #region Private Property
        private string ImageFolder { get; set; }
        private List<string> ThumbSizes { get; set; }
        private string[] ImageContentType = { "image/bmp", "image/cis-cod", "image/gif", "image/ief", "image/jpeg", "image/jpg",
                                                "image/jpeg", "image/pipeg", "image/svg+xml", "image/tiff", "image/tiff",
                                                "image/x-cmu-raster", "image/x-cmx", "image/x-icon", "image/x-portable-anymap",
                                                "image/x-portable-bitmap", "image/x-portable-graymap", "image/x-portable-pixmap",
                                                "image/x-rgb", "image/x-xbitmap", "image/x-xpixmap", "image/x-xwindowdump",
                                                "image/pjpeg", "image/png", "image/x-png" };
        #endregion

        #region Public Constructor
        public ProductImageHelpers(string imageFolder, List<string> thumbSizes)
        {
            this.ImageFolder = imageFolder;
            this.ThumbSizes = thumbSizes;
        }
        #endregion

        #region Public Metods
        #endregion

        #region ProductEdit

        public List<PictureModel> SaveProductImageEdit(HttpRequestBase imageFiles, int productID, string productName)
        {
            if (!Directory.Exists(HttpContext.Current.Server.MapPath(this.ImageFolder)))
            {
                DirectoryInfo directory = Directory.CreateDirectory(HttpContext.Current.Server.MapPath(this.ImageFolder));
            }

            if (!Directory.Exists(HttpContext.Current.Server.MapPath(this.ImageFolder) + productID.ToString()))
            {
                DirectoryInfo directory = Directory.CreateDirectory(HttpContext.Current.Server.MapPath(this.ImageFolder) + productID.ToString());
                directory.CreateSubdirectory("thumbs");
            }
            Models.Entities.TrinnkEntities entities = new Models.Entities.TrinnkEntities();
            int count = 0;
            var countmodel = entities.Pictures.Where(c => c.ProductId == productID).ToList();

            string productPicturePath = string.Empty;
            if (countmodel != null)
            {
                do
                {
                    count++;
                    productPicturePath = productName.ToImageFileName(count) + ".jpg";


                } while (countmodel.Where(c => c.PicturePath == productPicturePath).FirstOrDefault() != null);
            }

            List<PictureModel> pictureList = new List<PictureModel>();
            for (int i = 0; i < imageFiles.Files.Count; i++)
            {
                HttpPostedFileBase file = imageFiles.Files[i];
                // TODO  : resim gerekenden küçükse arkaplanı şeffaf olarak kaydetmesini sağla.
                //transparent background için png kayıt şart white backgroundu jpg ile oluır.
                //fill image
                if (file.ContentLength > 0)
                {
                    if (this.ImageContentType.Any(fc => fc == file.ContentType))
                    {
                        string fileName = file.FileName;
                        //string fileExtension = fileName.Substring(fileName.LastIndexOf("."), fileName.Length - fileName.LastIndexOf("."));
                        string filePath = string.Empty;

                        string newMainImageFilePath = this.ImageFolder + productID.ToString() + "\\";

                        fileName = productName.ToImageFileName(count) + ".jpg";
                        filePath = HttpContext.Current.Server.MapPath(newMainImageFilePath) + fileName;
                        file.SaveAs(filePath);
                        bool thumbResult = ImageProcessHelper.ImageResize(HttpContext.Current.Server.MapPath(newMainImageFilePath) + fileName, HttpContext.Current.Server.MapPath(newMainImageFilePath) + "thumbs\\" + productName.ToImageFileName(count), this.ThumbSizes);
                        if (thumbResult)
                        {
                            #region Webp Dönüşümü

                            /*Resimler boyutlandırıldıktan sonra webp formatları oluşturulmaktadır.*/

                            //var builder = new WebPEncoderBuilder();
                            //var encoder = builder
                            //    .LowMemory()
                            //    .MultiThread()
                            //    .Build();


                            //System.IO.FileInfo Dosya = new FileInfo(filePath);

                            //string simpleLosslessFileName = Dosya.FullName.Replace(Dosya.Extension, ".webp");
                            //System.IO.FileInfo DosyaKontrol = new FileInfo(simpleLosslessFileName);
                            //if (!DosyaKontrol.Exists)
                            //{
                            //    using (var outputFile = File.Open(simpleLosslessFileName, FileMode.Create))
                            //    using (var inputFile = File.Open(Dosya.FullName, FileMode.Open))
                            //    {
                            //        encoder.Encode(inputFile, outputFile);
                            //    }
                            //}

                            //foreach (var thumbsize in this.ThumbSizes)
                            //{
                            //    byte[] rawWebP;
                            //    try
                            //    {

                            //        string SourceFile = HttpContext.Current.Server.MapPath(newMainImageFilePath) + "thumbs\\" + productName.ToImageFileName(count);
                            //        SourceFile = SourceFile + "-" + thumbsize.Replace("x*", "X").Replace("*x", "X");
                            //        Dosya = new FileInfo(SourceFile);

                            //        simpleLosslessFileName = Dosya.FullName.Replace(Dosya.Extension, ".webp");
                            //        DosyaKontrol = new FileInfo(simpleLosslessFileName);
                            //        if (!DosyaKontrol.Exists)
                            //        {
                            //            using (var outputFile = File.Open(simpleLosslessFileName, FileMode.Create))
                            //            using (var inputFile = File.Open(Dosya.FullName, FileMode.Open))
                            //            {
                            //                encoder.Encode(inputFile, outputFile);
                            //            }
                            //        }
                            //    }
                            //    catch (Exception Hata)
                            //    {

                            //    }
                            //}
                            #endregion
                        }
                        pictureList.Add(new PictureModel { PictureId = count, ProductId = productID, PicturePath = fileName });
                        count++;
                    }
                }
            }

            return pictureList;
        }

        public List<PictureModel> SaveProductImageEdit2(Models.Entities.Product a)
        {

            Models.Entities.TrinnkEntities entities = new Models.Entities.TrinnkEntities();

            var countmodel = entities.Pictures.Where(c => c.ProductId == a.ProductId).ToList();
            if (countmodel.Count > 0)
            {
                string newMainImageFilePath = this.ImageFolder + a.ProductId.ToString() + "\\";
                string fileName = countmodel.OrderBy(z => z.PictureOrder).FirstOrDefault().PicturePath;
                if (!string.IsNullOrEmpty(fileName))
                {
                    try
                    {
                        bool thumbResult = ImageProcessHelper.ImageResize(HttpContext.Current.Server.MapPath(newMainImageFilePath) + fileName,HttpContext.Current.Server.MapPath(newMainImageFilePath) + "thumbs\\" + fileName.Replace(".jpg", ""), this.ThumbSizes);
                    }
                    catch (System.Exception)
                    {

                    }
                }
            }


            List<PictureModel> pictureList = new List<PictureModel>();


            return pictureList;
        }


        #endregion

    }
}