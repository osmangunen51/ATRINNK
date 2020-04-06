using MakinaTurkiye.Core.Data;
using MakinaTurkiye.Entities.Tables.Messages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MakinaTurkiye.Services.Messages
{
    public class UserMailTemplateService:IUserMailTemplateService
    {
        private readonly IRepository<UserMailTemplate> _userMailTemplateRepository;

        public UserMailTemplateService(IRepository<UserMailTemplate> userMailTemplateRepo)
        {
            this._userMailTemplateRepository = userMailTemplateRepo;
        }

        public void DeleteUserMailTemplate(UserMailTemplate userMailTemplate)
        {
            if (userMailTemplate == null)
                throw new ArgumentNullException();
            _userMailTemplateRepository.Delete(userMailTemplate);
        }

        public List<UserMailTemplate> GetAllUserMailTemplates()
        {
            var query = _userMailTemplateRepository.Table;
            return query.ToList();
        }

        public UserMailTemplate GetUserMailTemplateByUserMailTemplateId(int userMailTemplateId)
        {
            if (userMailTemplateId == 0)
                throw new ArgumentNullException();
            var query = _userMailTemplateRepository.Table;
            return query.FirstOrDefault(x => x.UserMailTemplateId ==userMailTemplateId);
        }

        public List<UserMailTemplate> GetUserMailTemplatesByUserId(int userId)
        {
            if (userId == 0)
                throw new ArgumentNullException();
            var query = _userMailTemplateRepository.Table;
            query = query.Where(x => x.UserId == userId);
            return query.ToList();
        }

        public void InsertUserMailTemplate(UserMailTemplate userMailTemplate)
        {
            if (userMailTemplate == null)
                throw new ArgumentNullException();
            _userMailTemplateRepository.Insert(userMailTemplate);
        }

        public void UpdateUserMailTemplate(UserMailTemplate userMailTemplate)
        {
            if (userMailTemplate == null)
                throw new ArgumentNullException();
            _userMailTemplateRepository.Update(userMailTemplate);
        }
    }
}
