using MakinaTurkiye.Caching;
using MakinaTurkiye.Core;
using MakinaTurkiye.Core.Data;
using MakinaTurkiye.Data;
using MakinaTurkiye.Entities.StoredProcedures.Videos;
using MakinaTurkiye.Entities.Tables.Videos;
using MakinaTurkiye.Services.Catalog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;

namespace MakinaTurkiye.Services.Videos
{
    public partial class VideoService : BaseService, IVideoService
    {
        #region Constants

        private const string VIDEOS_BY_POPULAR_VIDEOS_CATEGORY_ID_KEY = "makinaturkiye.video.popularvideo.bycategoryId-{0}";
        private const string VIDEOS_BY_PRODUCT_ID_KEY = "makinaturkiye.video.byproductId-{0}";
        private const string VIDEOS_BY_POPULAR_VIDEOS_KEY = "makinaturkiye.video.popularvideo.bynoneparameter";
        private const string VIDEOS_VIDEO_CATEGORY_BY_CATEGORY_PARENT_ID_KEY = "makinaturkiye.video.videocategory.bycategoryparentId-{0}";
        private const string VIDEOS_SP_CATEGORY_ID_KEY = "makinaturkiye.video.sp.bycategoryid-{0}-{1}-{2}";
        private const string VIDEOS_SP_OTHER_VIDEO_BY_CATEGORY_ID_KEY = "makinaturkiye.video.sp.othervideo.bycategoryid-{0}-{1}-{2}";
        private const string VIDEOS_SP_BY_SEARCH_TEXT_KEY = "makinaturkiye.video.sp.byseachtext-{0}-{1}-{2}-{3}";
        private const string VIDEOS_SP_SHOWONSHOWCASE_KEY = "makinaturkiye.video.sp.showonshowcase";
        private const string VIDEOS_BY_VIDEO_ID_KEY = "makinaturkiye.video.byid-{0}";
        private const string VIDEOS_SP_MAINPARTY_ID_KEY = "makinaturkiye.video.sp.bymainpartyid-{0}-{1}";

        private const string VIDEOS_STOREMAINPARTY_ID_KEY = "makinaturkiye.video.sp.bystoremainpartyid-{0}-{1}";
        private const string VIDEOS_STOREMAINPARTY_PATTERN = "makinaturkiye.video.sp.bymainpartyid-{0}";

        #endregion

        #region Fields

        private readonly IDbContext _dbContext;
        private readonly IDataProvider _dataProvider;
        private readonly IRepository<Video> _videoRepository;
        private readonly ICacheManager _cacheManager;
        private readonly ICategoryService _categoryService;

        #endregion

        #region Ctor

        public VideoService(IDbContext dbContext, IDataProvider dataProvider, 
            IRepository<Video> videoRepository, 
            ICacheManager cacheManager, 
            ICategoryService categoryService): base(cacheManager)
        {
            this._dbContext = dbContext;
            this._dataProvider = dataProvider;
            this._videoRepository = videoRepository;
            this._cacheManager = cacheManager;
            this._categoryService = categoryService;
        }

        #endregion


        #region Utilities

        private void RemoveVideoCache(Video video)
        {
            if(video.ProductId>0)
            {
                string productIdKey = string.Format(VIDEOS_BY_PRODUCT_ID_KEY, video.ProductId);
                _cacheManager.Remove(productIdKey);
            }

            if(video.StoreMainPartyId>0)
            {
                string patternKey = string.Format(VIDEOS_STOREMAINPARTY_PATTERN, video.StoreMainPartyId);
                _cacheManager.RemoveByPattern(patternKey);
            }

            string videoIdKey = string.Format(VIDEOS_BY_VIDEO_ID_KEY, video.VideoId);
            _cacheManager.Remove(videoIdKey);

        }

        #endregion


        #region Methods

        public IList<PopularVideoResult> GetSPPopularVideos()
        {
            string key = VIDEOS_BY_POPULAR_VIDEOS_KEY;
            return _cacheManager.Get(key, () =>
            {
                const int topCount = 10;
                var pTopCount = _dataProvider.GetParameter();
                pTopCount.ParameterName = "TopCount";
                pTopCount.Value = topCount;
                pTopCount.DbType = DbType.Int32;
                var popularVideos = _dbContext.SqlQuery<PopularVideoResult>("SP_GetPopularVideoTopCount @TopCount", pTopCount).ToList();
                return popularVideos;
            });
        }

