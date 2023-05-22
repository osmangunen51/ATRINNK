namespace NeoSistem.Trinnk.Classes
{
    using EnterpriseEntity.Business;
    using EnterpriseEntity.Business.Attributes;
    using System.Data;

    [Table("Packet")]
    public partial class Packet : EntityObject
    {
        [Column("PacketId", SqlDbType.Int, PrimaryKey = true, Identity = true)]
        public int PacketId { get; set; }

        [Column("PacketName", SqlDbType.VarChar)]
        public string PacketName { get; set; }

        [Column("PacketDescription", SqlDbType.VarChar)]
        public string PacketDescription { get; set; }

        [Column("PacketPrice", SqlDbType.Money)]
        public decimal PacketPrice { get; set; }

        [Column("PacketDay", SqlDbType.Int)]
        public int PacketDay { get; set; }

        [Column("PacketType", SqlDbType.TinyInt)]
        public byte PacketType { get; set; }

    }


}
