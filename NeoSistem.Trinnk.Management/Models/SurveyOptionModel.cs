namespace NeoSistem.Trinnk.Management.Models
{
    using System.ComponentModel;
    using Validation;

    public class SurveyOptionModel
    {
        public int OptionId { get; set; }

        public int SurveyId { get; set; }

        [RequiredValidation, StringLengthValidation(500)]
        [DisplayName("Seçenek")]
        public string OptionContent { get; set; }
    }
}
