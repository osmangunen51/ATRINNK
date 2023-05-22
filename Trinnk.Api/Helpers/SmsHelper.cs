using Trinnk.Core.Infrastructure;
using Trinnk.Services.Common;
using Trinnk.Services.Members;
using System;
using System.Configuration;
using System.Net;
using System.Text;

namespace Trinnk.Api.Helpers
{
    public class SmsHelper
    {
        private string kNumber = ConfigurationManager.AppSettings["smsKno"];
        private string password = ConfigurationManager.AppSettings["smsSifre"];
        private string uName = ConfigurationManager.AppSettings["smsKadi"];
        private string gonderen = ConfigurationManager.AppSettings["smsObj"];

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

        public string CreateActiveCode()
        {
            string code = Guid.NewGuid().ToString();
            string lastCode = "";
            foreach (char k in code)
            {
                if (char.IsNumber(k)) lastCode = lastCode + k;
            }

            lastCode = lastCode.Substring(0, 6);

            IPhoneService phoneService = EngineContext.Current.Resolve<IPhoneService>();
            var mCode = phoneService.GetPhoneByActivationCode(lastCode);
            if (mCode != null)
                lastCode = CreateActiveCode();
            return lastCode;
        }

        public string CreateOnlyUsePassword()
        {
            string code = Guid.NewGuid().ToString();
            string lastPassword = "";
            foreach (char k in code)
            {
                if (char.IsNumber(k)) lastPassword = lastPassword + k;
            }
            lastPassword = lastPassword.Substring(0, 6);

            IMemberService memberService = EngineContext.Current.Resolve<IMemberService>();
            var mCode = memberService.GetMemberByMemberPassword(lastPassword); ;
            if (mCode != null)
                lastPassword = CreateOnlyUsePassword();
            return lastPassword;
        }
    }
}