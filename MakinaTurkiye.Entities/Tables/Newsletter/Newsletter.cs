using MakinaTurkiye.Entities.Tables.Content;
using MakinaTurkiye.Entities.Tables.Stores;
using System;
using System.Collections.Generic;

namespace MakinaTurkiye.Entities.Tables.Newsletter
{
    public partial class Newsletter : BaseEntity
    {
        public int NewsletterId { get; set; }
        public string NewsletterEmail { get; set; }
        public DateTime NewsletterDate { get; set; }
        public bool Active { get; set; }
    }

}
