using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Collections.Specialized;
using System.Web;

namespace Trinnk.Seo.Analyzer
{
    public class Istemci : IDisposable
    {

        public Istemci()
        {

        }

        public KeywordAnalysis GetUrlAnalizWithUrl(string Url = "")
        {
            KeywordAnalysis Sonuc = new KeywordAnalysis();
            string Txt = "";
            using (Istemci1 HttpIstemci =new Istemci1())
            {
                HttpIstemci.Kodlama = Encoding.UTF8;
                Txt = HttpIstemci.HttpGet(Url);
                Txt=HttpUtility.HtmlDecode(Txt);
            }
            if (Txt!="")
            {
                Trinnk.Seo.Analyzer.HtmlText HtmlText = new HtmlText();
                Txt = HtmlText.Cevir(Txt);
                KeywordAnalyzer KeywordAnalyzer = new KeywordAnalyzer();
                var KeywordAnalyzerSonuc=KeywordAnalyzer.Analyze(Txt);
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
                //Trinnk.Seo.Analyzer.HtmlText HtmlText = new HtmlText();
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