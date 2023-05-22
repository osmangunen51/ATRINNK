using Trinnk.Core.Data;
using Trinnk.Data;
using Trinnk.Entities.Tables.Stores;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Trinnk.Services.Stores
{
    public class StoreSeoNotificationService : IStoreSeoNotificationService
    {
        #region fields
        IRepository<StoreSeoNotification> _storeSeoNotificationRepository;
        IDbContext _dbContext;
        #endregion
        #region const
        public StoreSeoNotificationService(IRepository<StoreSeoNotification> storeSeoNotificationRepository, IDbContext dbContext)
        {
            _storeSeoNotificationRepository = storeSeoNotificationRepository;
            _dbContext = dbContext;
        }
        #endregion
        #region methods
        public void DeleteStoreSeoNotification(StoreSeoNotification storeSeoNotification)
        {
            if (storeSeoNotification == null)
                throw new ArgumentNullException("storeSeoNotificaiton");

            _storeSeoNotificationRepository.Delete(storeSeoNotification);

        }

        public StoreSeoNotification GetStoreSeoNotificationByStoreSeoNotificationId(int storeSeoNotificationId)
        {
            if (storeSeoNotificationId == 0)
                throw new ArgumentNullException("storeSeoNotificationId");
            var query = _storeSeoNotificationRepository.Table;
            return query.FirstOrDefault(x => x.StoreSeoNotificationId == storeSeoNotificationId);
        }

        public IList<StoreSeoNotification> GetStoreSeoNotifications(int skip, int take, DateTime? createdDate, out int totalRecord)
        {
            var query = _storeSeoNotificationRepository.Table;
            if (createdDate.HasValue)
            {
                var date = createdDate.Value;
                query = query.Where(x =>
x.CreatedDate.Year == date.Year && x.CreatedDate.Month == date.Month && x.CreatedDate.Day == date.Day);
                totalRecord = query.Count();
                return query.OrderByDescending(x => x.CreatedDate).Skip(skip).Take(take).ToList();
            }
            else
            {
                totalRecord = query.Count();
                return query.OrderByDescending(x => x.RemindDate).Skip(skip).Take(take).ToList();
            }

        }

        public IList<StoreSeoNotification> GetStoreSeoNotificationsByDateWithStatus(DateTime date, byte status, byte userId)
        {
            var query = _storeSeoNotificationRepository.Table;


            query = query.Where(x => x.Status == status && x.RemindDate.HasValue &&
            x.RemindDate.Value.Year == date.Year && x.RemindDate.Value.Month == date.Month && x.RemindDate.Value.Day == date.Day &&
            ((x.RemindDate.Value.Hour < date.Hour) || (x.RemindDate.Value.Hour == date.Hour && x.RemindDate.Value.Minute <= date.Minute))
            && x.ToUserId == userId);
            var queryIsFirst = query.Where(x => x.IsFirst == true).OrderByDescending(x => x.RemindDate.Value).ToList();
            var queryAll = query.Where(x => x.IsFirst == false || !x.IsFirst.HasValue).OrderByDescending(x => x.RemindDate.Value).ToList();
            queryIsFirst.AddRange(queryAll);

            return queryIsFirst.ToList();
        }

        public IList<StoreSeoNotification> GetStoreSeoNotificationsByStoreMainPartyId(int storeMainPartyId)
        {
            if (storeMainPartyId == 0)
                throw new ArgumentNullException("storeMainPartyId");
            var query = _storeSeoNotificationRepository.Table;
            query = query.Where(x => x.StoreMainPartyId == storeMainPartyId);
            return query.ToList();
        }

        public void InsertStoreSeoNotification(StoreSeoNotification storeSeoNotification)
        {
            if (storeSeoNotification == null)
                throw new ArgumentNullException("storeSeoNotificaiton");

            _storeSeoNotificationRepository.Insert(storeSeoNotification);
        }

        public void SP_StoreSeoNotificationChangeDateForRest()
        {

            _dbContext.ExecuteSqlCommand("exec SP_StoreSeoNotificationChangeDateForRest");

        }

        public void UpdateStoreSeoNotification(StoreSeoNotification storeSeoNotification)
        {
            if (storeSeoNotification == null)
                throw new ArgumentNullException("storeSeoNotificaiton");

            _storeSeoNotificationRepository.Update(storeSeoNotification);
        }
        #endregion
    }
}
