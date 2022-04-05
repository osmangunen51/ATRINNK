using System;
using System.Text;
using System.Web;

namespace MakinaTurkiye.Utilities.Seo
{
    public class ClientProcessor : IDisposable
    {

        public ClientProcessor()
        {

        }

        public KeywordAnalysis GetUrlAnalizWithUrl(string Url = "")
        {
            KeywordAnalysis Sonuc = new KeywordAnalysis();
            string Txt = "";
            using (Client HttpIstemci = new Client())
            {
                HttpIstemci.Kodlama = Encoding.UTF8;
                Txt = HttpIstemci.HttpGet(Url);
                Txt = HttpUtility.HtmlDecode(Txt);
            }
            if (Txt != "")
            {
                HtmlText htmlText = new HtmlText();
                Txt = htmlText.Turn(Txt);
                KeywordAnalyzer KeywordAnalyzer = new KeywordAnalyzer();
                var KeywordAnalyzerSonuc = KeywordAnalyzer.Analyze(Txt);
                Sonuc = KeywordAnalyzerSonuc;
            }
            return Sonuc;
        }

        public string unicodeToTrEncoding(string strIn)
        {
            Encoding enTr = Encoding.GetEncoding("windows-1254");
            Encoding unicode = Encoding.UTF8;

            byte[] unicodeBytes = unicode.GetBytes(strIn);
            byte[] trBytes = Encoding.Convert(unicode, enTr, unicodeBytes);
            char[] trChars = new char[enTr.GetCharCount(trBytes, 0, trBytes.Length)];
            enTr.GetChars(trBytes, 0, trBytes.Length, trChars, 0);
            string trString = new string(trChars);

            return trString;
        }

        public KeywordAnalysis GetUrlAnalizWithTxt(string Txt = "")
        {
            KeywordAnalysis Sonuc = new KeywordAnalysis();
            if (Txt != "")
            {
                //MakinaTurkiye.Seo.Analyzer.HtmlText HtmlText = new HtmlText();
                //Txt = HtmlText.Cevir(Txt);
                //Txt = unicodeToTrEncoding(Txt);
                KeywordAnalyzer KeywordAnalyzer = new KeywordAnalyzer();
                var KeywordAnalyzerSonuc = KeywordAnalyzer.Analyze(Txt);
                Sonuc = KeywordAnalyzerSonuc;
            }
            return Sonuc;
        }
        public void Dispose()
        {

        }
    }
}