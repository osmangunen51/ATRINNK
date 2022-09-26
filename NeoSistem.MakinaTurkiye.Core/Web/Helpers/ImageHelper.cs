using ImageResizer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using ImageProcessor;
using ImageProcessor.Imaging.Formats;

namespace NeoSistem.MakinaTurkiye.Core.Web.Helpers
{
    public static class ImageProcessHelper
    {
        private static readonly Regex rgxFileName;

        static ImageProcessHelper()
        {
            rgxFileName = new Regex(@"[^A-Za-z0-9]+", RegexOptions.Compiled);
        }

        public static string ToImageFileName(this string productName, int? indexNumber = null)
        {
            string tmp = productName.Trim().ToLowerInvariant();
            tmp = tmp.Replace("ü", "u");
            tmp = tmp.Replace("ğ", "g");
            tmp = tmp.Replace("ö", "o");
            tmp = tmp.Replace("ş", "s");
            tmp = tmp.Replace("ç", "c");
            tmp = tmp.Replace("ı", "i");

            return rgxFileName.Replace(tmp, "_") + (indexNumber.HasValue ? "-" + indexNumber.ToString() : string.Empty);
        }


        public static string ToImageFileNameNew(this string productName, int? indexNumber = null)
        {
            string tmp = productName.Trim().ToLowerInvariant();
            tmp = tmp.Replace("ü", "u");
            tmp = tmp.Replace("ğ", "g");
            tmp = tmp.Replace("ö", "o");
            tmp = tmp.Replace("ş", "s");
            tmp = tmp.Replace("ç", "c");
            tmp = tmp.Replace("ı", "i");

            return rgxFileName.Replace(tmp, "_") + (indexNumber.HasValue ? "_" + indexNumber.ToString() : string.Empty);
        }

        public static Image resizeImageBanner(int newWidth, int newHeight, string stPhotoPath)
        {
            Image imgPhoto = Image.FromFile(stPhotoPath);

            int sourceWidth = imgPhoto.Width;
            int sourceHeight = imgPhoto.Height;

            if (sourceWidth < sourceHeight)
            {
                int buff = newWidth;

                newWidth = newHeight;
                newHeight = buff;

            }

            int sourceX = 0, sourceY = 0, destX = 0, destY = 0;
            float nPercent = 0, nPercentW = 0, nPercentH = 0;

            nPercentW = ((float)newWidth / (float)sourceWidth);
            nPercentH = ((float)newHeight / (float)sourceHeight);
            if (nPercentH < nPercentW)
            {
                nPercent = nPercentH;
                destX = System.Convert.ToInt16((newWidth -
                          (sourceWidth * nPercent)) / 2);
            }
            else
            {
                nPercent = nPercentW;
                destY = System.Convert.ToInt16((newHeight -
                          (sourceHeight * nPercent)) / 2);
            }

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);


            Bitmap bmPhoto = new Bitmap(newWidth, newHeight,
                          PixelFormat.Format24bppRgb);

            bmPhoto.SetResolution(imgPhoto.HorizontalResolution,
                         imgPhoto.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.InterpolationMode = InterpolationMode.Low;

            var attributes = new ImageAttributes();
            attributes.SetWrapMode(WrapMode.TileFlipXY);

            var destination = new Rectangle(0, 0, newWidth, newHeight);
            grPhoto.DrawImage(imgPhoto, destination, 0, 0, imgPhoto.Width, imgPhoto.Height,
                GraphicsUnit.Pixel, attributes);
            string destImagePath = stPhotoPath;
            //grPhoto.Clear(Color.Black);
            //grPhoto.InterpolationMode =
            //    System.Drawing.Drawing2D.InterpolationMode.Low;

            //grPhoto.DrawImage(imgPhoto,
            //    new Rectangle(destX, destY, destWidth, destHeight),
            //    new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
            //    GraphicsUnit.Pixel);

            grPhoto.Dispose();
            imgPhoto.Dispose();
            return bmPhoto;

        }
        /// <summary>
        /// Saves an image as a jpeg image, with the given quality
        /// </summary>
        /// <param name="path"> Path to which the image would be saved. </param>
        /// <param name="quality"> An integer from 0 to 100, with 100 being the highest quality. </param>
        public static void SaveJpeg(string path, Image img, int quality, string fromChangeKey, string toChangeKey)
        {
            if (quality < 0 || quality > 100)
                throw new ArgumentOutOfRangeException("quality must be between 0 and 100.");

            // Encoder parameter for image quality
            EncoderParameter qualityParam = new EncoderParameter(Encoder.Quality, quality);
            // JPEG image codec
            ImageCodecInfo jpegCodec = GetEncoderInfo("image/jpeg");
            EncoderParameters encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = qualityParam;
            img.Save(path.Replace(fromChangeKey, toChangeKey), jpegCodec, encoderParams);
        }

