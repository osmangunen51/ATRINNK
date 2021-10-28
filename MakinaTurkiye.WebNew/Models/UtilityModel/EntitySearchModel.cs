namespace NeoSistem.MakinaTurkiye.Web.Models.ViewModels
{
    using System;
    using System.Collections.Generic;

    public class EntitySearchModel<TCollection>
    {
        public int CurrentPage { get; set; }

        public int PageDimension { get; set; }

        public int TotalRecord { get; internal set; }

        public IEnumerable<int> TotalPages
        {
            get
            {
                int totalPage = TotalPage();

                for (int i = 1; i <= totalPage; i++)
                {
                    yield return i;
                }
            }
        }

        public IEnumerable<int> TotalLinkPages
        {
            get
            {
                int totalPage = TotalPage();

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

        private int TotalPage()
        {
            return this.TotalRecord % this.PageDimension == 0 ?
                   this.TotalRecord / this.PageDimension :
                   Convert.ToInt32(this.TotalRecord / this.PageDimension) + 1;
        }

        public IList<TCollection> Source { get; set; }

    }
}