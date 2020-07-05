using System;
using System.Collections.Generic;

namespace NeoSistem.MakinaTurkiye.Management.Models
{
    public class BulletinMemberModel
    {
        public BulletinMemberModel()
        {
            this.Categories = new List<CategoryModel>();
    
        }
        public int BulletinMemberId { get; set; }
        public List<CategoryModel> Categories { get; set; }
        public string MemberName { get; set; }
        public string MemberSurname { get; set; }
        public string Email { get; set; }
        public DateTime RecordDate { get; set; }
       
    }
}