using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Trinnk.Payment
{
    public static class SafeBase64UrlEncoder
    {
        private const string Plus = "+";
        private const string Minus = "-";
        private const string Slash = "/";
        private const string Underscore = "_";
        private const string EqualSign = "=";
        private const string Pipe = "|";

        private static readonly IDictionary<string, string> _mapper;

        static SafeBase64UrlEncoder()
        {
            _mapper = new Dictionary<string, string>
        {
            { Plus, Minus },
            { Slash, Underscore },
            { EqualSign, Pipe }
        };
        }

        public static string EncodeBase64Url(string base64Str)
        {
            if (string.IsNullOrEmpty(base64Str)) return base64Str;
            foreach (var pair in _mapper)
                base64Str = base64Str.Replace(pair.Key, pair.Value);
            return base64Str;
        }

        public static string DecodeBase64Url(string safe64Url)
        {
            if (string.IsNullOrEmpty(safe64Url)) return safe64Url;
            foreach (var pair in _mapper)
                safe64Url = safe64Url.Replace(pair.Value, pair.Key);
            return safe64Url;
        }
    }

    public class SifreOlusturucu : IDisposable
    {
        private string _Sifre = "MERHABASAHIPMERHABASAHIP";

        public string Sifre
        {
            get
            {
                return _Sifre;
            }
            set
            {
                _Sifre = value;
            }
        }

        private static Random random = new Random((int)DateTime.Now.Ticks);
        public List<string> KarakterListesi { get; set; }

        public SifreOlusturucu()
        {
        }

        public string SifreOlustur(int Boyut)
        {
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < Boyut; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }

            return builder.ToString();
        }

        private byte[] Sifrele(byte[] SifresizVeri, byte[] Key, byte[] IV)

        {
            MemoryStream ms = new MemoryStream();

            Rijndael alg = Rijndael.Create();

            alg.Key = Key;

            alg.IV = IV;

            CryptoStream cs = new CryptoStream(ms,

            alg.CreateEncryptor(), CryptoStreamMode.Write);

            cs.Write(SifresizVeri, 0, SifresizVeri.Length);

            cs.Close();

            byte[] sifrelenmisVeri = ms.ToArray();

            return sifrelenmisVeri;
        }

        private byte[] SifreCoz(byte[] SifreliVeri, byte[] Key, byte[] IV)
        {
            MemoryStream ms = new MemoryStream();
            Rijndael alg = Rijndael.Create();
            alg.Key = Key;
            alg.IV = IV;
            CryptoStream cs = new CryptoStream(ms, alg.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(SifreliVeri, 0, SifreliVeri.Length);
            cs.Close();
            byte[] SifresiCozulmusVeri = ms.ToArray();
            return SifresiCozulmusVeri;
        }

        public string TextSifrele(string sifrelenecekMetin)
        {
            byte[] sifrelenecekByteDizisi = System.Text.Encoding.Unicode.GetBytes(sifrelenecekMetin);
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(this.Sifre, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            byte[] SifrelenmisVeri = Sifrele(sifrelenecekByteDizisi, pdb.GetBytes(32), pdb.GetBytes(16));
            return Convert.ToBase64String(SifrelenmisVeri);
        }

        public string TextSifreCoz(string text)
        {
            byte[] SifrelenmisByteDizisi = Convert.FromBase64String(text);
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(this.Sifre, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            byte[] SifresiCozulmusVeri = SifreCoz(SifrelenmisByteDizisi, pdb.GetBytes(32), pdb.GetBytes(16));
            return System.Text.Encoding.Unicode.GetString(SifresiCozulmusVeri);
        }

        public string Sifrele(string SifrelenecekTxt, bool UrlEncode = false)
        {
            if (SifrelenecekTxt != "")
            {
                string EncryptedData = TextSifrele(SifrelenecekTxt);
                if (UrlEncode)
                {
                    EncryptedData = SafeBase64UrlEncoder.EncodeBase64Url(EncryptedData);
                }
                return EncryptedData;
            }
            return "";
        }

        public string Coz(string CozulecekVeri, bool UrlEncode = false)
        {
            if (CozulecekVeri != "")
            {
                if (UrlEncode)
                {
                    CozulecekVeri = SafeBase64UrlEncoder.DecodeBase64Url(CozulecekVeri);
                }
                string DecryptedData = TextSifreCoz(CozulecekVeri);
                return DecryptedData;
            }
            return "";
        }

        public void Dispose()
        {
        }
    }
}