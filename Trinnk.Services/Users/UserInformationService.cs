using Trinnk.Core.Data;
using Trinnk.Entities.Tables.Users;
using System;
using System.Linq;

namespace Trinnk.Services.Users
{
    public class UserInformationService : IUserInformationService
    {
        IRepository<UserInformation> _userInformationRepository;
        public UserInformationService(IRepository<UserInformation> userInformationRepository)
        {
            this._userInformationRepository = userInformationRepository;
        }

        public UserInformation GetUserInformationByUserId(int userId)
        {
            if (userId == 0)
                throw new ArgumentException("userId");
            var query = _userInformationRepository.Table;
            return query.FirstOrDefault(x => x.UserId == userId);
        }

        public void InsertUserInformation(UserInformation userInformation)
        {
            if (userInformation == null)
                throw new ArgumentNullException("userInformation");
            _userInformationRepository.Insert(userInformation);
        }

        public void UpdateUserInformation(UserInformation userInformation)
        {
            if (userInformation == null)
                throw new ArgumentNullException("userInformation");
            _userInformationRepository.Update(userInformation);
        }
    }
}
