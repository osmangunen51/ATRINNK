using System.Collections.Generic;
using System.Web.Mvc;

namespace NeoSistem.Trinnk.Management.Models.Orders
{
    public class SalesResponsibleUpdateModel
    {

        public SalesResponsibleUpdateModel()
        {
            this.SalesResponsibleUser = new List<SelectListItem>();
        }
        public int OrderId;
        public List<SelectListItem> SalesResponsibleUser;
        public int SalesUserId;
    }
}