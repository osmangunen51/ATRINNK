using MakinaTurkiye.Core.Data;
using MakinaTurkiye.Entities.Tables.Users;
using System;
using System.Linq;

namespace MakinaTurkiye.Services.Users
{
    public class UserService : IUserService
    {
        IRepository<User> _UserRepository;
        public UserService(IRepository<User> UserRepository)
        {
            this._UserRepository = UserRepository;
        }

        public User GetUserByUserId(int userId)
        {
            if (userId == 0)
                throw new ArgumentException("userId");
            var query = _UserRepository.Table;
            return query.FirstOrDefault(x => x.UserId == userId);
        }
        public IQueryable<User> GetAll()
        {
            var query = _UserRepository.Table;
            return query;
        }
        public void InsertUser(User User)
        {
            if (User == null)
                throw new ArgumentNullException("User");
            _UserRepository.Insert(User);
        }

        public void UpdateUser(User User)
        {
            if (User == null)
                throw new ArgumentNullException("User");
            _UserRepository.Update(User);
        }
    }
}
