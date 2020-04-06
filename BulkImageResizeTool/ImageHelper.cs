using ImageResizer;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BulkImageResizeTool
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

        public static bool ImageResize(string originalFile, string thumbFile, List<string> thumbSizes)
        {
            bool anyError = false;
            foreach (string thumbSize in thumbSizes)
            {
                Instructions settings = new Instructions();

                string width = thumbSize.Split('x')[0];
                string height = thumbSize.Split('x')[1];
                if (width != "*")
                    settings.Width = int.Parse(thumbSize.Split('x')[0]);
                if (height != "*")
                    settings.Height = int.Parse(thumbSize.Split('x')[1]);
                settings.OutputFormat = OutputFormat.Jpeg;
                if (width != "*" && height != "*")
                    settings.Mode = FitMode.Pad;

                try
                {
                    ImageBuilder.Current.Build(new ImageJob(originalFile, thumbFile + "-" + thumbSize.Replace("x*", "X"),
                        settings, false, true));
                }
                catch
                {
                    anyError = true;
                }
            }

            return !anyError;
        }
    }
}