using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakinaTurkiye.Entities.Tables.Stores
{
    public class StorePackagePurchaseRequest:BaseEntity
    {
        public int MainPartyId      { get; set; }
        public string FirstName     { get; set; }
        public string LastName      { get; set; }
        public string Phone         { get; set; }
        public int ProductQuantity { get; set; }
        public string Desciption { get; set; }
        public DateTime Date        { get; set; }
        [NotMapped]
        public string StoreName { get; set; } = "";
        [NotMapped]
        public int? AuthorizedId { get; set; } =0;
        [NotMapped]
        public int? PortfoyUserId { get; set; } =0;
    }
}
