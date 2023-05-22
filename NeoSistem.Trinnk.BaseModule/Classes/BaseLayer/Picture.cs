namespace NeoSistem.Trinnk.Classes
{
    using EnterpriseEntity.Business;
    using EnterpriseEntity.Business.Attributes;
    using System.Data;

    [Table("Picture")]
    public partial class Picture : EntityObject
    {
        [Column("PictureId", SqlDbType.Int, PrimaryKey = true, Identity = true)]
        public int PictureId { get; set; }

        [Column("ProductId", SqlDbType.Int)]
        public int? ProductId { get; set; }

        [Column("MainPartyId", SqlDbType.Int)]
        public int? MainPartyId { get; set; }

        [Column("PictureName", SqlDbType.NVarChar)]
        public string PictureName { get; set; }

        [Column("PicturePath", SqlDbType.NVarChar)]
        public string PicturePath { get; set; }
        [Column("PictureOrder", SqlDbType.Int)]
        public int PictureOrder { get; set; }
    }


}
