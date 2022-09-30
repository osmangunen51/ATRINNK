using ImageResizer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

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
                settings.OutputFormat = OutputFormat.Jpeg;
                settings.JpegQuality = 70;

                if (width != "*" && height != "*")
                {
                    settings.Mode = FitMode.Pad;
                }

                try
                {
                    string destFile = thumbFile + "-" + thumbSize.Replace("x*", "X").Replace("*x", "X");
                    ImageBuilder.Current.Build(new ImageJob(originalFile, destFile, settings, false, true));
                }
                catch
                {
                    anyError = true;
                }
            }
            return !anyError;
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