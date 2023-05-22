using System.Collections.Generic;

namespace NeoSistem.Trinnk.Management.Models
{
    public class StoreContactInfoModel
    {
        public StoreContactInfoModel()
        {
            this.StorePhones = new List<PhoneModel>();
        }
        public string StoreAddress { get; set; }
        public List<PhoneModel> StorePhones { get; set; }
    }
}