using System;

namespace MakinaTurkiye.Entities.Tables.Seos
{
    public class SeoDefinition : BaseEntity
    {
        public int SeoDefinitionId { get; set; }
        public int EntityId { get; set; }
        public int EntityTypeId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string SeoContent { get; set; }
        public string ContentSide { get; set; }
        public string ContentBottomCenter { get; set; }
        public bool Enabled { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