        public IList<PopularVideoResult> GetSPPopularVideos(int categoryId)
        {
            if (categoryId == 0)
                return new List<PopularVideoResult>();

            string key = string.Format(VIDEOS_BY_POPULAR_VIDEOS_CATEGORY_ID_KEY, categoryId);

            return _cacheManager.Get(key, () =>
            {
                const int topCount = 10;
                var pTopCount = _dataProvider.GetParameter();
                pTopCount.ParameterName = "TopCount";
                pTopCount.Value = topCount;
                pTopCount.DbType = DbType.Int32;

                var pCategoryId = _dataProvider.GetParameter();
                pCategoryId.ParameterName = "CategoryId";
                pCategoryId.Value = categoryId;
                pCategoryId.DbType = DbType.Int32;

                var popularVideos = _dbContext.SqlQuery<PopularVideoResult>("SP_GetPopularVideoTopCountByCategoryId @TopCount, @CategoryId", pTopCount, pCategoryId).ToList();
                return popularVideos;
            });
        }

        public IList<VideoCategoryResult> GetSPVideoCategoryByCategoryParentId(int categoryParentId = 0)
        {
            var pCategoryParentId = _dataProvider.GetParameter();
            pCategoryParentId.ParameterName = "CategoryParentId";
            pCategoryParentId.Value = categoryParentId;
            pCategoryParentId.DbType = DbType.Int32;
            var videoCategories = _dbContext.SqlQuery<VideoCategoryResult>("SP_GetVideoCategoryByCategoryParentId @CategoryParentId", pCategoryParentId).ToList();
            return videoCategories;
        }

        public IList<VideoCategoryResult> GetSPVideoCategoryByCategoryParentIdNew(int categoryParentId = 0)
        {
            string key = string.Format(VIDEOS_VIDEO_CATEGORY_BY_CATEGORY_PARENT_ID_KEY, categoryParentId);
            return _cacheManager.Get(key, () => 
            {
                var pCategoryParentId = _dataProvider.GetParameter();
                pCategoryParentId.ParameterName = "CategoryParentId";
                pCategoryParentId.Value = categoryParentId;
                pCategoryParentId.DbType = DbType.Int32;
                var videoCategories = _dbContext.SqlQuery<VideoCategoryResult>("SP_GetVideoCategoryByCategoryParentIdNew @CategoryParentId", pCategoryParentId).ToList();
                return videoCategories;
            });
        }

        public IPagedList<VideoResult> GetSPVideoByCategoryId(int categoryId, int pageIndex, int pageSize)
        {
            string key = string.Format(VIDEOS_SP_CATEGORY_ID_KEY, categoryId, pageIndex, pageSize);

            return _cacheManager.Get(key, () => 
            {
                var pCategoryId = _dataProvider.GetParameter();
                pCategoryId.ParameterName = "CategoryId";
                pCategoryId.Value = categoryId;
                pCategoryId.DbType = DbType.Int32;

                var pPageIndex = _dataProvider.GetParameter();
                pPageIndex.ParameterName = "PageIndex";
                pPageIndex.Value = pageIndex;
                pPageIndex.DbType = DbType.Int32;

                var pPageSize = _dataProvider.GetParameter();
                pPageSize.ParameterName = "PageSize";
                pPageSize.Value = pageSize;
                pPageSize.DbType = DbType.Int32;


                var pTotalRecords = _dataProvider.GetParameter();
                pTotalRecords.ParameterName = "TotalRecords";
                pTotalRecords.DbType = DbType.Int32;
                pTotalRecords.Direction = ParameterDirection.Output;

                var videos = _dbContext.SqlQuery<VideoResult>("SP_GetVideoByCategoryId @CategoryId, @PageIndex, @PageSize, @TotalRecords output", pCategoryId, pPageIndex, pPageSize, pTotalRecords).ToList();

                int totalRecords = (pTotalRecords.Value != DBNull.Value) ? Convert.ToInt32(pTotalRecords.Value) : 0;

                return new PagedList<VideoResult>(videos, pageIndex, pageSize, totalRecords);
            });
        }

