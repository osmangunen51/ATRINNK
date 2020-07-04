
using MakinaTurkiye.Entities.Tables.Seos;
using System.Collections.Generic;


namespace MakinaTurkiye.Services.Seos
{
    public interface ISeoDefinitionService
    {
        SeoDefinition GetSeoDefinitionByEntityIdWithEntityType(int entityId, EntityTypeEnum entityType);
        IList<Seo> GetAllSeos();
      
    }
}