        /// <summary>
        /// Returns the image codec with the given mime type
        /// </summary>
        private static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            // Get image codecs for all image formats
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

            // Find the correct image codec
            for (int i = 0; i < codecs.Length; i++)
                if (codecs[i].MimeType == mimeType)
                    return codecs[i];

            return null;
        }
        public static bool ImageResize(string originalFile, string thumbFile, List<string> thumbSizes)
        {
            bool anyError = false;
            foreach (string thumbSize in thumbSizes)
            {
               
                Instructions settings = new Instructions();
                string width = thumbSize.Split('x')[0];
                string height = thumbSize.Split('x')[1];
                if (width != "*") settings.Width = int.Parse(thumbSize.Split('x')[0]);
                if (height != "*") settings.Height = int.Parse(thumbSize.Split('x')[1]);
                if (settings.Width==null)
                {
                    settings.Width = 0;
                }
                if (settings.Height == null)
                {
                    settings.Height = 0;
                }
                
                try
                {
                    string destFile = thumbFile + "-" + thumbSize.Replace("x*", "X").Replace("*x", "X");
                    destFile = destFile.Replace("-.jpg-", "-");
                    if (!destFile.ToLower().EndsWith(".jpg"))
                    {
                        destFile = destFile + ".jpg";
                    }
                    var result = ReSize(originalFile, destFile, new Size((int)settings.Width, (int)settings.Height));
                    if (result)
                    {
                        //result = AddWatermark(destFile);
                        //if (!result)
                        //{
                        //    anyError = true;
                        //}
                    }
                    else
                    {
                        anyError = true;
                    }
                }
                catch (Exception Hata)
                {
                    anyError = true;
                }
            }
            return !anyError;
        }
        //public static bool ReSize(string source,string destination,Size size)
        //{
        //    bool result = false;
        //    try
        //    {
        //        ImageFactory imageFactory = new ImageFactory();
        //        imageFactory = imageFactory.BackgroundColor(Color.Red);
        //        imageFactory.AnimationProcessMode = ImageProcessor.Imaging.AnimationProcessMode.All;
        //        imageFactory.PreserveExifData = true;
        //        ImageProcessor.Imaging.ResizeLayer resizeLayer = new ImageProcessor.Imaging.ResizeLayer(size, ImageProcessor.Imaging.ResizeMode.BoxPad);
        //        resizeLayer.Upscale = true;
        //        ISupportedImageFormat format = new JpegFormat { Quality = 100 };
        //        imageFactory.Load(source).Resize(resizeLayer).Format(format).Quality(100).BackgroundColor(Color.White).Watermark(


        //            ).Save(destination);
        //        result = true;
        //    }
        //    catch (Exception Hata)
        //    {
        //        result = false;
        //    }
        //    return result;
        //}

        public static bool ReSize(string source, string destination, Size size)
        {
            bool result = false;
            try
            {
                string txt = "makinaturkiye.com";
                Image tmpImg = Image.FromFile(source);
                


                if (tmpImg.Width<size.Width)
                {
                    if (tmpImg.Width > tmpImg.Height)
                    {
                        if (size.Width>size.Height)
                        {
                            size.Height = size.Width;
                        }
                    }
                    //else
                    //{ 
                    //    size.Width=size.Height;
                    //}
                }
                else if (tmpImg.Height < size.Height)
                {
                    if (size.Height > size.Width)
                    {
                        size.Width = size.Height;
                    }
                    //else
                    //{
                    //    size.Height = size.Width;
                    //}
                }

                FontFamily fontFamily = new FontFamily("Arial");
                int fontSize = 40;
                int countOfChar = txt.Length;
                fontSize = ((((size.Width == 0 ? size.Height : size.Width) + (size.Height == 0 ? size.Width : size.Height)) / 3)*2) / countOfChar;
                fontSize += 4;

                ImageFactory imageFactory = new ImageFactory();
                imageFactory = imageFactory.BackgroundColor(Color.Red);
                imageFactory.AnimationProcessMode = ImageProcessor.Imaging.AnimationProcessMode.All;
                imageFactory.PreserveExifData = true;
                ImageProcessor.Imaging.ResizeLayer resizeLayer = new ImageProcessor.Imaging.ResizeLayer(size, ImageProcessor.Imaging.ResizeMode.Pad);
                resizeLayer.Upscale = true;
                resizeLayer.AnchorPosition = ImageProcessor.Imaging.AnchorPosition.Center;

                ISupportedImageFormat format = new JpegFormat { Quality = 100 };
                imageFactory = imageFactory.Load(source);
                imageFactory = imageFactory.Resize(resizeLayer);
                //imageFactory = imageFactory.Watermark(
                //new ImageProcessor.Imaging.TextLayer()
                //{
                //    Text = txt,
                //    FontSize = fontSize,
                //    FontFamily = fontFamily,
                //    FontColor = Color.Red,
                //    Opacity = 25,
                //    Style = FontStyle.Bold,
                //});
                //imageFactory = imageFactory.Format(format);
                //imageFactory = imageFactory.BackgroundColor(Color.White);
                imageFactory = imageFactory.Save(destination);
                result = true;
            }
            catch (Exception Hata)
            {
                result = false;
            }
            return result;
        }

