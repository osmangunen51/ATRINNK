namespace NeoSistem.Trinnk.Management.Models
{
    public class StoreDealerModel
    {
        public int StoreDealerId { get; set; }

        public int MainPartyId { get; set; }

        public string DealerName { get; set; }

        public byte DealerType { get; set; }
    }
}