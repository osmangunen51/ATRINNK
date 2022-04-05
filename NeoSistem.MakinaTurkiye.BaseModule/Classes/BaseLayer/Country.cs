namespace NeoSistem.MakinaTurkiye.Classes
{
    using EnterpriseEntity.Business;
    using EnterpriseEntity.Business.Attributes;
    using System.Data;

    [Table("Country")]
    public partial class Country : EntityObject
    {
        [Column("CountryId", SqlDbType.Int, PrimaryKey = true)]
        public int CountryId { get; set; }

        [Column("CountryName", SqlDbType.NVarChar)]
        public string CountryName { get; set; }

        [Column("Active", SqlDbType.Bit)]
        public bool Active { get; set; }

        [Column("CultureCode", SqlDbType.VarChar)]
        public string CultureCode { get; set; }

    }


}
