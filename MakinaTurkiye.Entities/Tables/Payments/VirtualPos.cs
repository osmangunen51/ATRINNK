namespace MakinaTurkiye.Entities.Tables.Payments
{
    public class VirtualPos : BaseEntity
    {
        public byte VirtualPosId { get; set; }
        public string VirtualPostName { get; set; }
        public string VirtualPosStoreKey { get; set; }
        public string VirtualPosStoreType { get; set; }

        public string VirtualPosClientId { get; set; }

        public string VirtualPosApiUrl { get; set; }

        public string VirtualPosApiUserName { get; set; }
        public string VirtualPosApiPass { get; set; }

        public string VirtualPosPostUrl { get; set; }

        public bool VirtualPosActive { get; set; }
    }
}
