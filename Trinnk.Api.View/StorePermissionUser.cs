using System.Collections.Generic;

namespace Trinnk.Api.View
{
    public class StorePermissionUserItem
    {
        public int PermissionMainPartyId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public byte Gender { get; set; } = 1;
        public bool? Active { get; set; }
    }

    public class StorePermissionUser
    {
        public List<StorePermissionUserItem> List { get; set; } = new List<StorePermissionUserItem>();
        public int MainPartyId { get; set; } = 0;
    }
}