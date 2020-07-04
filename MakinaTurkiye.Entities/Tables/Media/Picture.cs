namespace MakinaTurkiye.Entities.Tables.Media
{
    public class Picture:BaseEntity
    {
        public int PictureId { get; set; }
        public int? ProductId { get; set; }
        public int ? MainPartyId { get; set; }
        public int? StoreCertificateId { get; set; }
        public string PictureName { get; set; }
        public string PicturePath { get; set; }
        public string OldPath { get; set; }
        public int? StoreDealerId { get; set; }
        public int? PictureOrder { get; set; }
        public byte? StoreImageType { get; set; }
    }
}
