using MakinaTurkiye.Entities.Tables.Users;

namespace MakinaTurkiye.Services.Users
{
    public interface IUserInformationService
    {
        UserInformation GetUserInformationByUserId(int userId);
        void InsertUserInformation(UserInformation userInformation);
        void UpdateUserInformation(UserInformation userInformation);

    }
}
