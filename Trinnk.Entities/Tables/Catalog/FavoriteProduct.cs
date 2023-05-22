using System;

namespace Trinnk.Entities.Tables.Catalog
{
    public class FavoriteProduct : BaseEntity
    {
        public int FavoriteProductId { get; set; }

        public int? MainPartyId { get; set; }

        public int? ProductId { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}
