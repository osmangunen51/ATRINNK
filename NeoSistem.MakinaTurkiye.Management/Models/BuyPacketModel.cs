namespace NeoSistem.MakinaTurkiye.Management.Models
{
    public class BuyPacketModel
    {
        public int OrderType { get; set; }
        public int Installment { get; set; }
        public string Description { get; set; }
        public string Dates { get; set; }
        public int MainPartyId { get; set; }
        public string PayDate { get; set; }
        public string TaxOffice { get; set; }
        public string TaxNo { get; set; }
        public int? PacketPrice { get; set; }
        public string DiscountType { get; set; }
        public string DiscountAmount { get; set; }

        public int PacketDay { get; set; }
    }
}