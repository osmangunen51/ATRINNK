namespace Trinnk.Entities.Tables.Seos
{
    public class Seo : BaseEntity
    {
        public int SeoId { get; set; }
        public string PageName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Abstract { get; set; }
        public string Keywords { get; set; }
        public string Classification { get; set; }
        public string Robots { get; set; }
        public string RevisitAfter { get; set; }


    }
}
