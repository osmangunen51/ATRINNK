namespace NeoSistem.MakinaTurkiye.Management.Models
{
    using EnterpriseEntity.Extensions.Data;
    using MakinaTurkiye.Management.Models.Validation;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Web.Mvc;

    [Bind(Exclude = "UserGroupId")]
    public class UserGroupModel
    {
        public int UserGroupId { get; set; }

        [RequiredValidation, StringLengthValidation(50)]
        [DisplayName("Grup Adı")]
        public string GroupName { get; set; }

        public ICollection<PermissionModel> Permissions { get; set; }

        public static ICollection<UserGroupModel> UserGroups
        {
            get
            {
                return new Classes.UserGroup().GetDataTable().AsCollection<UserGroupModel>();
            }
        }
    }
}