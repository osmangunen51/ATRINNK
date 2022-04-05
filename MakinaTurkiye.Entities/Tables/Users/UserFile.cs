using System;

namespace MakinaTurkiye.Entities.Tables.Users
{
    public class UserFile : BaseEntity
    {
        public int UserFileId { get; set; }
        public byte UserId { get; set; }
        public byte FileType { get; set; }
        public string FileName { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
