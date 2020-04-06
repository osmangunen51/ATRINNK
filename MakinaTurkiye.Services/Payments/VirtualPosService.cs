using MakinaTurkiye.Core.Data;
using MakinaTurkiye.Entities.Tables.Payments;
using System;
using System.Linq;

namespace MakinaTurkiye.Services.Payments
{
    public class VirtualPosService : IVirtualPosService
    {
        #region Fileds

        private readonly IRepository<VirtualPos> _virtualPosRepository;

        #endregion

        #region Ctor

        public VirtualPosService(IRepository<VirtualPos> virtualPosRepository)
        {
            _virtualPosRepository = virtualPosRepository;
        }

        #endregion

        public VirtualPos GetVirtualPosByVirtualPosId(int virtualPosId)
        {
            if (virtualPosId == 0)
                throw new ArgumentNullException("virtualPosId");

            var query = _virtualPosRepository.Table;
            var virtualPos = query.FirstOrDefault(vp => vp.VirtualPosId == virtualPosId);
            return virtualPos;
        }
    }
}
