namespace NeoSistem.Trinnk.Classes
{
    using EnterpriseEntity.Business;
    using EnterpriseEntity.Business.Attributes;
    using System.Data;

    [Table("AddressType")]
    public partial class AddressType : EntityObject
    {
        [Column("AddressTypeId", SqlDbType.TinyInt, PrimaryKey = true, Identity = true)]
        public byte AddressTypeId { get; set; }

        [Column("AddressTypeName", SqlDbType.VarChar)]
        public string AddressTypeName { get; set; }

    }


}
