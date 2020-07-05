using System;

namespace MakinaTurkiye.Entities.Tables.SearchEngine
{
    public class IncrementalSearchEngineProduct : BaseEntity
    {
        public int SearchEngineTypeId { get; set; }
        public SearchEngineType SearchEngineType
        {
            get { return (SearchEngineType)SearchEngineTypeId; }
            set { SearchEngineTypeId = (int)value; }
        }
        public int LanguageId { get; set; }
        public int MainPartyId { get; set; }
        public int ProductId { get; set; }
        public int IsDeleted { get; set; }
        public bool Status { get; set; }
        public DateTime InTime { get; set; }
    }
}
