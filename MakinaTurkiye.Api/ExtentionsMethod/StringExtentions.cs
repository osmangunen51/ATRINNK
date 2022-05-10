using MakinaTurkiye.Utilities;
using System;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;

namespace MakinaTurkiye.Api.ExtentionsMethod
{
    public sealed class DataImage
    {
        private static readonly Regex DataUriPattern = new Regex(@"^data\:(?<type>image\/(png|tiff|jpg|gif));base64,(?<data>[A-Z0-9\+\/\=]+)$", RegexOptions.Compiled | RegexOptions.ExplicitCapture | RegexOptions.IgnoreCase);

        private DataImage(string mimeType, byte[] rawData)
        {
            MimeType = mimeType;
            RawData = rawData;
        }

        public string MimeType { get; }
        public byte[] RawData { get; }

        public Image Image => Image.FromStream(new MemoryStream(RawData));

        public MemoryPostedFile File(string FName)=> new MemoryPostedFile(RawData, FName);

        public static DataImage TryParse(string dataUri)
        {
            if (string.IsNullOrWhiteSpace(dataUri)) return null;
            var regex = new Regex(@"data:(?<mime>[\w/\-\.]+);(?<encoding>\w+),(?<data>.*)", RegexOptions.Compiled);

            var match = regex.Match(dataUri);

            var mime = match.Groups["mime"].Value;
            var encoding = match.Groups["encoding"].Value;
            var data = match.Groups["data"].Value;
            if (!match.Success) return null;

            string mimeType = match.Groups["type"].Value;
            string base64Data = match.Groups["data"].Value;

            try
            {
                byte[] rawData = Convert.FromBase64String(base64Data);
                return rawData.Length == 0 ? null : new DataImage(mimeType, rawData);
            }
            catch (FormatException)
            {
                return null;
            }
        }
    }

    public static class StringExtentions
    {
        public static Image ToImage(this string value)
        {
            DataImage dtimage = DataImage.TryParse(value);
            return dtimage.Image;
        }

        public static MemoryPostedFile ToFile(this string value,string filename="")
        {
            string Uzanti= value.GetUzanti();
            filename = $"{filename}.{Uzanti}";
            DataImage dtimage = DataImage.TryParse(value);
            return dtimage.File(filename);
        }

        public static string GetUzanti(this string value)
        {
            string Sonuc = "";
            if (value.Contains("data:image/png"))
            {
                Sonuc = "png";
            }
            else if (value.Contains("data:image/jpg"))
            {
                Sonuc = "jpg";
            }
            else if (value.Contains("data:application/pdf"))
            {
                Sonuc = "pdf";
            }
            return Sonuc;
        }
    }
}