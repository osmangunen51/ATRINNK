using Trinnk.Entities.Tables.Users;

namespace Trinnk.Services.Users
{
    public interface IUserInformationService
    {
        UserInformation GetUserInformationByUserId(int userId);
        void InsertUserInformation(UserInformation userInformation);
        void UpdateUserInformation(UserInformation userInformation);

    }
}
