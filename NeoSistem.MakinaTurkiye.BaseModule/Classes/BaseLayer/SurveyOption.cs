namespace NeoSistem.MakinaTurkiye.Classes
{
    using EnterpriseEntity.Business;
    using EnterpriseEntity.Business.Attributes;
    using System.Data;

    [Table("SurveyOption")]
    public partial class SurveyOption : EntityObject
    {
        [Column("OptionId", SqlDbType.Int, PrimaryKey = true, Identity = true)]
        public int OptionId { get; set; }

        [Column("SurveyId", SqlDbType.Int)]
        public int SurveyId { get; set; }

        [Column("OptionContent", SqlDbType.VarChar)]
        public string OptionContent { get; set; }

    }


}
