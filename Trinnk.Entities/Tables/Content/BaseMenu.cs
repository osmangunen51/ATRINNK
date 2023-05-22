using System;
using System.Collections.Generic;

namespace Trinnk.Entities.Tables.Content
{
    public class BaseMenu : BaseEntity
    {
        private ICollection<BaseMenuCategory> _baseMenuCategories;
        private ICollection<BaseMenuImage> _baseMenuImages;

        public int BaseMenuId { get; set; }
        public string BaseMenuName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public byte Order { get; set; }
        public byte? HomePageOrder { get; set; }
        public string BackgroundCss { get; set; }
        public string TabBackgroundCss { get; set; }
        public bool Active { get; set; }

        public virtual ICollection<BaseMenuCategory> BaseMenuCategories
        {
            get { return _baseMenuCategories ?? (_baseMenuCategories = new List<BaseMenuCategory>()); }
            protected set { _baseMenuCategories = value; }
        }
        public virtual ICollection<BaseMenuImage> BaseMenuImages
        {
            get { return _baseMenuImages ?? (_baseMenuImages = new List<BaseMenuImage>()); }
            protected set { _baseMenuImages = value; }
        }

    }
}
