using MakinaTurkiye.Utilities.Seo;
using NeoSistem.MakinaTurkiye.Management.Models.Validation;
using System.ComponentModel;

namespace NeoSistem.MakinaTurkiye.Management.Models
{
    public class SeoDefinitionModel
    {
        public int SeoDefinitionId { get; set; }
        public int? EntityId { get; set; }
        public int? EntityTypeId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        [DisplayName("İçerik")]
        [RequiredValidation]
        public string SeoContent { get; set; }

        [DisplayName("İçerik Sol")]
        public string ContentSide { get; set; }

         [DisplayName("İçerik Alt Orta")]
        public string ContentBottomCenter { get; set; }
        public bool Enabled { get; set; }

        [DisplayName("Kategori Adı")]
        public string CategoryName { get; set; }

        public KeywordAnalysis KeywordAnalysis = new KeywordAnalysis();
    }
}