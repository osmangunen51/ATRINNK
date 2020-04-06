namespace NeoSistem.MakinaTurkiye.Web.Models
{
  using System;
  using System.Collections.Generic;
  using System.Web.Mvc;
  using EnterpriseEntity.Extensions.Data;
  using System.Linq;
  using System.ComponentModel;

  public class StoreDealerModel
  {
    public int StoreDealerId { get; set; }

    public int MainPartyId { get; set; }

    public string DealerName { get; set; }

    public byte DealerType { get; set; }
  }
}