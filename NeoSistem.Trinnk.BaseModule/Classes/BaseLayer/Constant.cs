namespace NeoSistem.Trinnk.Classes
{
    using EnterpriseEntity.Business;
    using EnterpriseEntity.Business.Attributes;
    using System.Data;

    [Table("Constant")]
    public partial class Constant : EntityObject
    {
        [Column("ConstantId", SqlDbType.SmallInt, PrimaryKey = true, Identity = true)]
        public short ConstantId { get; set; }

        [Column("ConstantType", SqlDbType.TinyInt)]
        public byte ConstantType { get; set; }

        [Column("ConstantName", SqlDbType.VarChar)]
        public string ConstantName { get; set; }

        [Column("ConstantPropertie", SqlDbType.NVarChar)]
        public string ConstantPropertie { get; set; }

        [Column("Order", SqlDbType.Int)]
        public int Order { get; set; }

        [Column("MemberDescriptionIsOpened", SqlDbType.Bit)]
        public bool MemberDescriptionIsOpened { get; set; }
    }
}
