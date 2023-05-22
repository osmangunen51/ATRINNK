using Trinnk.Entities.Tables.Users;
using System.Linq;

namespace Trinnk.Services.Users
{
    public interface IUserService
    {
        User GetUserByUserId(int userId);
        void InsertUser(User User);
        void UpdateUser(User User);
        IQueryable<User> GetAll();

    }
}
