namespace NeoSistem.MakinaTurkiye.Management.Models
{
    using EnterpriseEntity.Extensions.Data;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using Validation;

    public class SurveyModel
    {
        public int SurveyId { get; set; }

        [RequiredValidation, StringLengthValidation(500)]
        [DisplayName("Soru")]
        public string SurveyQuestion { get; set; }

        [DisplayName("Aktif")]
        public bool Active { get; set; }

        public DateTime RecordDate { get; set; }

        public int RecordCreatorId { get; set; }

        public DateTime LastUpdateDate { get; set; }

        public int LastUpdaterId { get; set; }

        public IEnumerable<SurveyOptionModel> SurveyOptions { get; set; }

        public ICollection<SurveyOptionModel> GetOptions(int surveyId)
        {
            var dataSurveyOption = new Data.SurveyOption();
            var items = dataSurveyOption.GetItemsBySurveyId(surveyId);
            return items.AsCollection<SurveyOptionModel>();
        }
    }
}
