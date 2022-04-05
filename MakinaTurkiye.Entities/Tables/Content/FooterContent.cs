namespace MakinaTurkiye.Entities.Tables.Content
{
    public class FooterContent : BaseEntity
    {

        public int FooterContentId { get; set; }
        public int FooterParentId { get; set; }
        public string FooterContentName { get; set; }
        public string FooterContentUrl { get; set; }
        public int DisplayOrder { get; set; }

        public virtual FooterParent FooterParent { get; set; }

    }
}
