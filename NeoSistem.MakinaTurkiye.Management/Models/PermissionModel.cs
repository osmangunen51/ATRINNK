namespace NeoSistem.MakinaTurkiye.Management.Models
{
    using EnterpriseEntity.Extensions.Data;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Web.Mvc;
    using Validation;

    [Bind(Exclude = "PermissionId")]
    public class PermissionModel
    {
        public int PermissionId { get; set; }

        [DisplayName("İzin Grup Adı")]
        public string PermissionGroupName { get; set; }

        [RequiredValidation, StringLengthValidation(50)]
        [DisplayName("İzin Adı")]
        public string PermissionName { get; set; }

        public static ICollection<PermissionModel> Permissions
        {
            get
            {
                var permission = new Classes.Permission();
                return permission.GetDataSet().Tables[0].AsCollection<PermissionModel>();
            }
        }

    }
}