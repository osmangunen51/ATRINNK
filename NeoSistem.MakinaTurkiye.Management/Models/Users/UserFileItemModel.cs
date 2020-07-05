using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Management.Models.Users
{
    public class UserFileItemModel
    {
        public int UserFileId { get; set; }
        public string Type { get; set; }
        public string FilePath { get; set; }
    }
}