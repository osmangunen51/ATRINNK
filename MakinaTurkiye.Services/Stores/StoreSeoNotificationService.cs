﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MakinaTurkiye.Core.Data;
using MakinaTurkiye.Entities.Tables.Stores;

namespace MakinaTurkiye.Services.Stores
{
    public class StoreSeoNotificationService : IStoreSeoNotificationService
    {
        #region fields
        IRepository<StoreSeoNotification> _storeSeoNotificationRepository;
        #endregion
        #region const
        public StoreSeoNotificationService(IRepository<StoreSeoNotification> storeSeoNotificationRepository)
        {
            _storeSeoNotificationRepository = storeSeoNotificationRepository;
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
            x.RemindDate.Value.Hour <= date.Hour && x.RemindDate.Value.Minute <= date.Hour && x.ToUserId == userId).OrderByDescending(x => x.RemindDate.Value);
            return query.ToList();
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

        public void UpdateStoreSeoNotification(StoreSeoNotification storeSeoNotification)
        {
            if (storeSeoNotification == null)
                throw new ArgumentNullException("storeSeoNotificaiton");

            _storeSeoNotificationRepository.Update(storeSeoNotification);
        }
        #endregion
    }
}
