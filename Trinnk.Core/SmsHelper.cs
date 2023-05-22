using Trinnk.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Trinnk.Core
{
    public class SmsHelper:IDisposable
    {
        private string kNumber = ConfigurationManager.AppSettings["smsKno"];
        private string password = ConfigurationManager.AppSettings["smsSifre"];
        private string uName = ConfigurationManager.AppSettings["smsKadi"];
        private string gonderen = ConfigurationManager.AppSettings["smsObj"];

        public void Dispose()
        {
            
        }

        public string SendPhoneMessage(string gsmNumber, string message)
        {
            string tur = "Turkce";
            string smsNN = "data=<sms><kno>" + kNumber + "</kno><kulad>" + uName + "</kulad><sifre>" + password + "</sifre>" +
            "<gonderen>" + gonderen + "</gonderen>" +
            "<telmesajlar>" +
            "<telmesaj><tel>" + gsmNumber + "</tel><mesaj>" + message + "</mesaj></telmesaj>" +
            "</telmesajlar>" +
            "<tur>" + tur + "</tur></sms>";
            return XmlPost("http://panel.vatansms.com/panel/smsgonderNNpost.php", smsNN);
        }

        public string SendSmsOnlyPassword(string gsmNumber, string message)
        {
            string tur = "Turkce";
            string smsNN = "data=<sms><kno>" + kNumber + "</kno><kulad>" + uName + "</kulad><sifre>" + password + "</sifre>" +
            "<gonderen>" + gonderen + "</gonderen>" +
            "<telmesajlar>" +
            "<telmesaj><tel>" + gsmNumber + "</tel><mesaj>" + message + "</mesaj></telmesaj>" +
            "</telmesajlar>" +
            "<tur>" + tur + "</tur></sms>";
            return XmlPost("http://panel.vatansms.com/panel/smsgonderNNpost.php", smsNN);
        }

        public string XmlPost(string PostAddress, string xmlData)
        {
            using (WebClient wUpload = new WebClient())
            {
                wUpload.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                Byte[] bPostArray = Encoding.UTF8.GetBytes(xmlData);
                Byte[] bResponse = wUpload.UploadData(PostAddress, "POST", bPostArray);
                Char[] sReturnChars = Encoding.UTF8.GetChars(bResponse);
                string sWebPage = new string(sReturnChars);
                return sWebPage;
            }
        }
    }
}
