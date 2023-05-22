using NeoSistem.EnterpriseEntity.Business;
using System.Collections.Generic;

namespace NeoSistem.Trinnk.Management.Helper
{
    public class TreeHelpers
    {
        public static IEnumerable<int> TreeHelper(int categoryId)
        {
            using (var transaction = new TransactionUI())
            {
                var category = new Classes.Category();
                if (category.LoadEntity(categoryId, transaction))
                {
                    if (category.CategoryParentId == null)
                    {
                        transaction.Commit();
                        yield return 0;
                        yield break;
                    }
                    while (category.LoadEntity(category.CategoryParentId, transaction))
                    {
                        yield return category.CategoryId;
                    }
                    transaction.Commit();
                }
            }
        }

    }
}