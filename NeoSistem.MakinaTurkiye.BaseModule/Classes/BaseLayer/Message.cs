namespace NeoSistem.MakinaTurkiye.Classes
{
    using EnterpriseEntity.Business;
    using EnterpriseEntity.Business.Attributes;
    using System;
    using System.Data;

    [Table("Message")]
    public partial class Message : EntityObject
    {
        [Column("MessageId", SqlDbType.Int, PrimaryKey = true, Identity = true)]
        public int MessageId { get; set; }

        [Column("MessageSubject", SqlDbType.NVarChar)]
        public string MessageSubject { get; set; }

        [Column("MessageContent", SqlDbType.NVarChar)]
        public string MessageContent { get; set; }

        [Column("MessageRead", SqlDbType.Bit)]
        public bool MessageRead { get; set; }

        [Column("MessageDate", SqlDbType.DateTime)]
        public DateTime MessageDate { get; set; }

        [Column("Active", SqlDbType.Bit)]
        public bool Active { get; set; }

        [Column("ProductId", SqlDbType.Int)]
        public int ProductId { get; set; }
    }


}
