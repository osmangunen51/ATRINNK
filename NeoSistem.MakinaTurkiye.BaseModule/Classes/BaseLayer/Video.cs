namespace NeoSistem.MakinaTurkiye.Classes
{
    using EnterpriseEntity.Business;
    using EnterpriseEntity.Business.Attributes;
    using System;
    using System.Data;

    [Table("Video")]
    public partial class Video : EntityObject
    {
        [Column("VideoId", SqlDbType.Int, PrimaryKey = true, Identity = true)]
        public int VideoId { get; set; }

        [Column("ProductId", SqlDbType.Int)]
        public int ProductId { get; set; }

        [Column("VideoTitle", SqlDbType.NVarChar)]
        public string VideoTitle { get; set; }

        [Column("VideoPath", SqlDbType.NVarChar)]
        public string VideoPath { get; set; }

        [Column("VideoPicturePath", SqlDbType.NVarChar)]
        public string VideoPicturePath { get; set; }

        [Column("VideoSize", SqlDbType.BigInt)]
        public long? VideoSize { get; set; }

        [Column("VideoRecordDate", SqlDbType.DateTime)]
        public DateTime VideoRecordDate { get; set; }

        [Column("Active", SqlDbType.Bit)]
        public bool Active { get; set; }

    }


}
