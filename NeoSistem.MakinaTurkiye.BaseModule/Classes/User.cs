namespace NeoSistem.MakinaTurkiye.Classes
{
    using System.Collections.Generic;

    public partial class User
    {
        public ICollection<Permission> Permissions { get; set; }
    }
}
