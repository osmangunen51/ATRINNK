using System;

namespace NeoSistem.Trinnk.Management.Models
{
    public class HelpModel
    {
        public int ID { get; set; }
        public int ConstantId { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public DateTime RecordDate { get; set; }
        public NeoSistem.Trinnk.Management.Models.Entities.Constant Constant { get; set; } = new NeoSistem.Trinnk.Management.Models.Entities.Constant();
    }
}