using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakinaTurkiye.Api.View
{
    public class StoreDealerShip
    {
        public int DealerBrandId { get; set; } = 0;
        public int MainPartyId { get; set; } = 0;
        public string Name { get; set; }
        public string Logo { get; set; }
    }
}
