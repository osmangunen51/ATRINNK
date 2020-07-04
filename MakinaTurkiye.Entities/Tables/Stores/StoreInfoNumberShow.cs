namespace MakinaTurkiye.Entities.Tables.Stores
{
    public class StoreInfoNumberShow:BaseEntity
    {
   
        public int StoreInfoNumberShowId { get; set; }

        public int StoreMainpartyId { get; set; }

        public bool TaxNumberShow { get; set; }

        public bool TaxOfficeShow { get; set; }

        public bool TradeRegistryNoShow { get; set; }

        public bool MersisNoShow { get; set; }

    
        
    }
}
