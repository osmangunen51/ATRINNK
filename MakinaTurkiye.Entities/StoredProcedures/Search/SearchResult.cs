namespace MakinaTurkiye.Entities.StoredProcedures.Search
{
    public class SearchResult
    {
        public string Name { get; set; } = "";
        public string Category { get; set; } = "";
        public double Score { get; set; } = 0;
        public string Url { get; set; } = "";
    }

    public class SearchResultCategory
    {
        public string Name { get; set; } = "";
        public string Path { get; set; } = "";
        public string Url { get; set; } = "";
        public double Score { get; set; }
        public string Category { get; set; } = "";
    }
}
