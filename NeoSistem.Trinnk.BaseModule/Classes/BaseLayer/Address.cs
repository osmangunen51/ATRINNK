namespace NeoSistem.Trinnk.Classes
{
    using EnterpriseEntity.Business;
    using EnterpriseEntity.Business.Attributes;
    using System.Data;

    [Table("Address")]
    public partial class Address : EntityObject
    {
        [Column("AddressId", SqlDbType.Int, PrimaryKey = true, Identity = true)]
        public int AddressId { get; set; }

        [Column("MainPartyId", SqlDbType.Int)]
        public int? MainPartyId { get; set; }

        [Column("CountryId", SqlDbType.Int)]
        public int CountryId { get; set; }

        [Column("CityId", SqlDbType.Int)]
        public int CityId { get; set; }

        [Column("LocalityId", SqlDbType.Int)]
        public int LocalityId { get; set; }

        [Column("TownId", SqlDbType.Int)]
        public int TownId { get; set; }

        [Column("AddressTypeId", SqlDbType.TinyInt)]
        public byte AddressTypeId { get; set; }

        [Column("Avenue", SqlDbType.VarChar)]
        public string Avenue { get; set; }

        [Column("ApartmentNo", SqlDbType.VarChar)]
        public string ApartmentNo { get; set; }

        [Column("DoorNo", SqlDbType.VarChar)]
        public string DoorNo { get; set; }

        [Column("Street", SqlDbType.VarChar)]
        public string Street { get; set; }

        [Column("AddressDefault", SqlDbType.Bit)]
        public bool AddressDefault { get; set; }

        [Column("StoreDealerId", SqlDbType.Int)]
        public int? StoreDealerId { get; set; }
    }


}
