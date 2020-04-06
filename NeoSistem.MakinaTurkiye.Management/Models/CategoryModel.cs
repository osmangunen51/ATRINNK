


namespace NeoSistem.MakinaTurkiye.Management.Models
{
    using EnterpriseEntity.Extensions.Data;
    using NeoSistem.MakinaTurkiye.Cache;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CategoryModel
    {
        public int CategoryId { get; set; }

        public int? CategoryParentId { get; set; }

        public int PropertyId { get; set; }

        public int CategoryOrder { get; set; }

        public string CategoryName { get; set; }

        public string CategoryTreeName { get; set; }

        public byte CategoryType { get; set; }

        public byte MainCategoryType { get; set; }

        public string Content { get; set; }
        public byte CategoryRoute { get; set; }

        public bool Active { get; set; }
        public Int16? BaseMenuOrder { get; set; }
        public DateTime RecordDate { get; set; }
        public int RecordCreatorId { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public int LastUpdaterId { get; set; }
        public int IsParent { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Keywords { get; set; }
        public string CategoryContentTitle { get; set; }
        public string StorePageTitle { get; set; }
        public string CategoryUrl { get; set; }

        public static IEnumerable<CategoryModel> Childrens(int id)
        {
            var dataCategory = new Data.Category();

            var categories = CacheUtilities.GetCategories().AsCollection<CategoryModel>();
            var model = categories.Where(c => c.CategoryParentId == id).AsEnumerable<CategoryModel>();

            return model;
        }

    }
}