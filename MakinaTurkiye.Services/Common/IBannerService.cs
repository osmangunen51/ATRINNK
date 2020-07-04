using MakinaTurkiye.Entities.Tables.Common;
using System.Collections.Generic;

namespace MakinaTurkiye.Services.Common
{
    public interface IBannerService
    {
        IList<Banner> GetBannersByCategoryId(int categoryId);
        IList<Banner> GetBannersByBannerType(byte bannerType);
        Banner GetBannerByBannerId(int bannerId);
        void InsertBanner(Banner banner);
        void DeleteBanner(Banner banner);
        
    }
}
