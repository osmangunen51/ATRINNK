namespace MakinaTurkiye.Entities.Tables.Stores
{
    public class StoreDealer:BaseEntity
    {
        public int StoreDealerId{get;set;}
        public int MainPartyId { get; set; }
        public string DealerName {get;set;}
        public byte DealerType { get; set; }
        
      

    }
}
