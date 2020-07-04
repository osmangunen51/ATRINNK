using MakinaTurkiye.Entities.Tables.Common;
using System.Collections.Generic;

namespace MakinaTurkiye.Services.Common
{
    public interface IConstantService : ICachingSupported
    {
        IList<Constant> GetAllConstants();

        Constant GetConstantByConstantId(short constantId);
        IList<Constant> GetConstantsByConstantIds(List<short> constantIds);
        List<Constant> GetConstantByConstantType(ConstantTypeEnum constantType);
        void InsertConstant(Constant constant);
    }
}
