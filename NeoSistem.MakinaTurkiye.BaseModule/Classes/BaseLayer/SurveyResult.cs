namespace NeoSistem.MakinaTurkiye.Classes
{
    using EnterpriseEntity.Business;
    using EnterpriseEntity.Business.Attributes;
    using System.Data;

    [Table("SurveyResult")]
    public partial class SurveyResult : EntityObject
    {
        [Column("ResultId", SqlDbType.Int, PrimaryKey = true, Identity = true)]
        public int ResultId { get; set; }

        [Column("SurveyId", SqlDbType.Int)]
        public int SurveyId { get; set; }

        [Column("OptionId", SqlDbType.Int)]
        public int OptionId { get; set; }

        [Column("MainPartyId", SqlDbType.Int)]
        public int MainPartyId { get; set; }

    }


}
