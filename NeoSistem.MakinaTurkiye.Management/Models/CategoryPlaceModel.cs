using System.Collections.Generic;

namespace NeoSistem.MakinaTurkiye.Management.Models
{
    public class CategoryPlaceModel
    {
        public CategoryPlaceModel()
        {
            this.Categories = new List<CategoryItemModelForPlace>();
        }
        public IList<CategoryItemModelForPlace> Categories { get; set; }

    }
    public class CategoryItemModelForPlace
    {
        public int CategoryPlaceChoiceId { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public byte Order { get; set; }
        public string CategoryType { get; set; }
    }

}