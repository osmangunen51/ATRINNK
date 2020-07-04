namespace NeoSistem.MakinaTurkiye.Management.Models
{
    public class DemandsForPacketModel
    {
        public int PacketForDemandModelId { get; set; }
        public string NameSurname { get; set; }
        public string Email { get; set; }
        public string WebUrl { get; set; }
        public int StoreMainPartyId { get; set; }
        public string Phone { get; set; }
        public string StoreName { get; set; }
        public int Status { get; set; }
        public int MemberMainPartyId { get; set; }
        public string DemandDate { get; set; }
    }
}