#region Using Directives

using System.Collections.Generic;
using MakinaTurkiye.Entities.Tables.Catalog;
using NeoSistem.EnterpriseEntity.Extensions.Data;

#endregion

namespace NeoSistem.MakinaTurkiye.Web.Models.Account
{
    public class GetCategoriesModel
    {
        #region Using Directives
        public bool IsActive { get; set; }
        public IEnumerable<Category> ProductGroupList { get; set; }
        public int ParentCategoryID { get; set; }
        //public IEnumerable<RelMainPartyCategory> MemberRelatedCategory { get; set; }

        public ICollection<CategoryModel> CategoryList(int categoryID)
        { 
            var dataCategory = new Data.Category();
            return dataCategory.CategoryItemsByCategoryIdAndCategoryGroupType(categoryID, (byte)MainCategoryType.Ana_Kategori).AsCollection<CategoryModel>();
        }
        #endregion
        
    }
}