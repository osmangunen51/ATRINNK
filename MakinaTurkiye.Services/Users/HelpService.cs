using MakinaTurkiye.Core.Data;
using MakinaTurkiye.Entities.Tables.Users;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MakinaTurkiye.Services.Users
{
    public class HelpService:IHelpService
    {
        private IRepository<Help> _helpRepository;
        private IRepository<WebSiteError> _webSiteErrors;

       public HelpService(IRepository<Help> helpRepository, IRepository<WebSiteError> webSiteErrors)
        {
            this._helpRepository = helpRepository;
            this._webSiteErrors = webSiteErrors;
        }

        public void DeleteHelp(Help help)
        {
            if (help == null)
                throw new ArgumentNullException("help");

            _helpRepository.Delete(help);
        }

        public void DeleteWebSitError(WebSiteError webSiteError)
        {
            if (webSiteError == null)
                throw new ArgumentNullException("webSiteError");

            _webSiteErrors.Delete(webSiteError);
        }

        public IList<Help> GetAllHelp()
        {
            var query = _helpRepository.Table;
            return query.ToList();
        }
        public IList<Help> GetHelpByForPaging(int takeValue,int skipValue )
        {
            var query = _helpRepository.Table;
            return query.OrderBy(x=>x.HelpId).Skip(skipValue).Take(takeValue).ToList();
        }

        public Help GetHelpByHelpId(int helpId)
        {
            if (helpId == 0)
                throw new ArgumentNullException("helpId");

            var query = _helpRepository.Table;
            return query.FirstOrDefault(h => h.HelpId == helpId);
        }

        public WebSiteError GetWebSiteErrorByWebSiteErrorId(int websiteErrorId)
        {
            if (websiteErrorId == 0)
                throw new ArgumentNullException("webSiteError");
            var query = _webSiteErrors.Table;
            return query.FirstOrDefault(x => x.WebSiteErrorId == websiteErrorId);
        }

        public List<WebSiteError> GetWebSiteErrors()
        {
            var query = _webSiteErrors.Table;
            return query.ToList();
        }

        public IList<Help> HelpSearchBySearchText(string searchText)
        {
            var query = from m in _helpRepository.Table
                        where m.Subject.Contains(searchText) || m.Content.Contains(searchText)
                        select m;

            return query.ToList();
        }

        public void InsertHelp(Help help)
        {
            if (help == null)
                throw new ArgumentNullException("help");
            _helpRepository.Insert(help);
        }

        public void InsertWebSitError(WebSiteError webSiteError)
        {
            if (webSiteError == null)
                throw new ArgumentNullException("webSiteError");
            _webSiteErrors.Insert(webSiteError);
        }

        public void UpdateHelp(Help help)
        {
            if (help == null)
                throw new ArgumentNullException("help");
            _helpRepository.Update(help);
        }

        public void UpdateWebSitError(WebSiteError webSiteError)
        {
            if (webSiteError == null)
                throw new ArgumentNullException("webSiteError");

            _webSiteErrors.Update(webSiteError);
        }
    }
}
