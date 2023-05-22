
using Trinnk.Entities.Tables.Seos;
using System.Collections.Generic;


namespace Trinnk.Services.Seos
{
    public interface ISeoDefinitionService
    {
        SeoDefinition GetSeoDefinitionByEntityIdWithEntityType(int entityId, EntityTypeEnum entityType);
        IList<Seo> GetAllSeos();

    }
}
