using System.Collections.Generic;
using System.Web.Mvc;

namespace NeoSistem.Trinnk.Management.Models.Stores
{
    public class StoreSectorModel
    {
        public StoreSectorModel()
        {
            this.SectorCategories = new List<SelectListItem>();
        }
        public int MainPartyId { get; set; }
        public List<SelectListItem> SectorCategories { get; set; }

    }
}