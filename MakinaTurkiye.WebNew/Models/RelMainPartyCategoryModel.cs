namespace NeoSistem.MakinaTurkiye.Web.Models
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Web;
  using EnterpriseEntity.Extensions.Data;

  public class RelMainPartyCategoryModel
  {
    public int MainPartyId { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
  }
}