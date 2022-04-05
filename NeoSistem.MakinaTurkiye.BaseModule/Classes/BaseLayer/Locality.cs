namespace NeoSistem.MakinaTurkiye.Classes
{
    using EnterpriseEntity.Business;
    using EnterpriseEntity.Business.Attributes;
    using System.Data;

    [Table("Locality")]
    public partial class Locality : EntityObject
    {
        [Column("LocalityId", SqlDbType.Int, PrimaryKey = true)]
        public int LocalityId { get; set; }

        [Column("CountryId", SqlDbType.Int)]
        public int CountryId { get; set; }

        [Column("CityId", SqlDbType.Int)]
        public int CityId { get; set; }

        [Column("LocalityName_Big", SqlDbType.NVarChar)]
        public string LocalityName_Big { get; set; }

        [Column("LocalityName", SqlDbType.NVarChar)]
        public string LocalityName { get; set; }

        [Column("LocalithName_Small", SqlDbType.NVarChar)]
        public string LocalithName_Small { get; set; }

    }


}
