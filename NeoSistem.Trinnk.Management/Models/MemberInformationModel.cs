using System.Collections.Generic;

namespace NeoSistem.Trinnk.Management.Models
{
    public class MemberInformationModel
    {
        public MemberInformationModel()
        {
            this.MemberEmails = new List<string>();
        }
        public string MemberNo { get; set; }
        public string MemberName { get; set; }
        public string MemberSurname { get; set; }
        public List<string> MemberEmails { get; set; }

    }
}