using System.Collections.Generic;

namespace MakinaTurkiye.Entities.StoredProcedures.Catalog
{
    public static  class TopCategoryResultExtensions
    {
        public static string GetUnifiedCategories(this IList<TopCategoryResult> topCategories)
        {
            if (topCategories.Count==0)
                return string.Empty;

            string result = string.Empty;
            foreach (var item in topCategories)
            {
                if (string.IsNullOrWhiteSpace(result))
                {
                    result = item.CategoryName;
                }
                else
                {
                    result = result + ", " + item.CategoryName;
                }
            }
            return result;
        }
    }
}
