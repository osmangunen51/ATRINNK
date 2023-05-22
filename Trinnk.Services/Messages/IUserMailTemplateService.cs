using Trinnk.Entities.Tables.Messages;
using System.Collections.Generic;

namespace Trinnk.Services.Messages
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
