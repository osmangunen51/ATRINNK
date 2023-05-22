using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrinnkEntitiesTablesStores = Trinnk.Entities.Tables.Stores;

namespace NeoSistem.Trinnk.Management.Models.ViewModel
{
    public class StorePackagePurchaseRequest: TrinnkEntitiesTablesStores.StorePackagePurchaseRequest
    {
        public string PortfoyYonetici { get; set; }
        public string TeleSatisSorumlu { get; set; }
    }
}