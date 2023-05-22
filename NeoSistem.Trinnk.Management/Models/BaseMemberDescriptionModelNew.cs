using System.Collections.Generic;
using System.Web.Mvc;

namespace NeoSistem.Trinnk.Management.Models
{
    public class BaseMemberDescriptionModelNew
    {
        public BaseMemberDescriptionModelNew()
        {
            this.BaseMemberDescriptionModelItems = new List<BaseMemberDescriptionModelItem>();
            this.Users = new List<SelectListItem>();
        }
        public List<BaseMemberDescriptionModelItem> BaseMemberDescriptionModelItems { get; set; }
        public List<SelectListItem> Users { get; set; }

        public int CurrentPage { get; set; }
        public int TotalPage { get; set; }
        public string Type { get; set; }
        public string Order { get; set; }
        public int TotalCount { get; set; }
        public int AutherizedId { get; set; }
        public List<int> TotalLinkPages
        {
            get
            {
                int totalPage = this.TotalPage;

                int firstPage = CurrentPage >= 5 ? CurrentPage - 4 : 1;
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