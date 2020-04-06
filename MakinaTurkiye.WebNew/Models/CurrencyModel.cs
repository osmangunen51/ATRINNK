namespace NeoSistem.MakinaTurkiye.Web.Models
{
  using System.Collections.Generic;
  using EnterpriseEntity.Extensions.Data;
  using System;

  public class CurrencyModel
  {
    public byte CurrencyId { get; set; }
    public string CurrencyName { get; set; }
    public string CurrencyFullName { get; set; }
    public string CurrencyCodeName { get; set; }
    public bool Active { get; set; }
  }

}