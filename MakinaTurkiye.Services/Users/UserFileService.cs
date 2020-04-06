using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MakinaTurkiye.Core.Data;
using MakinaTurkiye.Entities.Tables.Users;

namespace MakinaTurkiye.Services.Users
{
    public class UserFileService : IUserFileService
    {
        IRepository<UserFile> _userFileRepository;

        public UserFileService(IRepository<UserFile> userFileRepository)
        {
            _userFileRepository = userFileRepository;
        }

        public void DeleteUserFile(UserFile userFile)
        {
            if (userFile == null)
                throw new ArgumentNullException("userFile");
            _userFileRepository.Delete(userFile);
        }

        public UserFile GetUserFileByUserFileId(int userFileId)
        {
            if (userFileId == 0)
                throw new ArgumentNullException("userFileId");
            var query = _userFileRepository.Table;
            return query.FirstOrDefault(x => x.UserFileId == userFileId);
        }

        public IList<UserFile> GetUserFilesByUserId(int userId)
        {
            if (userId == 0)
                throw new ArgumentNullException("userId");
            var query = _userFileRepository.Table;
            return query.Where(x => x.UserId == userId).ToList();

        }

        public void InsertUserFile(UserFile userFile)
        {
            if (userFile == null)
                throw new ArgumentNullException("userFile");
            _userFileRepository.Insert(userFile);
        }
    }
}
