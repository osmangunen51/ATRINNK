using System;

namespace NeoSistem.MakinaTurkiye.Management.Models
{
    public class StoreMemberDescriptionItem
    {
        public string UserName { get; set; }
        public string Title { get; set; }
        public int DescId { get; set; }
        public string Description { get; set; }
        public DateTime? RecordDate { get; set; }
    }
}