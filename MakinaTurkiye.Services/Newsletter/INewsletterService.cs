using MakinaTurkiye.Entities.Tables.Newsletter;
using System.Collections.Generic;

namespace MakinaTurkiye.Services.Newsletters
{
    public interface INewsletterService : ICachingSupported
    {
        void InsertNewsletter(Newsletter Newsletter);
        void UpdateNewsletter(Newsletter Newsletter);
        Newsletter GetNewsletterByNewsletterEmail(string NewsletterEmail);
    }
}
