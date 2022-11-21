using MakinaTurkiye.Entities.StoredProcedures.Messages;
using MakinaTurkiye.Entities.Tables.Messages;
using System.Collections.Generic;


namespace MakinaTurkiye.Services.Messages
{
    public interface IMessageService
    {
        IList<MessageResult> SP_GetAllMessageByMainPartyIdsByMessageType(string mainPartyIds, int messageType);

        Message GetMessageByMesssageId(int messageId);

        void InsertMessage(Message message);

        void UpdateMessage(Message message);

        void InsertMessageMainParty(MessageMainParty messageMainParty);

        void UpdateMessageMainParty(MessageMainParty messageMainParty);

        void InsertMessageCheck(MessageCheck messageCheck);
        void DeleteMessageCheck(MessageCheck messageCheck);

        MessageCheck GetMessageCheckByMessageId(int messageId);

        MessageMainParty GetMessageMainPartyByMessageIdWithMessageType(int messageId, MessageTypeEnum messageType);

        MessageMainParty GetFirstMessageMainPartyByMessageId(int messageId);

        IList<SendMessageError> GetAllSendMessageErrors(int senderId = 0, string messageSubject = "", int receiverId = 0);
        IList<SendMessageError> GetSendMessageErrorsBySenderId(int senderId);
        SendMessageError GetSendMessageErrorByMessageId(int messageId);

        void InsertSendMessageError(SendMessageError sendMessageError);
        void DeleteSendMessageError(SendMessageError sendMessageError);

        IList<MessageMainParty> GetAllMessageMainParty(int memberMainPartyId, byte messageType);
        IList<MessageMainParty> GetMessageMainPartyByMessageId(int messageId);

        IList<MessageMainParty> GetMessageMainPartyByFromAndTo(int From,int To);

        IList<Message> GetMessageByMessageIds(List<int> MessageIds);

    }
}
