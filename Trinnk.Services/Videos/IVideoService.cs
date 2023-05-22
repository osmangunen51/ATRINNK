using Trinnk.Core;
using Trinnk.Entities.StoredProcedures.Videos;
using Trinnk.Entities.Tables.Videos;
using System.Collections.Generic;

namespace Trinnk.Services.Videos
{
    public partial interface IVideoService : ICachingSupported
    {
        IList<PopularVideoResult> GetSPPopularVideos();

        IList<PopularVideoResult> GetSPPopularVideos(int CategoryId);

        IList<VideoCategoryResult> GetSPVideoCategoryByCategoryParentId(int categoryParentId = 0);

        IList<VideoCategoryResult> GetSPVideoCategoryByCategoryParentIdNew(int categoryParentId = 0);

        IPagedList<VideoResult> GetSPVideoByCategoryId(int categoryId, int pageIndex, int pageSize);

        IList<VideoResult> GetSPOtherVideoByCategoryIdAndSelectedCategoryId(int categoryId, int topCount, int selectedCategoryId);

        IList<ShowOnShowcaseVideoResult> GetSPShowOnShowcaseVideos();

        IList<Video> GetShowOnShowcaseVideos();

        Video GetVideoByVideoId(int videoId, bool showHidden = false);

        void UpdateVideo(Video video);
        void InsertVideo(Video video);
        void DeleteVideo(Video video);
        void ClearAllVideoCacheWithProductId(int ProductId = 0);

        List<Video> GetVideoByStoreMainPartyId(int storeMainPartyId, bool showHidden = false);

        ProductAndStoreDetailResult GetSPStoreAndProductDetailByProductId(int productId);

        IList<Video> GetVideosByProductId(int productId);

        //IList<VideoResult> GetVideoResultsWithPageSizeCompletion(IList<VideoResult> sendVideos,int categoryId, int pageSize);

        // int GetVideoByCategoryParentId(int categoryId);

        IPagedList<VideoResult> GetSpVideosBySearchText(string searchText, int categoryId, int pageSize, int pageIndex);

        IList<VideoResult> GetSPVideoByMainPartyIdAndCategoryId(int mainPartyId, int categoryId);

        IList<SiteMapVideoResult> GetSiteMapVideos();
    }
}
