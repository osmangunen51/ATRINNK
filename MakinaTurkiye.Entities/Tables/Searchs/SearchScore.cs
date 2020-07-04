using System;

namespace MakinaTurkiye.Entities.Tables.Searchs
{
    public class SearchScore:BaseEntity
    {
        public int Id { get; set; }
        public string Keyword { get; set; }
        public int Score { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
