using MakinaTurkiye.Entities.Tables.Common;
using System.Collections.Generic;

namespace MakinaTurkiye.Services.Common
{
    public interface IUrlRedirectService
    {
        void InsertUrlRedirect(UrlRedirect urlRedirect);
        void UpdateUrlRedirect(UrlRedirect urlRedirect);
        void DeleteUrlRedirect(UrlRedirect urlRedirect);

        IList<UrlRedirect> GetUrlRedirects(int skip, int take, out int totalCount);
        UrlRedirect GetUrlRedirectByOldUrl(string oldUrl);
        UrlRedirect GetUrlRedirectByUrlRedirectId(int urlRedirectId);
        IList<UrlRedirect> GetUrlRedirectAll();

    }
}
