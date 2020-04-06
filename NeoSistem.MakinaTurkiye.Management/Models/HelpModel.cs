using System;

namespace NeoSistem.MakinaTurkiye.Management.Models
{
    public class HelpModel
    {
        public int ID { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public DateTime RecordDate { get; set; }
    }
}