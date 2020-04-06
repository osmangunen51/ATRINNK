using System;

namespace NeoSistem.MakinaTurkiye.Management.Models.Catolog
{
    public class HomeSectorProductItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string SectorName { get; set; }
        public string ProductName { get; set; }
        public string ProductNo { get; set; }
        public string StoreName { get; set; }
        
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public byte Type { get; set; }
        public bool Active { get; set; }
        public int? ProductHomeOrder { get; set; }
        
    }
}