namespace NeoSistem.Trinnk.Web.Models
{
    public class DataFilterItemModel
    {
        public int FilterItemId { get; set; }
        public string FilterUrl { get; set; }
        public string FilterName { get; set; }
        public bool Selected { get; set; }
        public int Count { get; set; }
    }
}