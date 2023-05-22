namespace NeoSistem.Trinnk.Web.Models
{
    using global::Trinnk.Entities.Tables.Catalog;
    using NeoSistem.Trinnk.Web.Models.ViewModels;
    using System;
    using System.Collections.Generic;

    //  public class VideoDuration
    //{
    //      public long VideoId { get; set; }
    //      public  string VideoMinute { get; set; }
    //      public  string VideoSecond { get; set; }
    //}

    public class VideoModel
    {
        public SearchModel<VideoModel> SearchModel { get; set; }
        public IList<Category> SectorItems { get; set; }
        //public IList<ECVideo> VideoItems { get; set; }
        //public IList<VideoStore_Result> VideoStoreItems { get; set; }
        //public IList<VideoCategoryList> VideoCategoryList { get; set; }
        public int VideoId { get; set; }
        public string VideoTitle { get; set; }
        public string VideoPath { get; set; }
        public string VideoPicturePath { get; set; }
        public long? VideoSize { get; set; }
        public byte? VideoMinute { get; set; }

        public byte? VideoSecond { get; set; }
        public DateTime VideoRecordDate { get; set; }
        public bool Active { get; set; }
        public int ProductId { get; set; }
        public string CategoryName { get; set; }
        public long SingularViewCount { get; set; }
        public string ProductName { get; set; }
        public string StoreName { get; set; }
        public string ProductUrl { get; set; }
        public string VideoUrl { get; set; }
        //public List<VideoDuration> VideoDuration { get; set; }
    }
}

