#region Using Directives
using MakinaTurkiye.Core.Infrastructure;
using MakinaTurkiye.Services.Media;
using NeoSistem.MakinaTurkiye.Web.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
#endregion

namespace NeoSistem.MakinaTurkiye.Core.Web.Helpers
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
        public List<PictureModel> SaveProductImage(HttpRequestBase imageFiles, int productID, string productName)
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

            //MakinaTurkiye.Web.Models.Entities.MakinaTurkiyeEntities entities = new MakinaTurkiye.Web.Models.Entities.MakinaTurkiyeEntities();
            //int count = 1;
            //var countmodel = entities.Pictures.Where(c => c.ProductId == productID).ToList();
            //if (countmodel != null)
            //{
            //    count = countmodel.Count + 1;
            //}

            Random rastgele = new Random();
            List<PictureModel> pictureList = new List<PictureModel>();
            int counter = 1;
            for (int i = 0; i < imageFiles.Files.Count; i++)
            {
                int sayi = rastgele.Next(0, 1000);
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

                        fileName = productName.ToImageFileName(sayi) + ".jpg";
                        filePath = HttpContext.Current.Server.MapPath(newMainImageFilePath) + fileName;
                        file.SaveAs(filePath);

                        bool thumbResult = ImageProcessHelper.ImageResize(HttpContext.Current.Server.MapPath(newMainImageFilePath) + fileName,
                        HttpContext.Current.Server.MapPath(newMainImageFilePath) + "thumbs\\" + productName.ToImageFileName(sayi), this.ThumbSizes);

                        foreach (var thumbSize in ThumbSizes)
                        {
                            if (thumbSize == "980x*" || thumbSize == "500x375" || thumbSize == "*x980")
                            {
                                var yol = HttpContext.Current.Server.MapPath(newMainImageFilePath) + "thumbs\\" +
                                          productName.ToImageFileName(sayi) + "-" + thumbSize.Replace("*", "") + ".jpg";
                                AddWaterMarkNew(yol, thumbSize);
                            }
                        }

                        pictureList.Add(new PictureModel { PictureId = sayi, ProductId = productID, PicturePath = fileName, PictureOrder = counter });
                        counter++;
                    }
                }
            }

            return pictureList;
        }
        #endregion


        #region AddWaterMarkNew

        private static void AddWaterMarkNew(string orgImgFile, string size)
        {

            //create a image object containing the photograph to watermark
            Image imgPhoto;
            using (FileStream myStream = new FileStream(orgImgFile, FileMode.Open, FileAccess.Read))
            {
                imgPhoto = Image.FromStream(myStream);
            }
            int phWidth = imgPhoto.Width;
            int phHeight = imgPhoto.Height;


            size = imgPhoto.Width.ToString() + "x" + imgPhoto.Height.ToString();
            string sizeName = "675";
            //create a Bitmap the Size of the original photograph
            Bitmap bmPhoto = new Bitmap(imgPhoto);

            //bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);





            Graphics g = Graphics.FromImage(bmPhoto);

            // Trigonometry: Tangent = Opposite / Adjacent
            double tangent = (double)bmPhoto.Height / (double)bmPhoto.Width;


            // convert arctangent to degrees
            double angle = Math.Atan(tangent) * (180 / Math.PI);

            // a^2 = b^2 + c^2 ; a = sqrt(b^2 + c^2)
            double halfHypotenuse = (Math.Sqrt((bmPhoto.Height
                                   * bmPhoto.Height) +
                                   (bmPhoto.Width *
                                   bmPhoto.Width))) / 2;

            // Horizontally and vertically aligned the string
            // This makes the placement Point the physical 
            // center of the string instead of top-left.
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;
            FontFamily fontFamily = new FontFamily("Arial");
            int fontSize = 40;
            if (imgPhoto.Height < 500)
                fontSize = 30;
            else if (imgPhoto.Height < 980 && imgPhoto.Height > 500)
                fontSize = 35;

            var f = new System.Drawing.Font("Verdana ", fontSize);
            // Calculate the size of the text

            Color newColor = Color.FromArgb(100, 255, 255, 255);
            Brush myBrush = new SolidBrush(newColor);

            g.RotateTransform(-45);
            g.TranslateTransform(0, imgPhoto.Height, MatrixOrder.Append);

            g.DrawString("makinaturkiye.com", f, myBrush,
                             new Point((int)halfHypotenuse, 0),
                             stringFormat);


            //load the Bitmap into a Graphics object 
            //Graphics grPhoto = Graphics.FromImage(bmPhoto);




            ////#endregion Step #2 - Insert Watermark image

            ////Replace the original photgraphs bitmap with the new Bitmap

            //grPhoto.Dispose();
            imgPhoto = bmPhoto;
            g.Dispose();



            using (EncoderParameters encoderParameters = new EncoderParameters(1))
            using (
                EncoderParameter encoderParameter = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 80L)
                )
            {
                ImageCodecInfo codecInfo =
                    ImageCodecInfo.GetImageDecoders().First(codec => codec.FormatID == ImageFormat.Jpeg.Guid);
                encoderParameters.Param[0] = encoderParameter;
                imgPhoto.Save(orgImgFile, codecInfo, encoderParameters);

            }
            //save new image to file system.

            imgPhoto.Dispose();

        }
        #endregion AddWaterMark
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
            //MakinaTurkiye.Web.Models.Entities.MakinaTurkiyeEntities entities = new MakinaTurkiye.Web.Models.Entities.MakinaTurkiyeEntities();

            int count = 0;

            IPictureService pictureService = EngineContext.Current.Resolve<IPictureService>();
            var countmodel = pictureService.GetPicturesByProductId(productID);

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

                        bool thumbResult = ImageProcessHelper.ImageResize(HttpContext.Current.Server.MapPath(newMainImageFilePath) + fileName,
                        HttpContext.Current.Server.MapPath(newMainImageFilePath) + "thumbs\\" + productName.ToImageFileName(count), this.ThumbSizes);


                        foreach (var thumbSize in ThumbSizes)
                        {
                            if (thumbSize == "980x*" || thumbSize == "500x375" || thumbSize == "*x980")
                            {
                                var yol = HttpContext.Current.Server.MapPath(newMainImageFilePath) + "thumbs\\" +
                                          productName.ToImageFileName(count) + "-" + thumbSize.Replace("*", "") + ".jpg";
                                AddWaterMarkNew(yol, thumbSize);
                            }
                        }

                        pictureList.Add(new PictureModel { PictureId = count, ProductId = productID, PicturePath = fileName });

                        count++;
                    }

                }
            }

            return pictureList;
        }


        #endregion

    }
}
