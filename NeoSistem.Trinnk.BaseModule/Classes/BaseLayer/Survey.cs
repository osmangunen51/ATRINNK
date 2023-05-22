namespace NeoSistem.Trinnk.Classes
{
    using EnterpriseEntity.Business;
    using EnterpriseEntity.Business.Attributes;
    using System;
    using System.Data;

    [Table("Survey")]
    public partial class Survey : EntityObject
    {
        [Column("SurveyId", SqlDbType.Int, PrimaryKey = true, Identity = true)]
        public int SurveyId { get; set; }

        [Column("SurveyQuestion", SqlDbType.VarChar)]
        public string SurveyQuestion { get; set; }

        [Column("Active", SqlDbType.Bit)]
        public bool Active { get; set; }

        [Column("RecordDate", SqlDbType.DateTime)]
        public DateTime RecordDate { get; set; }

        [Column("RecordCreatorId", SqlDbType.Int)]
        public int RecordCreatorId { get; set; }

        [Column("LastUpdateDate", SqlDbType.DateTime)]
        public DateTime LastUpdateDate { get; set; }

        [Column("LastUpdaterId", SqlDbType.Int)]
        public int LastUpdaterId { get; set; }

    }


}
