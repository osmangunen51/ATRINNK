
using MakinaTurkiye.Entities.Tables.Messages;

namespace MakinaTurkiye.Services.Messages
{
    public interface IMessagesMTService
    {
        MessagesMT GetMessagesMTByMessageMTName(string messageMTName);
    }
}
