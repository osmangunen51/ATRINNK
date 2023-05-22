using Quartz;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebPWrapper.Encoder;

namespace Trinnk.Tasks.WebP.Tasks
{
    public class WebP : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            string DizinPath = System.Web.Hosting.HostingEnvironment.MapPath("~/UserFiles/Product/");
            DirectoryInfo Dizin = new DirectoryInfo(DizinPath);
            if (Dizin.Exists)
            {
                var builder = new WebPEncoderBuilder();
                var encoder = builder
                    .LowMemory()
                    .MultiThread()
                    .Build();

                List<System.IO.FileInfo> DosyaJpgListesi = Dizin.GetFiles("*.jpg", SearchOption.AllDirectories).ToList();
                List<System.IO.FileInfo> DosyaWebPListesi = Dizin.GetFiles("*.webp", SearchOption.AllDirectories).ToList();

                List<string> TxtDosyaJpgListesi = DosyaJpgListesi.Select(x => x.FullName.Replace(".jpg", ".islemyap")).ToList();
                List<string> TxtDosyaWebPListesi = DosyaWebPListesi.Select(x => x.FullName.Replace(".webp", ".islemyap")).ToList();
                var TxtIslemYapilacakListesi = TxtDosyaJpgListesi.Except(TxtDosyaWebPListesi);
                foreach (var DosyaTxt in TxtIslemYapilacakListesi)
                {
                    string TmpDosya = DosyaTxt.Replace(".islemyap", ".jpg");
                    System.IO.FileInfo Dosya = new FileInfo(TmpDosya);
                    try
                    {
                        string simpleLosslessFileName = Dosya.FullName.Replace(Dosya.Extension, ".webp");
                        System.IO.FileInfo DosyaKontrol = new FileInfo(simpleLosslessFileName);
                        if (!DosyaKontrol.Exists)
                        {
                            using (var outputFile = File.Open(simpleLosslessFileName, FileMode.Create))
                            using (var inputFile = File.Open(Dosya.FullName, FileMode.Open))
                            {
                                encoder.Encode(inputFile, outputFile);
                            }
                        }
                        else
                        {

                        }
                    }
                    catch (Exception Hata)
                    {

                    }
                }
            }
            return Task.CompletedTask;
        }
    }
}
