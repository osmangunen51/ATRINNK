using Trinnk.Entities.Tables.Messages;
using System.Collections.Generic;

namespace Trinnk.Services.Messages
{
    public interface IMobileMessageService
    {
        IList<MobileMessage> GetMobileMessagesByMessageType(MobileMessageTypeEnum messageType);

        MobileMessage GetMobileMessageById(int mobileMessageId);

        MobileMessage GetMobileMessageByMessageName(string messageName);


    }
}
