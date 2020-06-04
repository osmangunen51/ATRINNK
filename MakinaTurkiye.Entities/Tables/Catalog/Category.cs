using MakinaTurkiye.Entities.Tables.Content;
using MakinaTurkiye.Entities.Tables.Stores;
using System;
using System.Collections.Generic;

namespace MakinaTurkiye.Entities.Tables.Catalog
{
    public partial class Category:BaseEntity
    {
        //private ICollection<Product> _brandProducts;
        //private ICollection<Product> _modelProducts;
        //private ICollection<Product> _categoryProducts;
        //private ICollection<CategoryPlaceChoice> _categoryPlaceChoice;
        //private ICollection<BaseMenuCategory> _baseMenuCategories;
        //private ICollection<StoreActivityCategory> _storeActivityCategories;
        //private ICollection<ProductHomePage> _productHomePages;


        public int CategoryId { get; set; }
        public int? CategoryParentId { get; set; }
        public string CategoryName { get; set; }
        public int? CategoryOrder { get; set; }
        public byte? CategoryType { get; set; }
        public bool? Active { get; set; }
        public DateTime? RecordDate { get; set; }
        public int? RecordCreatorId { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public int? LastUpdaterId { get; set; }
        public int? ProductCount { get; set; }
        public byte? MainCategoryType { get; set; }
        public string Content { get; set; }
        public string Description { get; set; }

        public string CategoryIcon { get; set; }
        public int? ProductCountAll { get; set; }
        public int? ProductCountNew { get; set; }
        public int? ProductCountOld { get; set; }
        public string CategoryContentTitle { get; set; }
        public string Title { get; set; }
        public string Keywords { get; set; }
        public string FilterableCategoryIds { get; set; }
        public string StorePageTitle { get; set; }
        public string CategoryPath { get; set; }
        public string CategoryPathUrl { get; set; }
        public Int16 ? BaseMenuOrder { get; set; }
        public string HomeImagePath { get; set; }

        //public virtual ICollection<BaseMenuCategory> BaseMenuCategories
        //{
        //    get { return _baseMenuCategories ?? (_baseMenuCategories = new List<BaseMenuCategory>()); }
        //    protected set { _baseMenuCategories = value; }
        //}

        //public virtual ICollection<Product> BrandProducts
        //{
        //    get { return _brandProducts ?? (_brandProducts = new List<Product>()); }
        //    protected set { _brandProducts = value; }
        //}

        //public virtual ICollection<Product> ModelProducts
        //{
        //    get { return _modelProducts ?? (_modelProducts = new List<Product>()); }
        //    protected set { _modelProducts = value; }
        //}

        //public virtual ICollection<Product> CategoryProducts
        //{
        //    get { return _categoryProducts ?? (_categoryProducts = new List<Product>()); }
        //    protected set { _categoryProducts = value; }
        //}

        //public virtual ICollection<CategoryPlaceChoice> CategoryPlaceChoices
        //{
        //    get { return _categoryPlaceChoice ?? (_categoryPlaceChoice = new List<CategoryPlaceChoice>()); }
        //    protected set { _categoryPlaceChoice = value; }
        //}


        //public virtual ICollection<StoreActivityCategory> StoreActivityCategories
        //{
        //    get { return _storeActivityCategories ?? (_storeActivityCategories = new List<StoreActivityCategory>()); }
        //    protected set { _storeActivityCategories = value; }
        //}

        //public virtual ICollection<ProductHomePage> ProductHomePages
        //{
        //    get { return _productHomePages ?? (_productHomePages = new List<ProductHomePage>()); }
        //    protected set { _productHomePages = value; }
        //}


    }
}
