using System;

namespace MakinaTurkiye.Api.View
{
    public static class StringExtentions
    {
        public static string Sifrele(this string Text, string Key)
        {
            string Sonuc = new SifreOlusturucu() { Sifre = Key }.TextSifrele(Text);
            return Sonuc;
        }

        public static string Coz(this string Text, string Key)
        {
            string Sonuc = "";
            try
            {
                Sonuc = new SifreOlusturucu() { Sifre = Key }.TextSifreCoz(Text);
            }
            catch (Exception Hata)
            {
                throw;
            }
            return Sonuc;
        }
    }
}