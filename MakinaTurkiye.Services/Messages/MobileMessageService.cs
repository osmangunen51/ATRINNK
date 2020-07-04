using MakinaTurkiye.Caching;
using MakinaTurkiye.Core.Data;
using MakinaTurkiye.Entities.Tables.Messages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MakinaTurkiye.Services.Messages
{
    public class MobileMessageService : IMobileMessageService
    {

        #region Constants 

        private const string MOBILEMESSAGES_BY_MOBILEMESSAGE_ID_KEY = "makinaturkiye.mobilemessage.byid-{0}";
        private const string MOBILEMESSAGES_BY_MESSAGE_NAME_KEY = "makinaturkiye.mobilemessage.byname-{0}";
        private const string MOBILEMESSAGES_BY_MESSAGE_TYPE_KEY = "makinaturkiye.mobilemessage.bymessagetype-{0}";

        #endregion

        #region Fields

        private readonly IRepository<MobileMessage> _mobileMessageRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        public MobileMessageService(IRepository<MobileMessage> mobileMessageRepository, 
                                    ICacheManager cacheManager)
        {
            this._mobileMessageRepository = mobileMessageRepository;
            this._cacheManager = cacheManager;
        }

        #endregion


        #region Methods

        public MobileMessage GetMobileMessageById(int mobileMessageId)
        {
            if (mobileMessageId == 0)
                throw new ArgumentNullException("mobileMessageId");

            string key = string.Format(MOBILEMESSAGES_BY_MOBILEMESSAGE_ID_KEY, mobileMessageId);
            return _cacheManager.Get(key, () => 
            {
                var query = _mobileMessageRepository.Table;
                return query.FirstOrDefault(x => x.ID == mobileMessageId);
            });
        }

        public MobileMessage GetMobileMessageByMessageName(string messageName)
        {
            if (string.IsNullOrEmpty(messageName))
                throw new ArgumentNullException("messageName");

            string key = string.Format(MOBILEMESSAGES_BY_MESSAGE_NAME_KEY, messageName);
            return _cacheManager.Get(key, () =>
            {
                var query = _mobileMessageRepository.Table;
                return query.FirstOrDefault(x => x.MessageName == messageName);
            });
        }

        public IList<MobileMessage> GetMobileMessagesByMessageType(MobileMessageTypeEnum messageType)
        {
            if (messageType == 0)
                throw new ArgumentNullException("messageType");

            string key = string.Format(MOBILEMESSAGES_BY_MESSAGE_TYPE_KEY, messageType);
            return _cacheManager.Get(key, () => 
            {
                var query = _mobileMessageRepository.Table;
                query = query.Where(x => x.MessageType == (byte)messageType);

                var mobileMessages = query.ToList();
                return mobileMessages;
            });
        }

        #endregion
    }
}
