using System.Collections.Generic;

namespace NeoSistem.MakinaTurkiye.Management.Models.ViewModel
{
    public class MailSenderViewModel
    {
        public MailSenderViewModel()
        {
            this.MemberTypes = new List<MemberTypeModel>();
        }
        public virtual List<MemberTypeModel> MemberTypes { get; set; }
        public virtual List<global::MakinaTurkiye.Entities.Tables.Packets.Packet> Packets { get; set; }
               
    }
}