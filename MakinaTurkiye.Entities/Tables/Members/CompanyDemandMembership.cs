namespace MakinaTurkiye.Entities.Tables.Members
{
    public class CompanyDemandMembership:BaseEntity
    {
        public int CompanyDemandMembershipId { get; set; }
        public string NameSurname { get; set; }
        public string Email { get; set; }
        public string WebUrl { get; set; }
        public string Phone { get; set; }
        public string CompanyName { get; set; }
        public string Statement { get; set; }
        public int Status { get; set; }
        public string DemandDate { get; set; }
        public bool isDemandForPacket { get; set; }
        
    }
}
