namespace NeoSistem.MakinaTurkiye.Core.Web.Helpers
{
    using ImageResizer;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.IO;
    using System.Linq;
    using System.Web;

    public class VideoModelHelper
    {
        public string newFileName { get; set; }

        public string Duration { get; set; }
    }

    public class FileHelpers
    {
        public enum ThumbnailType { Width, Height }

        public static string Delete(string fileFullPath)
        {
            var targetFile = new FileInfo(HttpContext.Current.Server.MapPath(fileFullPath));
            if (targetFile.Exists)
            {
                targetFile.Delete();
            }
            return targetFile.FullName;
        }
        public static bool DeleteFullPath(string fileFullPath)
        {
            if (Directory.Exists(fileFullPath))
            {
                Directory.Delete(fileFullPath, true);
            }
            return  true;
        }

        public static string ImageResize(string filePath, HttpPostedFileBase postedFile, Dictionary<string, string> thumbs)
        {
            string fileName = string.Empty;
            if (postedFile.ContentLength > 0)
            {
                bool isImage = ImageContentType.Any(fc => fc == postedFile.ContentType);
                if (isImage)
                {
                    fileName = Upload(filePath, postedFile);

                    if (thumbs != null && thumbs.Count > 0)
                    {
                        foreach (var item in thumbs)
                        {
                            #region old code
                            //var image = Image.FromStream(ms);

                            //int size = 0;
                            //Size thumbSize = Size.Empty;

                            //int sizeWidth = image.Width;
                            //int sizeHeight = image.Height;

                            //int width = Convert.ToInt32(item.Value.Split('x').GetValue(0));
                            //int height = Convert.ToInt32(item.Value.Split('x').GetValue(1));

                            //if (sizeWidth > sizeHeight)
                            //{
                            //  if (width > sizeWidth && height > sizeHeight)
                            //    size = sizeWidth;
                            //  else
                            //    size = width;

                            //  thumbSize = new Size(size, Convert.ToInt32(((double)sizeHeight / sizeWidth) * size));
                            //  if (thumbSize.Height == 0)
                            //  {
                            //    thumbSize.Height = 1;
                            //  }
                            //  if (thumbSize.Width == 0)
                            //  {
                            //    thumbSize.Width = 1;
                            //  }
                            //}
                            //else
                            //{
                            //  if (width > sizeWidth && height > sizeHeight)
                            //    size = sizeHeight;
                            //  else
                            //    size = height;

                            //  thumbSize = new Size(Convert.ToInt32(((double)sizeWidth / sizeHeight) * size), size);
                            //  if (thumbSize.Height == 0)
                            //  {
                            //    thumbSize.Height = 1;
                            //  }
                            //  if (thumbSize.Width == 0)
                            //  {
                            //    thumbSize.Width = 1;
                            //  }
                            //}

                            //using (var bmp = new Bitmap(image, thumbSize))
                            //{
                            //  using (var gr = Graphics.FromImage(bmp))
                            //  {
                            //    gr.SmoothingMode = SmoothingMode.HighQuality;
                            //    gr.CompositingQuality = CompositingQuality.HighQuality;
                            //    gr.InterpolationMode = InterpolationMode.High;
                            //    var brush = new SolidBrush(Color.FromArgb(120, 0, 0, 0));


                            //    var rectDestination = new Rectangle(Point.Empty, thumbSize);
                            //    var StrFormat = new StringFormat();
                            //    StrFormat.Alignment = StringAlignment.Near;

                            //    gr.DrawImage(image, rectDestination, 0, 0, sizeWidth, sizeHeight, GraphicsUnit.Pixel);
                            //  }

                            //  bmp.Save(HttpContext.Current.Server.MapPath(item.Key) + fileName);
                            //  image.Dispose();
                            //  }

                            #endregion

                            string originalPath = string.Format("{0}{1}", HttpContext.Current.Server.MapPath(filePath), fileName);

                            string fileNameExtension = Path.GetExtension(originalPath);
                            string resizeFileName = fileName.Substring(0, fileName.Length - fileNameExtension.Length);
                            int width = Convert.ToInt32(item.Value.Split('x').GetValue(0));
                            int height = Convert.ToInt32(item.Value.Split('x').GetValue(1));

                            string resizePath = string.Format("{0}{1}", HttpContext.Current.Server.MapPath(item.Key), resizeFileName);
                            ResizeSettings resizeSettings = new ResizeSettings();
                            resizeSettings.BackgroundColor = Color.White;
                            resizeSettings.Width = width;
                            resizeSettings.Height = height;
                            resizeSettings.Mode = FitMode.Pad;

                            ImageBuilder.Current.Build(originalPath, resizePath, resizeSettings, false, true);

                        }
                    }
                }
            }
            return fileName;
        }

