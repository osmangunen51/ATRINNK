using System.Collections.Generic;
using System.Net;
using System.Net.Mail;

namespace MakinaTurkiye.Utilities.MailHelpers
{
    public  class MailHelper
    {

        public  string Subject { get; set; }
        public string Content { get; set; }
        public string FromMail { get; set; }
        public string FromName { get; set; }
        public List<string> ToMails { get; set; }
        public string Password { get; set; }

        public MailHelper()
        {

        }
        public MailHelper(string subject, string content, string fromMail, List<string> ToMails, string password, string fromName)
        {
            this.Subject = subject;
            this.Content = content;
            this.FromMail = fromMail;
            this.ToMails = ToMails;
            this.Password = password;
            this.FromName = fromName;
        }

        public MailHelper(string subject, string content, string fromMail, string toMail, string password, string fromName)
        {
            this.Subject = subject;
            this.Content = content;
            this.FromMail = fromMail;
            this.ToMails = new List<string>();
            ToMails.Add(toMail);
            this.Password = password;
            this.FromName = FromName;
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
            SmtpClient sc = new SmtpClient();
            sc.Port = 587; 
            sc.Host = "smtp.gmail.com"; 
            sc.EnableSsl = true;
            sc.Credentials = new NetworkCredential(FromMail, Password);
            sc.Send(mail); 
        }


    }
}
