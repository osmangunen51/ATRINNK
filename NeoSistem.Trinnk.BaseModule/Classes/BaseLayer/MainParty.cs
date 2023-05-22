namespace NeoSistem.Trinnk.Classes
{
    using EnterpriseEntity.Business;
    using EnterpriseEntity.Business.Attributes;
    using System;
    using System.Data;

    [Table("MainParty")]
    public partial class MainParty : EntityObject
    {
        [Column("MainPartyId", SqlDbType.Int, PrimaryKey = true, Identity = true)]
        public int MainPartyId { get; set; }

        [Column("MainPartyFullName", SqlDbType.NVarChar)]
        public string MainPartyFullName { get; set; }

        [Column("MainPartyType", SqlDbType.TinyInt)]
        public byte MainPartyType { get; set; }

        [Column("Active", SqlDbType.Bit)]
        public bool Active { get; set; }

        [Column("MainPartyRecordDate", SqlDbType.DateTime)]
        public DateTime MainPartyRecordDate { get; set; }

    }


}
