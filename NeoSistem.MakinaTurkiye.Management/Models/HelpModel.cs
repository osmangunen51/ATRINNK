using NeoSistem.MakinaTurkiye.BaseModule.Entities;
using System;
using System.Linq;
using System.Web.Mvc;

namespace NeoSistem.MakinaTurkiye.Management.Models
{
    public class HelpModel
    {
        public int ID { get; set; }
        public int ConstantId { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public DateTime RecordDate { get; set; }
        public NeoSistem.MakinaTurkiye.Management.Models.Entities.Constant Constant { get; set; } = new NeoSistem.MakinaTurkiye.Management.Models.Entities.Constant();
    }
}