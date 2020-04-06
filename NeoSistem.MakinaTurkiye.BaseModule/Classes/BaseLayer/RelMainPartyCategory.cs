namespace NeoSistem.MakinaTurkiye.Classes
{
    using EnterpriseEntity.Business;
    using EnterpriseEntity.Business.Attributes;
    using System.Data;

    [Table("RelMainPartyCategory")]
  public partial class RelMainPartyCategory : EntityObject
  {
    [Column("MainPartyId", SqlDbType.Int)]
    public int MainPartyId { get; set; }

    [Column("CategoryId", SqlDbType.Int)]
    public int CategoryId { get; set; }
  }

}
