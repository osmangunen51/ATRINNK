using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakinaTurkiye.Entities.StoredProcedures.Catalog
{
    public class SiteMapCategoryPlaceResult
    {
        public string PlaceName { get; set; }
        public int PlaceId { get; set; }
        public int UpPlaceId { get; set; }
        public string UpPlaceName { get; set; }
        public int CategoryId { get; set; }
        public string UpCategoryName { get; set; }
        public string CategoryName { get; set; }
        public string CategoryContentTitle { get; set; }
        public int? CountryId { get; set; }
    }
}
