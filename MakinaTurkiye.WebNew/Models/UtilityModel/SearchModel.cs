namespace NeoSistem.MakinaTurkiye.Web.Models.ViewModels
{
    using MakinaTurkiye.Web.Models.Validation;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    public class SearchModel<TCollection>
    {
        [RequiredValidation, StringLengthValidation(100)]
        public string SearchText { get; set; }

        public int CurrentPage { get; set; }

        public int PageDimension { get; set; }

        public int TotalRecord { get; internal set; }

        [DefaultValue(false)]
        public bool Desc { get; set; }

        [DefaultValue(0)]
        public int OrderField { get; set; }

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

                int firstPage = CurrentPage >= 9 ? CurrentPage - 7 : 1;
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

        public int TotalPage()
        {
            return this.TotalRecord % this.PageDimension == 0 ?
                   this.TotalRecord / this.PageDimension :
                   Convert.ToInt32(this.TotalRecord / this.PageDimension) + 1;
        }

        public ICollection<TCollection> Source { get; set; }

    }
}