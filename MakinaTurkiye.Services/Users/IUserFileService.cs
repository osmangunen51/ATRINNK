using MakinaTurkiye.Entities.Tables.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakinaTurkiye.Services.Users
{
    public interface IUserFileService
    {
        IList<UserFile> GetUserFilesByUserId(int userId);
        void InsertUserFile(UserFile userFile);
        UserFile GetUserFileByUserFileId(int userFileId);
        void DeleteUserFile(UserFile userFile);

    }
}
