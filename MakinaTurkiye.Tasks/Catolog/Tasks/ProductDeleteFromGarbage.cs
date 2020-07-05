using MakinaTurkiye.Core.Infrastructure;
using MakinaTurkiye.Entities.Tables.Catalog;
using MakinaTurkiye.Services.Catalog;
using MakinaTurkiye.Services.Media;
using MakinaTurkiye.Services.Videos;
using MakinaTurkiye.Tasks.Settings;
using NeoSistem.MakinaTurkiye.Core.Web.Helpers;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakinaTurkiye.Tasks.Catolog.Tasks
{
    public class ProductDeleteFromGarbage : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            IProductService productService = EngineContext.Current.Resolve<IProductService>();

            var products = productService.GetProductByProductActiveType(ProductActiveTypeEnum.CopKutusuYeni).Where(x=>x.ProductLastUpdate.HasValue && x.ProductLastUpdate.Value.AddMonths(3).Date < DateTime.Now.Date);
            IDeletedProductRedirectService deleteProductService = EngineContext.Current.Resolve<IDeletedProductRedirectService>();
            IPictureService pictureService = EngineContext.Current.Resolve<IPictureService>();
            IVideoService videoService = EngineContext.Current.Resolve<IVideoService>();
            foreach (var item in products)
            {
                var deletedProduct = new DeletedProductRedirect();
                deletedProduct.CategoryId = item.CategoryId.HasValue ? item.CategoryId.Value : 0;
                deletedProduct.ProductId = item.ProductId;
                deleteProductService.InsertDeletedProductRedirect(deletedProduct);
                var pictures = pictureService.GetPicturesByProductId(item.ProductId, false);
                FileHelpers.DeleteFullPath(ProductSettings.ProductImageFolder + item.ProductId);
                foreach (var picture in pictures)
                {
                    var thumbSizes = ProductSettings.ProductThumbSizes.Replace("*", "").Split(';');
                    pictureService.DeletePicture(picture);
                }
                var videos = videoService.GetVideosByProductId(item.ProductId);
                foreach (var video in videos)
                {
                    FileHelpers.Delete(ProductSettings.VideoFolder + video.VideoPath);
                    FileHelpers.Delete(ProductSettings.VideoThumbFodler + video.VideoPicturePath);
                    videoService.DeleteVideo(video);
                }
                productService.DeleteProduct(item);
            }
            return Task.CompletedTask;
        }
    }
}
