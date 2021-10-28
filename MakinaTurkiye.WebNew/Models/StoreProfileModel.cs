namespace NeoSistem.MakinaTurkiye.Web.Models
{
    using EnterpriseEntity.Extensions.Data;
    using global::MakinaTurkiye.Entities.Tables.Catalog;
    using global::MakinaTurkiye.Entities.Tables.Common;
    using global::MakinaTurkiye.Entities.Tables.Members;
    using global::MakinaTurkiye.Entities.Tables.Stores;
    using NeoSistem.MakinaTurkiye.Web.Models.Videos;
    using NeoSistem.MakinaTurkiye.Web.Models.ViewModels;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class StoreProfileModel
    {
        public StoreProfileModel()
        {
            Videos = new Dictionary<int, List<VideoModel>>();
            StoreImagePath = new List<string>();
            StoreInfoNumberShow = new StoreInfoNumberShow();
            VideosPage = new List<MTVideoModel>();
            VideoCategoryModel = new MTVideoCategoryModel();
        }

        public string ProductCapitalName { get; set; }
        public string StoreTypeName { get; set; }
        public string StoreEmployeesCount { get; set; }
        public string StoreEndorsementName { get; set; }

        public CategoryModel Sector { get; set; }
        public MTVideoCategoryModel VideoCategoryModel { get; set; }
        public int CategoryParentId { get; set; }

        public Classes.Category GetParentItem(int CategoryId)
        {
            var category = new Classes.Category();
            return category.GetParentItem(CategoryId);
        }

        public ICollection<CategoryModel> AnyCategory(int id)
        {
            var dataCategory = new Data.Category();

            return dataCategory.GetCategoryParentByCategoryId(id, (byte)MainCategoryType.Ana_Kategori).AsCollection<CategoryModel>();
        }

        public IEnumerable<CategoryModel> AnyCategory(int id, ICollection<CategoryModel> categoryItems)
        {
            return categoryItems.Where(c => c.CategoryParentId == id);
        }

        public IEnumerable<CategoryModel> GetLeftMenuCategories { get; set; }

        public Store Store { get; set; }

        public Member Member { get; set; }

        public StoreInfoNumberShow StoreInfoNumberShow { get; set; }

        public List<string> StoreImagePath { get; set; }

        public ICollection<ActivityTypeModel> ActivityTypeItems { get; set; }

        public IList<Address> StoreAddressItems { get; set; }

        public IList<StoreDealer> StoreDealerItems { get; set; }

        //public ICollection<PictureModel> StoreDealerPictureItems(int StoreDealerId)
        //{
        //  var dataPicture = new Data.Picture();
        //  return dataPicture.GetItemsByStoreDealerId(StoreDealerId).AsCollection<PictureModel>();
        //}

        public AddressModel StoreDealerAddress(int StoreDealerId)
        {
            var dataStoreDealer = new Data.Address();
            var items = dataStoreDealer.GetAddressByMainPartyIdAndStoreDealer(StoreDealerId).AsCollection<AddressModel>().ToList();
            if (items != null && items.Count > 0)
            {
                return items[0];
            }
            return new AddressModel();
        }

        public IEnumerable<DealerBrand> DealerBrandItems { get; set; }

        public IEnumerable<Address> DealerBayiiAddressItems { get; set; }
        public IEnumerable<Address> DealerServisAddressItems { get; set; }
        public IEnumerable<Address> DealerSubeAddressItems { get; set; }
        public IEnumerable<StoreBrand> StoreBrandItems { get; set; }

        //public ICollection<spProductSearchAsCategories_Result> CategoryLeftSide { get; set; }


        public ICollection<CategoryModel> CategoryItems { get; set; }

        public IEnumerable<CategoryModel> LeftMenuCategories { get; set; }

        public SearchModel<ProductModel> ProductItems { get; set; }
        public IList<Phone> PhoneItems { get; set; }

        public ICollection<RelMainPartyCategoryModel> MainPartyRelatedCategoryItems { get; set; }

        public IList<Category> StoreActivityCategories { get; set; }
        public string ProductCategoryText { get; set; }

        public IEnumerable<CategoryModel> TopCategories { get; set; }

        public IDictionary<int, List<VideoModel>> Videos { get; set; }
        public List<MTVideoModel> VideosPage { get; set; }

        public class InnerCategoryModel
        {
            public InnerCategoryModel(int id, ICollection<CategoryModel> categoryItems)
            {
                this.ParentCategoryId = id;
                this.StoreCategoryItems = categoryItems;
            }

            public int ParentCategoryId { get; set; }
            public ICollection<CategoryModel> StoreCategoryItems { get; set; }

            public IEnumerable<CategoryModel> AnyCategory(int id)
            {
                return StoreCategoryItems.Where(c => c.CategoryParentId == id);
            }
        }


        public class VideoModel
        {
            public bool? Active { get; set; }
            public int? BrandId { get; set; }
            public string BrandName { get; set; }
            public string CategoryName { get; set; }
            public int? ModelId { get; set; }
            public string ModelName { get; set; }
            public int? ProductId { get; set; }
            public string ProductName { get; set; }
            public int? SectorId { get; set; }
            public long? SingularViewCount { get; set; }
            public int VideoId { get; set; }
            public string VideoPath { get; set; }
            public string VideoPicturePath { get; set; }
            public DateTime? VideoRecordDate { get; set; }
            public long? VideoSize { get; set; }
            public string VideoTitle { get; set; }
            public string StoreName { get; set; }
        }

    }
}