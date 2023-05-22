using Trinnk.Core;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;

namespace Trinnk.Utilities.MailHelpers
{
    public class MailHelper
    {

        public string Host { get; set; }
        public int Port { get; set; }
        public bool Ssl { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public string FromMail { get; set; }
        public string FromName { get; set; }
        public List<string> ToMails { get; set; }
        public string Password { get; set; }

        public MailHelper()
        {

        }
        public MailHelper(string subject, string content, string fromMail, List<string> ToMails, string password, string fromName, string host, int port, bool ssl)
        {
            this.Subject = subject;
            this.Content = content;
            this.FromMail = fromMail;
            this.ToMails = ToMails;
            this.Password = password;
            this.FromName = fromName;
            this.Port = port;
            this.Ssl = ssl;
            this.Host = host;
        }

        public MailHelper(string subject, string content, string fromMail, string toMail, string password, string fromName, string host, int port, bool ssl)
        {
            this.Subject = subject;
            this.Content = content;
            this.FromMail = fromMail;
            this.ToMails = new List<string>();
            ToMails.Add(toMail);
            this.Password = password;
            this.FromName = fromName;
            this.Port = port;
            this.Ssl = ssl;
            this.Host = host;
        }

        public void Send()
        {

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(FromMail, FromName);
            foreach (var item in ToMails)
            {
                mail.To.Add(item);
            }
            mail.Subject = Subject;
            mail.Body = Content;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.Normal;
            using (SmtpClient sc = new SmtpClient())
            {
                sc.Port = AppSettings.MailPort;                                                                   //Gmail için geçerli Portu bildiriyoruz
                sc.Host = AppSettings.MailHost;                                                      //Gmailin smtp host adresini belirttik
                sc.EnableSsl = AppSettings.MailSsl;                                                             //SSL’i etkinleştirdik
                sc.Credentials = new NetworkCredential(AppSettings.MailUserName, AppSettings.MailPassword); //Gmail hesap kontrolü için bilgilerimizi girdi
                sc.Send(mail);
            }
        }
    }
}
