using System.Web.Mvc;

namespace MakinaTurkiye.Payment
{
    public static class FormCollectionExtentions
    {
        public static bool ContainsKey(this FormCollection form, string Key)
        {
            bool Sonuc = false;
            Sonuc = (form[Key] != null);
            return Sonuc;
        }
    }
}
