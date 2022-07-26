using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MakinaTurkiyeEntitiesTablesStores = MakinaTurkiye.Entities.Tables.Stores;

namespace NeoSistem.MakinaTurkiye.Management.Models.ViewModel
{
    public class StorePackagePurchaseRequest: MakinaTurkiyeEntitiesTablesStores.StorePackagePurchaseRequest
    {
        public string PortfoyYonetici { get; set; }
        public string TeleSatisSorumlu { get; set; }
    }
}