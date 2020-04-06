using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Models.Home
{
    public class MTHomeAdModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public string ProductUrl { get; set; }
        public string PicturePath { get; set; }
        public string TruncatedProductName { get; set; }
        public string SimilarUrl { get; set; }
        public string TruncatedCategoryName { get; set; }
        public bool HasVideo { get; set; }
        public string BrandName { get; set; }
        public bool IsFavoriteProduct { get; set; }
        public string ProductPrice { get; set; }
        public string CurrencyCssName { get; set; }
        public byte Index { get; set; }
    }
}