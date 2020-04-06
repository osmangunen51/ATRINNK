namespace NeoSistem.MakinaTurkiye.Management.Models.Users
{
    public class UserFormModel
    {
        public UserFormModel()
        {
            this.UserModel = new UserModel();
            this.UserInformationModel = new UserInformationModel();
        }
        public UserModel UserModel { get; set; }
        public UserInformationModel UserInformationModel { get; set; }
    }
}