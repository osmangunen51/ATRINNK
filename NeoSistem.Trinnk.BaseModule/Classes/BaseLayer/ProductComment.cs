namespace NeoSistem.Trinnk.Classes
{
    using EnterpriseEntity.Business;
    using EnterpriseEntity.Business.Attributes;
    using System;
    using System.Data;

    [Table("ProductComment")]
    public partial class ProductComment : EntityObject
    {
        [Column("ProductCommentId", SqlDbType.Int, PrimaryKey = true, Identity = true)]
        public int ProductCommentId { get; set; }

        [Column("ProductId", SqlDbType.Int)]
        public int ProductId { get; set; }

        [Column("MainPartyId", SqlDbType.Int)]
        public int MainPartyId { get; set; }

        [Column("ProductCommentText", SqlDbType.VarChar)]
        public string ProductCommentText { get; set; }

        [Column("Active", SqlDbType.Bit)]
        public bool Active { get; set; }

        [Column("RecordDate", SqlDbType.DateTime)]
        public DateTime RecordDate { get; set; }

    }


}
