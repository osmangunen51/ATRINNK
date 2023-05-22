using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trinnk.Entities.Tables.Users
{
    public class Help : BaseEntity
    {
        public int HelpId { get; set; }
        [MaxLength(250)]
        public string Subject { get; set; }
        [Column(TypeName = "text")]
        public string Content { get; set; }
        public DateTime RecordDate { get; set; }
        public int ConstantId { get; set; }
    }
}
