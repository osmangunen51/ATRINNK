using System.Collections.Generic;

namespace NeoSistem.MakinaTurkiye.Management.Models
{
    public class WhatsappStoreModel
    {
        public WhatsappStoreModel()
        {
            this.StoreWhatsappListItems = new List<StoreWhatsappListItemModel>();
        }
        public List<StoreWhatsappListItemModel> StoreWhatsappListItems { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPage { get; set; }
        public List<int> TotalLinkPages
        {
            get
            {
                int totalPage = this.TotalPage;

                int firstPage = CurrentPage >= 9 ? CurrentPage - 5 : 1;
                int lastPage = firstPage + 8;

                if (lastPage >= totalPage)
                {
                    lastPage = totalPage;
                }
                List<int> links = new List<int>();
                for (int i = firstPage; i <= lastPage; i++)
                {
                    links.Add(i);
                }
                return links;

            }
        }
    }
}