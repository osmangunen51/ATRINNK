using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

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



        public static string GetHiddenText(this string valuetxt, int KarakterSayisi, bool Sag = false)
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
                    if (index > valuetxt.Length - KarakterSayisi)
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