        public static string ImageResize(string filePath, HttpPostedFileBase postedFile, Dictionary<string, string> thumbs, bool Bvalue)
        {
            string fileName = string.Empty;
            if (postedFile.ContentLength > 0)
            {
                bool isImage = ImageContentType.Any(fc => fc == postedFile.ContentType);
                if (isImage)
                {
                    fileName = Upload(filePath, postedFile);

                    if (thumbs != null && thumbs.Count > 0)
                    {
                        foreach (var item in thumbs)
                        {
                            string originalPath = string.Format("{0}{1}", HttpContext.Current.Server.MapPath(filePath), fileName);

                            string fileNameExtension = Path.GetExtension(originalPath);
                            string resizeFileName = fileName.Substring(0, fileName.Length - fileNameExtension.Length);
                            int width = Convert.ToInt32(item.Value.Split('x').GetValue(0));
                            int height = Convert.ToInt32(item.Value.Split('x').GetValue(1));

                            string resizePath = string.Format("{0}{1}", HttpContext.Current.Server.MapPath(item.Key), resizeFileName);
                            postedFile.SaveAs(resizePath + fileNameExtension);

                            //ResizeSettings resizeSettings = new ResizeSettings();
                            ////resizeSettings.BackgroundColor = Color.White;
                            ////resizeSettings.Width = width;
                            ////resizeSettings.Height = height;
                            ////resizeSettings.Mode = FitMode.Pad;

                            //ImageBuilder.Current.Build(originalPath, resizePath, resizeSettings, false, true);

                        }
                    }
                }
            }
            return fileName;
        }
        public static string SaveExcel(string filePath, HttpPostedFileBase postedFile)
        {
            string filename = string.Empty;

            filename = Upload(filePath, postedFile);
            return filename;
        }

        public static string ImageThumbnail(string filePath, HttpPostedFileBase postedFile, int size, ThumbnailType type)
        {
            string filename = string.Empty;

            if (postedFile.ContentLength > 0)
            {

                bool isImage = ImageContentType.Any(fc => fc == postedFile.ContentType);

                if (isImage)
                {

                    string oldfile = postedFile.FileName;
                    string uzanti = oldfile.Substring(oldfile.LastIndexOf("."), oldfile.Length - oldfile.LastIndexOf("."));
                    filename = Guid.NewGuid().ToString("N") + uzanti;
                    var targetFile = new FileInfo(filePath + filename);

                    if (targetFile.Exists)
                    {
                        filename = Guid.NewGuid().ToString("N") + uzanti;
                    }

                    using (var image = Image.FromStream(postedFile.InputStream))
                    {
                        if (type == ThumbnailType.Height)
                        {
                            if (image.Height < size)
                            {
                                size = image.Height;
                            }

                        }
                        else
                        {
                            if (image.Width < size)
                            {
                                size = image.Width;
                            }
                        }

                        Size thumbSize = Size.Empty;

                        int sizeWidth = image.Width;
                        int sizeHeight = image.Height;

                        if (type == ThumbnailType.Width)
                        {
                            thumbSize = new Size(size, Convert.ToInt32(((double)sizeHeight / sizeWidth) * size));
                        }
                        else
                        {
                            thumbSize = new Size(Convert.ToInt32(((double)sizeWidth / sizeHeight) * size), size);
                        }

                        using (var bmp = new Bitmap(image, thumbSize))
                        {

                            using (var gr = Graphics.FromImage(bmp))
                            {
                                gr.SmoothingMode = SmoothingMode.HighQuality;
                                gr.CompositingQuality = CompositingQuality.HighQuality;
                                gr.InterpolationMode = InterpolationMode.High;

                                var rectDestination = new Rectangle(Point.Empty, thumbSize);
                                gr.DrawImage(image, rectDestination, 0, 0, sizeWidth, sizeHeight, GraphicsUnit.Pixel);
                            }

                            filename = Upload(filePath, postedFile);

                            bmp.Save(HttpContext.Current.Server.MapPath(filePath) + filename.Replace(".", "_th."));
                        }
                    }
                }
            }
            return filename;
        }

        public static bool HasFile(string filePath)
        {

            //#if DEBUG
            //            return true;
            //#else
            //        if (File.Exists(HttpContext.Current.Server.MapPath(filePath)))
            //          {
            //            return true;
            //          }
            //#endif

            //            return false;
            return true;
        }

