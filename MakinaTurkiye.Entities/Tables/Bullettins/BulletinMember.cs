using System;
using System.Collections.Generic;

namespace MakinaTurkiye.Entities.Tables.Bullettins
{
    public class BulletinMember:BaseEntity
    {
        private ICollection<BulletinMemberCategory> _bulletinMemberCategories;
        public int BulletinMemberId { get; set; }
        public string MemberName { get; set; }
        public string MemberSurname { get; set; }
        public string MemberEmail { get; set; }
        public DateTime RecordDate { get; set; }
        public virtual ICollection<BulletinMemberCategory> BulletinMemberCategories
        {
            get { return _bulletinMemberCategories ?? (_bulletinMemberCategories = new List<BulletinMemberCategory>()); }
            protected set { _bulletinMemberCategories = value; }
        }


    }
}
