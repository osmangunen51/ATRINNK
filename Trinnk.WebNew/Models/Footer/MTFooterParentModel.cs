using System.Collections.Generic;

namespace NeoSistem.Trinnk.Web.Models.Footer
{
    public class MTFooterParentModel
    {
        public MTFooterParentModel()
        {
            this.FooterContents = new List<MTFooterContentModel>();
        }
        public int FooterParentId { get; set; }
        public string FooterParentName { get; set; }
        public IList<MTFooterContentModel> FooterContents { get; set; }
    }
}