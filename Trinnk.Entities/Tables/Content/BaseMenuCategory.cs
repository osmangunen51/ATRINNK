using Trinnk.Entities.Tables.Catalog;
using System;

namespace Trinnk.Entities.Tables.Content
{
    public class BaseMenuCategory : BaseEntity
    {
        //private ICollection<Category> _categories;

        public int BaseMenuCategoryId { get; set; }
        public int BaseMenuId { get; set; }
        public int CategoryId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual BaseMenu BaseMenu { get; set; }

        public virtual Category Category { get; set; }




    }
}
