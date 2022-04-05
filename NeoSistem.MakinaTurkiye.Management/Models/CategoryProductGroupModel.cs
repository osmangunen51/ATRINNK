namespace NeoSistem.MakinaTurkiye.Management.Models
{
    using System.ComponentModel;
    using System.Web.Mvc;
    //using EnterpriseEntity.DataCache;

    [Bind(Exclude = "CategoryProductGroupId")]
    public class CategoryProductGroupModel
    {
        public const string CacheName = "ProductGroup";

        public byte CategoryProductGroupId { get; set; }

        [DisplayName("Ürün Grubu Adı")]
        public string CategoryProductGroupName { get; set; }


        [DisplayName("Ürün Grubu Adı(Çoğul)")]
        public string CategoryProductGroupPluralName { get; set; }

        public string GroupName { get; set; }

        [DisplayName("Aktif")]
        public bool Active { get; set; }

        public byte OrderNo { get; set; }

    }
}