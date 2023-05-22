using Trinnk.Core.Data;
using Trinnk.Entities.Tables.Messages;
using System.Linq;

namespace Trinnk.Services.Messages
{
    public class MessagesMTService : IMessagesMTService
    {
        private readonly IRepository<MessagesMT> _messageMtRepository;

        public MessagesMTService(IRepository<MessagesMT> messageMtRepository)
        {
            this._messageMtRepository = messageMtRepository;
        }

        public MessagesMT GetMessagesMTByMessageMTName(string messageMTName)
        {
            var query = _messageMtRepository.Table;
            return query.FirstOrDefault(x => x.MessagesMTName == messageMTName);
        }
    }
}
