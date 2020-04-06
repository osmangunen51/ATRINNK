using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
