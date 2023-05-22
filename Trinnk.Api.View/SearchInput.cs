
namespace Trinnk.Api.View
{
    public class SearchInput
    {
        public string name { get; set; } = "";
        public string companyName { get; set; } = "";
        public string country { get; set; } = "";
        public string town { get; set; } = "";
        public decimal minPrice { get; set; } = 0;
        public decimal maxPrice { get; set; } = 0;
        public bool isnew { get; set; } = true;

        public bool isold { get; set; } = true;

        public bool sortByViews { get; set; } = true;

        public bool sortByDate { get; set; } = true;

        public int Page { get; set; } = 0;

        public int PageSize { get; set; } = 50;

        public SearchInput()
        {
        }
    }
}