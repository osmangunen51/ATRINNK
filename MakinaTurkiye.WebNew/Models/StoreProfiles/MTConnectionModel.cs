﻿using MakinaTurkiye.Entities.Tables.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NeoSistem.MakinaTurkiye.Web.Models.Catalog;
using NeoSistem.MakinaTurkiye.Web.Models.Products;

namespace NeoSistem.MakinaTurkiye.Web.Models.StoreProfiles
{
  public class MTConnectionModel
  {

    public MTConnectionModel()
    {
      this.Phones = new List<MTStorePhoneModel>();
      this.StoreAddress = new Address();
      this.MTStoreProfileHeaderModel = new MTStoreProfileHeaderModel();
      MtJsonLdModel = new MTJsonLdModel();
    }
    public int MainPartyId { get; set; }
    public byte StoreActiveType { get; set; }
    public string AuthorizedNameSurname { get; set; }
    public string StoreName { get; set; }
    public Address StoreAddress { get; set; }
    public string AddressMap { get; set; }
    public IList<MTStorePhoneModel> Phones { get; set; }
    public string StoreWebUrl { get; set; }
    public MTStoreProfileHeaderModel MTStoreProfileHeaderModel { get; set; }
    public MTJsonLdModel MtJsonLdModel { get; set; }

  }
}