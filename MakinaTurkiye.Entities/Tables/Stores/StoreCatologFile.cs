namespace MakinaTurkiye.Entities.Tables.Stores
{
    public class StoreCatologFile : BaseEntity
    {
        public int StoreCatologFileId { get; set; }
        public int StoreMainPartyId { get; set; }
        public string Name { get; set; }
        public string FileName { get; set; }
        public int FileOrder { get; set; }

    }
}
