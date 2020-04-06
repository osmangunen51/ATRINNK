using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Models.Helpers
{
    public class PagingModel<T>
    {
        private int count = 20;
        public int CurrentPage { get; set; }

        public int PageDimension { get { return count; } set { count = value; } }
        public int TotalRecord { get; internal set; }

        public IEnumerable<int> TotalPages
        {
            get
            {
                int totalPage = GetTotalPage();

                for (int i = 1; i <= totalPage; i++)
                {
                    yield return i;
                }
            }
        }

        public string Order { get; set; }
        public int TotalPage { get { return this.GetTotalPage(); } }
        public string OrderName { get; set; }

        public IEnumerable<int> TotalLinkPages
        {
            get
            {
                int totalPage = GetTotalPage();

                int firstPage = CurrentPage >= 5 ? CurrentPage - 4 : 1;
                int lastPage = firstPage + 8;

                if (lastPage >= totalPage)
                {
                    lastPage = totalPage;
                }

                for (int i = firstPage; i <= lastPage; i++)
                {
                    yield return i;
                }
            }
        }

        private int GetTotalPage()
        {
            return this.TotalRecord % this.PageDimension == 0 ?
                   this.TotalRecord / this.PageDimension :
                   Convert.ToInt32(this.TotalRecord / this.PageDimension) + 1;
        }

        public IEnumerable<T> Source { get; set; }
    }
}