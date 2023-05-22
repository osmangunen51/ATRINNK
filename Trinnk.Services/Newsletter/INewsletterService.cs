using Trinnk.Entities.Tables.Newsletter;

namespace Trinnk.Services.Newsletters
{
    public interface INewsletterService : ICachingSupported
    {
        void InsertNewsletter(Newsletter Newsletter);
        void UpdateNewsletter(Newsletter Newsletter);
        Newsletter GetNewsletterByNewsletterEmail(string NewsletterEmail);
    }
}
