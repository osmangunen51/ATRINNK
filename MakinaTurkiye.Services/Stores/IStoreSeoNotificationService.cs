using MakinaTurkiye.Entities.Tables.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakinaTurkiye.Services.Stores
{
    public interface IStoreSeoNotificationService
    {
        void InsertStoreSeoNotification(StoreSeoNotification storeSeoNotification);
        void UpdateStoreSeoNotification(StoreSeoNotification storeSeoNotification);
        void DeleteStoreSeoNotification(StoreSeoNotification storeSeoNotification);

        StoreSeoNotification GetStoreSeoNotificationByStoreSeoNotificationId(int storeSeoNotificationId);

        IList<StoreSeoNotification> GetStoreSeoNotificationsByStoreMainPartyId(int storeMainPartyId);

        IList<StoreSeoNotification> GetStoreSeoNotificationsByDateWithStatus(DateTime date, byte status, byte userId);

        IList<StoreSeoNotification> GetStoreSeoNotifications(int skip, int take, DateTime? createdDate, out int totalRecord);


    }
}
