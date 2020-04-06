namespace NeoSistem.MakinaTurkiye.Web.Models.ViewModels
{
  using System.Collections.Generic;
  using EnterpriseEntity.Extensions;
  using System;
    using global::MakinaTurkiye.Entities.Tables.Seos;
    using global::MakinaTurkiye.Entities.Tables.Stores;
    using global::MakinaTurkiye.Entities.Tables.Common;

  public class ProductDetailViewModel
  {
        public ProductDetailViewModel()
        {
            this.Videos = new Dictionary<int,List<VideoModel>>();
            this.Seo = new Seo();
        }

        public MessageProductPage MessageProductPage { get; set; }
        //public ProductDetailInfo ProductDetailInfo { get; set; }
        public string MemberNo { get; set; }
        public bool? ReadyForSale { get; set; }
            

        public IDictionary<int,List<VideoModel>> Videos { get; set; }

        //public IList<Picture> ProductPictureItems { get; set; }
        //public IList<TopCategory> TopCategoryItems { get; set; }
        //public EntitySearchModel<OtherProduct> AuthorizedOtherProductItems { get; set; }

        public Store Store { get; set; }
        public bool HasFavoriteStore { get; set; }
        public bool HasFavoriteProduct { get; set; }
        public int? MenseiId { get; set; }
        public Banner ProductLeftSideBanner { get; set; }

        public string MapCode { get; set; }
        public Seo Seo { get; set; }
       
    
    //public IList<Phone> PhoneItems
    //{
    //  get
    //  {
    //    IList<Phone> phoneItems = new List<Phone>();
    //    string[] phones;

    //    if (ProductDetailInfo.MemberType == (byte)MemberType.Enterprise)
    //    {
    //      phones = ProductDetailInfo.StorePhoneText.Split('|');
    //    }
    //    else
    //    {
    //      phones = ProductDetailInfo.MemberPhoneText.Split('|');
    //    }

    //    foreach (var item in phones)
    //    {
    //      if (!string.IsNullOrWhiteSpace(item))
    //      {
    //        string[] items = item.Split(',');
    //        var phone = new Phone
    //        {
    //          PhoneId = items.GetValue(0).ToString().Replace("PhoneId=", "").ToInt32(),
    //          PhoneCulture = items.GetValue(3).ToString().Replace("PhoneCulture=", ""),
    //          PhoneAreaCode = items.GetValue(4).ToString().Replace("PhoneAreaCode=", ""),
    //          PhoneNumber = items.GetValue(5).ToString().Replace("PhoneNumber=", ""),
    //          PhoneType = items.GetValue(6).ToString().Replace("PhoneType=", "").ToByte(),
    //          GsmType = items.GetValue(7).ToString().Replace("GsmType=", "").ToString() != "" ? (items.GetValue(7).ToString().Replace("GsmType=", "").ToString()).ToByte() : (0).ToByte()
    //        };
    //        phoneItems.Add(phone);
    //      }
    //    }
    //    return phoneItems;
    //  }
    //}

        public class VideoModel
        {
            public long? SingularViewCount { get; set; }
            public int VideoId { get; set; }
            public string VideoPath { get; set; }
            public string VideoPicturePath { get; set; }
            public DateTime? VideoRecordDate { get; set; }
            public long? VideoSize { get; set; }
            public string VideoTitle { get; set; }
            public string ProductName { get; set; }
        }

  }
}
