namespace NeoSistem.MakinaTurkiye.Classes
{
    using EnterpriseEntity.Business;
    using EnterpriseEntity.Business.Attributes;
    using System.Data;

    [Table("Town")]
    public partial class Town : EntityObject
    {
        [Column("TownId", SqlDbType.Int, PrimaryKey = true, Identity = true)]
        public int TownId { get; set; }

        [Column("CityId", SqlDbType.Int)]
        public int CityId { get; set; }

        [Column("LocalityId", SqlDbType.Int)]
        public int LocalityId { get; set; }

        [Column("DistrictId", SqlDbType.Int)]
        public int DistrictId { get; set; }

        [Column("TownName_Big", SqlDbType.NVarChar)]
        public string TownName_Big { get; set; }

        [Column("TownName", SqlDbType.NVarChar)]
        public string TownName { get; set; }

        [Column("TownName_Small", SqlDbType.NVarChar)]
        public string TownName_Small { get; set; }

    }


}
