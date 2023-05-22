using Trinnk.Core;
using Trinnk.Core.Data;
using Trinnk.Entities.Tables.Logs;
using System.Linq;

namespace Trinnk.Services.Logs
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

        public void InsertApplicationLog(ApplicationLog applicationLog)
        {
            _applicationLogRepository.Insert(applicationLog);
        }
    }
}
