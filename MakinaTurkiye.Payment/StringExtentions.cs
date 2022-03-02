using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace MakinaTurkiye.Payment
{
    public static class StringExtentions
    {
        public static object GetInstance(this string value)
        {
            Type type = Type.GetType(value);
            if (type != null)
                return Activator.CreateInstance(type);
            foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                type = asm.GetType(value);
                if (type != null)
                    return Activator.CreateInstance(type);
            }
            return null;
        }

        public static string GenerateSeoUrl(this string value)
        {
            string phrase = string.Format("{0}", value);
            string str = RemoveAccent(phrase).ToLower();
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            str = Regex.Replace(str, @"\s+", " ").Trim();
            str = str.Substring(0, str.Length <= 500 ? str.Length : 500).Trim();
            str = Regex.Replace(str, @"\s", "-");
            return str;
        }

        public static string ToCevapMesaj(this string value)
        {
            string Sonuc = "";
            switch (value)
            {
                case "400":
                    {
                        Sonuc = "3D Secure Şifre Doğrulaması Yapılamadı"; break;
                    }
                case "4000":
                    {
                        Sonuc = "3D Secure Şifre Doğrulaması Yapılamadı"; break;
                    }
                default:break;
            }
            return Sonuc;
        }
                    public static string ToMesaj(this string value)
        {
            string Sonuc = "";
            switch (value)
            {
                case "1001":{ Sonuc = "Hatalı URL Biçimi Geçersiz"; break; }
                case "1002":{ Sonuc = "SuccessUrl formatı geçersiz."; break; }
                case "1003":{ Sonuc = "BrandId formatı geçersiz"; break; }
                case "1004":{ Sonuc = "DeviceCategory formatı geçersiz"; break; }
                case "1005":{ Sonuc = "SessionInfo biçimi geçersiz"; break; }
                case "1006":{ Sonuc = "Xid formatı geçersiz"; break; }
                case "1007":{ Sonuc = "Para birimi biçimi geçersiz"; break; }
                case "1008":{ Sonuc = "PurchaseAmount formatı geçersiz"; break; }
                case "1009":{ Sonuc = "Bitiş Tarihi formatı geçersiz"; break; }
                case "1010":{ Sonuc = "Pan formatı geçersiz"; break; }
                case "1011":{ Sonuc = "Ticari alıcı kutusu parola biçimi geçersiz"; break; }
                case "1012":{ Sonuc = "HostMerchant formatı geçersiz"; break; }
                case "1013":{ Sonuc = "BankId formatı geçersiz"; break; }
                case "1014":{ Sonuc = "Düzenli Biçim Geçersiz mi"; break; }
                case "1015":{ Sonuc = "Tekrarlayan Frekans Formatı Geçersiz"; break; }
                case "1016":{ Sonuc = "Tekrarlanan Bitiş Tarihi Formatı Geçersiz"; break; }
                case "1017":{ Sonuc = "Taksit sayma biçimi geçersiz"; break; }
                case "2000":{ Sonuc = "Alıcı bilgisi boş"; break; }
                case "2005":{ Sonuc = "Bu banka için satıcı bulunamadı"; break; }
                case "2006":{ Sonuc = "Ticari alıcı kutusu parolası gerekli"; break; }
                case "2009":{ Sonuc = "Marka bulunamadı"; break; }
                case "2010":{ Sonuc = "CardHolder bilgisi boş"; break; }
                case "2011":{ Sonuc = "Pan boş"; break; }
                case "2012":{ Sonuc = "DeviceCategory 0 ile 2 arasında olmalıdır"; break; }
                case "2013":{ Sonuc = "Threed güvenli mesajı bulunamadı"; break; }
                case "2014":{ Sonuc = "Ana mesaj kimliği, güvenli mesaj kimliği ile eşleşmiyor"; break; }
                case "2015":{ Sonuc = "İmza doğrulama yanlış"; break; }
                case "2017":{ Sonuc = "AcquireBin bulunamadı"; break; }
                case "2018":{ Sonuc = "Satıcı edinicisi kutusu şifresi yanlış"; break; }
                case "2019":{ Sonuc = "Banka bulunamadı"; break; }
                case "2020":{ Sonuc = "Banka Kimliği ticari banka ile eşleşmiyor"; break; }
                case "2021":{ Sonuc = "Geçersiz Para Birimi Kodu"; break; }
                case "2022":{ Sonuc = "EnrollmentRequest Kimliğinin boş olamayacağını doğrulayın"; break; }
                case "2023":{ Sonuc = "Kayıt İsteği Kimliğini Doğrula Bu satıcı için zaten var"; break; }
                case "2024":{ Sonuc = "Acs sertifikası veritabanında bulunamıyor"; break; }
                case "2025":{ Sonuc = "Sertifika deposunda sertifika bulunamadı"; break; }
                case "2026":{ Sonuc = "Marka sertifikası mağazada bulunamadı"; break; }
                case "2027":{ Sonuc = "Geçersiz xml dosyası"; break; }
                case "2028":{ Sonuc = "Threed Güvenli Mesajı Geçersiz Durumda"; break; }
                case "2029":{ Sonuc = "Geçersiz Pan"; break; }
                case "2030":{ Sonuc = "Geçersiz Son Kullanma Tarihi"; break; }
                case "2031":{ Sonuc = "Doğrulama başarısız oldu: Belgede İmza bulunamadı"; break; }
                case "2032":{ Sonuc = "Doğrulama başarısız oldu: Belge için birden fazla imza bulundu"; break; }
                case "2033":{ Sonuc = "Gerçek Marka Bulunamıyor"; break; }
                case "2034":{ Sonuc = "Geçersiz Miktar"; break; }
                case "2035":{ Sonuc = "Geçersiz Tekrarlanan Bilgiler"; break; }
                case "2036":{ Sonuc = "Geçersiz Tekrarlayan Frekans"; break; }
                case "2037":{ Sonuc = "Geçersiz Alım Bitiş Tarihi"; break; }
                case "2038":{ Sonuc = "Tekrarlanan Bitiş Tarihi Bitiş Tarihinden Büyük olmalıdır"; break; }
                case "2039":{ Sonuc = "Geçersiz x509 sertifikası Verileri"; break; }
                case "2040":{ Sonuc = "Geçersiz Taksit"; break; }
                case "2053":{ Sonuc = "Dizin Sunucusu İletişim Hatası"; break; }
                case "3000":{ Sonuc = "Banka bulunamadı"; break; }
                case "3001":{ Sonuc = "Ülke bulunamadı"; break; }
                case "3002":{ Sonuc = "Geçersiz BaşarısızUrl"; break; }
                case "3003":{ Sonuc = "HostMerchantNumber boş olamaz"; break; }
                case "3004":{ Sonuc = "MerchantBrandAcquirerBin boş bırakılamaz"; break; }
                case "3005":{ Sonuc = "MerchantName boş olamaz"; break; }
                case "3006":{ Sonuc = "MerchantPassword boş olamaz"; break; }
                case "3007":{ Sonuc = "Geçersiz Başarılı URL'si"; break; }
                case "3008":{ Sonuc = "Geçersiz MerchantSiteUrl"; break; }
                case "3009":{ Sonuc = "Geçersiz AlıcıBin uzunluğu"; break; }
                case "3010":{ Sonuc = "Marka boş olamaz"; break; }
                case "3011":{ Sonuc = "Geçersiz AlıcıBinPassword uzunluğu"; break; }
                case "3012":{ Sonuc = "Geçersiz HostMerchantNumber uzunluğu"; break; }
                default:
                    break;
            }
            return Sonuc;
        }


        private static string RemoveAccent(string text)
        {
            byte[] bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(text);
            return System.Text.Encoding.ASCII.GetString(bytes);
        }

        public static string Sifrele(this string value)
        {
            using (SifreOlusturucu sifreOlusturucu = new SifreOlusturucu())
            {
                return sifreOlusturucu.TextSifrele(value);
            }
        }

        public static string Coz(this string value)
        {
            using (SifreOlusturucu sifreOlusturucu = new SifreOlusturucu())
            {
                return sifreOlusturucu.TextSifreCoz(value);
            }
        }

        public static string Substring(this string @this, string from = null, string until = null, StringComparison comparison = StringComparison.InvariantCulture)
        {
            var fromLength = (from ?? string.Empty).Length;
            var startIndex = !string.IsNullOrEmpty(from)
                ? @this.IndexOf(from, comparison) + fromLength
                : 0;

            if (startIndex < fromLength) { throw new ArgumentException("from: Failed to find an instance of the first anchor"); }

            var endIndex = !string.IsNullOrEmpty(until)
            ? @this.IndexOf(until, startIndex, comparison)
            : @this.Length;

            if (endIndex < 0) { throw new ArgumentException("until: Failed to find an instance of the last anchor"); }

            var subString = @this.Substring(startIndex, endIndex - startIndex);
            return subString;
        }

        public static bool IsNumeric(this string theValue)
        {
            long retNum;
            return long.TryParse(theValue, System.Globalization.NumberStyles.Integer, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
        }

        

        public static string GetHiddenText(this string valuetxt,int KarakterSayisi,bool Sag=false)
        {
            string Sonuc = "";
            foreach (var item in valuetxt.ToCharArray().Select((value, i) => new { i, value }))
            {
                var value = item.value;
                var index = item.i;
                if (!Sag)
                {
                    if (index < KarakterSayisi)
                    {
                        Sonuc += value;
                    }
                    else
                    {
                        Sonuc += "X";
                    }
                }
                else
                {
                    if (index > valuetxt.Length-KarakterSayisi)
                    {
                        Sonuc += value;
                    }
                    else
                    {
                        Sonuc += "X";
                    }
                }
            }
            return Sonuc;
        }
        public static double CalculateSimilarity(this string source, string target)
        {
            if ((source == null) || (target == null)) return 0.0;
            if ((source.Length == 0) || (target.Length == 0)) return 0.0;
            if (source == target) return 1.0;

            int stepsToSame = ComputeLevenshteinDistance(source, target);
            return (1.0 - ((double)stepsToSame / (double)Math.Max(source.Length, target.Length)));
        }

        public static int ComputeLevenshteinDistance(string source, string target)
        {
            if ((source == null) || (target == null)) return 0;
            if ((source.Length == 0) || (target.Length == 0)) return 0;
            if (source == target) return source.Length;

            int sourceWordCount = source.Length;
            int targetWordCount = target.Length;

            // Step 1
            if (sourceWordCount == 0)
                return targetWordCount;

            if (targetWordCount == 0)
                return sourceWordCount;

            int[,] distance = new int[sourceWordCount + 1, targetWordCount + 1];

            // Step 2
            for (int i = 0; i <= sourceWordCount; distance[i, 0] = i++) ;
            for (int j = 0; j <= targetWordCount; distance[0, j] = j++) ;

            for (int i = 1; i <= sourceWordCount; i++)
            {
                for (int j = 1; j <= targetWordCount; j++)
                {
                    int cost = (target[j - 1] == source[i - 1]) ? 0 : 1;
                    distance[i, j] = Math.Min(Math.Min(distance[i - 1, j] + 1, distance[i, j - 1] + 1), distance[i - 1, j - 1] + cost);
                }
            }
            return distance[sourceWordCount, targetWordCount];
        }

        public static bool IsTelefonNumarasi(this string value)
        {
            string pattern = @"^((\d{3})(\d{3})(\d{2})(\d{2}))$";
            Match match = Regex.Match(value, pattern, RegexOptions.IgnoreCase);
            return match.Success;
        }

        public static bool IsValidTelefon(this string theValue)
        {
            return Regex.Match(theValue, @"^(\+[0-9]{9})$").Success;
        }

        public static string TrkToEng(this string theValue)
        {
            string Sonuc = String.Join("", theValue.Normalize(NormalizationForm.FormD).Where(c => char.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark));
            return Sonuc;
        }
        public static string GetUniqKey(this string value)
        {
            string Sonuc = Guid.NewGuid().ToString();
            return Sonuc;
        }


        public static bool IsValidEmail(this string value)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(value);
                return true;
            }
            catch
            {
                return false;
            }
        }


        public static bool ValidCardNumber(this string value)
        {
            string cardNumber = value.Replace("-", "").Replace(" ", "");
            int[] digits = new int[cardNumber.Length];
            for (int len = 0; len < cardNumber.Length; len++)
            {
                digits[len] = Int32.Parse(cardNumber.Substring(len, 1));
            }
            int sum = 0;
            bool alt = false;
            for (int i = digits.Length - 1; i >= 0; i--)
            {
                int curDigit = digits[i];
                if (alt)
                {
                    curDigit *= 2;
                    if (curDigit > 9)
                    {
                        curDigit -= 9;
                    }
                }
                sum += curDigit;
                alt = !alt;
            }

            //If Mod 10 equals 0, the number is good and this will return true
            return sum % 10 == 0;
        }

        public enum CreditCardTypeType
        {
            Visa,
            MasterCard,
            Discover,
            Amex,
            Switch,
            Solo
        }

        public static CreditCardTypeType? GetCardTypeFromNumber(string cardNum)
        {
            //Create new instance of Regex comparer with our
            //credit card regex patter
            Regex cardTest = new Regex(@"^(?:(?<Visa>4\\d{3})|
    (?<MasterCard>5[1-5]\\d{2})|(?<Discover>6011)|(?<DinersClub>
    (?:3[68]\\d{2})|(?:30[0-5]\\d))|(?<Amex>3[47]\\d{2}))([ -]?)
    (?(DinersClub)(?:\\d{6}\\1\\d{4})|(?(Amex)(?:\\d{6}\\1\\d{5})
    |(?:\\d{4}\\1\\d{4}\\1\\d{4})))$");

            //Compare the supplied card number with the regex
            //pattern and get reference regex named groups
            GroupCollection gc = cardTest.Match(cardNum).Groups;

            //Compare each card type to the named groups to
            //determine which card type the number matches
            if (gc[CreditCardTypeType.Amex.ToString()].Success)
            {
                return CreditCardTypeType.Amex;
            }
            else if (gc[CreditCardTypeType.MasterCard.ToString()].Success)
            {
                return CreditCardTypeType.MasterCard;
            }
            else if (gc[CreditCardTypeType.Visa.ToString()].Success)
            {
                return CreditCardTypeType.Visa;
            }
            else if (gc[CreditCardTypeType.Discover.ToString()].Success)
            {
                return CreditCardTypeType.Discover;
            }
            else
            {
                //Card type is not supported by our system, return null
                //(You can modify this code to support more (or less)
                // card types as it pertains to your application)
                return null;
            }
        }

        

        private static Random random = new Random();

        public static string SeriAnahtarUret(this string value, int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}