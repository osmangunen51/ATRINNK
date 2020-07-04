using MakinaTurkiye.Entities.Tables.Users;
using System.Collections.Generic;

namespace MakinaTurkiye.Services.Users
{
    public interface IHelpService
    {
        Help GetHelpByHelpId(int helpId);
        IList<Help> GetAllHelp();
        void InsertHelp(Help help);
        void DeleteHelp(Help help);
        void UpdateHelp(Help help);
        IList<Help>  GetHelpByForPaging(int takeValue, int skipValue);
        IList<Help> HelpSearchBySearchText(string searchText);

        WebSiteError GetWebSiteErrorByWebSiteErrorId(int websiteErrorId);
        void InsertWebSitError(WebSiteError webSiteError);
        void DeleteWebSitError(WebSiteError webSiteError);
        void UpdateWebSitError(WebSiteError webSiteError);
        List<WebSiteError> GetWebSiteErrors();


    }
}
