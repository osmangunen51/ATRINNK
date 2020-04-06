using MakinaTurkiye.Core.Data;
using MakinaTurkiye.Data;
using MakinaTurkiye.Entities.StoredProcedures.Messages;
using MakinaTurkiye.Entities.Tables.Messages;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MakinaTurkiye.Services.Messages
{
    public class MessageService:IMessageService
    {
        private readonly IRepository<Message> _messageRepository;
        private readonly IRepository<MessageMainParty> _messageMainPartyRepository;
        private readonly IRepository<SendMessageError> _sendMessageErrorRepository;
        private readonly IRepository<MessageCheck> _messageCheckRepository;

        private readonly IDbContext _dbContext;
        private readonly IDataProvider _dataProvider;

        public MessageService(IRepository<Message> messageRepository, IDbContext dbContext, 
            IDataProvider dataProvider, IRepository<MessageMainParty> messageMainPartyRepository, 
            IRepository<SendMessageError> sendMessageErrorRepository,
            IRepository<MessageCheck> messageCheckRepository)
        {
            this._messageRepository = messageRepository;
            this._dataProvider = dataProvider;
            this._dbContext = dbContext;
            this._messageMainPartyRepository = messageMainPartyRepository;
            this._sendMessageErrorRepository = sendMessageErrorRepository;
            this._messageCheckRepository = messageCheckRepository;
        }
        public IList<MessageResult> SP_GetAllMessageByMainPartyIdsByMessageType(string mainPartyIds, int messageType)
        {
            var pMainPartyId = _dataProvider.GetParameter();
            pMainPartyId.ParameterName = "MainPartyId";
            pMainPartyId.Value = mainPartyIds;
            pMainPartyId.DbType = DbType.String;

            var pMessageType = _dataProvider.GetParameter();
            pMessageType.ParameterName = "MessageType";
            pMessageType.Value = messageType;
            pMessageType.DbType = DbType.Byte;
            var messageList = _dbContext.SqlQuery<MessageResult>("SP_GetMessageByMainPartyIdByMessageType_New @MainPartyId,@MessageType", pMainPartyId, pMessageType);
            return messageList.ToList();
        }

        public Message GetMessageByMesssageId(int messageId)
        {
            var message = _messageRepository.Table;
            return message.FirstOrDefault(x => x.MessageId == messageId);
        }


        public IList<SendMessageError> GetSendMessageErrorsBySenderId(int senderId)
        {
           if(senderId==0)
               throw new ArgumentNullException("senderId");

           var query = _sendMessageErrorRepository.Table;
           query=query.Where(x=>x.SenderID==senderId);
           return query.ToList();
        }

        public SendMessageError GetSendMessageErrorByMessageId(int messageId)
        {
            if (messageId == 0)
                throw new ArgumentNullException("messageId");

            var sendMessageError = _sendMessageErrorRepository.Table;
            return sendMessageError.FirstOrDefault(x=>x.MessageID==messageId);
        }

        public IList<SendMessageError> GetAllSendMessageErrors(int senderId = 0, string messageSubject = "", int receiverId = 0)
        {
            var query = _sendMessageErrorRepository.Table;

            if (senderId >0)
                query = query.Where(s => s.SenderID == senderId);

            if (!string.IsNullOrEmpty(messageSubject))
                query = query.Where(s => s.MessageSubject == messageSubject);

            if (receiverId > 0)
                query = query.Where(s => s.ReceiverID == receiverId);

            var sendMessageErrors = query.ToList();
            return sendMessageErrors;
        }

        public MessageCheck GetMessageCheckByMessageId(int messageId)
        {
            if (messageId <= 0)
                throw new ArgumentNullException("messageId");

            var query = _messageCheckRepository.Table;

            var messageCheck = query.FirstOrDefault(m => m.MessageId == messageId);
            return messageCheck;
        }

        public MessageMainParty GetMessageMainPartyByMessageIdWithMessageType(int messageId, MessageTypeEnum messageType)
        {
            if (messageId <= 0)
                throw new ArgumentNullException("messageId");

            var query = _messageMainPartyRepository.Table;

            var messageMainParty = query.FirstOrDefault(m => m.MessageId == messageId && m.MessageType == (byte)messageType);
            return messageMainParty;
        }

        public MessageMainParty GetFirstMessageMainPartyByMessageId(int messageId)
        {
            if (messageId <= 0)
                throw new ArgumentNullException("messageId");

            var query = _messageMainPartyRepository.Table;

            var messageMainParty = query.FirstOrDefault(m => m.MessageId == messageId);
            return messageMainParty;
        }


        public void InsertMessageMainParty(MessageMainParty messageMainParty)
        {
            if (messageMainParty == null)
                throw new ArgumentNullException("messageMainParty");

            _messageMainPartyRepository.Insert(messageMainParty);

        }

        public void InsertMessage(Message message)
        {
            if (message == null)
                throw new ArgumentNullException("message");

            _messageRepository.Insert(message);

        }

        public void UpdateMessage(Message message)
        {
            if (message == null)
                throw new ArgumentNullException("message");

            _messageRepository.Update(message);
        }

        public void InsertSendMessageError(SendMessageError sendMessageError)
        {
            if (sendMessageError == null)
                throw new ArgumentNullException("sendMessageError");

            _sendMessageErrorRepository.Insert(sendMessageError);
        }

        public void DeleteSendMessageError(SendMessageError sendMessageError)
        {
            if (sendMessageError == null)
                throw new ArgumentNullException("sendMessageError");

            _sendMessageErrorRepository.Delete(sendMessageError);
        }

        public void UpdateMessageMainParty(MessageMainParty messageMainParty)
        {
            if (messageMainParty == null)
                throw new ArgumentNullException("messageMainParty");

            _messageMainPartyRepository.Update(messageMainParty);
        }

        public void InsertMessageCheck(MessageCheck messageCheck)
        {
            if (messageCheck == null)
                throw new ArgumentNullException("messageCheck");

            _messageCheckRepository.Insert(messageCheck); 
        }

        public void DeleteMessageCheck(MessageCheck messageCheck)
        {
            if (messageCheck == null)
                throw new ArgumentNullException("messageCheck");

            _messageCheckRepository.Delete(messageCheck);
        }
    }
}
