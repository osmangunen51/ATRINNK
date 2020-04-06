using MakinaTurkiye.Entities.Tables.Content;
using System.Collections.Generic;

namespace MakinaTurkiye.Services.Content
{
    public interface IBaseMenuService
    {
        void InsertBaseMenu(BaseMenu baseMenu);
        void DeleteBaseMenu(BaseMenu baseMenu);
        void UpdateBaseMenu(BaseMenu baseMenu);

        IList<BaseMenu> GetAllBaseMenu(bool showHidden = false);

        IList<BaseMenu> GetAllBaseMenus(int skip, int take);

        BaseMenu GetBaseMenuByBaseMenuId(int baseMenuId);


        //basemenu category
        void InsertBaseMenuCategory(BaseMenuCategory baseMenuCategory);

        void UpdateBaseMenuCategory(BaseMenuCategory baseMenuCategory);

        void DeleteBaseMenuCategory(BaseMenuCategory baseMenuCategory);

        BaseMenuCategory GetBaseMenuCategoryByBaseMenuCategoryId(int baseMenuCategoryId);

        IList<BaseMenuCategory> GetBaseMenuCategoriesByBaseMenuId(int baseMenuId);

        void InsertBaseMenuImage(BaseMenuImage baseMenuImage);

        void DeleteBaseMenuImage(BaseMenuImage baseMenuImage);

        void UpdateBaseMenuImage(BaseMenuImage baseMenuImage);

        BaseMenuImage GetBaseMenuImageByBaseMenuImageId(int baseMenuImageId);
    }
}
