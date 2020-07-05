using MakinaTurkiye.Entities.StoredProcedures.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakinaTurkiye.Services.Stores
{
    public class CategoryStoresResult
    {
        public CategoryStoresResult()
        {
            this.Stores = new List<WebSearchStoreResult>();
            this.FilterableCityIds = new List<int>();
            this.FilterableLocalityIds = new List<int>();
            this.FilterableActivityIds = new List<int>();
        }

        public IList<WebSearchStoreResult> Stores { get; set; }

        public IList<int> FilterableCityIds { get; set; }
        public IList<int> FilterableLocalityIds { get; set; }
        public IList<int> FilterableActivityIds { get; set; }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
    }
}