        public IList<VideoResult> GetSPOtherVideoByCategoryIdAndSelectedCategoryId(int categoryId, int topCount, int selectedCategoryId)
        {

            string key = string.Format(VIDEOS_SP_OTHER_VIDEO_BY_CATEGORY_ID_KEY, categoryId, topCount, selectedCategoryId);
            return _cacheManager.Get(key, () => 
            {

                var pCategoryId = _dataProvider.GetParameter();
                pCategoryId.ParameterName = "CategoryId";
                pCategoryId.Value = categoryId;
                pCategoryId.DbType = DbType.Int32;


                var pTopCount = _dataProvider.GetParameter();
                pTopCount.ParameterName = "TopCount";
                pTopCount.Value = topCount;
                pTopCount.DbType = DbType.Int32;

                var pSelectedCategoryId = _dataProvider.GetParameter();
                pSelectedCategoryId.ParameterName = "SelectedCategoryId";
                pSelectedCategoryId.Value = selectedCategoryId;
                pSelectedCategoryId.DbType = DbType.Int32;

                var videos = _dbContext.SqlQuery<VideoResult>("SP_GetOtherVideosByCategoryIdAndSelectedCategoryId @CategoryId,  @TopCount, @SelectedCategoryId", pCategoryId, pTopCount, pSelectedCategoryId).ToList();
                return videos;
            });
        }

        public IPagedList<VideoResult> GetSpVideosBySearchText(string searchText, int categoryId, int pageSize, int pageIndex)
        {

            string key = string.Format(VIDEOS_SP_BY_SEARCH_TEXT_KEY, searchText, categoryId, pageSize, pageIndex);
            return _cacheManager.Get(key, () => 
            {
                var pSearchText = _dataProvider.GetParameter();
                pSearchText.ParameterName = "SearchText";
                pSearchText.Value = searchText;
                pSearchText.DbType = DbType.String;

                var pCategoryId = _dataProvider.GetParameter();
                pCategoryId.ParameterName = "CategoryId";
                pCategoryId.Value = categoryId;
                pCategoryId.DbType = DbType.Int32;

                var pPageSize = _dataProvider.GetParameter();
                pPageSize.ParameterName = "PageSize";
                pPageSize.Value = pageSize;
                pPageSize.DbType = DbType.Int32;

                var pPageIndex = _dataProvider.GetParameter();
                pPageIndex.ParameterName = "PageIndex";
                pPageIndex.Value = pageIndex;
                pPageIndex.DbType = DbType.Int32;

                var pTotalRecord = _dataProvider.GetParameter();
                pTotalRecord.ParameterName = "TotalRecords";
                pTotalRecord.DbType = DbType.Int32;
                pTotalRecord.Direction = ParameterDirection.Output;

                var datatable = _dbContext.ExecuteDataTable("SP_WebSearchVideo", CommandType.StoredProcedure, pSearchText, pCategoryId, pPageSize, pPageIndex, pTotalRecord);
                var video = datatable.DataTableToObjectList<VideoResult>();

                int totalRecords = (pTotalRecord.Value != DBNull.Value) ? Convert.ToInt32(pTotalRecord.Value) : 0;

                return new PagedList<VideoResult>(video, pageIndex, pageSize, totalRecords);
            });
        }

        public IList<ShowOnShowcaseVideoResult> GetSPShowOnShowcaseVideos()
        {
            string key = string.Format(VIDEOS_SP_SHOWONSHOWCASE_KEY);
            return _cacheManager.Get(key, () => 
            {
                var showOnShowcaseVideos = _dbContext.SqlQuery<ShowOnShowcaseVideoResult>("SP_GetShowOnShowcaseVideo").ToList();
                return showOnShowcaseVideos;
            });
        }

        public IList<Video> GetShowOnShowcaseVideos()
        {
            var query = _videoRepository.Table;

            query = query.Where(v => v.ShowOnShowcase == true);
            query = query.Where(v => v.Active == true && v.Product.ProductActiveType == 1);
            query = query.Where(v => v.Product.ProductActive == true);

            var showOnShowcaseVideos = query.ToList();
            return showOnShowcaseVideos;
        }

