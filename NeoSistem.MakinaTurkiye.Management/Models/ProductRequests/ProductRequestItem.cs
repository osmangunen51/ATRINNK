using System;

namespace NeoSistem.MakinaTurkiye.Management.Models.ProductRequests
{
    public class ProductRequestItem
    {
        public int ProductRequestId { get; set; }
        public string CategoryName { get; set; }
        public string BrandName { get; set; }
        public string ModelName { get; set; }
        public string SeriesName { get; set; }
        public string MemberNameSurname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Message { get; set; }
        public DateTime RecordDate { get; set; }
        public bool IsControllled { get; set; }
    }
}