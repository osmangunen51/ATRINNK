using Trinnk.Core.Infrastructure;
using Trinnk.Entities.Tables.Catalog;
using Trinnk.Services.Catalog;
using Trinnk.Services.Media;
using Trinnk.Services.Videos;
using Trinnk.Tasks.Settings;
using NeoSistem.Trinnk.Core.Web.Helpers;
using Quartz;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Trinnk.Tasks.Catolog.Tasks
{
    public class ProductDeleteFromGarbage : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            IProductService productService = EngineContext.Current.Resolve<IProductService>();

            var products = productService.GetProductByProductActiveType(ProductActiveTypeEnum.CopKutusuYeni).Where(x => x.ProductLastUpdate.HasValue && x.ProductLastUpdate.Value.AddMonths(3).Date < DateTime.Now.Date);
            IDeletedProductRedirectService deleteProductService = EngineContext.Current.Resolve<IDeletedProductRedirectService>();
            IPictureService pictureService = EngineContext.Current.Resolve<IPictureService>();
            IVideoService videoService = EngineContext.Current.Resolve<IVideoService>();
            foreach (var item in products)
            {
                var deletedProduct = new DeletedProductRedirect();
                deletedProduct.CategoryId = item.CategoryId.HasValue ? item.CategoryId.Value : 0;
                deletedProduct.ProductId = item.ProductId;
                deleteProductService.InsertDeletedProductRedirect(deletedProduct);
                var pictures = pictureService.GetPicturesByProductId(item.ProductId,false);
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
