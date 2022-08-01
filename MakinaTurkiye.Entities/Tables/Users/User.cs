using System;

namespace MakinaTurkiye.Entities.Tables.Users
{
    public class User : BaseEntity
    {
        public byte UserId { get; set; }
        public string UserName { get; set; }
    }
}
