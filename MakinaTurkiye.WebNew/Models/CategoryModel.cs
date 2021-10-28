namespace NeoSistem.MakinaTurkiye.Web.Models
{
    using EnterpriseEntity.Extensions.Data;
    using NeoSistem.MakinaTurkiye.BaseModule.Entities;
    using System.Collections.Generic;
    using System.Linq;
    public class CategoryModel
    {
        public byte OrderNo { get; set; }

        public int CategoryId { get; set; }

        public int PropertyId { get; set; }

        public short CategoryGroupType { get; set; }

        public int? CategoryParentId { get; set; }

        public string CategoryTreeName { get; set; }

        public int CategoryOrder { get; set; }

        public string CategoryName { get; set; }

        public byte CategoryType { get; set; }

        public byte CategoryRoute { get; set; }

        public bool Active { get; set; }

        public int IsParent { get; set; }

        public int ProductCount { get; set; }

        public IList<Category> CategoryChildren(int Id)
        {
            IList<Category> categoryItems = null;
            using (var entities = new MakinaTurkiyeEntities())
            {
                categoryItems = entities.Categories.Where(c => c.CategoryParentId == Id).ToList();
            }

            return categoryItems;
        }

    }
}