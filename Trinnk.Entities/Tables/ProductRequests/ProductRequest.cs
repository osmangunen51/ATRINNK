using System;

namespace Trinnk.Entities.Tables.ProductRequests
{
    public class ProductRequest : BaseEntity
    {
        public int ProductRequestId { get; set; }
        public int MemberMainPartyId { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
        public int ModelId { get; set; }
        public int SeriesId { get; set; }
        public string NameSurname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Message { get; set; }
        public bool IsControlled { get; set; }
        public DateTime RecordDate { get; set; }
    }
}
