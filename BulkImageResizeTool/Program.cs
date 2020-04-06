using BulkImageResizeTool.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BulkImageResizeTool
{
    internal class Program
    {
        private static readonly MTEntities _db;
        private static readonly MTEntities _dbSave;

        static Program()
        {
            _db = new MTEntities();
            _dbSave = new MTEntities();
        }

        private static void Main(string[] args)
        {
            //if (!args.Length.Equals(1))
            //{
            //    Console.WriteLine("argument missing, job parameters: product or store.");
            //    Console.ReadLine();
            //    return;
            //}

            if (!Directory.Exists(Settings.Default.MainImagePath))
                Console.WriteLine("Main Image Path not exist!");

            //if (args[0].Equals("product"))
                //ProcessProduct();
            //if (args[0].Equals("store"))
                ProcessStore();

            LogConsole("\r\nI'm done, Enter for exit!");
            Console.ReadKey();
        }

        private static void ProcessProduct()
        {
            if (!Directory.Exists(Settings.Default.OldImagePath))
                Console.WriteLine("Old Original Product Image Path not found!");

            string oldOriginalImagesFolder = Settings.Default.OldImagePath;
            string newProductImageFolder = Settings.Default.MainImagePath + "Product\\";

            List<string> thumbSizes = new List<string>();
            thumbSizes.AddRange(Settings.Default.ThumbSizes.Split(';'));

            _totalCount = _db.Product.Count();
            foreach (
                var product in
                    _db.Product.OrderByDescending(x => x.ProductId).Select(x => new { x.ProductId, x.ProductName })
                    )
            {
                _processedCount++;

                // Yeni lokasyonda klasorler yoksa olustur
                var di = Directory.CreateDirectory(newProductImageFolder + product.ProductId.ToString());
                di.CreateSubdirectory("thumbs");

                int ndx = 1;
                foreach (
                    var image in _db.Picture
                            .Where(x => x.ProductId.HasValue && x.ProductId.Value.Equals(product.ProductId))
                    // Eger resim daha once islenmis ise hic bir sey yapma
                            .Where(x => string.IsNullOrEmpty(x.PicturePath))
                            .OrderBy(x => x.PictureId))
                {
                    string imageFilePath = oldOriginalImagesFolder + image.OldPath;
                    if (!File.Exists(imageFilePath))
                    {
                        LogProductErrorToDb(product.ProductId, image.PictureId, "Image not found: " + imageFilePath);
                        continue;
                    }

                    string newMainImageFilePath = newProductImageFolder + product.ProductId.ToString() + "\\";
                    string newMainImageFileName = product.ProductName.ToImageFileName(ndx) + ".jpg";
                    File.Copy(imageFilePath, newMainImageFilePath + newMainImageFileName, true);

                    bool thumbResult = ImageProcessHelper.ImageResize(newMainImageFilePath + newMainImageFileName,
                        newMainImageFilePath + "thumbs\\" + product.ProductName.ToImageFileName(ndx), thumbSizes);
                    if (!thumbResult)
                    {
                        LogProductErrorToDb(product.ProductId, image.PictureId, "An error occured while creating thumbnails.");
                    }

                    _db.Detach(image);

                    _dbSave.Attach(image);
                    image.PicturePath = newMainImageFileName;
                    _dbSave.SaveChanges();

                    ndx++;
                }

                LogConsole(product.ProductId + ": " + product.ProductName);
            }
        }

        private static void ProcessStore()
        {
            if (!Directory.Exists(Settings.Default.OldStoreLogoImagePath))
                Console.WriteLine("Old Original StoreLogo Image Path not found!");

            string oldStoreLogoImageFolder = Settings.Default.OldStoreLogoImagePath;
            string newStoreImageFolder = Settings.Default.MainImagePath + "Store\\";

            List<string> thumbSizesForStoreLogo = new List<string>();
            thumbSizesForStoreLogo.AddRange(Settings.Default.StoreLogoThumbSizes.Split(';'));

            _totalCount = _db.Stores.Count();
            foreach (
                var store in
                    _db.Stores.OrderByDescending(x => x.MainPartyId).Select(x => new { StoreId = x.MainPartyId, x.StoreName, x.OldStoreLogo, x.StoreLogo })
                    )
            {
                _processedCount++;

                // Yeni lokasyonda klasorler yoksa olustur
                var di = Directory.CreateDirectory(newStoreImageFolder + store.StoreId.ToString());
                di.CreateSubdirectory("thumbs");

                string oldStoreLogoImageFilePath = oldStoreLogoImageFolder + store.OldStoreLogo;
                if (!File.Exists(oldStoreLogoImageFilePath))
                {
                    _sbErrors.AppendLine(String.Format("Store:{0}, Type:{1}, ImgFile:{2}  [File Not Found]", store.StoreId, "LOGO", store.OldStoreLogo));
                    continue;
                }

                // eski logoyu kopyala, varsa ustune yaz
                string newStoreLogoImageFilePath = newStoreImageFolder + store.StoreId.ToString() + "\\";
                string newStoreLogoImageFileName = store.StoreName.ToImageFileName() + "_logo.jpg";
                File.Copy(oldStoreLogoImageFilePath, newStoreLogoImageFilePath + newStoreLogoImageFileName, true);

                bool thumbResult = ImageProcessHelper.ImageResize(newStoreLogoImageFilePath + newStoreLogoImageFileName,
                        newStoreLogoImageFilePath + "thumbs\\" + store.StoreName.ToImageFileName(), thumbSizesForStoreLogo);

                if (!thumbResult)
                {
                    _sbErrors.AppendLine(String.Format("Store:{0}, Type:{1},  [An error occured while creating thumbnails]", store.StoreId, "LOGO"));
                }

                var currentStore = _dbSave.Stores.Where(x => x.MainPartyId.Equals(store.StoreId)).Single();
                currentStore.StoreLogo = newStoreLogoImageFileName;
                _dbSave.SaveChanges();

                LogConsole(store.StoreId + ": " + store.StoreName);
            }
        }

        private static string _procTyp = "";
        private static int _totalCount = 0;
        private static int _processedCount = 0;
        private static readonly StringBuilder _sbErrors = new StringBuilder();

        private static void LogProductErrorToDb(int? productId, int? imageId, string message)
        {
            _sbErrors.AppendLine(String.Format("Product:{0}, Image:{1} [{2}]", productId ?? 0, imageId ?? 0, message));
            _dbSave.ImageProcessLogs.AddObject(new ImageProcessLog
            {
                ProductId = productId,
                PictureId = imageId,
                Message = message
            });
            _dbSave.SaveChanges();
        }

        private static void LogConsole(string message)
        {
            Console.Clear();
            Console.WriteLine(String.Format("Toplam {1}: {0}", _totalCount, _procTyp));
            Console.WriteLine("\r\n--------------------");
            Console.Write(_sbErrors.ToString());
            Console.WriteLine("\r\n--------------------");
            Console.WriteLine(String.Format("Suan: Islenen: {0}, Kalan: {1}", _processedCount,
                _totalCount - _processedCount));
            Console.WriteLine(message);
        }
    }
}