using MakinaTurkiye.Core;
using MakinaTurkiye.Core.Data;
using MakinaTurkiye.Entities.Tables.Logs;
using System.Linq;

namespace MakinaTurkiye.Services.Logs
{
    public class ApplicationLogService : IApplicationLogService
    {
        private IRepository<ApplicationLog> _applicationLogRepository;
        public ApplicationLogService(IRepository<ApplicationLog> applicationLogRepository)
        {
            this._applicationLogRepository = applicationLogRepository;
        }
        public IPagedList<ApplicationLog> GetApplicationLogs(int page, int pageDimension)
        {
            var query = _applicationLogRepository.Table;
            var source = query.OrderByDescending(x => x.Id).ToList();
            return new PagedList<ApplicationLog>(source, page, pageDimension);
        }
    }
}
