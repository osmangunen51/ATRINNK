namespace NeoSistem.Trinnk.Web.Models
{
    using System.Collections.Generic;

    public class StoreViewModel
    {

        public IList<AddressModel> AddressItems { get; set; }

        public IList<PictureModel> PictureList { get; set; }

        public ICollection<CategoryModel> CategoryItems { get; set; }

        public ICollection<StoreModel> StoreItems { get; set; }

    }
}