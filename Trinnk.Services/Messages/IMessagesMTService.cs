
using Trinnk.Entities.Tables.Messages;

namespace Trinnk.Services.Messages
{
    public interface IMessagesMTService
    {
        MessagesMT GetMessagesMTByMessageMTName(string messageMTName);
    }
}
