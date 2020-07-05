namespace MakinaTurkiye.Entities.Tables.Bullettins
{
    public class BulletinMemberCategory:BaseEntity
    {
        public int BulletinMemberCategoryId { get; set; }
        public int BulletinMemberId { get; set; }
        public int CategoryId { get; set; }

        public virtual BulletinMember BulletinMember { get; set; }
    }
}
