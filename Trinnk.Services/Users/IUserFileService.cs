using Trinnk.Entities.Tables.Users;
using System.Collections.Generic;

namespace Trinnk.Services.Users
{
    public interface IUserFileService
    {
        IList<UserFile> GetUserFilesByUserId(int userId);
        void InsertUserFile(UserFile userFile);
        UserFile GetUserFileByUserFileId(int userFileId);
        void DeleteUserFile(UserFile userFile);

    }
}
