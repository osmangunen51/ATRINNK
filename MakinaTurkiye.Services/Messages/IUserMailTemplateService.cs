using MakinaTurkiye.Entities.Tables.Messages;
using System.Collections.Generic;

namespace MakinaTurkiye.Services.Messages
{
    public interface IUserMailTemplateService
    {
        List<UserMailTemplate> GetAllUserMailTemplates();
        UserMailTemplate GetUserMailTemplateByUserMailTemplateId(int userMailTemplateId);
        void InsertUserMailTemplate(UserMailTemplate userMailTemplate);
        void DeleteUserMailTemplate(UserMailTemplate userMailTemplate);
        void UpdateUserMailTemplate(UserMailTemplate userMailTemplate);
        List<UserMailTemplate> GetUserMailTemplatesByUserId(int userId);
    }
}
