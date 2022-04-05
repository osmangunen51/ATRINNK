using MakinaTurkiye.Caching;
using MakinaTurkiye.Core.Data;
using MakinaTurkiye.Data;
using MakinaTurkiye.Entities.Tables.Newsletter;
using System;
using System.Linq;

namespace MakinaTurkiye.Services.Newsletters
{
    public class NewsletterService : BaseService, INewsletterService
    {
        #region Constants

        private const string NewsletterS_BY_MAINPARTY_ID_KEY = "makinaturkiye.Newsletter.bymainpartyId-{0}";

        #endregion

        #region Fields

        private readonly IRepository<Newsletter> _NewsletterRepository;
        private readonly ICacheManager _cacheManager;
        private readonly IDataProvider _dataProvider;
        private readonly IDbContext _dbContext;

        #endregion

        #region Ctor

        public NewsletterService(IRepository<Newsletter> NewsletterRepository,
            ICacheManager cacheManager, IDataProvider dataProvider, IDbContext dbContext) : base(cacheManager)
        {
            this._NewsletterRepository = NewsletterRepository;
            this._cacheManager = cacheManager;
            this._dataProvider = dataProvider;
            this._dbContext = dbContext;
        }

        #endregion

        #region Methods
        public void UpdateNewsletter(Newsletter Newsletter)
        {
            if (Newsletter == null)
                throw new ArgumentNullException("Newsletter");
            _NewsletterRepository.Update(Newsletter);
            string key = string.Format(NewsletterS_BY_MAINPARTY_ID_KEY, Newsletter.NewsletterEmail);
            _cacheManager.Remove(key);
        }

        public Newsletter GetNewsletterByNewsletterEmail(string NewsletterEmail)
        {

            if (string.IsNullOrEmpty(NewsletterEmail))
                throw new ArgumentNullException("NewsletterEmail");

            var query = _NewsletterRepository.Table;
            return query.FirstOrDefault(x => x.NewsletterEmail == NewsletterEmail);
        }

        public void InsertNewsletter(Newsletter Newsletter)
        {
            if (Newsletter == null)
                throw new ArgumentNullException("Newsletter");

            _NewsletterRepository.Insert(Newsletter);
        }

        #endregion

    }
}
