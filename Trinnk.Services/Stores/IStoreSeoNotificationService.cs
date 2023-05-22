using Trinnk.Entities.Tables.Stores;
using System;
using System.Collections.Generic;

namespace Trinnk.Services.Stores
{
    public interface IStoreSeoNotificationService
    {
        void InsertStoreSeoNotification(StoreSeoNotification storeSeoNotification);
        void UpdateStoreSeoNotification(StoreSeoNotification storeSeoNotification);
        void DeleteStoreSeoNotification(StoreSeoNotification storeSeoNotification);
        void SP_StoreSeoNotificationChangeDateForRest();

        StoreSeoNotification GetStoreSeoNotificationByStoreSeoNotificationId(int storeSeoNotificationId);

        IList<StoreSeoNotification> GetStoreSeoNotificationsByStoreMainPartyId(int storeMainPartyId);

        IList<StoreSeoNotification> GetStoreSeoNotificationsByDateWithStatus(DateTime date, byte status, byte userId);

        IList<StoreSeoNotification> GetStoreSeoNotifications(int skip, int take, DateTime? createdDate, out int totalRecord);



    }
}