        public static bool AddWatermark(string image)
        {
            bool result = false;
            try
            {
                Image img = Image.FromFile(image);
                string txt = "makinaturkiye.com";
                FontFamily fontFamily = new FontFamily("Arial");
                int fontSize = 40;

                int countOfChar = txt.Length;
                fontSize = (img.Width + img.Height / 2) / countOfChar;


                double tangent = (double)img.Height / (double)img.Width;
                double angle = Math.Atan(tangent) * (180 / Math.PI);
                double halfHypotenuse = (Math.Sqrt((img.Height
                                       * img.Height) +
                                       (img.Width *
                                       img.Width))) / 2;

                int x = (img.Width / 2);
                int y = ((img.Height) / 2);
                img.Dispose();

                ImageFactory imageFactory = new ImageFactory()
                {
                    AnimationProcessMode = ImageProcessor.Imaging.AnimationProcessMode.All,
                    PreserveExifData = true,
                    FixGamma = true,
                };
                imageFactory=imageFactory.Load(image).Watermark(new ImageProcessor.Imaging.TextLayer()
                {
                    Text = txt,
                    FontSize = fontSize,
                    FontFamily = fontFamily,
                    DropShadow = true,
                    FontColor = Color.Red,
                    Opacity = 15,
                    Position = new Point(x,y),
                    Style = FontStyle.Bold,
                }).Save(image);
                result = true;
            }
            catch (Exception Hata)
            {
                result = false;
            }

            return result;
            
        }


        public static void AddWaterMarkNew(string orgImgFile, string size)
        {
            string txt = "makinaturkiye.com";
            Image imgPhoto;
            using (FileStream myStream = new FileStream(orgImgFile, FileMode.Open, FileAccess.Read))
            {
                imgPhoto = Image.FromStream(myStream);
            }
            int phWidth = imgPhoto.Width;
            int phHeight = imgPhoto.Height;
            size = imgPhoto.Width.ToString() + "x" + imgPhoto.Height.ToString();
            Bitmap bmPhoto = new Bitmap(imgPhoto);
            Graphics g = Graphics.FromImage(bmPhoto);
            double tangent = (double)bmPhoto.Height / (double)bmPhoto.Width;
            double angle = Math.Atan(tangent) * (180 / Math.PI);
            double halfHypotenuse = (Math.Sqrt((bmPhoto.Height
                                   * bmPhoto.Height) +
                                   (bmPhoto.Width *
                                   bmPhoto.Width))) / 2;
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;
            FontFamily fontFamily = new FontFamily("Arial");
            int fontSize = 40;

            int countOfChar = txt.Length;
            fontSize = (imgPhoto.Width + imgPhoto.Height / 2) / countOfChar;
            
            var f = new System.Drawing.Font("Times New Roman ", fontSize);
            Color newColor = Color.FromArgb(100, 255, 255, 255);
            Brush myBrush = new SolidBrush(newColor);
            halfHypotenuse = halfHypotenuse - countOfChar;
            double drawanngle = angle * -1;
            g.RotateTransform((float)drawanngle);
            g.TranslateTransform(0, imgPhoto.Height, MatrixOrder.Append);
            g.DrawString(txt, f, myBrush,
                             new Point((int)halfHypotenuse, 0),
                             stringFormat);
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
            imgPhoto.Dispose();
        }


    }
}