        public static string Upload(string filepath, HttpPostedFileBase file)
        {
            string filename;
            if (file.ContentLength > 0)
            {
                string fn = file.FileName;
                string uzanti = fn.Substring(fn.LastIndexOf("."), fn.Length - fn.LastIndexOf("."));

                filename = Guid.NewGuid().ToString("N") + uzanti;
                var targetFile = new FileInfo(filepath + filename);
                if (targetFile.Exists)
                {
                    filename = Guid.NewGuid().ToString("N") + uzanti;
                }

                file.SaveAs(HttpContext.Current.Server.MapPath(filepath) + filename);
            }
            else
            {
                filename = "";
            }
            return filename;
        }

        public static void Download(string fileFullPath, string filename)
        {
            var targetFile = new FileInfo(HttpContext.Current.Server.MapPath("~") + "/Management" + fileFullPath);

            if (targetFile.Exists)
            {
                var stream = default(Stream);
                try
                {
                    using (stream = new FileStream(targetFile.FullName, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        int length = Convert.ToInt32(targetFile.Length.ToString());
                        var data = new byte[length + 1];

                        string uzanti = targetFile.FullName.Substring(targetFile.FullName.LastIndexOf("."), targetFile.FullName.Length - targetFile.FullName.LastIndexOf("."));

                        HttpContext.Current.Response.Clear();
                        HttpContext.Current.Response.ContentType = "application/octet-stream";
                        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + filename + uzanti);
                        HttpContext.Current.Response.AddHeader("Content-Length", targetFile.Length.ToString());

                        if (HttpContext.Current.Response.IsClientConnected)
                        {
                            stream.Read(data, 0, Convert.ToInt32(targetFile.Length.ToString()));
                            HttpContext.Current.Response.OutputStream.Write(data, 0, length);
                            HttpContext.Current.Response.Flush();
                        }
                    }
                }
                catch
                {

                }
                finally
                {
                    if (stream != null)
                    {
                        stream.Close();
                    }
                    HttpContext.Current.Response.End();
                }
            }
        }

        public static VideoModelHelper fffmpegVideoConvert(HttpPostedFileBase file, string tempFolder, string thumbnailImageFolder, string videoFolder, string ffmpegFolder, short genislik, short yukseklik)
        {
            FileInfo fileInfo = new FileInfo(file.FileName);


            string fileName = fileInfo.Name;
            file.SaveAs(HttpContext.Current.Server.MapPath(tempFolder + fileName));

            string newFileName = Guid.NewGuid().ToString("N");

            string video = HttpContext.Current.Server.MapPath(tempFolder + fileName);
            string picture = HttpContext.Current.Server.MapPath(thumbnailImageFolder + newFileName + ".jpg");

            var procThumbnail = new Process();

            procThumbnail.StartInfo.Arguments = " -i \"" + video + "\" -s " + genislik + "*" + yukseklik + " -ss 00:00:02 -vframes 1 -an -f rawvideo -vcodec mjpeg \"" + picture + "\"";
            procThumbnail.StartInfo.FileName = HttpContext.Current.Server.MapPath(ffmpegFolder + "ffmpeg.exe");
            procThumbnail.Start();
            procThumbnail.WaitForExit();
            procThumbnail.Close();
            string mp4 = HttpContext.Current.Server.MapPath(videoFolder + newFileName + ".mp4");

            var procConvertFlv = new Process();

            //procConvertFlv.StartInfo.Arguments = " -i \"" + video + "\" -acodec libmp3lame -ar 44100 -ab 64k -ac 2 -r 24 -b 650k  -f mp4  -s " + genislik + "x" + yukseklik + " \"" + mp4 + "\"";
            //ffmpeg -i INPUTFILE -b 1500k -vcodec libx264 -vpre slow -vpre baseline -g 30 "OUTPUTFILE.mp4
            //ffmpeg -i inputfile.avi -codec:v libx264 -profile:v baseline -preset slow -b:v 250k -maxrate 250k -bufsize 500k -vf scale=-1:360 -threads 0 -codec:a libfdk_aac -b:a 96k output.mp4

            //ffmpeg -i input_file.avi -codec:v libx264 -profile:v high -preset slow -b:v 500k -maxrate 500k -bufsize 1000k -vf scale=-1:480 -threads 0 -codec:a libfdk_aac -b:a 128k output_file.mp4

            //var Params = string.Format("ffmpeg -i \"{0}\" -b 1500k -vcodec libx264 -vpre slow -vpre baseline -g 30 \"{1}\"", video, mp4);
            //var Params = string.Format("-y -i \"{0}\" -coder ac -me_method full -me_range 16 -subq 5 -sc_threshold 40 -vcodec libx264 -cmp +chroma -partitions +parti4x4+partp8x8+partb8x8 -i_qfactor 0.71 -keyint_min 25 -b_strategy 1 -g 250 -r 20 \"{1}\"", video, mp4);
            //var Params = string.Format("-y -i \"{0}\" -acodec libfaac -ar 44100 -ab 96k -coder ac -me_method full -me_range 16 -subq 5 -sc_threshold 40 -vcodec libx264 -s 1280x544 -b 1600k -cmp +chroma -partitions +parti4x4+partp8x8+partb8x8 -i_qfactor 0.71 -keyint_min 25 -b_strategy 1 -g 250 -r 20 c:\\output3.mp4", video.Path, videoFileName);
            // procConvertFlv.StartInfo.Arguments = Params;
            procConvertFlv.StartInfo.Arguments = " -i \"" + video + "\" -s " + genislik + "x" + yukseklik + " \"" + mp4 + "\"";
            //procConvertFlv.StartInfo.Arguments = "ffmpeg -i " + video + " -codec:v libx264 -profile:v high -preset slow -b:v 500k -maxrate 500k -bufsize 1000k -vf scale=-1:480 -threads 0 -codec:a libfdk_aac -b:a 128k " + mp4;
            procConvertFlv.StartInfo.FileName = HttpContext.Current.Server.MapPath(ffmpegFolder + "ffmpeg.exe");
            procConvertFlv.StartInfo.UseShellExecute = false;
            procConvertFlv.StartInfo.RedirectStandardError = true;
            procConvertFlv.StartInfo.RedirectStandardOutput = true;
            procConvertFlv.StartInfo.CreateNoWindow = true;
            procConvertFlv.Start();

            string consoleData = "";
            string OneLine;
            StreamReader myStreamReader;
            myStreamReader = procConvertFlv.StandardError;
            OneLine = myStreamReader.ReadLine();
            do
            {
                consoleData = consoleData + OneLine + "<br>";
                OneLine = myStreamReader.ReadLine();
            }
            while (!(procConvertFlv.HasExited & (string.Compare(OneLine, "") == 0 | OneLine == null)));
            procConvertFlv.WaitForExit();
            if ((procConvertFlv.ExitCode == 0))
                procConvertFlv.Close();
            else
                procConvertFlv.Close();

            File.Delete(video);

            var videomodel = new VideoModelHelper();
            videomodel.newFileName = newFileName;
            videomodel.Duration = consoleData.Substring(consoleData.LastIndexOf("Duration"), 21).Replace("Duration: ", "").Replace("Duration:", "");

            return videomodel;
        }




