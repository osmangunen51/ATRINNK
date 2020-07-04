namespace MakinaTurkiye.Utilities.FileHelpers
{
    public static class FileUrlHelper
    {
        static string Url = "//s.makinaturkiye.com/";
        
        public static string GetStoreCatologUrl(string fileName,int storeMainPartyId)
        {
            return Url + "StoreCatolog/" + storeMainPartyId + "/" + fileName;
        }
        public static string GetProductCatalogUrl(string fileName,int productId)
        {
            return Url + "ProductCatolog/" + productId.ToString() + "/" + fileName;
        }
    }
}
