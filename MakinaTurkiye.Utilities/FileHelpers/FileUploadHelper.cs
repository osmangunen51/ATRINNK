using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace MakinaTurkiye.Utilities.FileHelpers
{
    public  static class FileUploadHelper
    {
      static  string[] ImageContentTypes = { "application/pdf","application/msword"};
       
        public static string UploadFile(HttpPostedFileBase file,string SaveFilePath,string newName,int counter)
        {
            string fileName = "";
            if (file.ContentLength > 0)
            {
                if(ImageContentTypes.Any(x => x == file.ContentType) && file.ContentLength > 0)
                {
              
                    string subdir = HttpContext.Current.Server.MapPath(SaveFilePath);
                    // If directory does not exist, create it. 
                    if (!Directory.Exists(subdir))
                    {
                        Directory.CreateDirectory(subdir);
                    }

                    
                    string oldfile = file.FileName;
                    string mapPath = HttpContext.Current.Server.MapPath(SaveFilePath+"/");

                    string uzanti = oldfile.Substring(oldfile.LastIndexOf("."), oldfile.Length - oldfile.LastIndexOf("."));
                    string filename = newName.ToFileName(counter)+uzanti;
                    var targetFile = new FileInfo(mapPath + filename);

                    if (targetFile.Exists && filename==null)
                    {
                        filename = Guid.NewGuid().ToString("N") + "_katolog" + uzanti;
                    }

                    string storeCatologPath = mapPath + filename;
                    file.SaveAs(storeCatologPath);
                    fileName = filename;


                }
            }
            return fileName;
        }


        public static string ToFileName(this string fileName, int? indexNumber = null)
        {
            Regex rgxFileName; rgxFileName = new Regex(@"[^A-Za-z0-9]+", RegexOptions.Compiled);
            string tmp = fileName.Trim().ToLowerInvariant();
            tmp = tmp.Replace("ü", "u");
            tmp = tmp.Replace("ğ", "g");
            tmp = tmp.Replace("ö", "o");
            tmp = tmp.Replace("ş", "s");
            tmp = tmp.Replace("ç", "c");
            tmp = tmp.Replace("ı", "i");

            return rgxFileName.Replace(tmp, "_") + (indexNumber!=0 ? "-" + indexNumber.ToString() : string.Empty);

        }
    }
}
