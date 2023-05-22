using System;

namespace Trinnk.Entities.Tables.Newsletter
{
    public partial class Newsletter : BaseEntity
    {
        public int NewsletterId { get; set; }
        public string NewsletterEmail { get; set; }
        public DateTime NewsletterDate { get; set; }
        public bool Active { get; set; }
    }

}
