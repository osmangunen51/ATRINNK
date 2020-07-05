namespace NeoSistem.MakinaTurkiye.Web.Models
{
  using System.Collections.Generic;
    using global::MakinaTurkiye.Entities.Tables.Common;
    using NeoSistem.MakinaTurkiye.Web.Areas.Account.Models;

  public class PersonalModel
  {
      public PersonalModel()
      {
          this.HelpList = new List<MTHelpModeltem>();
      }
     
    public List<MTHelpModeltem> HelpList{get;set;}
    //public IEnumerable<RelMainPartyCategory> MainPartyCategoryItems { get; set; }

    public IEnumerable<Phone> PhoneItems { get; set; }

    public IEnumerable<Address> AddressItems { get; set; }

    public LeftMenuModel LeftMenu { get; set; }
    public string StoreLogo { get;set;}
    public string StoreName { get; set; }
      public int StoreMainPartyId { get; set; }
  }
}