using MakinaTurkiye.Entities.Tables.SearchEngine;
using System.Collections.Generic;

namespace MakinaTurkiye.Services.SearchEngine
{
    public interface IIncrementalProductService
    {

        IncrementalSearchEngineProduct GetIncrementalSearchEngineProductById(int incrementalSearchEngineProductId);
        void InsertIncrementalProduct(IncrementalSearchEngineProduct incrementalSearchEngineProduct);
        void InsertIncrementalProducts(IList<IncrementalSearchEngineProduct> incrementalSearchEngineProduct);

        void UpdateIncrementalProduct(IncrementalSearchEngineProduct incrementalSearchEngineProduct);
        void UpdateIncrementalProducts(IList<IncrementalSearchEngineProduct> incrementalSearchEngineProduct);

        void DeleteIncrementalProduct(IncrementalSearchEngineProduct incrementalSearchEngineProduct);
    }
}
