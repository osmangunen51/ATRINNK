namespace NeoSistem.MakinaTurkiye.Classes
{
    using EnterpriseEntity.Business;
    using EnterpriseEntity.Business.Attributes;
    using System.Data;

    [Table("MessageMainParty")]
    public partial class MessageMainParty : EntityObject
    {
        [Column("MessageMainPartyId", SqlDbType.Int, PrimaryKey = true, Identity = true)]
        public int MessageMainPartyId { get; set; }

        [Column("MessageId", SqlDbType.Int)]
        public int MessageId { get; set; }

        [Column("MainPartyId", SqlDbType.Int)]
        public int MainPartyId { get; set; }

        [Column("InOutMainPartyId", SqlDbType.Int)]
        public int InOutMainPartyId { get; set; }

        [Column("MessageType", SqlDbType.TinyInt)]
        public byte MessageType { get; set; }

    }


}