        public Video GetVideoByVideoId(int videoId,bool showHidden=false)
        {

            string key = string.Format(VIDEOS_BY_VIDEO_ID_KEY,videoId);
            return _cacheManager.Get(key, () => 
            {
                var query = _videoRepository.Table;

                if (!showHidden)
                    query = query.Where(v => v.Active==true);

                query = query.Include(v => v.Product);
                query = query.Include(v => v.Product.Brand);
                query = query.Include(v => v.Product.Model);
                query = query.Include(v => v.Product.Category);

                var video = query.FirstOrDefault(v => v.VideoId == videoId);
                return video;
            });
        }

        public IList<Video> GetVideosByProductId(int productId)
        {
            if (productId == 0)
                return new List<Video>();

            string key = string.Format(VIDEOS_BY_PRODUCT_ID_KEY, productId);
            return _cacheManager.Get(key, () =>
            {
                var query = _videoRepository.Table;
                query = query.Where(v => v.ProductId == productId && v.Active == true);
                query = query.Include(v => v.Product);
                return query.ToList();
            });
        }

        public ProductAndStoreDetailResult GetSPStoreAndProductDetailByProductId(int productId)
        {
            if (productId == 0)
                throw new ArgumentException("productId");

            var pProductId = _dataProvider.GetParameter();
            pProductId.ParameterName = "ProductId";
            pProductId.Value = productId;
            pProductId.DbType = DbType.Int32;

            var productAndStoreDetails = _dbContext.SqlQuery<ProductAndStoreDetailResult>("SP_GetStoreAndProductDetailByProductId @ProductId", pProductId).ToList();

            return productAndStoreDetails.FirstOrDefault();
        }

        public IList<VideoResult> GetSPVideoByMainPartyIdAndCategoryId(int mainPartyId,int categoryId)
        {
            if (mainPartyId == 0)
                throw new ArgumentNullException("mainPartyId");

            string key = string.Format(VIDEOS_SP_MAINPARTY_ID_KEY, mainPartyId,categoryId);
            return _cacheManager.Get(key, () =>
            {
                var pMainPartyId = _dataProvider.GetParameter();
                pMainPartyId.ParameterName = "MainPartyId";
                pMainPartyId.Value = mainPartyId;
                pMainPartyId.DbType = DbType.Int32;

                var pCategoryId = _dataProvider.GetParameter();
                pCategoryId.ParameterName = "CategoryId";
                pCategoryId.Value = categoryId;
                pCategoryId.DbType = DbType.Int32;

                var videoResults = _dbContext.SqlQuery<VideoResult>("SP_GetVideoByMainPartyId @MainPartyId,@CategoryId", pMainPartyId, pCategoryId).ToList();
                return videoResults;
            });
        }

        public List<Video> GetVideoByStoreMainPartyId(int storeMainPartyId, bool showHidden=false)
        {
            if (storeMainPartyId == 0)
                throw new ArgumentNullException("storeMainPartyId");

            string key = string.Format(VIDEOS_STOREMAINPARTY_ID_KEY, storeMainPartyId,showHidden);
            return _cacheManager.Get(key, () => 
            {
                var query = _videoRepository.Table;

                if(!showHidden)
                    query = query.Where(v => v.Active == true);

                query = query.Where(x => x.StoreMainPartyId == storeMainPartyId);

                var videos = query.ToList();
                return videos;
            });
        }

        public IList<SiteMapVideoResult> GetSiteMapVideos()
        {
            const string sql = "SP_GetSiteMapVideo";
            var videos = _dbContext.SqlQuery<SiteMapVideoResult>(sql).ToList();
            return videos;
        }

        public void DeleteVideo(Video video)
        {
            if (video == null)
                throw new ArgumentNullException("video");

            _videoRepository.Delete(video);

            RemoveVideoCache(video);

        }

        public void InsertVideo(Video video)
        {
            if (video == null)
                throw new ArgumentNullException("video");

            _videoRepository.Insert(video);

            RemoveVideoCache(video);
        }

        public void UpdateVideo(Video video)
        {
            if (video == null)
                throw new ArgumentNullException("video");

            _videoRepository.Update(video);

            RemoveVideoCache(video);
        }

        #endregion

    }
}
