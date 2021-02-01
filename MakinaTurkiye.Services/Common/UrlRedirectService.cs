using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MakinaTurkiye.Core.Data;
using MakinaTurkiye.Entities.Tables.Common;

namespace MakinaTurkiye.Services.Common
{
    public class UrlRedirectService : IUrlRedirectService
    {
        IRepository<UrlRedirect> _urlRedirectRepository;

        public UrlRedirectService(IRepository<UrlRedirect> urlRedirectRepository)
        {
            this._urlRedirectRepository = urlRedirectRepository;
        }

        public void DeleteUrlRedirect(UrlRedirect urlRedirect)
        {
            if (urlRedirect == null)
                throw new ArgumentNullException("urlRedirect");

            _urlRedirectRepository.Delete(urlRedirect);
        }

        public UrlRedirect GetUrlRedirectByOldUrl(string oldUrl)
        {
            var query = _urlRedirectRepository.Table;
            return query.FirstOrDefault(x => x.OldUrl == oldUrl);
        }

        public UrlRedirect GetUrlRedirectByUrlRedirectId(int urlRedirectId)
        {
            if (urlRedirectId == 0)
                throw new ArgumentNullException("urlRedirectId");
            var query = _urlRedirectRepository.Table;
            return query.FirstOrDefault(x => x.UrlRedirectId == urlRedirectId);
        }

        public IList<UrlRedirect> GetUrlRedirects(int skip, int take, out int totalCount)
        {
            var query = _urlRedirectRepository.Table;
            totalCount = query.Count();
            query = query.OrderByDescending(x => x.UrlRedirectId).Skip(skip).Take(take);
            return query.ToList();
        }

        public void InsertUrlRedirect(UrlRedirect urlRedirect)
        {
            if (urlRedirect == null)
                throw new ArgumentNullException("urlRedirect");

            _urlRedirectRepository.Insert(urlRedirect);
        }

        public void UpdateUrlRedirect(UrlRedirect urlRedirect)
        {
            if (urlRedirect == null)
                throw new ArgumentNullException("urlRedirect");

            _urlRedirectRepository.Update(urlRedirect);
        }

        public IList<UrlRedirect> GetUrlRedirectAll()
        {
            var query = _urlRedirectRepository.Table;
            return query.ToList();
        }
    }
}