        public static string[] FileExtensions = { ".bmp", ".cod", ".gif", ".ief", ".jpeg", ".jpg", ".jpe", "jfif", ".svg", ".tif", ".tiff", ".ras", ".cmx", ".ico", ".pnm", ".pbm", ".pgm", ".ppm", ".rgb", ".xbm", ".xpm", ".xwd" };

        public static string[] ImageContentType = { "image/bmp", "image/cis-cod", "image/gif", "image/ief", "image/jpeg", "image/jpg",
                                                "image/jpeg", "image/pipeg", "image/svg+xml", "image/tiff", "image/tiff",
                                                "image/x-cmu-raster", "image/x-cmx", "image/x-icon", "image/x-portable-anymap",
                                                "image/x-portable-bitmap", "image/x-portable-graymap", "image/x-portable-pixmap",
                                                "image/x-rgb", "image/x-xbitmap", "image/x-xpixmap", "image/x-xwindowdump",
                                                "image/pjpeg", "image/png", "image/x-png" };

        public static string[] VideoContentType = { "audio/mpeg", "video/x-ms-wmv", "Microsoft WMV-video", "Відео-кліп Microsoft WMV",
                                                "video/x-sgi-movie", "x-world/x-vrml", "video/x-msvideo", "video/x-ms-asf",
                                                "video/x-la-asf", "video/quicktime", "video/x-ms-asf", "audio/x-ms-wma",
                                                "audio/x-ms-wax", "audio/x-ms-wmv", "video/x-ms-wvx ", "video/x-ms-wm",
                                                "video/x-ms-wmx" };


        public static string ImageFolder { get { return "/UserFiles/images/"; } }
        public static string ProfielImageFolder { get { return "/UserFiles/images/profile/"; } }
        public static string BannerFolder { get { return "/UserFiles/banner/"; } }
        public static string FlashImageFolder { get { return "/UserFiles/flash/photos/"; } }
        public static string SourceCodeFolder { get { return "/UserFiles/sourceCode/"; } }
    }
}
